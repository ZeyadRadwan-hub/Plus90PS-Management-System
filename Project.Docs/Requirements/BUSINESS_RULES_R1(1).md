# +90 PS — Release 1 Business Rules

**Document Type:** Business Rules  
**Release:** Release 1 — Operational MVP  
**Status:** Consolidated Pre-Code Baseline  
**Last Updated:** 2026-09-09
**Purpose:** Provide one clean, release-specific source for the business and operational rules that govern Release 1.

> This file intentionally contains **rules**, not C4 diagrams, database schema, API endpoints, security implementation, or UI layouts. Those are separate artifacts.

---

# 1. Source-of-Truth Rule

For Release 1, this document is the clean business-rule baseline produced after the AS-IS and TO-BE work.

When an older document conflicts with a rule here because the product decision changed later, the later explicitly approved rule in this file should be treated as the current Release 1 rule until the older document is updated.

The intended traceability is:

```text
AS-IS
  ↓
TO-BE
  ↓
BUSINESS RULES  ← this file
  ↓
BRD
  ↓
PRD
  ↓
Requirements
  ↓
UX / System Analysis
  ↓
Architecture / Data / API / Security
  ↓
Tests
```

---

# 2. Rule ID Convention

```text
BR-SCOPE-*     Release boundaries
BR-ACTOR-*     Actors / roles
BR-BRANCH-*    Branch and ownership
BR-AUTH-*      Authentication
BR-PIN-*       PIN security
BR-SYS-*       System operational state
BR-CONSOLE-*   Console operation
BR-PRICE-*     Pricing
BR-TIME-*      Timing
BR-SESSION-*   Sessions
BR-MATCH-*     Match sessions
BR-FIXED-*     Fixed sessions
BR-PAUSE-*     Pause / resume
BR-MONEY-*     Money rounding
BR-INVOICE-*   Invoices
BR-PAY-*       Payments
BR-SHIFT-*     Shifts
BR-EXP-*       Expenses
BR-ASSET-*     Assets/equipment
BR-REPORT-*    Reports
BR-OFFLINE-*   Offline operation
BR-SYNC-*      Synchronization behavior
BR-SUB-*       Subscription / offline license
BR-AUDIT-*     Audit behavior
BR-RECOVERY-*  Recovery / resilience
```

---

# 3. Release 1 Scope Rules

## BR-SCOPE-001 — Operational MVP
Release 1 must be usable for the core daily operation of a real PlayStation shop without depending on Release 2 or Release 3.

## BR-SCOPE-002 — Customer Accounts Deferred
Customer accounts are not part of Release 1.

## BR-SCOPE-003 — Customer History Deferred
Customer history is not part of Release 1.

## BR-SCOPE-004 — Full Booking Deferred
The full booking/reservation system is not part of Release 1.

## BR-SCOPE-005 — Website Deferred
A public/customer Website is not part of Release 1.

## BR-SCOPE-006 — Mobile Deferred
A Mobile application is not part of Release 1.

## BR-SCOPE-007 — Product/Cafeteria Sales Excluded
Release 1 does not include food, drink, cafeteria, or general product sales.

## BR-SCOPE-008 — POS Hardware Excluded
Release 1 does not require printer, barcode, or dedicated POS hardware integration.

## BR-SCOPE-009 — Membership Excluded
Membership is not part of Release 1.

## BR-SCOPE-010 — Discounts Deferred
Discount functionality is not a required Release 1 capability in the current approved direction. Predefined discounts, manual discounts, and advanced discount controls are deferred to a later release unless explicitly re-approved for R1.

---

# 4. Actors and Roles

## BR-ACTOR-001 — Primary Staff Roles
Release 1 recognizes Owner, Manager, and Cashier.

## BR-ACTOR-002 — Admin Meaning
“Admin” is not required to be a separate business role. An administrative action may be performed by an Owner or a Manager with the required permission.

## BR-ACTOR-003 — Customer Has No R1 Login
The customer does not authenticate into Release 1. The customer's operational request is handled through the Cashier.

## BR-ACTOR-004 — Personal Staff Identity
Each staff member who uses the application must have an identifiable application user.

## BR-ACTOR-005 — Shared PC
Multiple employees may use the same branch PC over time. The Windows user account is not sufficient as the business identity.

---

# 5. Branch and Ownership Rules

## BR-BRANCH-001 — Unique Branch Context
Every operational record must belong to the correct branch context where applicable.

## BR-BRANCH-002 — Branch Isolation
Users must not gain access to unrelated branch operational data.

