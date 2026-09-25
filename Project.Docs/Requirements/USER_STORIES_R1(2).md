# +90 PS — Release 1 User Stories

**Document Type:** User Story Backlog  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Baseline  
**Last Updated:** 2026-09-09
**Primary Inputs:** `PRD_R1.md`, `FUNCTIONAL_REQUIREMENTS_R1.md`, `NON_FUNCTIONAL_REQUIREMENTS_R1.md`, `BUSINESS_RULES_R1.md`  
**Purpose:** Translate Release 1 requirements into actor-centered stories that can drive UX, system analysis, acceptance criteria, design, implementation, and testing.

---

# 1. Story Convention

Each story uses:

```text
US-<AREA>-NNN
```

Format:

```text
As a <actor>
I want <capability>
So that <business value>
```

Each story includes:

```text
Priority
Trace
Dependencies
Notes
Acceptance Reference
```

Priority meanings:

```text
P0 = Required for the core operational loop / release viability
P1 = Required for complete R1 operation
P2 = Required but can follow the first internal slice before production
TBD = blocked by an unresolved business decision
```

This file does **not** contain the full Given/When/Then criteria. Those live in `ACCEPTANCE_CRITERIA_R1.md`.

---

# 2. Epic Map

```text
EPIC-AUTH       Authentication / Active User
EPIC-SECLOCK    PIN Security Lock / Recovery
EPIC-RBAC       Roles / Permissions
EPIC-BRANCH     Branch Context / Isolation
EPIC-CONSOLE    Console Management / Availability
EPIC-PRICE      Pricing
EPIC-SESSION    Session Lifecycle
EPIC-BILLING    Time / Money / Charge Calculation
EPIC-FINANCE    Invoice / Cash Payment
EPIC-SHIFT      Shift
EPIC-EXPENSE    Expense
EPIC-ASSET      Basic Equipment Foundation
EPIC-REPORT     Reporting
EPIC-OFFLINE    Offline Operation
EPIC-SYNC       Synchronization
EPIC-SUB        Subscription / Offline License
EPIC-AUDIT      Auditability
EPIC-RECOVERY   Crash / Power / Backup Recovery
```

---

# 3. EPIC-AUTH — Authentication and Active User

## US-AUTH-001 — Login with Personal PIN

**Priority:** P0

> As a Cashier, Manager, or Owner, I want to log in using my personal PIN so that every action I perform is associated with my own application identity.

**Trace:** `FR-AUTH-001`, `FR-AUTH-002`, `FR-AUTH-003`  
**Dependencies:** User exists and is authorized for the branch.  
**Acceptance:** `AC-US-AUTH-001-*`

---

## US-AUTH-002 — Login While Offline

**Priority:** P0

> As an authorized employee, I want to authenticate while the Internet is unavailable so that the branch can continue approved operation.

**Trace:** `FR-AUTH-004`, `FR-OFFLINE-002`, `NFR-OFFLINE-001`  
**Dependencies:** Valid cached/local authentication authority and valid offline subscription authority.  
**Acceptance:** `AC-US-AUTH-002-*`

---

## US-AUTH-003 — Reject Unauthorized or Disabled Login

**Priority:** P0

> As the business owner, I want users who are not currently authorized to be rejected so that inactive or invalid staff cannot operate the branch.

**Trace:** `FR-AUTH-005`, `FR-RBAC-007`  
**Acceptance:** `AC-US-AUTH-003-*`

---

## US-AUTH-004 — Switch Active User on Shared PC

**Priority:** P1

> As a branch employee, I want the application user to be switched without changing the Windows account so that multiple cashiers can safely share one branch PC.

**Trace:** `FR-AUTH-006`, `NFR-UX-007`  
**Acceptance:** `AC-US-AUTH-004-*`

---

## US-AUTH-005 — Reset Forgotten PIN

**Priority:** P1

> As an authorized Manager or Owner, I want to reset an employee's forgotten PIN to a new PIN so that access can be restored without exposing the old PIN.

**Trace:** `FR-AUTH-007`, `FR-AUTH-008`, `NFR-SEC-001`  
**Acceptance:** `AC-US-AUTH-005-*`

---

# 4. EPIC-SECLOCK — PIN Security Lock and Recovery

## US-SECLOCK-001 — First Five Failed Attempts

**Priority:** P1

> As the business owner, I want the system to temporarily block PIN attempts after five failures, play a warning sound, and record the event so that repeated guessing is detected.

