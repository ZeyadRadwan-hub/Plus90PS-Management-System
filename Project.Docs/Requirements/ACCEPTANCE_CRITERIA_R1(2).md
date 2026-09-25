# +90 PS — Release 1 Acceptance Criteria

**Document Type:** Acceptance Criteria Specification  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Baseline  
**Last Updated:** 2026-09-09
**Primary Input:** `USER_STORIES_R1.md`  
**Related Inputs:** `FUNCTIONAL_REQUIREMENTS_R1.md`, `NON_FUNCTIONAL_REQUIREMENTS_R1.md`, `BUSINESS_RULES_R1.md`, `PRD_R1.md`  
**Purpose:** Define testable acceptance behavior for each Release 1 user story before UX, system analysis, architecture, and implementation.

---

# 1. Acceptance Convention

Each acceptance criterion uses:

```text
AC-US-<AREA>-NNN-<LETTER>
```

The preferred format is:

```text
Given <initial state>
When <action/event>
Then <observable result>
```

Criteria marked `TBD/BLOCKED` must not be treated as final until the related open decision is closed.

---

# 2. Authentication Acceptance Criteria

## US-AUTH-001 — Login with Personal PIN

### AC-US-AUTH-001-A — Successful Login

**Given** an active employee belongs to the current Branch's Business<br>
**And** the employee has a valid personal PIN  
**And** the system state permits authentication  
**When** the employee enters the correct PIN  
**Then** the employee is authenticated  
**And** the current application user becomes that employee  
**And** the active context contains the correct branch, role, and permissions  
**And** the product opens the authorized operational experience.

### AC-US-AUTH-001-B — Wrong PIN

**Given** the login screen is available  
**When** an incorrect PIN is entered  
**Then** authentication is rejected  
**And** the incorrect user is not made active  
**And** the failed-attempt counter is updated according to the PIN-security rules.

### AC-US-AUTH-001-C — Windows Identity Does Not Replace App Identity

**Given** two cashiers use the same Windows account  
**When** Cashier B authenticates after Cashier A  
**Then** the active +90 PS user is Cashier B  
**And** future attributable actions use Cashier B's application identity.

---

## US-AUTH-002 — Login While Offline

### AC-US-AUTH-002-A — Valid Offline Login

**Given** Internet connectivity is unavailable  
**And** the employee has valid local authentication authority  
**And** the branch has valid offline license authority  
**And** no blocking system state applies  
**When** the employee enters the correct PIN  
**Then** login succeeds  
**And** the product enters the appropriate offline operational state.

### AC-US-AUTH-002-B — Offline License Not Valid

**Given** Internet connectivity is unavailable  
**And** the employee PIN is otherwise valid  
**But** the branch is `LicenseExpired`  
**When** the employee attempts operational login  
**Then** the product does not enter normal OfflineOperational use  
**And** the license-expired blocking state remains visible.

---

## US-AUTH-003 — Reject Unauthorized or Disabled Login

### AC-US-AUTH-003-A

**Given** an employee is disabled or otherwise not authorized by the applicable valid user state  
**When** that employee attempts login  
**Then** authentication is rejected  
**And** no active operational user context is created for that employee.

### AC-US-AUTH-003-B — Business-Wide Deactivation

**Given** an Employee belongs to Business X with Branch A and Branch B<br>
**When** that Employee is deactivated<br>
**Then** later authentication and operation are rejected at both Branches<br>
**And** previously recorded Session opener IDs and other historical references remain intact.

---

## US-AUTH-004 — Switch Active User

### AC-US-AUTH-004-A

**Given** Cashier A is the active application user  
**When** the application is locked/switched  
**And** Cashier B successfully authenticates  
**Then** Cashier B becomes the active application user  
**And** Cashier A remains the recorded actor for actions already performed by Cashier A.

---

## US-AUTH-005 — Reset Forgotten PIN

### AC-US-AUTH-005-A — Authorized Reset

**Given** an employee cannot remember the PIN  
**And** an authorized Manager/Owner performs the reset workflow  
**When** a valid new PIN is set  
**Then** the employee can authenticate using the new PIN subject to security policy.

### AC-US-AUTH-005-B — Old PIN Is Not Displayed

**Given** an authorized user opens PIN recovery/reset  
**Then** the product does not display the employee's old plaintext PIN.

---

# 3. PIN Security Acceptance Criteria

## US-SECLOCK-001 — First Five Failed Attempts

### AC-US-SECLOCK-001-A

**Given** the failed-attempt cycle starts below five failures  
**When** the fifth invalid PIN attempt is submitted  
**Then** PIN attempts are blocked for 20 seconds  
**And** the configured warning sound is triggered  
**And** a security event is recorded.

### AC-US-SECLOCK-001-B

**Given** the 20-second temporary lock is active  
**When** a user tries another PIN before the lock expires  
**Then** the product does not process it as a normal authentication attempt.

### AC-US-SECLOCK-001-C

**Given** the 20-second temporary lock finishes  
**Then** the next allowed attempt cycle can begin  
**And** the system has not deleted business data.

---

## US-SECLOCK-002 — Second Failure Cycle

### AC-US-SECLOCK-002-A

**Given** the first five-attempt lock already occurred  
**And** the temporary lock ended  
**When** another five invalid PIN attempts are reached  
**Then** the system enters `SecurityLocked`  
**And** normal operational access is blocked  
**And** the blocking contact/recovery message is shown.

### AC-US-SECLOCK-002-B