## BR-BRANCH-003 — Ownership Isolation
An Owner may access only business/branch data within the authorized ownership scope.

## BR-BRANCH-004 — Cashier Branch Scope
A Cashier operates only within the authorized branch context.

## BR-BRANCH-005 — Manager Branch Scope
A Manager does not automatically gain access to unrelated branches.

## BR-BRANCH-006 — Branch-Specific Configuration
Release 1 must allow branch-specific operational configuration where required, especially pricing.

---

# 6. Authentication Rules

## BR-AUTH-001 — Personal PIN
Each employee uses a personal PIN for application authentication.

## BR-AUTH-002 — Current User Is Always Known
While an authenticated operation is being performed, the application must know the current employee identity.

## BR-AUTH-003 — User Context
The active business user context must be able to determine User, Role, Branch, and Permissions.

## BR-AUTH-004 — Offline Authentication Required
Approved staff authentication must continue to work offline within the allowed offline security/license policy.

## BR-AUTH-005 — Plaintext PIN Prohibited
PINs must not be stored as readable plaintext.

## BR-AUTH-006 — Forgotten PIN Is Reset, Not Retrieved
If a PIN is forgotten, an authorized reset creates a new PIN. The system must not depend on showing the old PIN from the database.

---

# 7. PIN Security Rules

## BR-PIN-001 — First Failure Threshold
After five failed PIN attempts in the defined attempt cycle: 20-second temporary lock + warning sound + security event.

## BR-PIN-002 — Second Failure Threshold
If another five wrong PIN attempts occur after the first temporary lock cycle, the system enters `SecurityLocked`.

## BR-PIN-003 — Security Lock Message
When SecurityLocked, the system displays a clear blocking message telling the user to contact Engineer Ziyad for recovery.

## BR-PIN-004 — Security Lock Does Not Delete Data
SecurityLocked must never delete sessions, invoices, payments, expenses, audit history, or branch configuration.

## BR-PIN-005 — Online Security Unlock
When connectivity exists, an authorized central/owner recovery action may unlock the branch according to the later Security/API contract.

## BR-PIN-006 — Offline Security Unlock
If the branch is fully offline, Release 1 must support a controlled one-time challenge → authorized recovery response → local verification → unlock process.

## BR-PIN-007 — No Permanent Master PIN
A permanent universal master PIN that opens every branch must not be used.

## BR-PIN-008 — Offline Recovery Code Properties
The offline recovery code/response must be one-time, branch-specific, and challenge-specific.

## BR-PIN-009 — PIN Recovery Cannot Bypass Subscription Lock
PIN/security recovery may recover SecurityLocked, but must not bypass SubscriptionSuspended or LicenseExpired.

---

# 8. System State Rules

## BR-SYS-001 — Explicit Operational States
The Release 1 system model must distinguish at least:

```text
Operational
OfflineOperational
RestrictedOffline
SecurityLocked
SubscriptionSuspended
LicenseExpired
Maintenance
```

## BR-SYS-002 — State Meanings Must Not Be Collapsed
Security, subscription, offline, and maintenance states must not be represented as one undifferentiated “locked” state in business behavior.

## BR-SYS-003 — Data Preservation Across Locks
Entering a locked/restricted state must not erase operational or financial history.

---

# 9. Console Rules

## BR-CONSOLE-001 — Branch Ownership
Every console belongs to a branch.

## BR-CONSOLE-002 — Console Type
Release 1 supports at least PS4 and PS5.

## BR-CONSOLE-003 — Availability Required
A session may start only on a console that is operationally available for a new session.

## BR-CONSOLE-004 — No Double Active Session
A console must not have two simultaneous active gaming sessions.

## BR-CONSOLE-005 — Authorized Console Management
Only an authorized administrative user can add/edit/activate/deactivate console configuration.

## BR-CONSOLE-006 — Final State Model Deferred
The detailed console state machine will be finalized during System Analysis.

---

# 10. Pricing Rules

## BR-PRICE-001 — Branch-Specific Pricing
Pricing is branch-specific.

## BR-PRICE-002 — Console-Type Dimension
Pricing may differ between PS4 and PS5.

## BR-PRICE-003 — Mode Dimension
Pricing may differ between Single and Multi.

## BR-PRICE-004 — Pricing Method Dimension
Pricing supports Hourly and Match.

## BR-PRICE-005 — Independent Price Combinations
The system must support independent valid prices for:

```text
PS4 + Single + Hourly
PS4 + Multi  + Hourly
PS5 + Single + Hourly
PS5 + Multi  + Hourly
PS4 + Single + Match
PS4 + Multi  + Match
PS5 + Single + Match
PS5 + Multi  + Match
```