**Trace:** `FR-PIN-001` through `FR-PIN-004`  
**Acceptance:** `AC-US-SECLOCK-001-*`

---

## US-SECLOCK-002 — Security Lock after Second Failure Cycle

**Priority:** P1

> As the business owner, I want the branch application to enter SecurityLocked after another five failed attempts so that suspicious repeated access attempts stop normal operation.

**Trace:** `FR-PIN-005`, `FR-PIN-006`  
**Acceptance:** `AC-US-SECLOCK-002-*`

---

## US-SECLOCK-003 — Online Security Unlock

**Priority:** P1

> As an authorized Owner/support operator, I want to unlock a SecurityLocked branch when it is online so that legitimate branch operation can resume.

**Trace:** `FR-PIN-007`  
**Acceptance:** `AC-US-SECLOCK-003-*`

---

## US-SECLOCK-004 — Offline One-Time Recovery

**Priority:** P1

> As an authorized Owner/support operator, I want a one-time challenge/response recovery flow for a fully offline SecurityLocked branch so that legitimate access can be restored without a permanent master PIN.

**Trace:** `FR-PIN-008`, `FR-PIN-009`, `NFR-SEC-007`, `NFR-SEC-008`  
**Acceptance:** `AC-US-SECLOCK-004-*`

---

## US-SECLOCK-005 — Prevent Security Recovery from Bypassing Subscription

**Priority:** P0

> As the product owner, I want PIN recovery to clear only the security lock and never bypass subscription/license restrictions so that the support mechanism cannot be used to avoid payment enforcement.

**Trace:** `FR-PIN-010`, `FR-STATE-003`, `NFR-SEC-009`  
**Acceptance:** `AC-US-SECLOCK-005-*`

---

# 5. EPIC-RBAC — Roles and Permissions

## US-RBAC-001 — Role-Based Experience

**Priority:** P0

> As a logged-in user, I want the product to expose only actions appropriate to my authorized role/permissions so that I can operate safely without unnecessary administrative controls.

**Trace:** `FR-RBAC-001`, `FR-RBAC-002`  
**Acceptance:** `AC-US-RBAC-001-*`

---

## US-RBAC-002 — Protect Pricing Management

**Priority:** P0

> As an Owner, I want pricing changes to require authorized Manager/Owner permission so that a Cashier cannot alter branch prices.

**Trace:** `FR-RBAC-003`, `FR-PRICE-007`  
**Acceptance:** `AC-US-RBAC-002-*`

---

## US-RBAC-003 — Protect Expense Creation

**Priority:** P1

> As an Owner, I want expense creation to require authorized Manager/Owner permission so that branch spending records cannot be created by unauthorized staff.

**Trace:** `FR-RBAC-004`, `FR-EXP-001`  
**Acceptance:** `AC-US-RBAC-003-*`

---

## US-RBAC-004 — Protect Financial Cancel/Void

**Priority:** P1

> As an Owner, I want invoice cancel/void to require protected authorization so that financial history cannot be casually altered.

**Trace:** `FR-RBAC-005`, `FR-INVOICE-006`  
**Acceptance:** `AC-US-RBAC-004-*`  
**Note:** Cancel/Void reason is optional; authorization, traceability, and valid revenue treatment remain required.

---

# 6. EPIC-BRANCH — Branch Context and Isolation

## US-BRANCH-001 — Correct Branch Context

**Priority:** P0

> As an employee, I want every operation to use the correct branch context so that sessions, prices, payments, expenses, and reports belong to the right location.

**Trace:** `FR-BRANCH-001`, `FR-BRANCH-002`  
**Acceptance:** `AC-US-BRANCH-001-*`

---

## US-BRANCH-002 — Prevent Unauthorized Branch Access

**Priority:** P0

> As an Owner, I want users blocked from unrelated branch data so that one branch cannot access another branch's protected operational information.

**Trace:** `FR-BRANCH-003`, `FR-RBAC-007`, `NFR-SEC-004`  
**Acceptance:** `AC-US-BRANCH-002-*`

---

# 7. EPIC-CONSOLE — Console Management and Availability

## US-CONSOLE-001 — Manage Consoles

**Priority:** P1

> As an authorized Manager/Owner, I want to add, edit, activate, and deactivate consoles so that the branch inventory of playable stations reflects reality.