**Given** the system enters `SecurityLocked`  
**Then** existing sessions, invoices, payments, expenses, and audit history remain preserved.

---

## US-SECLOCK-003 — Online Security Unlock

### AC-US-SECLOCK-003-A

**Given** the branch is `SecurityLocked`  
**And** the branch is online  
**When** a valid authorized central unlock is received  
**Then** the SecurityLocked state is cleared  
**And** the product can return to an otherwise permitted operational/login state.

### AC-US-SECLOCK-003-B — Unauthorized Unlock

**Given** the branch is `SecurityLocked`  
**When** an invalid or unauthorized unlock request is received  
**Then** the security lock remains active.

---

## US-SECLOCK-004 — Offline One-Time Recovery

### AC-US-SECLOCK-004-A — Generate Challenge

**Given** the branch is `SecurityLocked`  
**And** the branch has no Internet connection  
**When** the offline recovery flow is opened  
**Then** the system produces a recovery challenge tied to that recovery attempt/branch according to Security Design.

### AC-US-SECLOCK-004-B — Valid Response

**Given** a current valid challenge exists  
**When** a valid authorized one-time recovery response for that challenge is entered  
**Then** local verification succeeds  
**And** the SecurityLocked state is cleared.

### AC-US-SECLOCK-004-C — Reuse Rejected

**Given** a recovery response has already been successfully used  
**When** the same response is submitted again  
**Then** it is rejected.

### AC-US-SECLOCK-004-D — Wrong Branch/Challenge Rejected

**Given** a response was generated for a different branch or challenge  
**When** it is entered  
**Then** verification fails  
**And** SecurityLocked remains active.

---

## US-SECLOCK-005 — No Subscription Bypass

### AC-US-SECLOCK-005-A

**Given** the branch is both subject to a subscription/license block and recovering from a security lock  
**When** a valid PIN-security recovery succeeds  
**Then** only the security lock is cleared  
**And** `SubscriptionSuspended` or `LicenseExpired` remains enforced.

---

# 4. RBAC Acceptance Criteria

## US-RBAC-001 — Role-Based Experience

### AC-US-RBAC-001-A

**Given** a Cashier is logged in  
**When** the operational UI loads  
**Then** Cashier-authorized actions are available  
**And** unauthorized administrative actions are unavailable or disabled in the UX.

### AC-US-RBAC-001-B — Backend/Trusted Rejection Direction

**Given** a user lacks permission for a protected action  
**When** the action is attempted through a non-UI path or manipulated request  
**Then** the trusted authorization layer rejects the operation.

---

## US-RBAC-002 — Protect Pricing

### AC-US-RBAC-002-A

**Given** a Cashier is logged in  
**When** the Cashier attempts to change pricing  
**Then** the action is rejected.

### AC-US-RBAC-002-B

**Given** an authorized Manager/Owner is logged in  
**When** a valid pricing change is submitted  
**Then** the change can succeed  
**And** the appropriate audit behavior occurs.

---

## US-RBAC-003 — Protect Expense

### AC-US-RBAC-003-A

**Given** a Cashier lacks expense permission  
**When** the Cashier attempts to create an expense  
**Then** the action is rejected.

### AC-US-RBAC-003-B

**Given** an authorized Manager/Owner  
**When** a valid expense is submitted  
**Then** the expense can be created for the current branch.

---

## US-RBAC-004 — Protect Cancel/Void

### AC-US-RBAC-004-A

**Given** a Cashier lacks protected cancel/void permission  
**When** the Cashier attempts to cancel/void an invoice  
**Then** the operation is rejected.

### AC-US-RBAC-004-B — Authorized Flow

**Given** an authorized Manager/Owner  
**When** the protected cancel/void flow is completed  
**Then** the original invoice remains historically traceable.

**And** the Cancel/Void reason field is optional rather than required.

---

# 5. Branch Acceptance Criteria

## US-BRANCH-001 — Correct Branch Context

### AC-US-BRANCH-001-A

**Given** an employee is operating Branch A  
**When** a session is created  
**Then** the session belongs to Branch A.

### AC-US-BRANCH-001-B

**Given** an employee is operating Branch A  
**When** pricing is resolved  
**Then** Branch A pricing is used.

### AC-US-BRANCH-001-C

**Given** a payment/expense/shift is recorded in Branch A  
**Then** the record is associated with Branch A.

---

## US-BRANCH-002 — Unauthorized Branch Access

### AC-US-BRANCH-002-A

**Given** a user authorized only for Branch A  
**When** the user attempts to request or manipulate access to Branch B operational data  
**Then** access is rejected  
**And** Branch B data is not returned/modified.

## US-BRANCH-003 — Business and Employee Scope

### AC-US-BRANCH-003-A — One Business, Multiple Branches

**Given** Branch A and Branch B belong to Business X<br>
**And** an active Employee belongs to Business X<br>
**Then** that same Employee identity is within Business scope at both Branches<br>
**Without** adding a BranchId to Employee. PIN, device authority, and action permissions remain separate checks.

### AC-US-BRANCH-003-B — Cross-Business Boundary

**Given** Branch C belongs to Business Y<br>
**When** an Employee from Business X, including an Owner, is checked against Branch C<br>
**Then** the Employee is outside Business scope; Owner role does not grant cross-Business access.

---

# 6. Console Acceptance Criteria

## US-CONSOLE-001 — Manage Consoles

### AC-US-CONSOLE-001-A

**Given** an authorized Manager/Owner  
**When** a valid PS4 or PS5 console is added  
**Then** it becomes part of the current branch's console configuration.