Not every branch must enable every combination.

## BR-PRICE-006 — Missing Price Blocks Start
A session must not start if a required applicable price is missing or invalid.

## BR-PRICE-007 — Authorized Price Changes
A Cashier cannot change branch pricing. An authorized Manager/Owner may change pricing according to the final permission matrix.

## BR-PRICE-008 — Price Change Audit
A sensitive price change should preserve who changed it, branch, pricing entry, old value, new value, and timestamp.

## BR-PRICE-009 — Price Snapshot
A session captures the applicable pricing context when it starts.

## BR-PRICE-010 — Running Session Is Stable
Changing the current branch price after a session starts must not silently change the captured price used by that running session.

## BR-PRICE-011 — New Session Uses New Price
A new session started after the approved price change uses the new valid price.

---

# 11. Time Recording Rules

## BR-TIME-001 — Actual Timestamps
The system preserves actual timestamps with sufficient precision for recovery and audit.

```text
StartAt = 5:10:23 PM
EndAt   = 6:28:03 PM
```

## BR-TIME-002 — Billing Is Minute-Based
Seconds may be stored internally, but billing is not performed as fractional-second billing.

## BR-TIME-003 — Partial Minute Rounds Up

```text
1:17:00 → 1:17
1:17:01 → 1:18
1:17:40 → 1:18
1:17:59 → 1:18
1:18:00 → 1:18
```

Any positive seconds beyond a completed minute push the billable base to the next minute.

## BR-TIME-004 — Quarter-Hour Rule
After converting to the billable minute base:
- If the remaining time to the next quarter-hour point is 3 minutes or less, round upward to that quarter-hour.
- Otherwise keep the billable minute duration.
- A duration already exactly on the quarter-hour stays unchanged.

Examples:

```text
2:20 → 2:20
2:27 → 2:30
2:42 → 2:45
2:57 → 3:00
3:00 → 3:00
```

## BR-TIME-005 — Never Round Time Down Under This Rule
The approved quarter-hour behavior does not reduce a duration to an earlier quarter-hour.

## BR-TIME-006 — No Minimum Charge
Release 1 has no minimum charge.

---

# 12. Session Rules

## BR-SESSION-001 — Authorized Start
Only an authorized logged-in user may start a session.

## BR-SESSION-002 — Opening User Is Recorded
The employee who opens the session must be recorded.

## BR-SESSION-003 — Branch Is Recorded
A session belongs to the correct branch.

## BR-SESSION-004 — Console Is Recorded
A session belongs to the selected console.

## BR-SESSION-005 — Pricing Context Is Recorded
The session preserves Console Type, Mode, Pricing Method, and Applicable Price Snapshot.

## BR-SESSION-006 — Session Start Is Persisted
The session start must become persistent before the UI reports a successful start.

## BR-SESSION-007 — Completed Session Is Historical
A completed session is not arbitrarily reopened as if it had never completed.

## BR-SESSION-008 — Valid State Transitions
Invalid transitions such as Resume on a non-paused session must be rejected.

## BR-SESSION-009 — Session Responsibility
The system may distinguish the original opener from the current responsible employee.

## BR-SESSION-010 — Authorized Responsibility Transfer
If session responsibility is transferred, it requires the authorized manager process and retains original opener, new responsible user, time, reason where required, and audit evidence.

## BR-SESSION-011 — Single/Multi Cannot Change In Place
Single/Multi must not be changed in-place inside the current session pricing context. The current context must be completed/closed, then a new valid context starts.

---

# 13. Open Hourly Session Rules

## BR-SESSION-OPEN-001 — Open Has No Predefined End
An Open hourly session starts without a fixed end time.

## BR-SESSION-OPEN-002 — Employee Completes on Customer Request
The customer asks to finish, and the employee completes the Open session.

## BR-SESSION-OPEN-003 — No Automatic End
The system does not automatically end an Open session.

---

# 14. Pause / Resume Rules

## BR-PAUSE-001 — Customer May Request Pause
The customer is allowed to request Pause.

## BR-PAUSE-002 — Pause Is Free
Paused time is not billable.

## BR-PAUSE-003 — Pause Event Is Persisted
The system records the pause event/timestamp.

## BR-PAUSE-004 — Resume Event Is Persisted
The system records the resume event/timestamp.

## BR-PAUSE-005 — Multiple Pauses Supported
The design must support multiple Pause/Resume cycles without losing earlier pauses.

