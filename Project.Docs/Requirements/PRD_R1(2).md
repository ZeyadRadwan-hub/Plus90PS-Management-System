# +90 PS — Release 1 Product Requirements Document (PRD)

**Document Type:** Product Requirements Document  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Product Baseline  
**Last Updated:** 2026-09-09
**Primary Inputs:** `TO_BE_PROCESS.md`, `BUSINESS_RULES_R1.md`, `BRD_R1.md`  
**Purpose:** Convert the approved Release 1 business direction into a product-level specification that can drive requirements, UX, system analysis, architecture, database, API, security, offline/sync, and testing.

---

# 1. Product Summary

+90 PS Release 1 is the first operationally usable version of a PlayStation-shop management product.

It is intended to replace manual session timing, manual pricing lookup, manual charge calculation, weak recovery, and disconnected operational records with a structured desktop-first system that can continue approved branch operation without Internet connectivity and synchronize later.

Release 1 is not the complete final product. It deliberately focuses on the shop's core daily operation.

---

# 2. Product Mission

> Enable a real PlayStation shop to operate its core daily workflow through +90 PS with correct timing, pricing, billing, cash records, expenses, reporting, offline continuity, security controls, and recovery.

The product must be dependable enough that the shop does not have to return to a notebook merely because the Internet is unavailable.

---

# 3. Product Context

Release 1 is designed around:

```text
Branch Staff
    ↓
WPF Desktop Client
    ↓
Local Operational Persistence
    ↓
Background Synchronization
    ↓
Central ASP.NET Core API
    ↓
Central SQL Server
```

This PRD describes product behavior, not the final technical architecture.

The exact C4, ERD, API contracts, cryptography, database transactions, and deployment topology are defined later.

---

# 4. Product Users

## 4.1 Cashier

Primary product goals:

- Log in quickly.
- See available consoles.
- Start a session correctly.
- Pause/resume when requested.
- Finish the session accurately.
- See the correct charge.
- Record cash payment.
- Continue core work during approved offline operation.
- Know whether data is saved locally or synchronized.

## 4.2 Manager

Primary product goals:

- Perform branch administration.
- Start/end shifts.
- Manage consoles.
- Manage pricing.
- Manage users according to permission.
- Reset employee PINs.
- Record expenses.
- View reports.
- Authorize protected actions.
- Review operational/sync problems.

## 4.3 Owner

Primary product goals:

- View authorized business information.
- Manage ownership-level access.
- Review reports.
- Perform approved branch/security recovery.
- Manage subscription/entitlement state.
- Support multiple branches in the product model.

## 4.4 Customer

The customer is not an authenticated Release 1 product user.

The customer is represented operationally through the cashier workflow.

---

# 5. Product Scope — In Scope

Release 1 includes the following product capabilities:

```text
Staff Authentication
Personal PIN
Shared-PC Active User
Owner / Manager / Cashier Context
Branch Context / Isolation Foundation

Console Management
Console Availability
PS4 / PS5

Branch Pricing
Single / Multi
Hourly Pricing
Match Pricing
Price Snapshot

Gaming Sessions
Open Hourly Session
Fixed Duration Session
Match Session
Pause
Resume
Complete
Session Responsibility / Protected Transfer

Time Calculation
Money Calculation
No Minimum Charge

Invoices
Cash Payments
Protected Cancel/Void Direction

Shifts
Expenses
Basic Asset/Equipment Quantity Foundation
Daily / Weekly / Monthly / Yearly Reports

Offline Operation
Local Persistence
Synchronization
Retry / Idempotency
Conflict Visibility

Security Lock
Online Unlock
Offline Challenge/Recovery
Subscription State
Offline License
License Expiry
Clock-Tamper Response

Audit
Recovery
Local Backup Direction
Central Backup / RPO / RTO Planning Baseline
```

---

# 6. Product Scope — Out of Scope

Release 1 does not include:

```text
Customer Accounts
Customer History
Full Booking / Reservation System
Booking Deposit
Customer Login
Public Customer Website
Mobile App
Food / Drinks / Cafeteria Sales
General Product Sales
Printer Integration
Barcode Integration
Dedicated POS Hardware
Membership
Advanced Inter-Branch Transfers
Advanced Accounting Integration
Advanced Customer Analytics
```

Discount functionality is currently deferred from Release 1.

---

# 7. Product Principles

## PP-001 — Local Success Before User Success

A product action must not be shown as successful before required local persistence succeeds.

## PP-002 — Online and Offline Use the Same Core Business Flow

Connectivity changes sync timing, not the authoritative creation path of approved branch operations.

