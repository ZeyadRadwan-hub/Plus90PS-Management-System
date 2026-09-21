# +90 PS — Release 1 TO-BE Process

**Document Type:** Target Operating Process / Business Process Design  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Draft for Approval  
**Last Updated:** 2026-09-09
**Purpose:** Define how the shop is expected to operate after Release 1 is introduced, before UX, system analysis, architecture, database, API, security implementation, or code are finalized.

---

# 1. Document Purpose

This document describes the **TO-BE operational process** for +90 PS Release 1.

It answers:

> How should a real PlayStation shop operate after +90 PS Release 1 is introduced?

It does **not** define the final database schema, API endpoints, C4 architecture, classes, or implementation details. Those are separate documents that come later.

This TO-BE process is the business/process bridge between:

```text
AS-IS
(Current manual operation)
        ↓
TO-BE
(Target operational process)
        ↓
Business Rules
        ↓
BRD / PRD / Requirements
        ↓
UX / System Analysis
        ↓
Architecture / Database / API / Security
        ↓
Development
```

---

# 2. Current Problem Being Replaced

The AS-IS process depends heavily on manual work:

```text
Manual session management
+
Manual time tracking
+
Manual arithmetic
+
Manual pricing
+
Limited structured history
+
High dependency on employee attention
```

Release 1 changes the operational model so that the system becomes the operational source used by the branch for sessions, timing, billing, cash records, expenses, shifts, reporting, local persistence, and synchronization.

---

# 3. Release 1 TO-BE Objective

Release 1 must allow a real branch to perform its daily core operation through +90 PS.

The expected operational outcome is:

```text
Staff Authentication
        ↓
Branch Operation
        ↓
Console Availability
        ↓
Session
        ↓
Accurate Timing
        ↓
Billing
        ↓
Invoice
        ↓
Cash Payment
        ↓
Persistent Local Record
        ↓
Synchronization
        ↓
Reports / Audit
```

The shop must continue core approved operation when Internet connectivity is unavailable.

A sudden application crash, Internet failure, or power interruption must not make the system depend only on temporary UI state or an in-memory timer.

---

# 4. Release 1 Actors

## 4.1 Cashier

The Cashier handles daily customer-facing operation.

Main responsibilities:

- Log in using a personal PIN.
- View console availability.
- Start eligible sessions.
- Select the required console/mode/pricing method.
- Pause and resume a session when the customer requests it.
- Complete sessions.
- Record cash payment.
- View the operational status needed to perform the job.
- Continue approved core work during OfflineOperational state.

The Cashier does not automatically receive management permissions.

## 4.2 Manager

The Manager is an administrative user for the branch.

Depending on the permission matrix finalized later, the Manager can perform administrative operations such as:

- Start and end shifts.
- Manage branch consoles.
- Manage branch pricing.
- Manage employees/users.
- Reset an employee PIN.
- Create expenses.
- View branch reports.
- Authorize protected actions.
- Review operational problems.
- Review sync/error status.

The exact permission matrix will be finalized in the Authorization/RBAC document.

## 4.3 Owner

The Owner has ownership-level access.

The Owner is expected to:

- View authorized business information.
- Manage higher-level branch access.
- Perform or authorize administrative actions.
- Review reports.
- Manage branch subscription/entitlement state through the owner/platform administration flow.
- Unlock a branch after supported security-lock recovery where authorized.
- Access operations within the ownership boundary.

Owner access must not silently bypass branch/ownership isolation.

## 4.4 Customer

The customer does not have a Release 1 account or login.

In Release 1 the customer interacts operationally through the Cashier:

```text
Customer Request
      ↓
Cashier
      ↓
+90 PS
```

Customer accounts, customer history, full booking features, and customer login are not part of Release 1.

---

# 5. Core TO-BE Principles

## 5.1 Local Save Before Success

A business operation is not shown to the employee as successful until the required local persistent save succeeds.

```text
Employee Action
      ↓
Validate
      ↓
Persist Locally
      ↓
Persistence Success?
   /             \
 No               Yes
 ↓                 ↓
Show Error       Show Success
```

## 5.2 Same Core Operational Path Online and Offline

Core branch operations must not use two different business processes based only on connectivity.

```text
Business Action
      ↓
Local Persistent Save
      ↓
Pending Sync Record
      ↓
Internet Available?
   /               \
 No                 Yes
 ↓                   ↓
Continue Locally    Background Sync
```

Internet connectivity changes synchronization timing, not whether the core branch operation can be created locally.

## 5.3 Timer UI Is Not the Source of Truth

The displayed timer is not the permanent record.

The process preserves actual timestamps/events such as:

```text
StartAt
PauseAt
ResumeAt
CompletedAt
```

Example:

```text
StartAt = 5:10:23 PM
EndAt   = 6:28:03 PM

Actual elapsed:
1:17:40
```

The seconds may be preserved internally for recovery and audit accuracy.

Billing itself is minute-based.

```text
1:17:00 → 1:17 billable base
1:17:01 → 1:18 billable base
1:17:40 → 1:18 billable base
```

The user-facing cashier workflow does not need to display billing in seconds.

## 5.4 Financial Records Are Persistent and Non-Destructive

A completed financial event must not depend on temporary memory.

Financial history should be preserved. Protected cancel/void behavior is preferred over unrestricted destructive deletion.

## 5.5 Branch Isolation

Every branch operation is associated with the correct branch context.

A user must not gain access to unrelated branch data merely by changing a branch identifier in the UI or request.

Detailed enforcement belongs to the later Security and API documents.

---

# 6. High-Level Release 1 TO-BE Flow

```text
Employee Arrives
      ↓
Open +90 PS
      ↓
System Determines Operational State
      ↓
Employee Login / PIN
      ↓
Authorized?
   /        \
 No          Yes
 ↓            ↓
Reject      Dashboard
              ↓
       Administrative Shift State
              ↓
       Console Availability
              ↓
      Customer Requests Play
              ↓
         Select Console
              ↓
       Select Session Context
              ↓
        Start Session
              ↓
       Persist Immediately
              ↓
     Session Active / Paused
              ↓
       Customer Finishes
              ↓
       Complete Session
              ↓
        Calculate Charge
              ↓
         Create Invoice
              ↓
       Record Cash Payment
              ↓
       Persist Transaction
              ↓
       Pending/Background Sync
              ↓
            Reports
```

---

# 7. System Startup TO-BE Process

When +90 PS starts:

```text
Application Starts
      ↓
Load Required Local State
      ↓
Check Local Data Availability
      ↓
Recover Persisted Operational State
      ↓
Determine System State
      ↓
Check Connectivity
      ↓
If Online:
    Perform required central verification/checks
      ↓
If Offline:
    Evaluate valid offline permissions/license state
      ↓
Show Correct Login / Lock / Maintenance State
```

The application must not assume that every startup begins from an empty operational state.

If a previous session was active before a crash or power interruption, the application must be able to identify that persisted state.

---

# 8. System Operational States

Release 1 uses explicit conceptual system states.

## 8.1 Operational
Normal operation is available and required online verification state is valid.

## 8.2 OfflineOperational
Internet is unavailable, but the branch has valid offline authority/license to continue approved core operations.

## 8.3 RestrictedOffline
The branch is offline and can continue approved essential operations, while sensitive administrative operations are restricted according to the later Security/RBAC design.

## 8.4 SecurityLocked
The branch is locked because of a security event such as repeated invalid PIN attempts. This is different from subscription suspension.

## 8.5 SubscriptionSuspended
The central platform has confirmed that the branch subscription/entitlement is suspended. Business data must remain preserved.

## 8.6 LicenseExpired
The locally valid offline license window has expired and the system has not obtained the required successful renewal/verification.

## 8.7 Maintenance
The system is intentionally unavailable for a controlled maintenance condition.

---

# 9. Employee Authentication TO-BE Process

```text
Employee Opens Login
      ↓
Enter Personal PIN
      ↓
System Identifies Employee
      ↓
Validate Local/Allowed Authentication State
      ↓
Check User Status + Role + Permissions
      ↓
Authorized?
   /        \
 No          Yes
 ↓            ↓
Reject      Create Active User Context
              ↓
           Dashboard
```

Each employee uses a personal identity/PIN.

The current active user must always be known for operational and audit purposes.

---

# 10. PIN Failure and Security Lock Process

## 10.1 First Five Failed Attempts

```text
Wrong PIN
   ↓
Count Failure
   ↓
5 Failed Attempts?
   /          \
 No            Yes
 ↓              ↓
Retry       20-Second Lock
                +
          Warning Sound
                +
        Security Event Saved
```

After the 20-second lock finishes, another attempt cycle may begin.

## 10.2 Second Five Failed Attempts

```text
Another 5 Invalid Attempts
        ↓
SecurityLocked
        ↓
Show Center-Screen Message
        ↓
"Contact Engineer Ziyad to restore access"
```

The lock must not delete sessions, invoices, payments, or branch data.

---

# 11. Security Lock Recovery — Online