## BR-PAUSE-006 — Paused Time Is Counted Once
Paused periods must not be double-counted when calculating billable active duration.

## BR-PAUSE-007 — Pause Eligibility by Session Type
Pause/Resume is allowed only for Open and Fixed Sessions. Match Sessions cannot Pause/Resume. The future full Session aggregate/Application workflow must enforce this; the BE-02 timing component alone does not decide eligibility.

---

# 15. Fixed Session Rules

## BR-FIXED-001 — Fixed Session Has Intended Duration
A Fixed session records an intended duration.

## BR-FIXED-002 — Expiry Generates Alert
When the intended duration is reached, the system shows a prominent center-screen message that the selected console's reserved/fixed time has finished.

## BR-FIXED-003 — No Automatic Forced Stop
Reaching the intended duration does not automatically terminate the session.

## BR-FIXED-004 — Employee Remains Responsible
The employee handles the operational follow-up after the alert.

---

# 16. Match Rules

## BR-MATCH-001 — Match Is a Separate Pricing Method
Match is not billed as Hourly.

## BR-MATCH-002 — Match Price Is Not Calculated from Elapsed Time
The system does not calculate the Match charge based on how long the football match lasted.

## BR-MATCH-003 — Employee Registers the Match
The Cashier/employee records that the customer is playing a Match.

## BR-MATCH-004 — Employee Determines Match End
The employee determines when the Match has finished.

## BR-MATCH-005 — No Automatic Timer End
The Match is not automatically ended by a system timer in the approved Release 1 business process.

## BR-MATCH-006 — Match Uses Applicable Match Price
Billing uses the valid branch/console/mode Match price captured for that match session.

## BR-MATCH-007 — One Match per Session
One Match Session represents exactly one match. A second match requires a new Session. The branch-configured Match duration is a reference, not an automatic end or a source of elapsed-time pricing.

---

# 17. Money Rounding Rules

## BR-MONEY-001 — Final Customer Amount Is Whole EGP
The displayed/collected final amount does not show piasters.

## BR-MONEY-002 — 50-Piaster Threshold

```text
100.00 → 100
100.01 → 100
100.49 → 100
100.50 → 101
100.99 → 101
```

Fraction < 0.50 keeps the current whole EGP; fraction >= 0.50 moves to the next whole EGP.

## BR-MONEY-003 — Multiple-of-Five Rule Is Upward Only
After whole-EGP rounding, the system may move the price to the next multiple of 5 only when that increase is 1 or 2 EGP.

## BR-MONEY-004 — No Downward Multiple-of-Five Rounding

```text
100 → 100
101 → 101
102 → 102
103 → 105
104 → 105
105 → 105
106 → 106
107 → 107
108 → 110
109 → 110
110 → 110
```

Invalid: `102 → 100`.

---

# 18. Invoice Rules

## BR-INVOICE-001 — Invoice Belongs to Branch
Every invoice belongs to the correct branch.

## BR-INVOICE-002 — Invoice Comes from Valid Operation
An invoice is created from a valid completed financial/session operation.

## BR-INVOICE-003 — One Accepted Completion Must Not Duplicate Invoice
Retry/synchronization must not create duplicate invoices for the same accepted operation.

## BR-INVOICE-004 — Free Destructive Delete Is Prohibited
Operational users must not freely erase financial invoice history.

## BR-INVOICE-005 — Protected Cancel/Void
Cancellation/void of an eligible unpaid Invoice is a protected operation requiring appropriate authorization. A successfully paid Invoice cannot be cancelled (BR-INVOICE-011).

## BR-INVOICE-006 — Cancelled/Void Record Remains Historical
A cancelled/void invoice remains traceable in history.

## BR-INVOICE-007 — Cancelled/Void Revenue Treatment
A cancelled/void invoice must not silently count as valid active revenue.

## BR-INVOICE-008 — Financial Completion Must Be Persisted Before Success
The UI must not report a successful invoice/payment completion before required local financial persistence succeeds.

## BR-INVOICE-009 — Cancellation Reason Is Optional
The protected Cancel/Void flow may include a reason field, but entering a reason is optional. A valid authorized cancellation must be able to proceed without a reason. If a reason is entered, it is preserved with the cancellation/audit context.

## BR-INVOICE-010 — Invoice May Precede Payment
An invoice may be issued before cash payment. Payment is a separate business step; issuing an invoice does not require immediate cash receipt.