## PP-003 — Timer UI Is Not Permanent State

Persisted timestamps/events are the source for recovery and billing derivation.

## PP-004 — Financial History Is Non-Destructive

Financial actions should preserve historical traceability.

## PP-005 — Branch Context Is Mandatory

Branch-scoped data must remain within the authorized branch/ownership boundary.

## PP-006 — Security Lock and Subscription Lock Are Different

The product must not use one generic lock state for unrelated causes.

## PP-007 — Do Not Guess Unresolved Business Rules

Any unresolved business decision stays explicit until approved.

---

# 8. Product State Model

Release 1 must represent distinct system states:

```text
Operational
OfflineOperational
RestrictedOffline
SecurityLocked
SubscriptionSuspended
LicenseExpired
Maintenance
```

The UI must communicate the relevant state clearly.

The detailed state machine is created during System Analysis.

---

# 9. Feature Catalog

```text
PF-AUTH       Authentication / Active User
PF-RBAC       Roles / Permissions
PF-BRANCH     Branch Context / Isolation
PF-CONSOLE    Console Management / Availability
PF-PRICE      Pricing
PF-SESSION    Session Lifecycle
PF-TIME       Time / Billing Duration
PF-MONEY      Money Rounding
PF-INVOICE    Invoice
PF-PAYMENT    Cash Payment
PF-SHIFT      Shift
PF-EXPENSE    Expense
PF-ASSET      Basic Asset/Equipment Foundation
PF-REPORT     Reporting
PF-OFFLINE    Offline Operation
PF-SYNC       Synchronization
PF-SECLOCK    PIN Security Lock
PF-SUB        Subscription / Offline License
PF-AUDIT      Audit
PF-RECOVERY   Crash / Power / Backup Recovery
```

---

# 10. PF-AUTH — Authentication and Active User

## Product Goal

Allow each employee to operate under a personal application identity even when multiple employees share the same Windows PC.

## Actors

```text
Cashier
Manager
Owner
```

## Core Behavior

```text
Open Login
   ↓
Enter Personal PIN
   ↓
Identify User
   ↓
Check User Status
   ↓
Load Branch / Role / Permissions
   ↓
Create Active User Context
   ↓
Open Allowed Product Experience
```

## Product Requirements

- Each staff user has a personal PIN.
- The currently active business user is always known.
- Offline authentication must work within allowed offline policy.
- Plaintext PIN storage is not allowed.
- Forgotten PIN is reset, not retrieved.
- The product supports switching the active application user on the same branch PC.

## Important Edge Cases

- Invalid PIN.
- Disabled user.
- User valid centrally but stale local state.
- Internet unavailable.
- Subscription/license state does not permit operation.
- Security lock already active.
- Application restarts.

## Dependencies

```text
PF-RBAC
PF-BRANCH
PF-OFFLINE
PF-SECLOCK
PF-SUB
```

---

# 11. PF-RBAC — Roles and Permissions

## Product Goal

Prevent users from performing actions outside their authorized role/permission scope.

## Roles

```text
Owner
Manager
Cashier
```

“Admin” is not required to be a separate role.

## Product Behavior

The product should evaluate permissions for protected actions instead of relying only on hiding buttons.

Candidate protected actions include:

```text
ManageUsers
ManagePricing
StartShift
EndShift
CreateExpense
ResetEmployeePin
CancelOrVoidInvoice
TransferSessionResponsibility
ViewSensitiveReports
SecurityUnlock
```

The exact permission matrix is finalized in Security/RBAC Design.

---

# 12. PF-BRANCH — Branch Context and Isolation

## Product Goal

Ensure operations and reporting use the correct branch context.

## Product Behavior

- Each console belongs to a branch.
- Pricing is branch-specific.
- Sessions belong to a branch.
- Invoices/payments/expenses/shifts belong to a branch.
- Cashiers and Managers operate only in authorized branches.
- Owner access is limited to authorized ownership scope.

The UI must not be treated as the security boundary.

---

# 13. PF-CONSOLE — Console Management and Availability

## Product Goal

Give staff a reliable operational view of available consoles.

## Supported Console Types

```text
PS4
PS5
```

## Manager/Authorized Admin Capabilities

```text
Add Console
Edit Console
Set Type
Activate / Deactivate
Set Operational Status
```

## Cashier Experience

The dashboard/console view must allow the cashier to determine whether a console can accept a new session.

## Product Rules

- An unavailable console cannot start a new session.
- A console cannot have two simultaneous active sessions.
- Final console state machine is deferred to System Analysis.

---

# 14. PF-PRICE — Pricing Management