```text
Branch = SecurityLocked
       ↓
Owner / Authorized Recovery Action
       ↓
Central Verification
       ↓
Unlock Instruction
       ↓
Branch Receives Valid Unlock
       ↓
Security Lock Cleared
       ↓
Operational State Restored
```

The later Security/API design will define the exact authorization and protocol.

---

# 12. Security Lock Recovery — Offline

If the branch is completely offline, the branch must still have a controlled recovery process without using a permanent master PIN.

```text
SecurityLocked
      ↓
Branch Generates One-Time Challenge
      ↓
Employee Communicates Challenge to Ziyad
      ↓
Authorized Owner/Engineer Tool Generates Recovery Response
      ↓
Employee Enters One-Time Recovery Code
      ↓
Local Cryptographic Verification
      ↓
Valid?
   /      \
 No        Yes
 ↓          ↓
Reject     Clear Security Lock
             ↓
       Resume Allowed Operation
```

Requirements of the recovery concept:

- One-time.
- Branch-specific.
- Challenge-specific.
- Not a universal fixed master PIN.
- Must not bypass SubscriptionSuspended or LicenseExpired state.
- Must be auditable.

---

# 13. Shift TO-BE Process

The administrative Start/End Shift action is performed by an authorized Manager or Owner.

## 13.1 Start Shift

```text
Manager/Owner Login
      ↓
Select Start Shift
      ↓
Validate Permission
      ↓
Record Branch + Actor + Start Time
      ↓
Persist Shift
      ↓
Shift = Open
```

## 13.2 End Shift

```text
Manager/Owner Selects End Shift
      ↓
Validate Permission
      ↓
Active Sessions Assigned to Closing Cashier?
   /                         \
 No                           Yes
 ↓                            ↓
Continue                Select Receiving Cashier
                              ↓
                      Transfer Responsibility
                              ↓
                      Preserve Original OpenedBy
                              ↓
                      Audit Handover
                              ↓
                    Continue Shift Close
      ↓
Record End Time
      ↓
Persist
      ↓
Shift = Closed
```

The session remains historical under its original opener, but current responsibility after transfer belongs to the receiving Cashier. The remaining operational/session responsibility is attributed to that receiving Cashier.

---

# 14. Dashboard / Console Availability Process

After successful login:

```text
Dashboard
     ↓
Load Branch Consoles
     ↓
Display Current Operational Status
```

A console must have a clear usable state.

At minimum, the process must distinguish whether it is available for a new session or already occupied/unavailable.

The exact final console state machine belongs in System Analysis.

---

# 15. Start Session — Common TO-BE Process

```text
Customer Requests Service
      ↓
Cashier Checks Available Consoles
      ↓
Select Console
      ↓
Select Console Type Context
      ↓
PS4 / PS5
      ↓
Select Mode
      ↓
Single / Multi
      ↓
Select Pricing Method
      ↓
Hourly / Match
      ↓
If Hourly:
    Select Open or Fixed Duration behavior
      ↓
Validate Console Availability
      ↓
Validate Applicable Branch Price
      ↓
Capture Pricing Snapshot
      ↓
Capture Opening Cashier
      ↓
Persist Session Locally
      ↓
Create Pending Sync Operation
      ↓
Local Commit Successful?
   /             \
 No               Yes
 ↓                 ↓
Show Failure    Mark Console Occupied
                   ↓
               Show Session Started
```

A second active session on the same occupied console must not be started.

---

# 16. Pricing Snapshot TO-BE Process

```text
Session Start
     ↓
Resolve Branch Price
     ↓
Capture Selected Combination
     ↓
Capture Price Snapshot
     ↓
Persist with Session
```

If a Manager changes pricing later:

```text
Existing Active Session
→ continues with its captured price

New Session
→ uses the new valid price
```

---

# 17. Hourly Open Session Process

```text
Start Open Session
      ↓
Persist Start Timestamp
      ↓
Session = Active
      ↓
Customer Plays
      ↓
Possible Pause / Resume
      ↓
Customer Requests End
      ↓
Cashier Selects Complete
      ↓
Persist Actual End Timestamp
      ↓
Calculate Billable Duration
      ↓
Calculate Amount
      ↓
Invoice + Cash Payment Flow
```

The system does not automatically stop an Open session.

---

# 18. Fixed Duration Session Process

```text
Start Fixed Session
      ↓
Save Start + Intended Duration
      ↓
Session Active
      ↓
Duration Reached
      ↓
Show Clear Center-Screen Alert
      ↓
"Console X reserved duration has ended"
```

The system **does not automatically terminate the session** merely because the intended duration was reached.

The employee remains responsible for the operational follow-up.

Detailed early-exit/extension business rules can be finalized in Business Rules if still required.

---

# 19. Match Session Process