### AC-US-CONSOLE-001-B

**Given** an existing console  
**When** an authorized user edits valid console configuration  
**Then** the changes are saved.

### AC-US-CONSOLE-001-C

**Given** an existing console  
**When** it is deactivated by an authorized user  
**Then** it is not available for starting a new session.

---

## US-CONSOLE-002 — View Availability

### AC-US-CONSOLE-002-A

**Given** branch consoles exist with different operational states  
**When** the Cashier opens the console overview  
**Then** the product clearly identifies which consoles can accept a new session.

---

## US-CONSOLE-003 — Reject Unavailable Console

### AC-US-CONSOLE-003-A

**Given** Console PS5-01 already has an active session  
**When** another start is attempted on PS5-01  
**Then** the new session is rejected  
**And** the original active session remains unchanged.

### AC-US-CONSOLE-003-B — Double Click

**Given** a console is available  
**When** the Cashier double-clicks Start fast enough to submit two attempts  
**Then** at most one active session is created for that console.

---

# 7. Pricing Acceptance Criteria

## US-PRICE-001 — Pricing Matrix

### AC-US-PRICE-001-A

**Given** an authorized Manager/Owner  
**When** a valid price is configured for `PS5 + Multi + Hourly` in Branch A  
**Then** the price is stored as Branch A's valid pricing context for that combination.

### AC-US-PRICE-001-B

**Given** Branch A and Branch B have different prices for the same combination  
**When** a session starts in each branch  
**Then** each session uses its own branch's price.

### AC-US-PRICE-001-C — Missing Price

**Given** no valid price exists for the selected combination  
**When** the Cashier attempts to start a session  
**Then** the start is rejected  
**And** a pricing/configuration error is shown.

---

## US-PRICE-002 — Price Audit

### AC-US-PRICE-002-A

**Given** an authorized user changes a price  
**When** the update succeeds  
**Then** audit evidence can identify the actor, branch, affected pricing context, old value, new value, and timestamp where the final audit design requires them.

---

## US-PRICE-003 — Snapshot

### AC-US-PRICE-003-A

**Given** a session starts at 150 EGP/hour  
**When** the Manager changes the current branch price to 170 EGP/hour while the session is active  
**Then** the active session retains the captured 150 EGP/hour pricing context.

---

## US-PRICE-004 — Updated Price for New Session

### AC-US-PRICE-004-A

**Given** the current valid branch price has been changed to 170 EGP/hour  
**When** a new eligible session starts after that change  
**Then** the new session captures 170 EGP/hour.

---

# 8. Session Acceptance Criteria

## US-SESSION-001 — Start Session

### AC-US-SESSION-001-A — Happy Path

**Given** the Cashier is authorized  
**And** the selected console is available  
**And** the selected mode/method combination has a valid branch price  
**When** the Cashier starts the session  
**Then** the session records branch, console, mode, pricing method, price snapshot, opener, and start time  
**And** the start is persisted locally  
**And only after successful local persistence** the UI shows the session as started  
**And** the console becomes unavailable for a second active session.

### AC-US-SESSION-001-B — Local Persistence Failure

**Given** all business validation passes  
**But** the local persistent save fails  
**When** Start is submitted  
**Then** the UI does not show a successful active session  
**And** the console is not falsely presented as successfully occupied by that failed session.

---

## US-SESSION-002 — Open Hourly

### AC-US-SESSION-002-A

**Given** the Cashier selects Hourly + Open  
**When** the session starts  
**Then** no predefined completion time is required.

### AC-US-SESSION-002-B

**Given** an Open session is active  
**When** the customer requests to finish and the Cashier completes it  
**Then** the actual completion time is recorded  
**And** billing proceeds using the Hourly rules.

### AC-US-SESSION-002-C

**Given** an Open session remains active  
**When** arbitrary elapsed time passes  
**Then** the system does not auto-complete it solely because of elapsed time.

---

## US-SESSION-003 — Fixed

### AC-US-SESSION-003-A

**Given** a Fixed session has a valid intended duration  
**When** the intended duration is reached  
**Then** a prominent center-screen alert identifies that the reserved/fixed time finished.

### AC-US-SESSION-003-B

**Given** the Fixed duration has been reached  
**Then** the session remains active until the employee performs the appropriate completion/follow-up action.

---

## US-SESSION-004 — Match

### AC-US-SESSION-004-A

**Given** a valid Match price exists  
**When** the Cashier starts/registers a Match  
**Then** the Match captures the applicable price context.

### AC-US-SESSION-004-B

**Given** a Match is active  
**When** the players finish and the employee completes it  
**Then** billing uses the captured Match price  
**And** elapsed Match time is not converted into Hourly billing.

### AC-US-SESSION-004-C

**Given** a Match is active  
**When** time passes  
**Then** the system does not automatically end it based on a timer.

### AC-US-SESSION-004-D — One Match per Session

**Given** a Match Session already represents one match
**When** players request a second match
**Then** a new Session is required
**And** the first Match Session is not extended into a second match.

---

## US-SESSION-005 — Pause

### AC-US-SESSION-005-A

**Given** an eligible active Hourly session  
**When** the Cashier selects Pause  
**Then** a Pause event/timestamp is persisted  
**And** the session enters Paused state.

### AC-US-SESSION-005-B

**Given** a paused interval exists  
**When** final Hourly billing is calculated  
**Then** that paused interval is excluded from billable active time.

