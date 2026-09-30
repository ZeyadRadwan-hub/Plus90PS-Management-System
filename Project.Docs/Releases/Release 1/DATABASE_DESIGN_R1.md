# +90 PS — R1 Physical Database Design (DB-01 + DB-01.1)

Status: DB-01.1 FINANCIAL + BUSINESS DAY DESIGN COMPLETED / READY FOR REVIEW, not implemented. No EF mapping, migration, SQLite file, SQL Server database, or physical index exists from this document. BE-09 requires separate authorization.

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
| Branches | FROZEN | Yes, selected Branch cache | Yes | Central → local | Central ownership; TimeZoneId defaults to Africa/Cairo in R1 |
| BusinessDays | PROVISIONAL | Yes | Yes | Local → central | Explicit Start Day / End Day operational interval; one open per Branch |
| Employees | FROZEN | Yes, business-scope cache | Yes | Central → local | Business-wide identity/deactivation; no Employee.BranchId |
| GameConsoles | FROZEN | Yes | Yes | Central config → local | IsActive is not occupancy |
| PricingEntries | FROZEN | Yes | Yes | Central config → local | Current price; Session price snapshot remains stable |
| Sessions | PROVISIONAL | Yes | Yes | Local → central | Branch operational creation source |
| SessionEvents | PROVISIONAL | Yes | Yes | Local → central | Append-only lifecycle/revision evidence |
| ConsoleOccupancies | PROVISIONAL | Yes | No | Local only | Local double-session guard; central derives current state |
| Invoices | PROVISIONAL | Yes | Yes | Local → central | Cancel-only historical invoice; no separate Void or duplicate paid state |
| Payments | PROVISIONAL | Yes | Yes | Local → central | Zero or one full Cash receipt per Invoice |
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

Shared logical tables are Businesses, Branches, BusinessDays, Employees, GameConsoles, PricingEntries, Sessions, SessionEvents, Invoices, Payments, Shifts, Expenses, BranchAssets, and AuditEvents; their logical shape is mostly shared, physical mapping provider-specific. Outbox/Inbox and security structures differ by purpose. Q-REPORT-01 is resolved: an operational Business Day is the explicit Start Day → End Day interval, not midnight-to-midnight. Branches.TimeZoneId uses the R1 IANA baseline Africa/Cairo for display and calendar-period selection, not to replace that interval.

## Relationships and deletion

Business/history foreign keys default to NO ACTION/RESTRICT in both providers: Business→Branch/Employee; Branch→BusinessDay/Console/Pricing/Session/Invoice/Payment/Shift/Expense/Asset/Audit; BusinessDay→Session/Invoice/Payment/Expense; Employee→BusinessDay starter/ender, Session opener/responsible, Invoice issuer/canceller, Payment receiver, Shift actor, Expense actor, Audit actor; Console→Session/Occupancy; Session→Events/Occupancy/Invoice; Invoice→Payment. Local/central FK targets exist only when both rows exist in that provider. Technical queue/security FKs also use NO ACTION; no cascade is proposed. Nullable end actor/Branch fields are FKs only when present. Same-Branch links need provider-specific composite-FK or transactional validation review. Use deactivation/status/history retention, not normal physical deletion. No Employee delete → Sessions, Branch delete → Invoices, BusinessDay delete → Payments, or Session delete → Payments path may exist.

Business Id/Name; Branch Id/BusinessId/Name; Employee Id/BusinessId/Name/Role/IsActive; and GameConsole Id/BranchId/Name/Generation/IsActive mirror actual Domain. Branches.TimeZoneId is new R1 reporting configuration (default Africa/Cairo), not an authoritative day-boundary clock. Owner is EmployeeRole.Owner, not Business.OwnerId. Employee has no BranchId and may operate across active same-Business Branches subject to future authorization. Console IsActive is configuration, not IsBusy. No unapproved name uniqueness.

PricingEntries add a stable persistence Guid row Id for later management/audit and enforce UNIQUE (BranchId, ConsoleGeneration, PlayMode, PricingMethod). A Branch may configure a subset; missing means no valid price, not zero/default. Updates to current pricing never rewrite Sessions.CapturedPricePiasters.