**Trace:** `FR-CONSOLE-001` through `FR-CONSOLE-004`  
**Acceptance:** `AC-US-CONSOLE-001-*`

---

## US-CONSOLE-002 — View Console Availability

**Priority:** P0

> As a Cashier, I want to see which consoles are available so that I can place the next customer quickly.

**Trace:** `FR-CONSOLE-005`, `NFR-UX-002`  
**Acceptance:** `AC-US-CONSOLE-002-*`

---

## US-CONSOLE-003 — Reject Session on Unavailable Console

**Priority:** P0

> As the business owner, I want the system to reject a new session on an unavailable/occupied console so that two customers are not assigned to the same station.

**Trace:** `FR-CONSOLE-006`, `FR-CONSOLE-007`, `NFR-DATA-006`  
**Acceptance:** `AC-US-CONSOLE-003-*`

---

# 8. EPIC-PRICE — Pricing

## US-PRICE-001 — Configure Branch Pricing Matrix

**Priority:** P0

> As an authorized Manager/Owner, I want to configure prices by branch, console type, Single/Multi mode, and Hourly/Match method so that the system can calculate the correct charge.

**Trace:** `FR-PRICE-001` through `FR-PRICE-007`  
**Acceptance:** `AC-US-PRICE-001-*`

---

## US-PRICE-002 — Audit Pricing Changes

**Priority:** P1

> As an Owner, I want pricing changes to be auditable so that I can identify who changed a price and what changed.

**Trace:** `FR-PRICE-008`, `FR-AUDIT-001`  
**Acceptance:** `AC-US-PRICE-002-*`

---

## US-PRICE-003 — Preserve Session Price Snapshot

**Priority:** P0

> As a Cashier, I want a running session to keep the price it started with even if the current branch price changes so that the customer's bill remains consistent.

**Trace:** `FR-PRICE-009`, `FR-PRICE-010`, `NFR-DATA-004`  
**Acceptance:** `AC-US-PRICE-003-*`

---

## US-PRICE-004 — New Sessions Use Updated Price

**Priority:** P0

> As a Manager, I want sessions started after a valid price update to use the new price so that new business follows current pricing.

**Trace:** `FR-PRICE-011`  
**Acceptance:** `AC-US-PRICE-004-*`

---

# 9. EPIC-SESSION — Session Start and Lifecycle

## US-SESSION-001 — Start a Valid Session

**Priority:** P0

> As a Cashier, I want to start a session on an available console with the selected mode and pricing method so that the customer can begin playing and the system starts tracking the operation.

**Trace:** `FR-SESSION-001` through `FR-SESSION-009`  
**Acceptance:** `AC-US-SESSION-001-*`

---

## US-SESSION-002 — Start Open Hourly Session

**Priority:** P0

> As a Cashier, I want to start an Open hourly session with no predefined end time so that the customer can play until requesting to stop.

**Trace:** `FR-OPEN-001`, `FR-OPEN-002`, `FR-OPEN-003`  
**Acceptance:** `AC-US-SESSION-002-*`

---

## US-SESSION-003 — Start Fixed Session and Receive Expiry Alert

**Priority:** P1

> As a Cashier, I want to set an intended duration for a Fixed session and receive a clear alert when the time finishes so that I can respond without the system forcibly ending the session.

**Trace:** `FR-FIXED-001`, `FR-FIXED-002`, `FR-FIXED-003`  
**Acceptance:** `AC-US-SESSION-003-*`

---

## US-SESSION-004 — Run Match Session

**Priority:** P0

> As a Cashier, I want to register a Match session and complete it when the players finish so that the system charges the captured Match price rather than elapsed-time billing.

**Trace:** `FR-MATCH-001` through `FR-MATCH-005`  
**Acceptance:** `AC-US-SESSION-004-*`

---

## US-SESSION-005 — Pause Session

**Priority:** P0

> As a Cashier, I want to pause an eligible active session when the customer requests it so that paused time is not charged.

**Trace:** `FR-PAUSE-001`, `FR-PAUSE-002`, `FR-PAUSE-006`  
**Acceptance:** `AC-US-SESSION-005-*`

---

## US-SESSION-006 — Resume Session

**Priority:** P0

> As a Cashier, I want to resume a paused session so that active billable play continues from the correct state.

**Trace:** `FR-PAUSE-003` through `FR-PAUSE-007`  
**Acceptance:** `AC-US-SESSION-006-*`

---