## BR-INVOICE-011 — Paid Invoice Cannot Be Cancelled
Once an Invoice has a successful recorded Payment, Cancel must be rejected. Protected Cancel applies only to an eligible unpaid Invoice; an unpaid cancelled Invoice remains historical and is excluded from active revenue. Cancel does not automatically refund or reverse cash, and neither paid nor cancelled invoices are destructively deleted. The distinction between Cancel and Void for unpaid states remains to be finalized.

## BR-INVOICE-012 — Total Paused Duration on Invoice
If an Open or Fixed Session has one or more Pause intervals, its customer-facing Invoice shows the total free/non-billable paused duration. Individual intervals may remain in history/audit but are not listed on the customer Invoice by default.

---

# 19. Payment Rules

## BR-PAY-001 — Release 1 Payment Method
Cash is the only Release 1 payment method. This does not require cash to be received at the moment an invoice is issued.

## BR-PAY-002 — Payment Is Linked to Invoice
A payment is associated with the relevant invoice/financial transaction.

## BR-PAY-003 — Payment Is Persistent
A recorded cash payment must become persistent.

## BR-PAY-004 — Payment Sync Is Idempotent
Retrying synchronization must not record the same cash payment twice.

## BR-PAY-005 — Financial Atomicity
Where Session Completion + Invoice + Pending Sync are one business completion, the local process should treat the required records as one controlled commit boundary. If cash is collected in that same operation, include Payment in its required commit boundary. An invoice may also be issued before Payment; neither path may show success before its own required local records persist.

---

# 20. Shift Rules

## BR-SHIFT-001 — Administrative Start
A shift is started by an authorized Manager or Owner.

## BR-SHIFT-002 — Administrative End
A shift is ended by an authorized Manager or Owner.

## BR-SHIFT-003 — Shift Records Context
A shift records at least the relevant Branch, Authorized Actor, Start Time, and End Time when closed.

## BR-SHIFT-004 — Cashier Does Not Gain Admin Shift Permission by Default
A Cashier does not automatically get Start Shift / End Shift permission.

## BR-SHIFT-005 — Active Sessions Must Be Handed Over Before Shift Close
If the shift being closed still has active sessions assigned to its Cashier, an authorized Manager/Owner must transfer those active sessions to another authorized Cashier before the shift closes.

## BR-SHIFT-006 — Handover Preserves Original Opener
A shift/session handover preserves the original `OpenedBy` employee for history and audit, while `CurrentResponsibleUser` becomes the receiving Cashier.

## BR-SHIFT-007 — Responsibility After Handover
From the handover time forward, the active session is operationally the responsibility of the receiving Cashier and is attributed to that receiving Cashier for the remaining shift/session responsibility. The transfer itself must be auditable.

---

# 21. Expense Rules

## BR-EXP-001 — Authorized Expense Creation
Expenses are created only by an authorized Manager/Owner according to the permission matrix.

## BR-EXP-002 — Expense Belongs to Branch
Every expense belongs to a branch.

## BR-EXP-003 — Expense Information
An expense preserves at least Amount, Description/Category, Actor, Timestamp, and Branch. A note may be used where relevant.

## BR-EXP-004 — Controller Repair Is an Expense
If controllers/equipment are damaged and repaired, Release 1 does not require a dedicated damage workflow. Repair cost can be recorded as an Expense with a note such as “Repair of 2 controllers”.

## BR-EXP-005 — Expenses Affect Profit
Operational profit is `Revenue - Expenses = Profit`. This is operational reporting, not a full accounting system.

---

# 22. Asset / Equipment Rules

## BR-ASSET-001 — No Product-Sales Inventory
Internal equipment/assets must not be confused with stock for product sales.

## BR-ASSET-002 — Quantity Model Where Appropriate
Release 1 retains basic branch-scoped quantity tracking for internal equipment where appropriate. The approved R1 equipment categories are Controllers, Accessories, Equipment, and Assets. Console devices continue to be managed through Console Management rather than duplicated as quantity-only equipment records.

## BR-ASSET-003 — No Dedicated Damage Lifecycle Required
Release 1 does not need a detailed per-controller damage/repair lifecycle. Repair cost can be represented through Expenses.

## BR-ASSET-004 — Advanced Transfer Deferred
Advanced inter-branch asset transfer is not required in Release 1.

## BR-ASSET-005 — Minimum R1 Asset Capability
An authorized Manager/Owner can view and maintain the approved branch-scoped quantity records for the R1 internal equipment categories. Release 1 does not require per-unit serial tracking, product-sales inventory behavior, or a dedicated damage lifecycle.

---

# 23. Reporting Rules

## BR-REPORT-001 — Daily Reports
Release 1 supports daily reporting.