BusinessDays is a new shared logical operational record: Id, BranchId, StartedByEmployeeId, StartedAtUtc, nullable EndedByEmployeeId/EndedAtUtc, and application-managed VersionToken. Id is supplied Guid, never database-generated. An authorized Manager explicitly starts and ends the day, confirmed by the owner on 2026-09-30; do not infer Cashier permission or Owner inheritance. Future RBAC/service/API enforcement remains unimplemented. Open means EndedAtUtc is NULL; Closed means it is populated. There is no redundant IsOpen or Status column. A provider CHECK should require end actor and end instant to be both NULL or both present, and EndedAtUtc >= StartedAtUtc when closed. A unique partial SQLite index and unique filtered SQL Server index on BranchId WHERE EndedAtUtc IS NULL enforce at most one open BusinessDay per Branch (alongside Application checks). Midnight, Windows date change, minimize, network loss, crash or restart never auto-close or auto-start a day. Restart reads the persisted open row. End Day changes VersionToken; closed days remain historical, not casually reopened/reset/deleted. StartDay/EndDay later commit state + required Audit + Outbox atomically with stable OperationId, and central Inbox/replay cannot create a second day or double-close with conflicting data. Two offline devices may conflict centrally; retain local evidence and surface Conflict, never silently merge.

Sessions.BusinessDayId records the open day at Session start; Invoices.BusinessDayId records the day of issue; Payments.BusinessDayId records the day cash is actually received; Expenses.BusinessDayId records the day of recording. These are required same-Branch FKs, not inferred from wall-clock midnight. The operation's local transaction must select/validate the applicable open day; if no day is open, the later Application workflow must handle that explicitly rather than inventing a date. Shifts.BusinessDayId is intentionally omitted: a Shift may straddle explicit days, and no one-Shift-to-one-day rule is approved. End Day is blocked while any Session is unfinished (Active or Paused); pause does not mean completion. Do not auto-complete or transfer Sessions to bypass this guard. This owner-confirmed End Day requirement is separate from Shift-close handover. Exact normal application Exit handling is a later UX/engineering contract, with crash recovery still required.

Sessions preserve BE-07 identity, opening/current responsible employee, SessionType Open/Fixed/Match, captured generation/mode/method/price, optional fixed or Match reference duration, UTC lifecycle/state, VersionToken and AggregateRevision. One Match Session is exactly one Match; there is no MatchCount. Match reference duration is not an automatic end or hourly bill. The hourly final-charge formula remains unapproved. Sessions hold current durable state; SessionEvents hold ordered append-only Start/Pause/Resume/Complete and later ResponsibilityTransfer evidence. UNIQUE (SessionId, Revision), with revision N following N−1. Display timer derives from timestamps/events, not per-second writes.

ConsoleOccupancies has ConsoleId as PK and SessionId UNIQUE, both FKs NO ACTION. Session Start must atomically write Session + Start event + occupancy + Outbox in one local SQLite transaction; duplicate ConsoleId fails. Completion removes the technical occupancy row in a controlled local transaction without deleting history. Central may derive active occupancy from synchronized Sessions; it does not enforce the local immediate lock.

Invoices retain one issued record for each completed Session, its issue BusinessDayId, piaster amount, total paused duration, and nullable CancelledAtUtc/CancelledByEmployeeId/CancelReason. Invoice can precede Payment. Only eligible unpaid Invoice may be cancelled; any successful recorded Payment prohibits cancellation. Cancelled record remains and is excluded from active revenue; no refund is implied. UNIQUE SessionId prevents a second Invoice for the same R1 Session; no reissue workflow is approved. Cancel is the only R1 cancellation action: all three cancellation fields are NULL before Cancel; when Cancelled, timestamp and actor are required while reason remains optional. Derive cancellation from CancelledAtUtc, and payment state from presence of the one Payment; no StatusCode/IsPaid/IsOpen duplication. Provider CHECKs can enforce cancellation-field nullability and timestamp chronology, but **cannot** check another table. An Invoice with any successful Payment cannot be cancelled: later Application + local database transaction/concurrency + central sync validation must reject that race. Cancelled history remains, with no destructive delete, refund or Payment reversal.

Payments are separate Cash receipts linked to Invoice, with UNIQUE InvoiceId in both providers: zero or one successful Payment per Invoice, never partial/installment/split receipts. Cash is the only R1 method, so no MethodCode column or speculative other method is stored. AmountPiasters must be positive and equal Invoice.AmountPiasters, but cross-table equality is checked by later Application and database transaction, not a false CHECK. Creation later verifies Invoice exists, is not Cancelled, has no Payment, amount equals the final Invoice amount, then inserts and commits atomically; central replay repeats these validations under its own transaction and Inbox idempotency. Invoice can precede Payment. Example: Invoice.BusinessDayId = Day A, paid later Payment.BusinessDayId = Day B; never rewrite the Invoice's day. The hourly charge formula and actual workflow remain unimplemented.