## US-SESSION-007 — Multiple Pause/Resume Cycles

**Priority:** P1

> As a Cashier, I want the system to preserve multiple pause/resume cycles so that the final bill excludes every paused interval exactly once.

**Trace:** `FR-PAUSE-005`, `FR-PAUSE-006`, `NFR-DATA-005`  
**Acceptance:** `AC-US-SESSION-007-*`

---

## US-SESSION-008 — Change Single/Multi via New Context

**Priority:** P1

> As a Cashier, I want a requested Single/Multi change to close the current context and start a new valid context so that pricing and history remain clear.

**Trace:** `FR-MODE-001`, `FR-MODE-002`  
**Acceptance:** `AC-US-SESSION-008-*`

---

## US-SESSION-009 — Transfer Session Responsibility

**Priority:** P1

> As an authorized Manager, I want to transfer current session responsibility while preserving the original opener and audit context so that employee handover is traceable.

**Trace:** `FR-RBAC-006`, Business Rule `BR-SESSION-010`  
**Acceptance:** `AC-US-SESSION-009-*`

---

# 10. EPIC-BILLING — Time, Completion, and Money

## US-BILLING-001 — Preserve Actual Timestamps

**Priority:** P0

> As the business owner, I want actual session timestamps preserved with seconds internally so that billing, audit, and recovery use accurate events.

**Trace:** `FR-TIME-001`, `NFR-REL-002`  
**Acceptance:** `AC-US-BILLING-001-*`

---

## US-BILLING-002 — Convert Partial Minute Upward

**Priority:** P0

> As a Cashier, I want any positive seconds after a full minute converted to the next billable minute so that the approved minute-billing rule is consistent.

**Trace:** `FR-TIME-002`, `FR-TIME-003`  
**Acceptance:** `AC-US-BILLING-002-*`

---

## US-BILLING-003 — Apply Quarter-Hour Rule

**Priority:** P0

> As a Cashier, I want the approved three-minute-to-next-quarter rule applied consistently so that time billing is predictable and never rounded backward.

**Trace:** `FR-TIME-004` through `FR-TIME-007`  
**Acceptance:** `AC-US-BILLING-003-*`

---

## US-BILLING-004 — Complete Hourly Session and Calculate Charge

**Priority:** P0

> As a Cashier, I want to complete an Hourly session and have active time, pauses, price snapshot, and rounding rules applied automatically so that the final charge is correct.

**Trace:** `FR-COMPLETE-001` through `FR-COMPLETE-007`  
**Acceptance:** `AC-US-BILLING-004-*`

---

## US-BILLING-005 — Apply Whole-EGP Rounding

**Priority:** P0

> As a Cashier, I want piasters rounded using the approved 50-piaster threshold so that the displayed customer amount is a whole number of EGP.

**Trace:** `FR-MONEY-001` through `FR-MONEY-004`  
**Acceptance:** `AC-US-BILLING-005-*`

---

## US-BILLING-006 — Apply Upward Multiple-of-Five Rule

**Priority:** P0

> As a Cashier, I want the next-multiple-of-five rule to increase only by one or two EGP and never decrease the amount so that the agreed final-payment rule is preserved.

**Trace:** `FR-MONEY-005`, `FR-MONEY-006`  
**Acceptance:** `AC-US-BILLING-006-*`

---

# 11. EPIC-FINANCE — Invoice and Cash Payment

## US-FINANCE-001 — Create Invoice from Completed Operation

**Priority:** P0

> As a Cashier, I want an invoice created from the valid completed session so that the customer charge is recorded.

**Trace:** `FR-INVOICE-001` through `FR-INVOICE-004`  
**Acceptance:** `AC-US-FINANCE-001-*`

---

## US-FINANCE-002 — Record Cash Payment

**Priority:** P0

> As a Cashier, I want to record the customer's cash payment against the invoice so that the transaction is financially complete.

**Trace:** `FR-PAY-001` through `FR-PAY-003`  
**Acceptance:** `AC-US-FINANCE-002-*`

---

## US-FINANCE-003 — Prevent Half-Completed Financial Success

**Priority:** P0

> As the business owner, I want the product to avoid reporting success if required session-completion, invoice, local sync-intent, or payment persistence (when cash is actually collected) fails so that financial records remain consistent. Issuing an invoice does not require immediate payment.

**Trace:** `FR-FIN-001`, `FR-FIN-002`, `NFR-DATA-002`  
**Acceptance:** `AC-US-FINANCE-003-*`