```text
Cashier Selects Match
      ↓
Select Console + Mode + Valid Match Price
      ↓
Start/Register Match
      ↓
Persist Match Session
      ↓
Customer Plays Match
      ↓
Employee Determines Match Has Finished
      ↓
Employee Completes Match
      ↓
Use Match Price
      ↓
Invoice
      ↓
Cash Payment
```

The system does not calculate Match price from elapsed match time.

A match is not automatically ended by a timer in the agreed process.

---

# 20. Pause Process

```text
Active Session
      ↓
Customer Requests Pause
      ↓
Cashier Selects Pause
      ↓
Validate Session Is Active
      ↓
Persist Pause Timestamp/Event
      ↓
Session = Paused
```

The paused period is free and does not contribute to billable active duration.

---

# 21. Resume Process

```text
Paused Session
      ↓
Customer Resumes
      ↓
Cashier Selects Resume
      ↓
Persist Resume Timestamp/Event
      ↓
Session = Active
```

Multiple pause/resume events must remain traceable without counting paused time twice.

---

# 22. Mode Change Process

Single/Multi is not changed in-place inside the existing session context.

```text
Current Session/Mode
      ↓
Complete Current Context Properly
      ↓
Persist Completion
      ↓
Select New Mode
      ↓
Start New Valid Session/Context
```

---

# 23. Complete Session TO-BE Process

```text
Cashier Selects Complete
      ↓
Validate Session State
      ↓
Capture Actual CompletedAt
      ↓
Calculate Active Elapsed Time
      ↓
Exclude Paused Periods
      ↓
Convert Seconds to Billable Minute Base
      ↓
Apply Approved Time Rounding Rule
      ↓
Apply Pricing Snapshot
      ↓
Calculate Raw Amount
      ↓
Apply Approved Money Rounding
      ↓
Generate Final Amount
      ↓
Create Invoice
      ↓
Record Cash Payment
      ↓
Persist Financial Operation
      ↓
Mark Console Available
      ↓
Queue Sync
```

For Match, billing follows the match price rather than hourly elapsed-time pricing.

---

# 24. Time Calculation Process

Actual timestamps can preserve seconds.

```text
StartAt = 5:10:23 PM
EndAt   = 6:28:03 PM

Actual elapsed = 1:17:40
```

The billable base duration is minute-based.

```text
Seconds == 0
→ keep completed minute

Seconds > 0
→ move to next minute
```

Therefore:

```text
1:17:00 → 1:17
1:17:01 → 1:18
1:17:40 → 1:18
```

After this base-minute conversion, the approved hourly rounding rule is applied:

- If the remaining time to the next quarter-hour mark is 3 minutes or less, round upward to that quarter-hour.
- Otherwise, keep the actual billable minute duration.
- A duration already on the quarter-hour mark remains unchanged.

Examples already used in the project:

```text
2:27 → 2:30
2:20 → 2:20
2:42 → 2:45
2:57 → 3:00
```

No minimum charge is applied.

---

# 25. Money Rounding Process

The system may calculate an internal amount containing piasters, but the cashier/customer-facing final amount is whole EGP.

First, round to a whole EGP using 50 piasters as the threshold:

```text
100.00 → 100
100.01 → 100
100.49 → 100
100.50 → 101
100.99 → 101
```

Then apply the upward-only rule for the next multiple of 5:

- Never reduce the whole-EGP amount merely to reach a multiple of 5.
- If the next multiple of 5 is only 1 or 2 EGP above the amount, round upward to it.
- Otherwise keep the amount.

Examples:

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

Invalid example:

```text
102 → 100  ❌
```

The UI should display the final whole-EGP amount rather than piasters.

---

# 26. Invoice + Cash Payment TO-BE Process

The Release 1 payment method is cash.

```text
Valid Completed Session
      ↓
Final Charge Calculated
      ↓
Create Invoice
      ↓
Record Cash Payment
      ↓
Persist Related Financial Records Safely
      ↓
Persist Pending Sync Record
      ↓
Commit
      ↓
Show Payment/Completion Success
```

Sensitive Cancel/Void behavior follows an authorized non-destructive process rather than unrestricted deletion. The UI may offer a cancellation reason, but that reason is optional; if provided, it is preserved with the history/audit context.

---

# 27. Financial Atomicity Process Requirement

Where the operation is treated as one business completion, related local records should succeed together.

```text
Complete Session
+
Create Invoice
+
Create Payment
+
Create Pending Sync Operation
        ↓
One Controlled Local Transaction
        ↓
Commit
```

If the commit fails, the UI must not report a successful completed financial operation.

---

