# +90 PS — R1 Physical Database Design (DB-01)

Status: DESIGNED / READY FOR REVIEW, not implemented. No EF mapping, migration, SQLite file, SQL Server database, or physical index exists from this document. Remaining decision tickets and separate BE-09 authorization are required before implementation.

## Authority and boundaries

This design applies the actual BE-01–BE-08 Domain, BUSINESS_RULES_R1(1), FUNCTIONAL_REQUIREMENTS_R1(2), ACCEPTANCE_CRITERIA_R1(2), BRD_R1(2), PRD_R1(2), TECHNICAL_DECISIONS_R1, and current DECISION_REQUESTS_R1. Historical DECISIONS.md and old UI screenshots are not authority over current decisions. The companion [dictionary](DATA_DICTIONARY_R1.md) defines columns; the [index plan](INDEX_QUERY_PLAN_R1.md) defines query-backed candidates.

Path: WPF → one-Branch local SQLite transaction + durable Outbox → immediate SavedLocally → background authenticated/authorized API synchronization → central SQL Server. WPF never writes SQL Server directly. Core Branch actions use the same local path online and offline; sensitive administration is online-only under the approved baseline. Central rejection/conflict never silently erases locally committed financial evidence.

FROZEN means approved shape/invariant, not implemented DDL. PROVISIONAL means reasonable technical shape requiring implementation/security review. BLOCKED means a named owner decision prevents final state/constraint design. A BLOCKED table may contain stable base fields.

## Type and mapping contract

| Concept | Domain | SQLite | SQL Server | Boundary |
|---|---|---|---|---|
| Persistent ID | Guid | BLOB(16) | uniqueidentifier | Application generates Guid v7 outside Domain; preserve 16 bytes; never use ID order as chronology |
| UTC instant | DateTimeOffset | INTEGER epoch milliseconds | datetime2(3) UTC | Normalize before write; no PC/server local time as authoritative history |
| Money | decimal EGP | INTEGER signed piasters | bigint piasters | Validate at most 2 decimals before exact ×100 conversion; never REAL/FLOAT |
| Duration | TimeSpan | INTEGER signed milliseconds | bigint milliseconds | Validate range; no formatted string storage |
| Enum | Domain enum | INTEGER | int | Stable numeric meanings; EmployeeRole Owner=1, Manager=2, Cashier=3 |
| Boolean | bool | INTEGER 0/1 | bit | SQLite CHECK 0/1 |
| SHA-256 | byte[32] | BLOB(32) | binary(32) | Exact 32 bytes |
| JSON envelope | structured data | TEXT | nvarchar(max) | Versioned canonical UTF-8 sync serialization; not a query key |

String limits are explicit per column in the dictionary. Current Domain constructors do not enforce all proposed storage lengths; BE-09/Application validation must align before migration. Command-supplied IDs, timestamps, amounts, revisions, and actors must not be silently fabricated by database defaults.

## Table inventory and local/central authority

Sync arrows show intended direction, not an implemented pipeline. Shared logical rows keep the same Guid identity but have provider-specific physical mapping. Central configuration can be cached locally; caching does not authorize offline edits.