## Product Goal

Apply the correct branch price automatically.

## Pricing Dimensions

```text
Console Type
+
Mode
+
Pricing Method
```

Console Type: `PS4`, `PS5`  
Mode: `Single`, `Multi`  
Pricing Method: `Hourly`, `Match`

Not every combination must be enabled in every branch.

## Price Management

An authorized Manager/Owner may change branch pricing. Cashier cannot change pricing.

## Price Snapshot

```text
Resolve Current Valid Price
      ↓
Capture Price Context
      ↓
Persist with Session
```

Changing the current price later does not modify the running session's captured price.

---

# 15. PF-SESSION — Session Lifecycle

## Product Goal

Replace manual session tracking with an explicit, persistent lifecycle.

## Supported Session Forms

```text
Hourly Open
Hourly Fixed
Match
```

## Common Start Flow

```text
Select Available Console
      ↓
Select PS4/PS5 Context
      ↓
Select Single/Multi
      ↓
Select Hourly/Match
      ↓
If Hourly: Open or Fixed
      ↓
Validate Price
      ↓
Capture Price Snapshot
      ↓
Capture Opening Employee
      ↓
Persist Start
      ↓
Show Active
```

At product level a session must preserve enough context to identify branch, console, opening employee, responsible employee where applicable, mode, pricing method, price snapshot, timestamps/events, and status.

---

# 16. PF-SESSION — Open Hourly Session

```text
Start
  ↓
Active
  ↓
Optional Pause / Resume
  ↓
Customer Requests Finish
  ↓
Cashier Completes
```

No automatic end time exists.

---

# 17. PF-SESSION — Fixed Duration Session

The cashier chooses an intended duration.

When the intended duration is reached:

```text
Show Prominent Center Alert
```

The session is **not** automatically stopped.

Detailed early-exit/extension behavior remains a business-rule follow-up if needed.

---

# 18. PF-SESSION — Match

```text
Register Match
      ↓
Customer Plays
      ↓
Employee Determines Match Finished
      ↓
Employee Completes Match
      ↓
Use Captured Match Price
```

The match is not billed by elapsed time and is not automatically ended by a timer.

---

# 19. PF-SESSION — Pause / Resume

- Customer may request Pause.
- Cashier records Pause.
- Paused time is free.
- Resume records a Resume event.
- Multiple pause/resume cycles are supported.
- Paused intervals are excluded once from billable active duration.

---

# 20. PF-SESSION — Single/Multi Change

Single/Multi cannot be changed in-place within the same active pricing context.

```text
Complete/Close Current Context
      ↓
Start New Valid Context
```

---

# 21. PF-TIME — Time Calculation

The product may retain seconds internally.

```text
StartAt = 5:10:23 PM
EndAt   = 6:28:03 PM
Elapsed = 1:17:40
```

Billable base:

```text
1:17:00 → 1:17
1:17:01 → 1:18
1:17:40 → 1:18
1:17:59 → 1:18
```

After billable-base conversion:

- If 3 minutes or less remain to the next quarter-hour, round upward to the next quarter-hour.
- Otherwise keep the billable minute duration.
- Never round backward under this rule.
- No minimum charge exists.

Examples:

```text
2:20 → 2:20
2:27 → 2:30
2:42 → 2:45
2:57 → 3:00
```

---

# 22. PF-MONEY — Money Rounding

Stage 1 — Piasters to EGP:

```text
100.00 → 100
100.01 → 100
100.49 → 100
100.50 → 101
100.99 → 101
```

Stage 2 — upward multiple of five only when increase is 1 or 2 EGP:

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
```

Never reduce the amount to reach a multiple of 5.

---

# 23. PF-INVOICE — Invoice

```text
Valid Completion
      ↓
Final Amount
      ↓
Create Invoice
      ↓
Record Payment
      ↓
Persist
      ↓
Show Completed
```

Rules:

- Invoice belongs to branch.
- Cashier cannot freely erase invoice history.
- Protected cancel/void requires authorization.
- Cancelled/void records remain traceable.
- Retry must not duplicate invoices.

The Cancel/Void reason field is optional. Protected authorization, non-destructive history, and exclusion from valid active revenue remain required. If a reason is entered, preserve it with the audit/history context.

---

# 24. PF-PAYMENT — Cash Payment

- Payment is linked to the relevant invoice/transaction.
- Payment is persistent.
- Sync retry must not duplicate payment.
- The UI must not show payment success before required persistence succeeds.
- Cash is the only Release 1 payment method. An invoice may be issued before cash is received; Payment is a separate step.

---

# 25. PF-SHIFT — Shift

Actors: Owner or Manager with permission.

```text
Start Shift
→ Record Branch / Actor / Time