---

## US-FINANCE-004 — Protected Cancel/Void

**Priority:** P1 / partially TBD

> As an authorized Manager/Owner, I want to cancel/void an invoice through a protected non-destructive flow so that corrections remain traceable.

**Trace:** `FR-INVOICE-005` through `FR-INVOICE-009`  
**Acceptance:** `AC-US-FINANCE-004-*`  
**Decision:** Cancel/Void reason is optional. Authorization, historical traceability, and valid revenue treatment remain required.

---

# 12. EPIC-SHIFT — Shift

## US-SHIFT-001 — Start Shift

**Priority:** P1

> As an authorized Manager/Owner, I want to start a branch shift so that the operational period has an identifiable start and actor.

**Trace:** `FR-SHIFT-001`, `FR-SHIFT-002`  
**Acceptance:** `AC-US-SHIFT-001-*`

---

## US-SHIFT-002 — End Shift

**Priority:** P1 / partially TBD

> As an authorized Manager/Owner, I want to end a branch shift so that the operational period has an identifiable end.

**Trace:** `FR-SHIFT-003`, `FR-SHIFT-004`, `FR-SHIFT-006`  
**Acceptance:** `AC-US-SHIFT-002-*`  
**Decision:** Active sessions assigned to the closing Cashier are handed to another authorized Cashier before the shift closes; original opener remains historical and current responsibility moves to the receiving Cashier.

---

# 13. EPIC-EXPENSE — Expenses

## US-EXPENSE-001 — Record Branch Expense

**Priority:** P1

> As an authorized Manager/Owner, I want to record a branch expense with amount and description so that operational spending is included in reports.

**Trace:** `FR-EXP-001` through `FR-EXP-004`  
**Acceptance:** `AC-US-EXPENSE-001-*`

---

## US-EXPENSE-002 — Record Equipment Repair Cost

**Priority:** P1

> As an authorized Manager/Owner, I want to record controller/equipment repair cost as an Expense so that repairs affect profit without requiring a separate damage subsystem.

**Trace:** `FR-EXP-005`  
**Acceptance:** `AC-US-EXPENSE-002-*`

---

# 14. EPIC-ASSET — Basic Equipment Foundation

## US-ASSET-001 — Track Basic Equipment Quantities

**Priority:** P1

> As an authorized Manager/Owner, I want to track approved internal equipment quantities by branch so that basic equipment visibility exists without turning R1 into a product-inventory system.

**Trace:** `FR-ASSET-001` through `FR-ASSET-006`  
**Acceptance:** `AC-US-ASSET-001-*`  
**Decision:** R1 tracks branch quantities for Controllers, Accessories, Equipment, and Assets; no product-sales behavior, detailed per-unit damage lifecycle, or advanced transfer.

---

# 15. EPIC-REPORT — Reports

## US-REPORT-001 — View Revenue / Expenses / Profit by Period

**Priority:** P1

> As an authorized Manager/Owner, I want to view Revenue, Expenses, and operational Profit for daily, weekly, monthly, and yearly periods so that I can understand branch performance.

**Trace:** `FR-REPORT-001` through `FR-REPORT-009`  
**Acceptance:** `AC-US-REPORT-001-*`

---

## US-REPORT-002 — Optional Monthly Review

**Priority:** P1

> As an Owner, I want an optional month-end review view so that I can review the completed month's business performance when needed.

**Trace:** `FR-REPORT-010`, `FR-REPORT-011`  
**Acceptance:** `AC-US-REPORT-002-*`  
**Decision:** The monthly review is reporting/review only and does not mutate financial state or lock the period.

---

# 16. EPIC-OFFLINE — Offline Operation

## US-OFFLINE-001 — Continue Core Work Offline

**Priority:** P0

> As a Cashier, I want approved core operations to continue when Internet access fails so that the shop can keep serving customers.

**Trace:** `FR-OFFLINE-001` through `FR-OFFLINE-004`, `NFR-AVL-001`  
**Acceptance:** `AC-US-OFFLINE-001-*`

---

## US-OFFLINE-002 — Know Local Save vs Sync State

**Priority:** P0

> As a Cashier, I want to know whether an operation is saved locally, pending sync, syncing, synced, or failed/conflicted so that I never confuse local success with central synchronization.