### AC-US-SESSION-005-C — Open/Fixed Eligible, Match Ineligible

**Given** an active Open or Fixed Session
**When** an authorized employee Pauses it
**Then** the future Session workflow accepts the Pause and records its timestamp.

**Given** an active Match Session
**When** Pause is attempted
**Then** the future Session workflow rejects it without changing Session timing.

---

## US-SESSION-006 — Resume

### AC-US-SESSION-006-A

**Given** the session is Paused  
**When** the Cashier selects Resume  
**Then** a Resume event/timestamp is persisted  
**And** the session returns to Active.

### AC-US-SESSION-006-B — Invalid Resume

**Given** the session is not Paused  
**When** Resume is attempted  
**Then** the transition is rejected.

### AC-US-SESSION-006-C — Match Cannot Resume

**Given** a Match Session
**When** Resume is attempted
**Then** the future Session workflow rejects it; Match is never made eligible for Pause/Resume.

---

## US-SESSION-007 — Multiple Pauses

### AC-US-SESSION-007-A

**Given** an Hourly session has three completed pause intervals  
**When** final billable active time is calculated  
**Then** all three pause durations are excluded  
**And** none is excluded more than once.

---

## US-SESSION-008 — Single/Multi Change

### AC-US-SESSION-008-A

**Given** an active Single session  
**When** the Cashier attempts to switch that active session directly to Multi  
**Then** the in-place change is rejected.

### AC-US-SESSION-008-B

**Given** the customer wants Multi instead  
**When** the current context is properly closed/completed and a new Multi context is started  
**Then** the old pricing/history remains unchanged  
**And** the new context captures the Multi price.

---

## US-SESSION-009 — Responsibility Transfer

### AC-US-SESSION-009-A

**Given** Cashier A opened a session  
**And** an authorized Manager transfers current responsibility to Cashier B  
**When** the transfer succeeds  
**Then** `OpenedBy` remains Cashier A  
**And** current responsibility becomes Cashier B  
**And** transfer context is auditable according to the final design.

---

# 9. Billing Acceptance Criteria

## US-BILLING-001 — Actual Timestamps

### AC-US-BILLING-001-A

**Given** a session starts at `17:10:23`  
**And** completes at `18:28:03`  
**Then** the persisted operational time can preserve those actual timestamps for audit/recovery  
**And** elapsed time can be derived as `1:17:40`.

---

## US-BILLING-002 — Partial Minute Up

Acceptance table:

| Actual elapsed | Billable base |
|---|---:|
| `1:17:00` | `1:17` |
| `1:17:01` | `1:18` |
| `1:17:40` | `1:18` |
| `1:17:59` | `1:18` |
| `1:18:00` | `1:18` |

### AC-US-BILLING-002-A

**Given** elapsed time contains positive seconds after the completed minute  
**When** billable base duration is calculated  
**Then** it moves to the next whole minute.

---

## US-BILLING-003 — Quarter-Hour Rule

Acceptance table:

| Billable base | Final billable duration |
|---|---:|
| `2:20` | `2:20` |
| `2:27` | `2:30` |
| `2:30` | `2:30` |
| `2:42` | `2:45` |
| `2:57` | `3:00` |

### AC-US-BILLING-003-A

**Given** 3 minutes or less remain to the next quarter-hour  
**When** the quarter-hour rule is applied  
**Then** duration rounds upward to that next quarter-hour.

### AC-US-BILLING-003-B

**Given** more than 3 minutes remain to the next quarter-hour  
**When** the rule is applied  
**Then** the billable minute duration remains unchanged.

### AC-US-BILLING-003-C

**Given** a duration already lies on a quarter-hour boundary  
**When** the rule is applied  
**Then** it stays unchanged.

### AC-US-BILLING-003-D

**Given** any duration  
**When** the quarter-hour rule is applied  
**Then** it never reduces the duration to an earlier quarter-hour.

---

## US-BILLING-004 — Complete Hourly Session

### AC-US-BILLING-004-A

**Given** an active Hourly session with a known price snapshot and zero/more pause intervals  
**When** the Cashier completes it  
**Then** actual completion timestamp is recorded  
**And** paused time is excluded  
**And** minute conversion is applied  
**And** quarter-hour rule is applied  
**And** captured price is applied  
**And** final money rounding is applied  
**And** a final payable amount is produced.

### AC-US-BILLING-004-B — No Minimum

**Given** a valid short Hourly session  
**When** billing is calculated  
**Then** no separate minimum charge is added.

---

## US-BILLING-005 — Whole EGP

Acceptance table:

| Internal amount | Whole-EGP stage |
|---:|---:|
| 100.00 | 100 |
| 100.01 | 100 |
| 100.20 | 100 |
| 100.49 | 100 |
| 100.50 | 101 |
| 100.99 | 101 |

### AC-US-BILLING-005-A

**Given** a fractional EGP amount below `.50`  
**Then** the whole-EGP stage rounds to the current pound.

### AC-US-BILLING-005-B

**Given** a fractional EGP amount at or above `.50`  
**Then** the whole-EGP stage rounds to the next pound.

---

## US-BILLING-006 — Upward Multiple of Five

Acceptance table:

| Whole-EGP stage | Final |
|---:|---:|
| 100 | 100 |
| 101 | 101 |
| 102 | 102 |
| 103 | 105 |
| 104 | 105 |
| 105 | 105 |
| 106 | 106 |
| 107 | 107 |
| 108 | 110 |
| 109 | 110 |
| 110 | 110 |

### AC-US-BILLING-006-A