End Shift
→ Record End Time
```

Cashier does not receive administrative shift control by default.

If active sessions are still assigned to the Cashier whose shift is being closed, the Manager/Owner must hand those sessions to another authorized Cashier before closing the shift. `OpenedBy` remains the original employee; current responsibility changes to the receiving Cashier, and the transfer is audited.

---

# 26. PF-EXPENSE — Expense

Authorized Manager/Owner can record:

```text
Amount
Category / Description
Note where relevant
Branch
Actor
Timestamp
```

Controller/equipment repair is represented as an Expense rather than requiring a dedicated damage workflow.

---

# 27. PF-ASSET — Basic Asset/Equipment Foundation

Release 1 includes branch-scoped quantity tracking for internal Controllers, Accessories, Equipment, and Assets. Consoles remain managed through the Console Management feature.

This is not product-sales inventory. Authorized Manager/Owner users maintain the approved quantity records.

Advanced transfer and detailed per-unit damage lifecycle are not R1 requirements; repair cost is handled through Expenses.

---

# 28. PF-REPORT — Reporting

Required periods:

```text
Daily
Weekly
Monthly
Yearly
```

Core views:

```text
Revenue
Expenses
Profit
```

Operational Profit:

```text
Revenue - Expenses
```

An optional Owner month-end review is allowed. In Release 1 it is review/reporting only: it does not lock a period or mutate invoice, payment, revenue, expense, or profit state.

---

# 29. PF-OFFLINE — Offline Operation

Approved core offline work, subject to valid permission/license:

```text
Login
View required local branch data
Start Session
Pause
Resume
Complete Session
Calculate Charge
Create Invoice
Record Cash Payment
```

Cashier-facing states must distinguish:

```text
Saved Locally
Pending Sync
Syncing
Synced
Sync Failed / Conflict
```

Offline operation does not change the user's role. The same Cashier / Manager / Owner permission boundaries remain in force; Security/RBAC Design may require live central verification for selected protected operations.

---

# 30. PF-SYNC — Synchronization

```text
Local Commit
      ↓
Pending Sync
      ↓
Connection Available
      ↓
Send Stable Operation
      ↓
Central Validation
      ↓
Accepted / Rejected / Conflict
      ↓
Local Sync State Updated
```

Requirements:

- Retry supported.
- Stable operation identity.
- No duplicate financial effect.
- Lost acknowledgement safe.
- Conflict/rejection visible.
- Local evidence not silently erased.

Exact payloads/protocol/backoff belong to later design.

---

# 31. PF-SECLOCK — PIN Security Lock

First threshold:

```text
5 Wrong PIN Attempts
      ↓
20 Second Temporary Lock
      ↓
Warning Sound
      ↓
Security Event
```

Second threshold:

```text
Another 5 Wrong Attempts
      ↓
SecurityLocked
```

The product displays a blocking message instructing the user to contact Engineer Ziyad.

Online recovery may use authorized central/owner recovery.

Offline recovery:

```text
Generate One-Time Challenge
      ↓
Communicate Challenge
      ↓
Generate Authorized Recovery Response
      ↓
Enter Response
      ↓
Local Verification
      ↓
Unlock
```

No permanent universal master PIN is allowed. Recovery cannot bypass subscription lock.

---

# 32. PF-SUB — Subscription and Offline License

Check opportunities:

```text
Application Startup
Internet Reconnection
Periodic Online Verification
At Least Daily While Online
```

If more than 10 days remain:

```text
Maximum Offline Lease = 72 hours
```

If 10 days or less remain:

```text
Offline Allowed Time
=
Remaining Subscription Time
+
10 Hour Payment Grace
```

Examples:

```text
10 days → 10 days + 10 hours
8 days  → 8 days + 10 hours
7 days  → 7 days + 10 hours
1 day   → 1 day + 10 hours
```

If offline authority expires without successful renewal:

```text
LicenseExpired
→ Operational Lock
```

If central suspension is confirmed:

```text
SubscriptionSuspended
```

Business data remains preserved. Changing Windows time backward must not extend offline authority.

---

# 33. PF-AUDIT — Audit

Candidate events include repeated login failure, security lock/unlock, PIN reset, pricing change, user/permission change, expense creation, protected invoice cancel/void, session responsibility transfer, subscription state change, and important sync conflict/rejection.

Audit evidence should identify relevant actor, branch, action, timestamp, target, reason, and before/after context where appropriate.

Secrets and plaintext PINs must not be logged.

---

# 34. PF-RECOVERY — Crash / Power Recovery

Persist meaningful events:

```text
Start
Pause
Resume
Complete
```

Do not save the visible timer every second.

On restart:

```text
Application Starts
      ↓