## BR-REPORT-002 — Weekly Reports
Release 1 supports weekly reporting.

## BR-REPORT-003 — Monthly Reports
Release 1 supports monthly reporting.

## BR-REPORT-004 — Yearly Reports
Release 1 supports yearly reporting.

## BR-REPORT-005 — Core Financial Views
Release 1 provides Revenue, Expenses, and Profit operational views.

## BR-REPORT-006 — Revenue Comes from Valid Financial Operations
Revenue is derived from valid financial/payment records as operations occur. It is not created only at month-end.

## BR-REPORT-007 — Optional Monthly Review
The Owner may use an optional month-end review function. This does not replace normal continuous recording of revenue and expenses.

## BR-REPORT-008 — Monthly Review Is Read-Only Business Review
The monthly review is a reporting/review action only. It does not lock the accounting period, mutate invoice/payment state, or create a separate financial closing state in Release 1.

## BR-REPORT-009 — Reports Respect Scope
Reports must respect authorized branch/ownership scope.


---

# 24. Offline Operation Rules

## BR-OFFLINE-001 — Core Operation Continues Without Internet
Approved core branch operation must continue when Internet access is unavailable, subject to valid security/subscription offline authority.

## BR-OFFLINE-002 — Same Business Path
Online and offline operation must not become two separate business processes that produce different records.

## BR-OFFLINE-003 — Local Persistence Before Success
Core branch business operations are persisted locally before success is shown.

## BR-OFFLINE-004 — Local Success Is Different from Sync Success
The business/UI state must distinguish Saved Locally, Pending Sync, Synced, and Sync Failed/Rejected/Conflict.

## BR-OFFLINE-005 — Approved Offline Operational Actions
Subject to permissions/license, offline operations include Login, View required local branch data, Start Session, Pause, Resume, Complete Session, Calculate Charge, Create Invoice, and Record Cash Payment.

## BR-OFFLINE-006 — Offline Does Not Elevate Role Permissions
Offline operation does not give a user permissions that the same role does not have online. A Cashier remains limited to Cashier-authorized actions; Manager and Owner actions remain permission-controlled.

## BR-OFFLINE-007 — Central-Verification Dependency Is a Security Design Detail
A protected administrative action that inherently requires current central verification may be unavailable while offline. The exact technical enforcement belongs to Security/RBAC Design, but the business rule is fixed: offline mode never creates extra privileges.

---

# 25. Synchronization Rules

## BR-SYNC-001 — Pending Sync Record
A locally committed operation that requires central synchronization must become a pending synchronization item.

## BR-SYNC-002 — Retry Required
Failed synchronization must support retry.

## BR-SYNC-003 — Stable Operation Identity
A synchronization operation must have a stable identity so retry can be recognized as the same operation.

## BR-SYNC-004 — No Duplicate Business Effect
Retrying the same accepted operation must not create a duplicate Session, Invoice, or Payment.

## BR-SYNC-005 — Lost Acknowledgement Is Safe
If the server commits an operation but the acknowledgement is lost, resending the same stable operation must reconcile with the existing accepted result instead of creating another financial record.

## BR-SYNC-006 — Conflict Is Visible
A central rejection/conflict must be visible to an authorized user/process.

## BR-SYNC-007 — Conflict Does Not Silently Erase Local Evidence
A rejected/conflicting local operation must not be silently deleted just to make local and central state appear equal.

## BR-SYNC-008 — Financial Conflict Is Not Silently Overwritten
Financial data must not be silently modified to hide a synchronization conflict.

---

# 26. Subscription / Offline License Rules

## BR-SUB-001 — Offline Cannot Bypass Payment Forever
A branch must not be able to avoid subscription enforcement indefinitely simply by disconnecting Internet access.

## BR-SUB-002 — Online Subscription Checks
When connectivity exists, subscription/license verification occurs at Application Startup, Internet Reconnection, Periodic Check During Operation, and at least daily while online.

## BR-SUB-003 — Verifiable Offline Authority
A successful online verification provides/refreshes verifiable offline authority for that branch. The exact signed-license format belongs to Security Design.

## BR-SUB-004 — More Than 10 Days Remaining
If the paid subscription has more than 10 days remaining when a successful verification occurs, maximum offline lease is 72 hours.

## BR-SUB-005 — 10 Days or Less Remaining
If the subscription has 10 days or less remaining:

```text
Offline Allowed Time
=
Remaining Subscription Time
+
10 Hours Payment Grace
```

Examples:

```text
10 days remaining → 10 days + 10 hours
8 days remaining  → 8 days + 10 hours
7 days remaining  → 7 days + 10 hours
3 days remaining  → 3 days + 10 hours
1 day remaining   → 1 day + 10 hours
```

## BR-SUB-006 — 10-Hour Grace Meaning
The extra 10 hours are a payment grace period, not the original paid subscription duration.

## BR-SUB-007 — Offline License Expiration Locks Operation
If the offline-authority window ends and no successful renewal/verification occurs, the system enters LicenseExpired and operational use is locked even if the branch remains offline.

## BR-SUB-008 — Central Suspension Locks Operation
If the central system successfully communicates that the branch subscription is suspended, the system enters SubscriptionSuspended.

## BR-SUB-009 — Subscription Lock Never Deletes Business Data
Subscription suspension/expiration must not delete business history.

## BR-SUB-010 — Clock Rollback Must Not Extend License
Changing the Windows clock backward must not be allowed to extend offline subscription authority. Suspicious rollback requires restricted behavior or successful trusted verification.

---

# 27. Audit Rules

## BR-AUDIT-001 — Sensitive Actions Are Auditable
Sensitive actions should create audit evidence.

## BR-AUDIT-002 — Candidate Sensitive Events
Audit coverage includes relevant events such as repeated login failure, security lock/unlock, PIN reset, pricing change, permission/user change, expense creation, protected invoice cancel/void, authorized session responsibility transfer, subscription state change, and important sync conflict/rejection.

## BR-AUDIT-003 — Audit Identity
Audit records should preserve Actor, Branch, Action, Timestamp, Affected Record, Reason where required, Before/After where appropriate, and Operation Identifier.

## BR-AUDIT-004 — Secrets Are Not Audit Data
Do not store plain PINs, passwords, private signing keys, or connection secrets in normal audit/log output.

---

# 28. Recovery / Persistence Rules

## BR-RECOVERY-001 — RAM Is Not Permanent Business State
The system must not depend only on RAM/UI state to remember a business-critical active session.

## BR-RECOVERY-002 — Session Events Are Persisted
Important session events such as Start, Pause, Resume, and Complete are persisted.

## BR-RECOVERY-003 — No Per-Second Database Timer Write Required
The system does not need to write the timer value to the database every second. Elapsed display is derived from persisted timestamps/events.

## BR-RECOVERY-004 — Restart Restores Active Session
After application restart/power return, an active persisted session must be recoverable using stored state/timestamps rather than restarting from zero.

## BR-RECOVERY-005 — Business Success Requires Persistent Commit
A business action must not be presented as successfully completed if the required local persistent commit failed.

## BR-RECOVERY-006 — Local Backup Direction
The current Release 1 operational direction includes automatic local SQLite backup approximately every 30 minutes.

## BR-RECOVERY-007 — No Required Extra Branch Hardware
Release 1 does not require the shop to purchase External SSD, NAS, UPS, or special backup hardware as a condition for use.

## BR-RECOVERY-008 — Central Backup Targets
Current central SQL Server backup baseline:

```text
Full Backup            → Nightly
Differential Backup    → Every 6 Hours
Transaction Log Backup → Every 5 Minutes
```

## BR-RECOVERY-009 — Central RPO Target
Current Release 1 central RPO target: `≤ 5 Minutes`.

## BR-RECOVERY-010 — Branch RTO Target
After power/device availability returns, branch operational recovery target: `≤ 5 Minutes`.

## BR-RECOVERY-011 — Central RTO Target
Current Release 1 central API + SQL Server recovery target: `≤ 4 Hours`.

## BR-RECOVERY-012 — Central Failure Must Not Lose Committed Local Work
If the central API/database is unavailable but the branch PC/local storage remains healthy, locally committed branch operations remain available for later synchronization.

---

# 29. Superseded / Corrected Older Rules

## 29.1 Discounts in R1
Older R1 material that requires discounts in Release 1 is superseded by the current decision to defer discounts.

## 29.2 Match Duration / Timer-Based Completion
Older material that assumes Match must have a system-controlled fixed duration or automatic timer completion is superseded. Current direction: employee registers Match, employee determines Match finished, and Match price is not calculated from elapsed match time.

## 29.3 PIN Retrieval
Any design that depends on retrieving the employee's old readable PIN is superseded. Current direction is Reset PIN, not Read Old PIN.

## 29.4 Online-vs-Offline Dual Creation Path
Any process that treats central online creation and local offline creation as two separate authoritative business paths is superseded. Current direction is Persist Locally First → Sync Centrally.

---

# 30. Decisions Closed for UX / Remaining Design Detail