**Given** the next multiple of 5 is 1 or 2 EGP higher  
**When** final rounding is applied  
**Then** the amount increases to that multiple.

### AC-US-BILLING-006-B

**Given** reaching a multiple of 5 would require lowering the amount  
**Then** the amount is not lowered.

---

# 10. Finance Acceptance Criteria

## US-FINANCE-001 — Invoice

### AC-US-FINANCE-001-A

**Given** a valid session completion has a final payable amount  
**When** the invoice creation step succeeds  
**Then** an invoice exists for the correct branch and operation  
**And** its amount matches the approved final charge.

### AC-US-FINANCE-001-B

**Given** an invoice already exists for the accepted operation identity  
**When** the same synchronization operation is retried  
**Then** a second invoice is not created.

### AC-US-FINANCE-001-C — Invoice Before Payment

**Given** a valid completed session has a final payable amount
**When** its invoice is issued before cash is received
**Then** the invoice can exist without a Payment record
**And** a later cash payment is a separate business step.

### AC-US-FINANCE-001-D — Total Paused Duration

**Given** an Open or Fixed Session has multiple completed Pause intervals
**When** its customer Invoice is created
**Then** it shows one total non-billable paused duration, not an itemized list of Pause intervals.

---

## US-FINANCE-002 — Cash Payment

### AC-US-FINANCE-002-A

**Given** a valid invoice exists  
**When** the Cashier records the full cash payment  
**Then** the payment is linked to the correct invoice/transaction  
**And** the payment becomes locally persistent before success is displayed.

### AC-US-FINANCE-002-B

**Given** the same accepted payment sync operation is retried  
**Then** a duplicate payment is not created.

---

## US-FINANCE-003 — Financial Consistency

### AC-US-FINANCE-003-A

**Given** an operation requires Session completion + Invoice + Pending Sync persistence, and Payment only if cash is collected in that operation
**When** the required local commit fails before completion  
**Then** the UI does not report success for that operation
**And** the product does not intentionally present a half-completed operation as complete.

---

## US-FINANCE-004 — Cancel/Void

### AC-US-FINANCE-004-A

**Given** an eligible unpaid invoice exists
**When** an authorized protected cancel/void succeeds  
**Then** the original invoice remains historically traceable  
**And** it is not treated as a normal active-revenue invoice.

### AC-US-FINANCE-004-B — Optional Reason

**Given** an authorized Manager/Owner is completing the protected Cancel/Void flow  
**When** no reason is entered  
**Then** the cancellation/void may still succeed if every other rule is valid  
**And** the invoice remains historically traceable.

### AC-US-FINANCE-004-C — Reason Preserved When Supplied

**Given** an authorized Cancel/Void flow  
**When** the user enters an optional reason  
**Then** the reason is preserved in the relevant history/audit context.

### AC-US-FINANCE-004-D — Paid Invoice Cannot Be Cancelled

**Given** an Invoice has a successful recorded Payment
**When** an authorized user attempts Cancel
**Then** the operation is rejected, the paid Invoice and Payment remain historical, and no automatic refund/reversal is created.

### AC-US-FINANCE-004-E — Eligible Unpaid Cancel

**Given** an eligible unpaid Invoice
**When** an authorized protected Cancel succeeds
**Then** the Invoice remains historical and is excluded from active revenue; the optional reason rule still applies.

---

# 11. Shift Acceptance Criteria

## US-SHIFT-001 — Start Shift

### AC-US-SHIFT-001-A

**Given** an authorized Manager/Owner  
**When** Start Shift succeeds  
**Then** the shift records the branch, starting actor, and start time.

### AC-US-SHIFT-001-B

**Given** a Cashier lacks StartShift permission  
**When** the Cashier attempts to start a shift  
**Then** the operation is rejected.

---

## US-SHIFT-002 — End Shift

### AC-US-SHIFT-002-A

**Given** an authorized Manager/Owner  
**And** the shift is eligible to close under the final shift rules  
**When** End Shift succeeds  
**Then** an end time is recorded.

### AC-US-SHIFT-002-B — Active Session Handover

**Given** an authorized Manager/Owner is ending a Cashier shift  
**And** one or more active sessions are currently assigned to that Cashier  
**When** End Shift is requested  
**Then** the shift is not closed until those active sessions are transferred to another authorized Cashier  
**And** each transferred session preserves its original opener  
**And** the receiving Cashier becomes current responsible user  
**And** the handover is auditable.

### AC-US-SHIFT-002-C — Attribution After Handover

**Given** a session was transferred from Cashier A to Cashier B during shift close  
**When** the session continues after the transfer  
**Then** the remaining operational/session responsibility is attributed to Cashier B while the original `OpenedBy` history remains Cashier A.

---

# 12. Expense Acceptance Criteria

## US-EXPENSE-001 — Record Expense

### AC-US-EXPENSE-001-A

**Given** an authorized Manager/Owner  
**When** a valid amount and description/category are submitted  
**Then** an expense is persisted for the current branch  
**And** the actor and timestamp are attributable.

### AC-US-EXPENSE-001-B

**Given** a Cashier without expense permission  
**When** expense creation is attempted  
**Then** the action is rejected.

---

## US-EXPENSE-002 — Repair Cost

### AC-US-EXPENSE-002-A

**Given** two controllers require repair  
**When** an authorized user records the repair cost as an expense with a suitable description/note  
**Then** the expense is included in operational expense reporting  
**And** no dedicated damage lifecycle is required for that recording.

---