# 28. Expense TO-BE Process

Expenses are created by an authorized Manager/Owner according to the finalized permission matrix.

```text
Authorized Admin User
      ↓
Create Expense
      ↓
Enter Amount
      ↓
Enter Category/Description
      ↓
Optional/Required Note according to final validation
      ↓
Persist Branch + Actor + Timestamp
      ↓
Queue Sync
```

If controllers or other equipment need repair, Release 1 does not require a separate damage-management subsystem.

Example:

```text
Expense:
"Repair of 2 controllers"

Amount:
Repair cost

Note:
Relevant repair details
```

The cost is handled through Expenses.

---

# 29. Pricing Management TO-BE Process

```text
Manager/Owner with Permission
      ↓
Open Pricing Management
      ↓
Select Branch Pricing Combination
      ↓
PS4/PS5
+
Single/Multi
+
Hourly/Match
      ↓
Enter/Update Price
      ↓
Validate
      ↓
Persist Change
      ↓
Audit Who/What/When
      ↓
New Sessions Use New Price
```

An already-running session keeps its captured pricing snapshot.

---

# 30. Console Management TO-BE Process

Authorized administration can:

```text
Add Console
Edit Console
Set Console Type
Activate / Deactivate
Set Allowed Operational Status
```

Every console belongs to its branch context.

A console that is not available must not start a new active session.

The detailed final console state machine will be defined later.

---

# 31. Reports TO-BE Process

Release 1 must support operational reporting for:

```text
Daily
Weekly
Monthly
Yearly
```

Core financial reporting includes:

```text
Revenue
Expenses
Profit
```

Revenue is derived from valid financial operations rather than being created only at month-end.

```text
Payments / Valid Financial Records
      ↓
Period Filter
      ↓
Revenue
      ↓
Expenses
      ↓
Profit
```

---

# 32. Optional Monthly Review

The Owner may choose to perform a month-end review.

This is a business review/reporting action and does not replace normal continuous recording of revenue and expenses.

```text
End of Month
      ↓
Owner Selects Monthly Review
      ↓
Review Period Financial Data
      ↓
Review Revenue / Expenses / Profit
      ↓
Exit / Finish Review
```

Release 1 does not create a financial closing state, lock the month, or mutate invoices/payments merely because the monthly review occurred.

---

# 33. Offline Operation TO-BE Process

When Internet access becomes unavailable, the user's role does not change and offline mode does not grant extra permissions. Protected operations remain subject to the same Cashier / Manager / Owner boundaries.

```text
Connectivity Lost
      ↓
System Detects Offline
      ↓
Validate Offline License / Permission State
      ↓
Allowed?
   /        \
 No          Yes
 ↓            ↓
Show Lock/   OfflineOperational
Restriction      ↓
            Core Work Continues
                 ↓
         Every Operation Saves Locally
                 ↓
           Pending Sync Queue
```

The Cashier must be able to tell that:

- The business operation is saved locally.
- Synchronization may still be pending.
- Local success and central synchronization are different states.

---

# 34. Offline Local Persistence Process

```text
Employee Action
      ↓
Local Validation
      ↓
Local Persistent Transaction
      ↓
Business Record
+
Pending Sync Operation
      ↓
Commit
      ↓
Show "Saved Locally / Pending Sync"
```

The shop does not wait for Internet connectivity to complete approved core branch operation.

---

# 35. Reconnection and Synchronization TO-BE Process

```text
Internet Restored
      ↓
System Detects Connection
      ↓
Perform Required Security/Subscription Checks
      ↓
Background Sync Starts
      ↓
Read Pending Operations
      ↓
Send Operation with Stable Identifier
      ↓
Central Validation
      ↓
Accepted?
   /        \
 No          Yes
 ↓            ↓
Rejected/   Central Save
Conflict       ↓
Status         Acknowledgement
 ↓              ↓
Keep Local    Mark Local = Synced
Evidence
```

A cashier should not have to manually recreate the original session or invoice merely because synchronization was delayed.

---

# 36. Retry / Lost Acknowledgement Process

```text
Branch Sends Operation
      ↓
Server Successfully Saves It
      ↓
Connection Drops Before ACK Reaches Branch
      ↓
Branch Still Thinks Operation Is Pending
      ↓
Retry Same Operation Identifier
      ↓
Server Detects Already-Processed Operation
      ↓
Return Existing Success / ACK
```

The result must not be:

```text
Duplicate Invoice
Duplicate Payment
Duplicate Session
```

Detailed idempotency design belongs in Offline/Sync Architecture and API Contracts.

---

# 37. Sync Conflict / Rejection Process