The following business decisions are now closed for Release 1 UX and requirements:

```text
Shift close with active sessions
→ Transfer active session responsibility to another Cashier before closing the shift.

Invoice Cancel/Void reason
→ Reason field is optional; authorization and non-destructive history remain required.

Basic Asset scope
→ Branch-scoped quantity tracking for Controllers / Accessories / Equipment / Assets; no product-sales inventory, no per-unit damage lifecycle, no advanced transfer.

Monthly Closing
→ Review/report only; no financial-state mutation or accounting-period lock in R1.

Offline role behavior
→ Offline never elevates permissions. Role/permission boundaries remain in force.
```

Remaining later **design detail**, not an unresolved business permission decision:

- Security/RBAC Design will define which protected actions technically require live central verification and therefore cannot execute in `RestrictedOffline`.

---

# 31. Business Rule Invariants

```text
1.  A second active session cannot silently start on an occupied console.
2.  A session preserves its opening employee.
3.  A running session preserves its applicable price snapshot.
4.  Pause is free and excluded from billable active duration.
5.  A Match is not billed as Hourly.
6.  Match completion is employee-driven in the approved R1 process.
7.  Fixed-duration expiry alerts; it does not auto-stop.
8.  Seconds may be persisted, while billing is minute-based.
9.  Positive leftover seconds round the billable base to the next minute.
10. The approved quarter-hour time rule only rounds upward.
11. There is no minimum charge.
12. Final displayed payment is whole EGP.
13. The multiple-of-five money rule never lowers the amount.
14. Financial success is not shown before required persistence succeeds.
15. Internet loss does not erase locally committed work.
16. Sync retry does not duplicate money.
17. Power/app restart does not reset a persisted active session to zero.
18. Security lock does not delete business data.
19. Subscription lock does not delete business data.
20. PIN recovery cannot bypass subscription enforcement.
21. Offline mode cannot be used forever to avoid payment.
22. Branch/ownership isolation applies regardless of UI behavior.
23. Release 1 does not silently absorb Release 2 customer/booking scope.
24. Discounts are currently deferred from R1.
25. A shift with active assigned sessions is not closed until those sessions are handed to another authorized Cashier.
26. Invoice Cancel/Void reason is optional, not mandatory.
27. Monthly review is read-only and does not create a financial closing state.
28. Offline mode never elevates a user beyond normal role/permission boundaries.
```

---

# 32. Traceability Into the Next Artifacts

Each later requirement should reference one or more rule IDs from this document.

```text
Business Rule
      ↓
Functional Requirement
      ↓
User Story
      ↓
Acceptance Criteria
      ↓
Use Case
      ↓
Sequence Diagram
      ↓
Database / API
      ↓
Security / Offline Rules
      ↓
Tests
```

---

# 33. Placement

Recommended filename:

```text
BUSINESS_RULES_R1.md
```

Keep it as its own artifact.

Historical flat-layout example (not this repository's current paths; see [README.md](../../README.md) for the actual solution and documentation locations):

```text
+90PS/
├── DISCOVERY(1).md
├── TO_BE_PROCESS.md
├── BUSINESS_RULES_R1.md
├── BUSINESS_RULES(1).md     ← keep as historical/merged source if desired
├── R1_OPERATIONAL_MVP.md
├── R1_ACCEPTANCE.md
├── DECISIONS.md
├── ROADMAP.md
└── ...
```

If folders are introduced later without renaming the file:

```text
docs/
└── 02_Requirements/
    └── BUSINESS_RULES_R1.md
```

---

# 34. Historical Pre-Code Status (as written 2026-09-09)

> This section is preserved as a historical planning snapshot. It does not describe the current repository: BE-00 through BE-04 now contain tested backend foundation and Domain code. See [CURRENT_STATE.md](../Reviews/CURRENT_STATE.md) for current implementation status; the wider R1 gate remains HOLD.

```text
AS-IS                              ✅
TO-BE                              ✅
Business Rules R1                  ✅ Updated baseline
BRD                                ✅
PRD                                ✅
Functional Requirements            ✅
Non-Functional Requirements        ✅
User Stories                       ✅
Acceptance Criteria                ✅
RBAC UX Matrix                     ✅
Information Architecture          ✅
UX Flows                           ✅

Current 74-screen UI correction    ⏭️ NEXT
UI Review                          ⏳ After corrected UI
Design System                      ⏳ After corrected UI
System Analysis                    ⏳
Architecture / Data / API / Sec   ⏳
Code                               0%
```

No code, database implementation, security implementation, or runtime test result is claimed by this document.