# 13. Asset Acceptance Criteria

## US-ASSET-001 — Basic Quantities

### AC-US-ASSET-001-A — Approved Quantity Scope

**Given** an authorized Manager/Owner is in an authorized branch  
**When** the user opens basic equipment management  
**Then** the product supports branch-scoped quantity records for Controllers, Accessories, Equipment, and Assets.

### AC-US-ASSET-001-B — Maintain Quantity Records

**Given** an approved equipment quantity record exists  
**When** an authorized Manager/Owner changes the maintained quantity  
**Then** the updated quantity remains associated with the correct branch.

### AC-US-ASSET-001-C — Scope Boundary

**Then** R1 does not treat these records as product-sales stock  
**And** does not require per-unit controller damage lifecycle or advanced inter-branch transfer.

---

# 14. Report Acceptance Criteria

## US-REPORT-001 — Period Reports

### AC-US-REPORT-001-A

**Given** valid financial activity exists in a date period  
**When** an authorized user selects Daily, Weekly, Monthly, or Yearly reporting  
**Then** the product can provide the corresponding period view.

### AC-US-REPORT-001-B

**Given** valid revenue and expenses exist in the selected period  
**Then** operational Profit equals Revenue minus Expenses according to the R1 model.

### AC-US-REPORT-001-C — Branch Scope

**Given** a user is authorized only for Branch A  
**When** the user views branch reports  
**Then** unauthorized Branch B data is not included.

---

## US-REPORT-002 — Monthly Review

### AC-US-REPORT-002-A

**Given** the Owner chooses to review a completed month  
**When** the monthly review is opened  
**Then** the relevant period financial information can be reviewed.

### AC-US-REPORT-002-B — Review Does Not Mutate Financial State

**Given** the Owner opens or completes the monthly review  
**Then** invoices, payments, revenue, expenses, and profit records are not changed merely because the review occurred  
**And** the period is not locked by this R1 review feature.

---

# 15. Offline Acceptance Criteria

## US-OFFLINE-001 — Continue Core Work

### AC-US-OFFLINE-001-A

**Given** Internet connectivity is lost  
**And** the branch has valid offline license/security authority  
**When** the Cashier performs approved core operations  
**Then** the product continues to allow:

```text
Login
View required local operational data
Start Session
Pause
Resume
Complete
Calculate Charge
Create Invoice
Record Cash Payment
```

subject to user permissions.

### AC-US-OFFLINE-001-B

**Given** the central API is unavailable  
**When** a core approved operation is successfully committed locally  
**Then** the operation remains locally successful  
**And** central unavailability alone does not erase it.

---

## US-OFFLINE-002 — Local vs Sync Status

### AC-US-OFFLINE-002-A

**Given** an operation is successfully saved locally but not yet centrally acknowledged  
**Then** the UI indicates a local/pending sync state rather than falsely showing central synchronization.

### AC-US-OFFLINE-002-B

**Given** synchronization succeeds later  
**Then** the displayed sync state can move to Synced.

### AC-US-OFFLINE-002-C

**Given** synchronization fails or conflicts  
**Then** the UI exposes a failed/conflict state to an authorized user rather than silently claiming success.

---

## US-OFFLINE-003 — Restricted Admin

### AC-US-OFFLINE-003-A — No Permission Elevation

**Given** any Cashier, Manager, or Owner is operating offline  
**When** role/permission checks are evaluated  
**Then** the user does not gain any permission that the same user/role did not already have online.

### AC-US-OFFLINE-003-B — Central Verification Dependency

**Given** a protected action is defined by Security/RBAC Design as requiring current central verification  
**And** that verification is unavailable offline  
**When** the action is attempted  
**Then** the action is unavailable/rejected rather than being granted extra offline authority.

---

# 16. Sync Acceptance Criteria

## US-SYNC-001 — Queue Operations

### AC-US-SYNC-001-A

**Given** a core operation successfully commits locally  
**And** it requires central synchronization  
**Then** a corresponding pending sync representation exists.

### AC-US-SYNC-001-B

**Given** the local business operation fails to commit  
**Then** the system does not create a misleading successful business state solely because a sync record exists.

---

## US-SYNC-002 — Retry

### AC-US-SYNC-002-A

**Given** a pending operation fails because the network/API is unavailable  
**When** retry occurs later  
**Then** the same logical operation identity is reused according to the final sync design.

### AC-US-SYNC-002-B

**Given** connectivity returns  
**Then** pending operations can be processed without the Cashier manually recreating the original business operation.

---

## US-SYNC-003 — Lost ACK Deduplication

### AC-US-SYNC-003-A

**Given** the server successfully commits an operation  
**But** the acknowledgement is lost before the branch receives it  
**When** the branch retries the same stable operation  
**Then** the server recognizes/reconciles the existing accepted operation  
**And** does not create a duplicate financial/business effect.

### AC-US-SYNC-003-B

The same acceptance must hold for at least:

```text
Session
Invoice
Payment
```

---

## US-SYNC-004 — Conflict/Rejection

### AC-US-SYNC-004-A

**Given** a pending local operation is rejected centrally  
**Then** the local evidence remains available  
**And** its sync status becomes rejected/conflict as appropriate  
**And** it is not silently deleted.

### AC-US-SYNC-004-B

**Given** the conflict affects financial data  
**Then** the product does not silently overwrite the financial record merely to hide the conflict.

---

# 17. Subscription Acceptance Criteria

## US-SUB-001 — Online Verification

### AC-US-SUB-001-A