**Trace:** `FR-OFFLINE-005`, `NFR-UX-003`  
**Acceptance:** `AC-US-OFFLINE-002-*`

---

## US-OFFLINE-003 — Restrict Sensitive Offline Admin Actions

**Priority:** P1

> As an Owner, I want offline operation to preserve the same role/permission boundaries used online so that losing Internet access never gives any user extra authority.

**Trace:** `FR-OFFLINE-006`, `FR-OFFLINE-007`, `NFR-OFFLINE-006`  
**Acceptance:** `AC-US-OFFLINE-003-*`  
**Design Note:** Security/RBAC will identify protected actions that require current central verification and therefore cannot execute offline.

---

# 17. EPIC-SYNC — Synchronization

## US-SYNC-001 — Queue Local Operations for Sync

**Priority:** P0

> As the system, I want locally committed operations queued for central synchronization so that offline work eventually reaches the central platform.

**Trace:** `FR-LOCAL-001` through `FR-LOCAL-003`, `FR-SYNC-001`  
**Acceptance:** `AC-US-SYNC-001-*`

---

## US-SYNC-002 — Retry Failed Sync Safely

**Priority:** P0

> As the system, I want failed sync operations retried with the same stable operation identity so that transient network/server failures do not lose data.

**Trace:** `FR-SYNC-002`, `FR-SYNC-003`  
**Acceptance:** `AC-US-SYNC-002-*`

---

## US-SYNC-003 — Deduplicate Lost Acknowledgement Retry

**Priority:** P0

> As the business owner, I want a retry after a lost server acknowledgement to return/reconcile the existing result rather than create a duplicate session, invoice, or payment.

**Trace:** `FR-SYNC-004`, `FR-SYNC-005`, `NFR-REL-005`  
**Acceptance:** `AC-US-SYNC-003-*`

---

## US-SYNC-004 — Surface Conflicts and Rejections

**Priority:** P1

> As an authorized user/support operator, I want synchronization conflicts/rejections kept visible without erasing local evidence so that data problems can be investigated and resolved.

**Trace:** `FR-SYNC-007`, `FR-SYNC-008`, `NFR-REL-006`  
**Acceptance:** `AC-US-SYNC-004-*`

---

# 18. EPIC-SUB — Subscription and Offline License

## US-SUB-001 — Verify Subscription While Online

**Priority:** P0

> As the product owner, I want the branch to verify subscription/license state at startup, reconnection, periodically, and at least daily while online so that central subscription state is enforced.

**Trace:** `FR-SUB-001` through `FR-SUB-004`  
**Acceptance:** `AC-US-SUB-001-*`

---

## US-SUB-002 — Allow 72-Hour Offline Lease When More Than 10 Days Remain

**Priority:** P0

> As a paying branch, I want up to 72 hours of offline authority after a successful check when more than 10 subscription days remain so that normal Internet outages do not stop the shop.

**Trace:** `FR-SUB-005`  
**Acceptance:** `AC-US-SUB-002-*`

---

## US-SUB-003 — Use Remaining Subscription Plus 10-Hour Grace Near Expiry

**Priority:** P0

> As a paying branch close to expiry, I want offline authority equal to my remaining subscription time plus the approved 10-hour payment grace so that I have a limited payment window without unlimited free operation.

**Trace:** `FR-SUB-006`  
**Acceptance:** `AC-US-SUB-003-*`

---

## US-SUB-004 — Lock When Offline License Expires

**Priority:** P0

> As the product owner, I want the branch to enter LicenseExpired when its offline authority ends without successful renewal so that intentionally disconnecting Internet cannot bypass payment forever.

**Trace:** `FR-SUB-007`, `FR-SUB-009`  
**Acceptance:** `AC-US-SUB-004-*`

---

## US-SUB-005 — Enforce Central Subscription Suspension

**Priority:** P0

> As the product owner, I want a branch to enter SubscriptionSuspended when the central platform confirms suspension so that unpaid/suspended access can be stopped without deleting business data.

**Trace:** `FR-SUB-008` through `FR-SUB-010`  
**Acceptance:** `AC-US-SUB-005-*`

---

## US-SUB-006 — Prevent Clock Rollback Extension

**Priority:** P0

> As the product owner, I want suspicious Windows clock rollback to be prevented from extending offline authority so that a branch cannot bypass license expiry by changing local time.

**Trace:** `FR-SUB-011`, `NFR-SEC-011`  
**Acceptance:** `AC-US-SUB-006-*`