```text
Pending Operation
      ↓
Central Validation Fails / Conflict Exists
      ↓
Local Status = Conflict or Rejected
      ↓
Preserve Local Evidence
      ↓
Show Authorized User
      ↓
Resolve According to Defined Conflict Policy
```

Financial data must not be silently overwritten to hide a conflict.

---

# 38. Application Crash / Power Failure TO-BE Process

```text
Session Starts at 5:00 PM
      ↓
Start Event Persisted
      ↓
Power Fails at 5:37 PM
      ↓
Application/PC Stops
      ↓
Power Returns
      ↓
Application Starts
      ↓
Read Local Persistent State
      ↓
Find Active Session
      ↓
Restore Original Start Context
      ↓
Recalculate Displayed Duration from Persisted Time Data
```

The session must not restart from zero merely because the application restarted.

---

# 39. Event Persistence Process

The system does not need to write the timer value every second.

Instead it persists meaningful events:

```text
Start
Pause
Resume
Complete
```

The displayed elapsed time is derived from persisted timestamps/events.

---

# 40. Local Database Operational Requirement

When database implementation begins, the agreed branch persistence direction is:

```text
SQLite
+
WAL Mode
+
synchronous = FULL
```

This belongs technically to the later Database/Offline Architecture documents, but it is recorded here because it directly supports the TO-BE requirement that a local operation be persisted before success is shown.

---

# 41. Local Backup TO-BE Operational Process

The current agreed direction is an automatic local SQLite backup approximately every 30 minutes.

No extra branch hardware such as an external SSD, NAS, or UPS is required by Release 1.

```text
Running Local Database
      ↓
Scheduled Local Backup
      ↓
Backup Copy on Branch Machine
```

This local backup is a recovery layer, not a replacement for central synchronization or central backup.

A total loss of the branch machine/storage requires recovery from centrally synchronized information to the extent available.

---

# 42. Subscription Check TO-BE Process

The branch must not be able to use permanent offline mode to avoid subscription enforcement.

Subscription/entitlement verification occurs at appropriate opportunities such as:

```text
Application Startup
+
Internet Reconnection
+
Periodic Online Verification
+
At Least Daily Check While Connectivity Exists
```

When a successful online verification occurs:

```text
Server Verifies Subscription
      ↓
Server Issues/Refreshes Signed Offline License
      ↓
Branch Stores Verifiable License
      ↓
Branch Can Continue Offline Within Allowed Window
```

---

# 43. Offline License Rule

## 43.1 More Than 10 Days Remaining

```text
Remaining Subscription > 10 Days
      ↓
Maximum Offline Lease = 72 Hours
```

Example:

```text
20 Days Remaining
      ↓
Successful Check
      ↓
Offline Allowed Up To 72 Hours Without New Verification
```

## 43.2 Ten Days or Less Remaining

```text
Offline Allowed Until
=
Subscription Remaining Time
+
10-Hour Payment Grace Period
```

Examples:

```text
10 days remaining → 10 days + 10 hours
8 days remaining  → 8 days + 10 hours
7 days remaining  → 7 days + 10 hours
3 days remaining  → 3 days + 10 hours
1 day remaining   → 1 day + 10 hours
```

The extra 10 hours are a payment grace period, not an extension of the paid subscription term.

---

# 44. License Expiration While Offline

```text
Offline License Window Ends
      ↓
No Successful Renewal
      ↓
LicenseExpired
      ↓
Operational Use Locked
```

This lock can occur even when the branch is still offline.

Business data remains preserved.

---

# 45. Subscription Suspension

```text
Successful Online Check
      ↓
Subscription = Suspended
      ↓
SubscriptionSuspended
      ↓
Operational System Locked
```

Suspending the branch must not delete local or central business history.

---

# 46. Clock Tampering TO-BE Requirement

A user must not be able to bypass offline license expiration simply by changing the Windows clock backward.

```text
Trusted Previous Time/License State
      ↓
Machine Time Moves Back Suspiciously
      ↓
Clock Tampering Suspected
      ↓
Do Not Extend Offline Authority
      ↓
Restricted / Require Online Verification
```

Detailed cryptographic and secure-storage design belongs in Security Design.

---

# 47. Security Lock vs Subscription Lock

These states must remain separate.

```text
PIN Recovery → SecurityLocked        ✅
PIN Recovery → Subscription Lock     ❌
PIN Recovery → LicenseExpired        ❌
```

---

# 48. Audit TO-BE Process

Sensitive operations should create audit evidence.

Candidate auditable actions include:

- Login failures.
- Security lock.
- Security unlock.
- PIN reset.
- Pricing changes.
- User/permission changes.
- Expense creation.
- Protected financial cancellation/void.
- Session ownership/authorized administrative changes.
- Subscription/entitlement state changes where relevant.
- Sync conflicts/rejections where operationally important.

Audit evidence should identify, where relevant:

```text
Actor
Branch
Action
Timestamp
Affected Record
Reason
Before / After where appropriate
Operation Identifier
```

PINs, passwords, secrets, and private signing keys must not be written into audit/log output.

---

# 49. Error TO-BE Principle

Errors must not create false success.

```text
Local save fails
→ Do not say "Session Started"

Invoice transaction fails
→ Do not say "Payment Complete"

Sync fails
→ Keep local committed operation and show Pending/Failed Sync

Unauthorized action
→ Reject operation

Invalid console state
→ Reject second active session

Invalid price configuration
→ Prevent session start that cannot be billed correctly
```

---

# 50. Daily Operational TO-BE Summary

```text
Manager/Owner Starts Shift
        ↓
Cashiers Log In Individually
        ↓
Customers Arrive
        ↓
Consoles Selected
        ↓
Sessions Started
        ↓
Local Persistence
        ↓
Pause / Resume / Complete as Needed
        ↓
Invoices
        ↓
Cash Payments
        ↓
Expenses Recorded by Authorized Admin
        ↓
Data Synchronizes in Background
        ↓
Reports Available
        ↓
Manager/Owner Ends Shift
```

Internet loss during this day does not replace the workflow with a separate manual process; approved operations continue locally and synchronize later.

---

# 51. Release 1 Out of Scope for This TO-BE

The following must not be silently introduced into Release 1 TO-BE:

- Customer accounts.
- Customer history.
- Full booking system.
- Booking deposits.
- Customer website/login.
- Mobile application.
- Public website as an R1 client.
- Food/drink/cafeteria sales.
- General product sales.
- Printer integration.
- Barcode integration.
- POS hardware integration.
- Membership.
- Advanced inter-branch transfers.
- Advanced accounting integration.
- Advanced customer analytics.

Discounts are currently deferred from Release 1 based on the latest working decision and should not be treated as a required R1 operational flow.

---

# 52. Controllers / Assets Clarification

Release 1 retains a basic branch-scoped quantity model for internal equipment:

```text
Controllers
Accessories
Equipment
Assets
```

Consoles continue to be managed by Console Management and are not duplicated as quantity-only equipment entries.

Authorized Manager/Owner users maintain the approved quantity records. Release 1 does not require a specialized per-controller damage lifecycle or advanced inter-branch transfer.

If equipment repair creates a cost:

```text
Repair Event
      ↓
Authorized Expense
      ↓
Amount + Description/Note
      ↓
Persist
```

---

# 53. TO-BE Data Created by Core Operation

The target process is expected to create/maintain business information such as:

```text
Branch Context
Current Employee Identity
Console Context
Session Context
Session Status
Actual Timestamps
Pause/Resume Events
Pricing Snapshot
Calculated Amount
Invoice
Cash Payment
Shift
Expense
Audit Event
Sync Status
Subscription/Offline License State
Security Lock State
```

This list is a process-level data view, **not** the final ERD.

The final entities/tables/relationships belong to Database Design.

---

# 54. TO-BE Success Criteria

The TO-BE process is acceptable for Release 1 when the documented target operation can support the following business journey without relying on R2/R3 functionality:

```text
Employee Login
      ↓
Correct Branch Context
      ↓
Console Availability
      ↓
Session Start
      ↓
Local Save
      ↓
Pause / Resume if needed
      ↓
Session Completion
      ↓
Correct Billing
      ↓
Invoice
      ↓
Cash Payment
      ↓
Reportable Financial Record
      ↓
Offline Continuation if Internet Fails
      ↓
Synchronization after Reconnection
      ↓
Recovery after Application/Power Interruption
      ↓
Protected Security/Subscription Behavior
```

---

# 55. Process Invariants

1. An occupied console cannot silently receive a second active session.
2. A session has an identifiable opening employee.
3. The applicable session price is preserved for that session.
4. Paused time is not billed.
5. Match billing does not become hourly billing.
6. A Match ends through the employee action in the agreed R1 process.
7. Fixed-duration expiry produces an alert, not an automatic forced stop.
8. The system preserves actual timestamps even though billing is minute-based.
9. Any seconds beyond a complete minute push the billable base to the next minute.
10. There is no minimum charge.
11. Money is shown as whole EGP according to the approved rounding rules.
12. The final multiple-of-five rule never reduces an amount.
13. A financial completion is not shown as successful before required persistence succeeds.
14. Internet loss does not erase locally committed work.
15. Retry must not create duplicate financial records.
16. A power/application interruption must not reset a persisted active session to zero.
17. A security lock must not delete business data.
18. Subscription suspension/expiry must not delete business data.
19. PIN security recovery must not bypass subscription enforcement.
20. A user must not be able to bypass offline subscription enforcement indefinitely by disconnecting the Internet.
21. Cashier/Manager/Owner access must be constrained by the later finalized permission and branch/ownership rules.
22. Release 1 must not silently expand into Release 2 customer/booking scope.