**Given** Internet connectivity is available when the application starts  
**Then** the system performs the required subscription/license verification according to the final implementation policy.

### AC-US-SUB-001-B

**Given** the branch transitions from offline to online  
**Then** a required subscription/license verification is triggered.

### AC-US-SUB-001-C

**Given** the branch remains online continuously  
**Then** periodic verification occurs  
**And** there is at least one successful verification opportunity per day under normal service availability.

---

## US-SUB-002 — More Than 10 Days

### AC-US-SUB-002-A

**Given** a successful verification occurs with 20 days remaining  
**Then** the issued/recorded offline authority does not exceed 72 hours from the applicable successful verification point.

### AC-US-SUB-002-B

**Given** more than 10 days remain  
**Then** the general R1 offline-lease rule is 72 hours maximum before another successful verification is required.

---

## US-SUB-003 — 10 Days or Less

Acceptance examples:

| Remaining at successful check | Offline allowed |
|---|---:|
| 10 days | 10 days + 10 hours |
| 8 days | 8 days + 10 hours |
| 7 days | 7 days + 10 hours |
| 3 days | 3 days + 10 hours |
| 1 day | 1 day + 10 hours |

### AC-US-SUB-003-A

**Given** the remaining subscription is 8 days at successful verification  
**Then** the offline authority ends after 8 days + 10 hours unless renewed earlier.

### AC-US-SUB-003-B

**Given** the paid subscription reaches its ordinary expiry during this window  
**Then** only the approved 10-hour grace remains.

---

## US-SUB-004 — LicenseExpired

### AC-US-SUB-004-A

**Given** the branch remains offline past `OfflineAllowedUntil`  
**And** no successful renewal occurred  
**When** the allowed window ends  
**Then** the system enters `LicenseExpired`  
**And** operational use is blocked according to the subscription policy even though the device remains offline.

### AC-US-SUB-004-B

**Given** business data existed before LicenseExpired  
**Then** that data remains preserved.

---

## US-SUB-005 — SubscriptionSuspended

### AC-US-SUB-005-A

**Given** the branch is online  
**And** the central platform confirms the subscription is suspended  
**Then** the system enters `SubscriptionSuspended`.

### AC-US-SUB-005-B

**Given** SubscriptionSuspended is active  
**Then** PIN security recovery cannot return the product to normal operation.

### AC-US-SUB-005-C

**Given** the subscription is later validly restored and the branch successfully receives/validates the new authority  
**Then** the product can leave SubscriptionSuspended according to the final state machine  
**Without deleting prior business data**.

---

## US-SUB-006 — Clock Rollback

### AC-US-SUB-006-A

**Given** trusted prior license/time history indicates a later time  
**When** the Windows clock is moved suspiciously backward  
**Then** the system does not extend `OfflineAllowedUntil` based on the backward clock.

### AC-US-SUB-006-B

**Given** suspicious rollback is detected  
**Then** the product enters the defined restricted/verification-required behavior rather than silently granting more offline time.

Exact trusted-time detection belongs to Security Design.

---

# 18. Audit Acceptance Criteria

## US-AUDIT-001 — Sensitive Actions

### AC-US-AUDIT-001-A

For each sensitive event selected by the final audit design, the audit evidence must be able to identify the relevant:

```text
Actor
Branch
Action
Timestamp
Affected record/reference
Reason / before-after where required
```

Candidate events include:

```text
Repeated Login Failure
Security Lock / Unlock
PIN Reset
Pricing Change
User/Permission Change
Expense Creation
Invoice Cancel/Void
Session Responsibility Transfer
Subscription State Change
Important Sync Rejection/Conflict
```

---

## US-AUDIT-002 — No Secret Leakage

### AC-US-AUDIT-002-A

**Given** authentication or recovery activity occurs  
**When** normal logs/audit data are produced  
**Then** plaintext PINs and private signing/connection secrets are not intentionally written into them.

---

# 19. Recovery Acceptance Criteria

## US-RECOVERY-001 — Active Session Recovery

### AC-US-RECOVERY-001-A

**Given** a session starts and its Start event is locally committed  
**When** the application closes unexpectedly and restarts  
**Then** the session can be identified as still active/paused according to persisted state  
**And** its original Start timestamp remains unchanged.

### AC-US-RECOVERY-001-B

**Given** a session started at 5:00 PM  
**And** the PC loses power at 5:37 PM after the start was committed  
**When** power returns and the application restarts  
**Then** the recovered session does not restart its timing at the reboot time.

### AC-US-RECOVERY-001-C — Pause Recovery

**Given** a session was Paused and the pause event had committed before interruption  
**When** the application restarts  
**Then** the recovered state reflects the persisted pause state/history.

---

## US-RECOVERY-002 — No Per-Second Writes

### AC-US-RECOVERY-002-A

**Given** an active session timer is displayed  
**When** seconds change on screen  
**Then** the product does not require a database update every displayed second.

### AC-US-RECOVERY-002-B

**Given** Start/Pause/Resume/Complete events are persisted  
**Then** the displayed elapsed time can be derived from those timestamps/events.

---

## US-RECOVERY-003 — Local Backup

### AC-US-RECOVERY-003-A

**Given** the branch product is running under normal conditions  
**When** the configured local backup schedule reaches its interval  
**Then** the system can produce an automatic local SQLite backup approximately every 30 minutes according to the later backup implementation.

### AC-US-RECOVERY-003-B

**Given** a standard R1 branch deployment  
**Then** an external SSD, NAS, or UPS is not a mandatory requirement for the software to operate.