---

# 19. EPIC-AUDIT — Auditability

## US-AUDIT-001 — Audit Sensitive Actions

**Priority:** P1

> As an Owner/support reviewer, I want important sensitive actions recorded with actor, branch, time, action, and relevant context so that incidents and business changes are traceable.

**Trace:** `FR-AUDIT-001` through `FR-AUDIT-004`  
**Acceptance:** `AC-US-AUDIT-001-*`

---

## US-AUDIT-002 — Keep Secrets out of Audit/Logs

**Priority:** P0

> As the product owner, I want plaintext PINs and private secrets excluded from normal audit/log output so that diagnostics do not become a credential leak.

**Trace:** `FR-AUDIT-005`, `NFR-SEC-006`  
**Acceptance:** `AC-US-AUDIT-002-*`

---

# 20. EPIC-RECOVERY — Crash / Power / Backup Recovery

## US-RECOVERY-001 — Recover Active Session after Restart

**Priority:** P0

> As a Cashier, I want an active or paused persisted session restored after an application restart or power interruption so that the session does not start again from zero.

**Trace:** `FR-REC-001` through `FR-REC-007`, `NFR-REL-001` through `NFR-REL-003`  
**Acceptance:** `AC-US-RECOVERY-001-*`

---

## US-RECOVERY-002 — Avoid Per-Second Database Timer Writes

**Priority:** P1

> As the product owner, I want the visible timer derived from persisted events instead of writing to the database every second so that reliability and performance are improved without losing session timing.

**Trace:** `FR-REC-004`, `NFR-PERF-002`, `NFR-PERF-003`  
**Acceptance:** `AC-US-RECOVERY-002-*`

---

## US-RECOVERY-003 — Local Automatic Backup

**Priority:** P1

> As the business owner, I want the branch database backed up automatically about every 30 minutes so that there is a local recovery layer without requiring extra branch hardware.

**Trace:** `FR-BACKUP-001`, `FR-BACKUP-002`, `NFR-BACKUP-001`, `NFR-BACKUP-002`  
**Acceptance:** `AC-US-RECOVERY-003-*`

---

# 21. Non-Functional Story Constraints

Every relevant story must also satisfy the applicable NFRs.

Examples:

```text
Authentication stories
→ Security + Offline + Usability

Session stories
→ Reliability + Data Integrity + Performance + Recovery

Invoice/Payment stories
→ Data Integrity + Auditability + Idempotency

Sync stories
→ Reliability + Offline Resilience + Observability

Subscription stories
→ Security + Recoverability + Testability
```

A story is not considered complete merely because its happy-path UI works.

---

# 22. Stories Unblocked for UX

The stories that were previously waiting on the five R1 UX-blocking decisions are now requirements-ready:

```text
US-FINANCE-004 → reason optional.
US-SHIFT-002   → handover active sessions before shift close.
US-ASSET-001   → minimum quantity scope defined.
US-REPORT-002  → monthly review is read-only.
US-OFFLINE-003 → offline never elevates permissions.
```

Security/RBAC still owns the later technical question of which protected operations require live central verification in `RestrictedOffline`; this no longer blocks UX role/navigation definition.

---

# 23. R1 Core Journey Story Chain

The minimum operational story chain is:

```text
US-AUTH-001
      ↓
US-CONSOLE-002
      ↓
US-SESSION-001
      ↓
US-SESSION-005 / 006 where needed
      ↓
US-BILLING-004
      ↓
US-FINANCE-001
      ↓
US-FINANCE-002
      ↓
US-OFFLINE-001 when network fails
      ↓
US-SYNC-001 / 002 / 003 when connection returns
      ↓
US-REPORT-001
```

Release 1 is not operationally complete if this chain cannot be executed reliably.

---

# 24. Placement

Recommended filename:

```text
USER_STORIES_R1.md
```

Place in:

```text
Project.docs/Requirements/
```

---

# 25. Next

`ACCEPTANCE_CRITERIA_R1.md` is already present.

Current next step:

```text
RBAC_MATRIX_R1.md + INFORMATION_ARCHITECTURE_R1.md + UX_FLOWS_R1.md   ✅
        ↓
Correct the current 74-screen UI                                      ⏭️ NEXT
        ↓
UI_REVIEW_R1.md + DESIGN_SYSTEM_R1.md
        ↓
System Analysis
```

No code or implementation status is claimed by these user stories.