Shifts record Branch, start/end actor/time and VersionToken, without payroll or an assumed one-day link. Expenses store Branch, recording BusinessDayId, actor, piaster amount, description/category/note, UTC time and token. BranchAssets store named category quantity and token, without product stock, serial lifecycle, or inter-Branch transfer. Audits are append-oriented/redacted; never put plaintext PIN, pepper, raw device credential, private key, or plaintext access token in Audit or any database.

Revenue is the sum of valid successful Cash Payments assigned to the selected BusinessDay(s), **not** all issued Invoices. An unpaid Invoice contributes no received-cash revenue; a Cancelled Invoice contributes none and cannot have a Payment under the transaction rule. Expense total comes from Expenses assigned to those same day IDs; operational Profit = Revenue − Expenses. One selected BusinessDay is the Daily report, even if its interval crosses midnight. Monthly, selected six-calendar-month, and Yearly reports select BusinessDays for the Branch using its TimeZoneId to interpret calendar labels/range boundaries, then aggregate Payment/Expense rows by BusinessDayId. R1 makes no DailyReports/MonthlyReports/SixMonthReports/YearlyReports tables or precomputed totals. The owner approved whole-day attribution by the Branch-local Start Day label on 2026-09-30: a day starting 30 September 23:55 and ending 1 October belongs wholly to September; a day starting 31 December and ending 1 January belongs to the starting year. Use StartedAtUtc interpreted in Branches.TimeZoneId, not EndedAtUtc or individual receipt timestamps, to select the day IDs. At an explicit transition to the next day, previous EndedAtUtc equals next StartedAtUtc; use one shared UTC transition instant and the engineering half-open interval convention [start, end). Finalize atomic transition/operation ordering in an authorized workflow task. No automatic midnight/day creation follows.

## Concurrency, atomicity, retention

Application-managed Guid VersionToken is proposed on mutable Businesses, Branches, BusinessDays, Employees, GameConsoles, PricingEntries, Sessions, Invoices, Shifts, Expenses, BranchAssets, Devices, LocalDeviceBindings, PinVerifiers, OfflinePinVerifiers, PinAttemptStates, DeviceSecurityStates and SubscriptionStates. Regenerate after accepted meaningful mutation, including End Day; EF later treats it as concurrency token in both contexts. It is not SQL Server rowversion. Sessions also have signed 64-bit AggregateRevision for event order; these are different mechanisms. Append-only SessionEvents, AuditEvents and InboxReceipts have no VersionToken. Outbox status/attempt metadata can change, but identity/payload/hash never change; worker uses controlled compare-and-update.

One local SQLite transaction commits business state, required event/audit, and Pending OutboxOperation; UI success follows commit only. Stable OperationId survives retries. Central uses one SQL Server transaction for new InboxReceipt plus business effect. Same BranchId+OperationId+PayloadHash replays stored accepted result after lost ACK; same identity/different hash is integrity Conflict. Rejected/conflicted local evidence remains visible. Acknowledged Outbox cleanup is controlled after at least 30 days; central receipts remain long-term with business retention.

Production SQLite belongs on protected local storage, not repository/OneDrive/share. Future runtime enables foreign_keys, short transactions and busy timeout; WAL/synchronous/backup details follow TECHNICAL_DECISIONS_R1 and verified bundled SQLite version. Never back up an open WAL database by copying only its main file. Backup and RTO/RPO targets have not been operationally proven by DB-01.

## Migration and review gate (design only)

Future BranchDbContext owns SQLite-specific mappings/migrations; CentralDbContext owns SQL Server-specific mappings/migrations. Share configuration only where compatible. Use separate ordered provider sets (for example Sqlite/R1_YYYYMMDD_Description and SqlServer/R1_YYYYMMDD_Description), record applied version per provider, and review both DDL outputs for every change. Prefer additive expand-then-contract upgrades; verify client/API compatibility, safe backup/restore, staged migration on representative data, and rollback/forward-fix before destructive production changes. Never run one provider's migration on the other.

Before BE-09: Q-BIZ-01, Q-BIZ-02 and Q-REPORT-01 are resolved and reflected here. The 2026-09-30 Manager, unfinished-Session End Day guard, start-month/year attribution and shared-boundary choices are resolved; review their concrete RBAC/command/atomic transition contracts without reopening those business choices. Do not infer Owner inheritance or a one-Shift-to-one-day relationship; security-review Device/PIN/lease/recovery/token schema and external secret protection; align string bounds with Domain/Application; review actual FK/CHECK/partial-filtered-index DDL, UTC/Guid/money conversion and overflow, atomicity, idempotency, both-provider plans and backup. Q-OPS-01 blocks rollout/DR proof. BE-09 requires separate explicit authorization. DB-01.1 does not lift PRE_CODE_GATE_R1.