---

# 20. Cross-Cutting NFR Acceptance

These criteria apply across applicable stories.

## AC-NFR-001 — No False Success

**Given** a required local business persistence step fails  
**Then** the product does not show the operation as successfully committed.

Trace: `NFR-DATA-002`

## AC-NFR-002 — Offline Independence

**Given** the central API is unavailable  
**And** valid offline authority exists  
**Then** approved core branch operations remain available.

Trace: `NFR-AVL-001`, `NFR-AVL-002`

## AC-NFR-003 — Branch RTO Target

A planned branch recovery test must demonstrate the target:

```text
≤ 5 minutes
```

after power/device availability under the agreed normal recovery scenario.

Trace: `NFR-REC-001`

## AC-NFR-004 — Central RPO Target

Central backup/restore validation must be designed to verify the target:

```text
≤ 5 minutes
```

Trace: `NFR-REC-003`

## AC-NFR-005 — Central RTO Target

Central disaster-recovery exercises/plans must target:

```text
≤ 4 hours
```

Trace: `NFR-REC-002`

## AC-NFR-006 — Cashier Status Clarity

The UX must make the following states distinguishable where applicable:

```text
Online
Offline
Saved Locally
Pending Sync
Syncing
Synced
Sync Failed / Conflict
```

Trace: `NFR-UX-003`

## AC-NFR-007 — Lock-State Clarity

The UX must not present these as the same cause:

```text
SecurityLocked
SubscriptionSuspended
LicenseExpired
Maintenance
```

Trace: `NFR-UX-004`

## AC-NFR-008 — Security Secrets

Security testing must confirm that normal logs/audit output do not intentionally expose protected secrets.

Trace: `NFR-SEC-006`

## AC-NFR-009 — Authorization Negative Test

At least one non-UI/manipulated-path test must verify that UI hiding alone is not the authorization control.

Trace: `NFR-SEC-003`

## AC-NFR-010 — Price Snapshot Regression

Automated tests must verify that changing current pricing during an active session does not change that session's captured price.

Trace: `NFR-DATA-004`

---

# 21. Required Boundary Test Matrix

The final test plan must include at least the following boundaries.

## Time

```text
0 seconds remainder
1 second remainder
59 seconds remainder

Quarter boundary exactly
4 minutes before quarter
3 minutes before quarter
2 minutes before quarter
1 minute before quarter
```

## Money

```text
.00
.01
.49
.50
.51
.99

Whole amounts:
100
101
102
103
104
105
106
107
108
109
110
```

## PIN

```text
Attempt 1
Attempt 4
Attempt 5
Temporary lock
Post-lock attempt cycle
Second cycle attempt 5
SecurityLocked
Valid online recovery
Valid offline recovery
Invalid/reused recovery response
```

## Subscription

```text
>10 days remaining
Exactly 10 days
8 days
7 days
1 day
Grace boundary
OfflineAllowedUntil - 1 second
OfflineAllowedUntil exact
OfflineAllowedUntil + 1 second
Clock rollback
```

## Sync

```text
Offline before operation
Disconnect after local commit
Disconnect while sending
Server commit + lost ACK
Retry same operation
Duplicate retry
Conflict
Server rejection
Reconnect backlog
```

---

# 22. Formerly Blocked Acceptance Items — Now Resolved for UX

The five acceptance dependencies that previously blocked the UX gate are now resolved:

```text
AC-US-FINANCE-004-B → reason is optional.
AC-US-SHIFT-002-B   → active-session handover required before shift close.
AC-US-ASSET-001-*   → basic quantity scope defined.
AC-US-REPORT-002-B  → monthly review does not mutate financial state.
AC-US-OFFLINE-003-* → offline does not elevate permissions.
```

Later Security/RBAC testing will still verify central-verification requirements for protected actions in `RestrictedOffline`.

---

# 23. Definition of Acceptance Readiness

A story is ready to move from requirements into detailed design when:

```text
User Story exists
+
Trace to FR/NFR exists
+
Acceptance Criteria are testable
+
No hidden business decision remains
+
Relevant edge cases are identified
```

Stories blocked by an open decision remain visible but are not considered fully requirements-locked.

---

# 24. R1 End-to-End Acceptance Scenario

A mandatory later UAT/E2E scenario should demonstrate:

```text
Authorized Manager starts branch shift
      ↓
Cashier authenticates
      ↓
Cashier sees available consoles
      ↓
Starts a valid PS5 Multi Hourly session
      ↓
Local persistence succeeds
      ↓
Session pauses
      ↓
Session resumes
      ↓
Internet fails
      ↓
Branch continues locally
      ↓
Session completes
      ↓
Correct duration is calculated
      ↓
Correct amount is rounded
      ↓
Invoice is created
      ↓
Cash payment is recorded
      ↓
Operation remains locally saved
      ↓
Internet returns
      ↓
Pending operations synchronize
      ↓
No duplicates are created
      ↓
Manager views financial reporting
```

Additional mandatory resilience paths:

```text
Active session + application restart
SecurityLocked + recovery
Offline license expiry
Price changes during active session
Lost sync acknowledgement
```

---

# 25. Placement

Recommended filename:

```text
ACCEPTANCE_CRITERIA_R1.md
```

Place in:

```text
Project.docs/Requirements/
```

---

# 26. Next Step

The UX preparation baseline is now:

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
SYSTEM ANALYSIS
```

No implementation or test-pass status is claimed by this document.