Read Local Persistent State
      ↓
Find Active / Paused Sessions
      ↓
Restore Original Timestamps
      ↓
Recalculate Display
```

A persisted active session must not restart from zero.

---

# 35. PF-RECOVERY — Backup Direction

Local direction:

```text
Automatic Local SQLite Backup ≈ Every 30 Minutes
```

Release 1 does not require the branch to purchase External SSD, NAS, or UPS.

Central baseline:

```text
Full Backup            → Nightly
Differential Backup    → Every 6 Hours
Transaction Log Backup → Every 5 Minutes
```

Targets:

```text
Central RPO   ≤ 5 Minutes
Branch RTO    ≤ 5 Minutes after power/device availability
Central RTO   ≤ 4 Hours
```

---

# 36. Product-Level Error Behavior

```text
Session local save fails
→ Do not show successful start

Financial commit fails
→ Do not show payment complete

Sync fails
→ Keep local committed operation visible as pending/failed

Unauthorized action
→ Reject

Occupied console start attempt
→ Reject

Missing required price
→ Reject
```

---

# 37. Candidate Information Architecture

This is not final UI design.

```text
Login / Lock
Dashboard / Console Overview
Sessions
Invoices / Payments
Shift

Management
├── Consoles
├── Pricing
├── Users
├── Expenses
└── Basic Assets

Reports

System
├── Offline / Sync Status
├── Security / Recovery as authorized
└── Settings as authorized
```

---

# 38. Cross-Cutting Edge Cases

Later requirements/tests must cover:

```text
Double-click Start
Double-click Complete
Two operations target same console
Internet fails before local action
Internet fails after local commit
Internet fails during sync
Server commits but ACK is lost
Application crashes during active session
Power fails during active session
Price changes during active session
Session crosses midnight
Repeated invalid PIN
Branch becomes SecurityLocked
Branch is offline during SecurityLocked
Subscription expires while offline
Windows clock moves backward
Local database unavailable
Central API unavailable
Central SQL unavailable
Sync conflict
```

---

# 39. Product Decisions Closed for UX / Remaining Design Detail

The product questions that previously blocked UX are closed:

```text
Shift with active sessions
→ handover to another Cashier before shift close.

Cancel/Void reason
→ optional field.

Basic Assets
→ branch quantity tracking for Controllers / Accessories / Equipment / Assets.

Monthly Review
→ review/report only; no financial-state mutation.

Offline role behavior
→ no role elevation offline.
```

Remaining later design detail: Security/RBAC will define which protected actions require live central verification in `RestrictedOffline`.

---

# 40. Product Acceptance Direction

Release 1 is product-ready only when the branch can demonstrate:

```text
Authenticate
↓
Select Available Console
↓
Start Valid Session
↓
Persist Locally
↓
Pause / Resume
↓
Complete
↓
Correct Duration
↓
Correct Final Amount
↓
Invoice
↓
Cash Payment
↓
Reports
↓
Internet Failure
↓
Continue Approved Offline Work
↓
Reconnect
↓
Sync Without Duplicates
↓
Restart / Power Recovery
↓
Security Lock / Recovery
↓
Subscription Offline Enforcement
```

A collection of CRUD screens alone does not satisfy Release 1.

---

# 41. Traceability

Example:

```text
BR-TIME-003
      ↓
BRQ-TIME-003
      ↓
PF-TIME
      ↓
FR-TIME-003
      ↓
Acceptance Criteria
      ↓
Tests
```

---

# 42. Placement

Recommended filename:

```text
PRD_R1.md
```

Current flat layout:

```text
+90PS/
├── DISCOVERY(1).md
├── TO_BE_PROCESS.md
├── BUSINESS_RULES_R1.md
├── BRD_R1.md
├── PRD_R1.md
└── ...
```

If folders are introduced later:

```text
docs/
└── 03_Product/
    └── PRD_R1.md
```

---

# 43. Next Artifacts

The requirements chain is already present. The next product/UX artifacts are:

```text
RBAC_MATRIX_R1.md                 ✅
INFORMATION_ARCHITECTURE_R1.md    ✅
UX_FLOWS_R1.md                    ✅
      ↓
Correct current 74-screen UI      ⏭️ NEXT
      ↓
UI_REVIEW_R1.md
DESIGN_SYSTEM_R1.md
      ↓
System Analysis
```

No implementation is claimed by this PRD.