---

# 56. Items Intentionally Deferred to Later Documents

The TO-BE process defines **what the target operation does**.

The following are intentionally finalized elsewhere:

## Business Rules Finalization
- Formal BR identifiers for the latest agreed rules.
- Protected-action enforcement details that belong to Security/RBAC.
- Cancel/Void reason is already fixed as optional for R1 UX.
- Shift-close handover behavior is already fixed for R1 UX.
- Basic asset quantity scope is already fixed for R1 UX.

## BRD / PRD
- Formal business requirements.
- Product feature specifications.
- Business success metrics.
- Product acceptance definition.

## UX / UI
- Screen layouts.
- Number of clicks.
- Dialog placement.
- Exact alert appearance.
- Offline/status indicators.
- Error-state visual behavior.

## System Analysis
- Final Use Cases.
- Activity flows.
- State Machines.
- Sequence flows.
- Entity behavior.

## Architecture
- C4 Context/Container/Component.
- Component boundaries.
- Deployment topology.
- Background-service design.

## Database
- Final entities/tables.
- ERD.
- Data Dictionary.
- Keys.
- Indexes.
- Constraints.
- SQLite/SQL Server schema mapping.

## API
- Endpoint paths.
- DTO contracts.
- Error model.
- Idempotency contract.
- OpenAPI definition.

## Security
- PIN hashing implementation.
- Token/session model.
- Permission matrix.
- Device identity.
- Challenge-response cryptography.
- Signed offline-license format.
- Secure key storage.
- Threat model.
- Anti-tampering controls.

## Offline / Sync
- Outbox schema.
- Retry/backoff.
- Conflict categories.
- Server acknowledgement protocol.
- Idempotency implementation.
- Recovery states.

## Testing
- Unit/integration/E2E cases.
- Offline tests.
- Sync tests.
- Power-failure tests.
- Security tests.
- UAT.

---

# 57. Recommended Traceability from This Document

```text
TO-BE: Start Session
      ↓
Business Rule
      ↓
Functional Requirement
      ↓
User Story
      ↓
Acceptance Criteria
      ↓
Use Case / Activity
      ↓
Sequence Diagram
      ↓
Domain / ERD
      ↓
API Contract
      ↓
Security Requirement
      ↓
Offline Rule
      ↓
Test Case
```

This prevents a feature from being designed in the UI or code without a clear business/process origin.

---

# 58. Next Step After Approval

The TO-BE decisions are already reflected through Business Rules, BRD, PRD, Requirements, User Stories, and Acceptance Criteria.

Current next step:

```text
TO-BE / Business Rules / Requirements   ✅
RBAC / IA / UX Flows                    ✅
      ↓
Correct current 74-screen UI            ⏭️ NEXT
      ↓
UI Review + Design System
      ↓
System Analysis
```

Do **not** jump from the current UX/UI stage directly into code.

---

# 59. Document Ownership / Placement

Recommended filename:

```text
TO_BE_PROCESS.md
```

For the project's **current flat-file structure**, keep it as its own file beside the current Discovery and business documents:

```text
+90PS/
├── DISCOVERY(1).md
├── TO_BE_PROCESS.md
├── BUSINESS_RULES(1).md
├── R1_OPERATIONAL_MVP.md
├── R1_ACCEPTANCE.md
├── DECISIONS.md
├── ROADMAP.md
└── ...
```

Do not merge this file into `DISCOVERY(1).md` if the project rule is now **one major artifact per file**.

If the repository is later reorganized into folders without renaming files, this file belongs conceptually under:

```text
docs/
└── 01_Discovery/
    └── TO_BE_PROCESS.md
```

---

# 60. Status

```text
AS-IS                              ✅
TO-BE                              ✅ Updated
Business Rules                     ✅
BRD / PRD                          ✅
FR / NFR                           ✅
User Stories / Acceptance          ✅
RBAC / IA / UX Flows               ✅
Current 74-screen UI correction    ⏭️ NEXT
UI Review / Design System          ⏳
System Analysis                    ⏳
Code                               0%
```

No implementation status is claimed by this document.