| Table/structure | Status | Local SQLite | Central SQL Server | Creation source → sync | Authority/reason |
|---|---|---|---|---|---|
| Businesses | FROZEN | Yes, cache | Yes | Central → local | Central Business identity |
| Branches | FROZEN | Yes, selected Branch cache | Yes | Central → local | Central ownership; report cutoff open |
| Employees | FROZEN | Yes, business-scope cache | Yes | Central → local | Business-wide identity/deactivation; no Employee.BranchId |
| GameConsoles | FROZEN | Yes | Yes | Central config → local | IsActive is not occupancy |
| PricingEntries | FROZEN | Yes | Yes | Central config → local | Current price; Session price snapshot remains stable |
| Sessions | PROVISIONAL | Yes | Yes | Local → central | Branch operational creation source |
| SessionEvents | PROVISIONAL | Yes | Yes | Local → central | Append-only lifecycle/revision evidence |
| ConsoleOccupancies | PROVISIONAL | Yes | No | Local only | Local double-session guard; central derives current state |
| Invoices | BLOCKED Q-BIZ-01 | Yes | Yes | Local → central | Issued evidence stable; unpaid Cancel/Void model open |
| Payments | BLOCKED Q-BIZ-02 | Yes | Yes | Local → central | Cash receipts; cardinality/partial settlement open |
| Shifts | PROVISIONAL | Yes | Yes | Local → central | Operational shift, not payroll |
| Expenses | PROVISIONAL | Yes | Yes | Local → central | Branch profit evidence |
| BranchAssets | PROVISIONAL | Yes | Yes | Local → central | Branch equipment quantities only |
| AuditEvents | PROVISIONAL | Yes | Yes | Local → central for local actions; central creates central actions | Append-oriented, redacted evidence |
| OutboxOperations | PROVISIONAL | Yes | No | Local → API | Atomic durable queue and retry evidence |
| InboxReceipts | PROVISIONAL | No | Yes | API only | Central dedup/result, atomic with effect |
| Devices | PROVISIONAL | No | Yes | Central → local binding metadata | Credential hash, revocation, Branch binding |
| LocalDeviceBindings | PROVISIONAL | Yes | No | Provisioning → local | One installed Device bound to one Branch |
| PinVerifiers | PROVISIONAL | No | Yes | Central provisioning | Central hash metadata, no plaintext PIN/pepper |
| OfflinePinVerifiers | PROVISIONAL | Yes | No | Controlled provisioning → local | Device-bound local hash metadata |
| PinAttemptStates | PROVISIONAL | Yes | No | Local only | Durable Branch+Device+Employee failure cycle |
| DeviceSecurityStates | PROVISIONAL | Yes | No | Authority/recovery → local | SecurityLocked/version/trusted-time floor |
| OfflineAuthorities | PROVISIONAL | Yes | No | Central signed grant → local | Bounded signed authority; no private key |
| UsedRecoveryChallenges | PROVISIONAL | Yes | No | Local recovery | One-time replay defense |
| ApiSessionTokens | PROVISIONAL | No | Yes | API only | Opaque reference token hash/expiry, never plaintext |
| SubscriptionStates | PROVISIONAL | No | Yes | Central billing/admin → grant | Paid-through/suspension facts for bounded authority |

Shared logical tables are Businesses, Branches, Employees, GameConsoles, PricingEntries, Sessions, SessionEvents, Invoices, Payments, Shifts, Expenses, BranchAssets, and AuditEvents; their logical shape is mostly shared, physical mapping provider-specific. Outbox/Inbox and security structures differ by purpose. No Branch reporting timezone/cutoff column is frozen: Q-REPORT-01 must specify identifier convention and day boundary before reporting grouping or derived dates are finalized.

## Relationships and deletion

Business/history foreign keys default to NO ACTION/RESTRICT in both providers: Business→Branch/Employee; Branch→Console/Pricing/Session/Invoice/Payment/Shift/Expense/Asset/Audit; Employee→Session opener/responsible, Invoice issuer/canceller, Payment receiver, Shift actor, Expense actor, Audit actor; Console→Session/Occupancy; Session→Events/Occupancy/Invoice; Invoice→Payment. Local/central FK targets exist only when both rows exist in that provider. Technical queue/security FKs also use NO ACTION; no cascade is proposed. Nullable actor/Branch fields are FKs only when present. Use deactivation/status/history retention, not normal physical deletion. No Employee delete → Sessions, Branch delete → Invoices, or Session delete → Payments path may exist.

Business Id/Name; Branch Id/BusinessId/Name; Employee Id/BusinessId/Name/Role/IsActive; and GameConsole Id/BranchId/Name/Generation/IsActive mirror actual Domain. Owner is EmployeeRole.Owner, not Business.OwnerId. Employee has no BranchId and may operate across active same-Business Branches subject to future authorization. Console IsActive is configuration, not IsBusy. No unapproved name uniqueness.

PricingEntries add a stable persistence Guid row Id for later management/audit and enforce UNIQUE (BranchId, ConsoleGeneration, PlayMode, PricingMethod). A Branch may configure a subset; missing means no valid price, not zero/default. Updates to current pricing never rewrite Sessions.CapturedPricePiasters.

Sessions preserve BE-07 identity, opening/current responsible employee, SessionType Open/Fixed/Match, captured generation/mode/method/price, optional fixed or Match reference duration, UTC lifecycle/state, VersionToken and AggregateRevision. One Match Session is exactly one Match; there is no MatchCount. Match reference duration is not an automatic end or hourly bill. The hourly final-charge formula remains unapproved. Sessions hold current durable state; SessionEvents hold ordered append-only Start/Pause/Resume/Complete and later ResponsibilityTransfer evidence. UNIQUE (SessionId, Revision), with revision N following N−1. Display timer derives from timestamps/events, not per-second writes.

ConsoleOccupancies has ConsoleId as PK and SessionId UNIQUE, both FKs NO ACTION. Session Start must atomically write Session + Start event + occupancy + Outbox in one local SQLite transaction; duplicate ConsoleId fails. Completion removes the technical occupancy row in a controlled local transaction without deleting history. Central may derive active occupancy from synchronized Sessions; it does not enforce the local immediate lock.

Invoices retain an issued record for each accepted Session completion, issued time, piaster amount, total paused duration, and nullable protected-cancellation fields. Invoice can precede Payment. Only eligible unpaid Invoice may be cancelled; any successful recorded Payment prohibits cancellation. Cancelled record remains and is excluded from active revenue; no refund is implied. Whether a voided unpaid Invoice can be reissued for the same Session is part of Q-BIZ-01: SessionId gets a lookup index, **not final uniqueness** yet. A later transaction/idempotency rule or approved filtered unique constraint must prevent duplicate accepted completion effects without blocking authorized corrections. Status values/unpaid Cancel versus Void transitions are BLOCKED by Q-BIZ-01, so no final status CHECK/eligibility predicate is approved. Optional reason is nullable and preserved if entered.

Payments are separate Cash receipts linked to Invoice. No UNIQUE InvoiceId or one-payment CHECK is proposed until Q-BIZ-02 resolves partial cash/settlement. Paid-invoice cancellation prohibition must be transactional with payment recording, not merely a UI filter. Charge formula and exact Invoice/Payment workflow are not implemented by DB-01.

Shifts record Branch, start/end actor/time and VersionToken, without payroll. Expenses store Branch, actor, piaster amount, description/category/note, UTC time and token. BranchAssets store named category quantity and token, without product stock, serial lifecycle, or inter-Branch transfer. Audits are append-oriented/redacted; never put plaintext PIN, pepper, raw device credential, private key, or plaintext access token in Audit or any database.

## Concurrency, atomicity, retention

Application-managed Guid VersionToken is proposed on mutable Businesses, Branches, Employees, GameConsoles, PricingEntries, Sessions, Invoices, Shifts, Expenses, BranchAssets, Devices, LocalDeviceBindings, PinVerifiers, OfflinePinVerifiers, PinAttemptStates, DeviceSecurityStates and SubscriptionStates. Regenerate after accepted meaningful mutation; EF later treats it as concurrency token in both contexts. It is not SQL Server rowversion. Sessions also have signed 64-bit AggregateRevision for event order; these are different mechanisms. Append-only SessionEvents, AuditEvents and InboxReceipts have no VersionToken. Outbox status/attempt metadata can change, but identity/payload/hash never change; worker uses controlled compare-and-update.

One local SQLite transaction commits business state, required event/audit, and Pending OutboxOperation; UI success follows commit only. Stable OperationId survives retries. Central uses one SQL Server transaction for new InboxReceipt plus business effect. Same BranchId+OperationId+PayloadHash replays stored accepted result after lost ACK; same identity/different hash is integrity Conflict. Rejected/conflicted local evidence remains visible. Acknowledged Outbox cleanup is controlled after at least 30 days; central receipts remain long-term with business retention.

Production SQLite belongs on protected local storage, not repository/OneDrive/share. Future runtime enables foreign_keys, short transactions and busy timeout; WAL/synchronous/backup details follow TECHNICAL_DECISIONS_R1 and verified bundled SQLite version. Never back up an open WAL database by copying only its main file. Backup and RTO/RPO targets have not been operationally proven by DB-01.

## Migration and review gate (design only)

Future BranchDbContext owns SQLite-specific mappings/migrations; CentralDbContext owns SQL Server-specific mappings/migrations. Share configuration only where compatible. Use separate ordered provider sets (for example Sqlite/R1_YYYYMMDD_Description and SqlServer/R1_YYYYMMDD_Description), record applied version per provider, and review both DDL outputs for every change. Prefer additive expand-then-contract upgrades; verify client/API compatibility, safe backup/restore, staged migration on representative data, and rollback/forward-fix before destructive production changes. Never run one provider's migration on the other.

Before BE-09: resolve Q-BIZ-01 unpaid Cancel/Void and Q-BIZ-02 partial settlement for affected constraints; resolve Q-REPORT-01 timezone/day cutoff before report grouping; security-review Device/PIN/lease/recovery/token schema and external secret protection; align string bounds with Domain/Application; review actual FK/CHECK DDL, UTC/Guid/money conversion and overflow, atomicity, idempotency, both-provider plans and backup. Q-OPS-01 blocks rollout/DR proof. BE-09 requires separate explicit authorization. DB-01 does not lift PRE_CODE_GATE_R1.
