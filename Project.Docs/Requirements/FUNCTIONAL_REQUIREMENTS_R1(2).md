# +90 PS — Release 1 Functional Requirements

**Document Type:** Functional Requirements Specification  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Baseline  
**Last Updated:** 2026-09-09
**Primary Inputs:** `BUSINESS_RULES_R1.md`, `BRD_R1.md`, `PRD_R1.md`  
**Purpose:** Define what Release 1 must functionally do without defining implementation classes, tables, or endpoint routes.

---

# 1. Requirement Format

Each functional requirement uses `FR-<AREA>-NNN`.

All requirements are `MUST` unless explicitly stated otherwise.

---

# 2. Authentication

## FR-AUTH-001
The system shall allow an active employee to authenticate using a personal PIN.

## FR-AUTH-002
The system shall identify the authenticated application user independently from the Windows account.

## FR-AUTH-003
The system shall maintain a current active-user context containing user, role, branch, and permissions.

## FR-AUTH-004
The system shall allow approved offline authentication when the branch has valid offline authority.

## FR-AUTH-005
The system shall reject authentication for a user whose applicable status/authority does not permit login.

## FR-AUTH-006
The system shall support switching the current application user on the shared branch PC.

## FR-AUTH-007
The system shall support authorized PIN reset to a new PIN.

## FR-AUTH-008
The system shall not provide a function that reveals the employee's previous readable PIN.

---

# 3. PIN Security

## FR-PIN-001
The system shall count failed PIN attempts within the defined attempt cycle.

## FR-PIN-002
After five failed attempts, the system shall block further PIN attempts for 20 seconds.

## FR-PIN-003
At the first threshold, the system shall trigger the configured warning sound.

## FR-PIN-004
At the first threshold, the system shall record a security event.

## FR-PIN-005
If another five failed attempts occur after the first temporary lock cycle, the system shall enter `SecurityLocked`.

## FR-PIN-006
While `SecurityLocked`, the system shall display a blocking recovery message.

## FR-PIN-007
The system shall support authorized online recovery from `SecurityLocked`.

## FR-PIN-008
The system shall support an offline one-time challenge/recovery flow when the branch has no connectivity.

## FR-PIN-009
An offline recovery response shall be usable only for the relevant branch/challenge according to Security Design.

## FR-PIN-010
A PIN security recovery shall not change `SubscriptionSuspended` or `LicenseExpired` into Operational.

---

# 4. Roles and Authorization

## FR-RBAC-001
The system shall recognize Owner, Manager, and Cashier roles.

## FR-RBAC-002
The system shall evaluate permission for protected operations.

## FR-RBAC-003
The system shall prevent a Cashier from changing pricing unless a later explicit permission rule authorizes it.

## FR-RBAC-004
The system shall prevent a Cashier from creating expenses unless a later explicit permission rule authorizes it.

## FR-RBAC-005
The system shall require appropriate authorization for protected invoice cancel/void actions.

## FR-RBAC-006
The system shall require appropriate authorization for session-responsibility transfer.

## FR-RBAC-007
The system shall restrict branch access to authorized branch/ownership scope.

## FR-RBAC-008
The UI shall not expose Manager/Owner administration as normal Cashier functionality.

## FR-RBAC-009
The UX role visibility baseline shall follow `RBAC_MATRIX_R1.md`, while trusted authorization enforcement is finalized in Security/RBAC Design.

---

# 5. Branch

## FR-BRANCH-001
The system shall maintain an active branch context for branch operations.

## FR-BRANCH-002
The system shall associate branch-scoped records with the correct branch.

## FR-BRANCH-003
The system shall prevent users from viewing or modifying unauthorized branch data through trusted controls.

---

# 6. Console Management

## FR-CONSOLE-001
An authorized administrative user shall be able to add a console.

## FR-CONSOLE-002
An authorized administrative user shall be able to edit a console.

## FR-CONSOLE-003
An authorized administrative user shall be able to activate/deactivate a console.

## FR-CONSOLE-004
The system shall support PS4 and PS5 console types.

## FR-CONSOLE-005
The system shall display whether a console is available for a new session.

## FR-CONSOLE-006
The system shall reject starting a new session on a console that is not available.

## FR-CONSOLE-007
The system shall prevent two simultaneous active sessions on the same console.

---

# 7. Pricing

## FR-PRICE-001
The system shall support branch-specific pricing.

## FR-PRICE-002
The system shall support independent prices by console type.

## FR-PRICE-003
The system shall support Single and Multi modes.

## FR-PRICE-004
The system shall support Hourly and Match pricing methods.

## FR-PRICE-005
The system shall support valid independent combinations of console type, mode, and pricing method.

## FR-PRICE-006
The system shall reject a session start when no valid applicable price exists.

## FR-PRICE-007
An authorized Manager/Owner shall be able to create/update branch pricing according to the permission model.

## FR-PRICE-008
The system shall record an auditable pricing change context.

## FR-PRICE-009
The system shall capture the applicable pricing snapshot when a session starts.

## FR-PRICE-010
The system shall preserve the active-session pricing snapshot when current pricing changes.

## FR-PRICE-011
A session started after a valid price update shall use the updated price.

---

# 8. Session Start

## FR-SESSION-001
The Cashier shall be able to select an available console for a new session.

## FR-SESSION-002
The Cashier shall be able to choose Single or Multi.

## FR-SESSION-003
The Cashier shall be able to choose Hourly or Match.

## FR-SESSION-004
For Hourly, the Cashier shall be able to choose Open or Fixed behavior.

## FR-SESSION-005
Before start, the system shall validate console availability and applicable price.

## FR-SESSION-006
The system shall capture the employee who opens the session.

## FR-SESSION-007
The system shall capture branch, console, selected mode, pricing method, and price snapshot.

## FR-SESSION-008
The system shall persist the session start locally before reporting successful start.

## FR-SESSION-009
After successful start, the selected console shall no longer be available for another active session.

---

# 9. Open Hourly Session

## FR-OPEN-001
The system shall support an Open hourly session with no predefined end time.

## FR-OPEN-002
The Cashier shall be able to complete an Open session when the customer requests to finish.

## FR-OPEN-003
The system shall not automatically complete an Open session based only on elapsed time.

---

# 10. Fixed Session

## FR-FIXED-001
The system shall allow an intended duration to be set for a Fixed session.

## FR-FIXED-002
When the intended duration is reached, the system shall display a prominent center-screen alert identifying the affected console/session.

## FR-FIXED-003
The system shall not automatically stop the session when the intended duration is reached.

---

# 11. Match Session

## FR-MATCH-001
The system shall allow the Cashier to start/register a Match session.

## FR-MATCH-002
The system shall use the applicable captured Match price.

## FR-MATCH-003
The system shall allow the employee to complete the Match when the players have finished.

## FR-MATCH-004
The system shall not calculate Match price from elapsed time.

## FR-MATCH-005
The system shall not automatically end the Match based on a system timer.

## FR-MATCH-006
One Match Session shall represent exactly one match. A second match shall require a new Session; the configured Match duration is a reference, not an automatic end or Hourly billing input.

---

# 12. Pause / Resume

## FR-PAUSE-001
The Cashier shall be able to pause an active eligible session.

## FR-PAUSE-002
The system shall persist the Pause event/timestamp.

## FR-PAUSE-003
The Cashier shall be able to resume a paused session.

## FR-PAUSE-004
The system shall persist the Resume event/timestamp.

## FR-PAUSE-005
The system shall support multiple Pause/Resume cycles.

## FR-PAUSE-006
The system shall exclude paused duration from billable active duration.

## FR-PAUSE-007
The system shall prevent invalid pause/resume state transitions.

---

# 13. Single/Multi Change

## FR-MODE-001
The system shall not allow Single/Multi to be changed in-place in an active session pricing context.

## FR-MODE-002
To use a different mode, the current context shall be properly completed/closed before the new mode is started.

---

# 14. Time Calculation

## FR-TIME-001
The system shall retain actual start/end timestamps with seconds precision or better as required by later data design.

## FR-TIME-002
Billing shall use minute-based billable duration.

## FR-TIME-003
If elapsed time contains positive seconds beyond a complete minute, the billable base shall move to the next minute.

Examples:

```text
1:17:00 → 1:17
1:17:01 → 1:18
1:17:40 → 1:18
```

## FR-TIME-004
After billable-base conversion, if 3 minutes or less remain to the next quarter-hour, the system shall round upward to that quarter-hour.

## FR-TIME-005
The quarter-hour rule shall not round a duration backward.

## FR-TIME-006
A duration already exactly on the relevant quarter-hour boundary shall remain unchanged.

## FR-TIME-007
The system shall not apply a minimum charge.

---

# 15. Money Calculation

## FR-MONEY-001
The system shall use a currency representation suitable for exact monetary calculation. The technical type is defined later.

## FR-MONEY-002
The customer-facing final amount shall be whole EGP.

## FR-MONEY-003
Amounts below 0.50 EGP fraction shall round to the current whole EGP.

## FR-MONEY-004
Amounts at or above 0.50 EGP fraction shall round to the next whole EGP.

## FR-MONEY-005
After whole-EGP rounding, if the next multiple of 5 is 1 or 2 EGP higher, the system shall round upward to it.

## FR-MONEY-006
The multiple-of-five rule shall never lower the amount.

---

# 16. Session Completion

## FR-COMPLETE-001
The Cashier shall be able to complete an eligible active session.

## FR-COMPLETE-002
The system shall capture the actual completion timestamp.

## FR-COMPLETE-003
For Hourly sessions, the system shall calculate active elapsed duration excluding pauses.

## FR-COMPLETE-004
The system shall apply approved time rules.

## FR-COMPLETE-005
The system shall apply the captured price snapshot.

## FR-COMPLETE-006
The system shall apply approved money-rounding rules.

## FR-COMPLETE-007
The system shall produce the final payable amount before invoice/payment completion.

---

# 17. Invoice

## FR-INVOICE-001
The system shall create an invoice for a valid completed financial operation.

## FR-INVOICE-002
The invoice shall belong to the correct branch.

## FR-INVOICE-003
The invoice shall reference the relevant operational transaction/session.

## FR-INVOICE-004
The system shall preserve invoice history.

## FR-INVOICE-005
The system shall prevent unrestricted destructive invoice deletion.

## FR-INVOICE-006
The system shall support an authorized protected cancel/void flow.

## FR-INVOICE-007
A cancelled/void invoice shall remain traceable.

## FR-INVOICE-008
A cancelled/void invoice shall not count as valid active revenue according to the final financial rule.

## FR-INVOICE-009
The cancel/void workflow shall provide an optional reason field. Leaving the reason empty shall not by itself prevent an otherwise authorized valid cancellation/void.

## FR-INVOICE-010
If a cancellation/void reason is entered, the system shall preserve it with the relevant history/audit context.

## FR-INVOICE-011
The system shall allow an invoice to be issued before cash payment. Invoice creation shall not require payment in the same operation.

---

# 18. Cash Payment

## FR-PAY-001
The system shall support Cash as the only Release 1 payment method.

## FR-PAY-002
The system shall record the cash payment for the relevant invoice/transaction.

## FR-PAY-003
The payment shall become locally persistent before success is shown.

## FR-PAY-004
A retried sync of the same accepted payment shall not create another payment.

---

# 19. Financial Commit Behavior

## FR-FIN-001
The system shall not report successful financial completion when required local records failed to persist.

## FR-FIN-002
The local completion workflow shall preserve consistency among the records required for that operation. Session completion, invoice, and pending-sync records must persist before their success is shown; if cash is collected in the same operation, its payment record is required too. An invoice may be issued before a separate later cash payment.

The exact transaction boundary is defined in Database/Offline Design.

---

# 20. Shift

## FR-SHIFT-001
An authorized Manager/Owner shall be able to start a shift.

## FR-SHIFT-002
The system shall record branch, starting actor, and start time.

## FR-SHIFT-003
An authorized Manager/Owner shall be able to end a shift.

## FR-SHIFT-004
The system shall record the shift end time.

## FR-SHIFT-005
The system shall not grant shift-administration capability to Cashier by default.

## FR-SHIFT-006
If active sessions are still assigned to the Cashier whose shift is being closed, the system shall require an authorized transfer of those sessions to another Cashier before the shift can close.

## FR-SHIFT-007
A handover shall preserve the original session opener and set the receiving Cashier as current responsible user.

## FR-SHIFT-008
The remaining operational/session responsibility after the handover shall be attributed to the receiving Cashier, and the transfer shall be auditable.

---

# 21. Expenses

## FR-EXP-001
An authorized Manager/Owner shall be able to create an expense.

## FR-EXP-002
The expense shall include amount and description/category.

## FR-EXP-003
The system shall associate the expense with branch, actor, and timestamp.

## FR-EXP-004
The system shall allow a relevant note.

## FR-EXP-005
Controller/equipment repair cost shall be recordable as an Expense.

---

# 22. Basic Assets / Equipment

## FR-ASSET-001
The system shall support basic branch-scoped quantity tracking for the approved internal equipment categories: Controllers, Accessories, Equipment, and Assets.

## FR-ASSET-002
The system shall not treat internal equipment quantity as product-sales inventory.

## FR-ASSET-003
A dedicated per-controller damage lifecycle is not required.

## FR-ASSET-004
Advanced inter-branch transfer is not required.

## FR-ASSET-005
An authorized Manager/Owner shall be able to view and maintain the approved equipment quantity records for the current authorized branch.

## FR-ASSET-006
Console devices shall continue to use Console Management and shall not be duplicated as quantity-only equipment records.

---

# 23. Reports

## FR-REPORT-001
Authorized users shall be able to view daily reports.

## FR-REPORT-002
Authorized users shall be able to view weekly reports.

## FR-REPORT-003
Authorized users shall be able to view monthly reports.

## FR-REPORT-004
Authorized users shall be able to view yearly reports.

## FR-REPORT-005
The system shall provide Revenue reporting.

## FR-REPORT-006
The system shall provide Expense reporting.

## FR-REPORT-007
The system shall provide operational Profit reporting.

## FR-REPORT-008
Operational Profit shall be derived as Revenue minus Expenses for the R1 reporting model.

## FR-REPORT-009
Reports shall respect branch/ownership authorization.

## FR-REPORT-010
The product may provide an optional Owner monthly review action.

## FR-REPORT-011
The monthly review shall be read-only with respect to financial state: it shall not lock a period or mutate invoice, payment, revenue, expense, or profit records merely because the review was opened/completed.

---

# 24. Offline State

## FR-OFFLINE-001
The system shall detect loss of Internet connectivity.

## FR-OFFLINE-002
The system shall determine whether the branch has valid offline security/subscription authority.

## FR-OFFLINE-003
If allowed, the system shall enter an appropriate offline operational state.

## FR-OFFLINE-004
The system shall allow approved core offline operations.

## FR-OFFLINE-005
The system shall clearly display whether data is Saved Locally, Pending Sync, Syncing, Synced, or Failed/Conflict as applicable.

## FR-OFFLINE-006
Offline operation shall preserve the same role/permission boundaries used online and shall not elevate a user to additional permissions.

## FR-OFFLINE-007
If a protected action requires current central verification according to later Security/RBAC Design, the product shall make that action unavailable while the required verification cannot be performed rather than granting extra offline authority.

---

# 25. Local Persistence

## FR-LOCAL-001
The system shall persist approved core business operations locally.

## FR-LOCAL-002
The system shall not require central API availability before reporting an approved locally committed core operation as locally saved.

## FR-LOCAL-003
The system shall create the required pending synchronization representation for operations requiring central sync.

## FR-LOCAL-004
If local persistence fails, the UI shall not report the business operation as successful.

---

# 26. Synchronization

## FR-SYNC-001
The system shall maintain pending synchronization items for locally committed operations requiring central sync.

## FR-SYNC-002
The sync process shall retry failed operations according to later Offline/Sync Design.

## FR-SYNC-003
Each sync operation shall have a stable operation identity.

## FR-SYNC-004
Central synchronization shall be idempotent for the same accepted operation identity.

## FR-SYNC-005
If the central server has already committed an operation but the acknowledgement was lost, retrying that operation shall not duplicate the business effect.

## FR-SYNC-006
The system shall mark successful local synchronization state after accepted acknowledgement.

## FR-SYNC-007
The system shall represent rejected/conflicting synchronization explicitly.

## FR-SYNC-008
A conflict/rejection shall not silently erase the local committed business record.

---

# 27. Subscription / Offline License

## FR-SUB-001
The product shall verify subscription/license state at application startup when possible.

## FR-SUB-002
The product shall verify subscription/license state when Internet connectivity returns.

## FR-SUB-003
The product shall perform periodic subscription verification while online.

## FR-SUB-004
The product shall perform at least one verification per day while continuously online.

## FR-SUB-005
After successful verification with more than 10 days remaining, the offline-authority window shall be at most 72 hours.

## FR-SUB-006
After successful verification with 10 days or less remaining, the offline-authority window shall equal remaining subscription time plus 10 hours grace.

## FR-SUB-007
When offline authority expires without successful renewal, the system shall enter `LicenseExpired`.

## FR-SUB-008
When central suspension is successfully received, the system shall enter `SubscriptionSuspended`.

## FR-SUB-009
`LicenseExpired` and `SubscriptionSuspended` shall block operational use according to subscription policy.

## FR-SUB-010
Subscription lock shall not delete business data.

## FR-SUB-011
The system shall detect/handle suspicious local-clock rollback so it cannot extend the offline authority period.

---

# 28. System States

## FR-STATE-001
The system shall distinguish Operational, OfflineOperational, RestrictedOffline, SecurityLocked, SubscriptionSuspended, LicenseExpired, and Maintenance.

## FR-STATE-002
The UI shall communicate the current blocking/restricted system state.

## FR-STATE-003
Security recovery shall not automatically clear subscription/license states.

---

# 29. Audit

## FR-AUDIT-001
The system shall create audit evidence for defined sensitive actions.

## FR-AUDIT-002
Audit evidence shall identify relevant actor and branch when applicable.

## FR-AUDIT-003
Audit evidence shall include timestamp and action.

## FR-AUDIT-004
Audit evidence shall include affected record/reference and reason/before-after context where required.

## FR-AUDIT-005
The system shall not intentionally write plaintext PINs or private security secrets into audit output.

---

# 30. Crash / Restart Recovery

## FR-REC-001
The system shall persist the start of an active session.

## FR-REC-002
The system shall persist Pause and Resume events.

## FR-REC-003
The system shall persist session completion.

## FR-REC-004
The system shall not require saving the displayed timer every second.

## FR-REC-005
After application restart, the system shall identify persisted active/paused sessions.

## FR-REC-006
The system shall reconstruct displayed duration/state from persisted timestamps/events.

## FR-REC-007
A recovered active session shall retain its original start time rather than restarting at zero.

---

# 31. Backup / Recovery Support

## FR-BACKUP-001
The R1 operational design shall support automatic local database backup approximately every 30 minutes.

## FR-BACKUP-002
The branch product shall not require an external SSD, NAS, or UPS as a mandatory prerequisite.

## FR-BACKUP-003
Central backup/restore design shall support approved RPO/RTO targets.

---

# 32. Explicitly Excluded Functions

```text
Customer Account CRUD
Customer History
Booking CRUD
Booking Deposit
Customer Login
Customer Website
Mobile Application
Food/Drink/Product Sales
Membership
Discounts in current R1 direction
Advanced Asset Transfer
Advanced Accounting
```

---

# 33. Functional Decisions Closed for UX / Remaining Design Detail

The former `OFR-001` through `OFR-005` UX-blocking business decisions are resolved in this requirements baseline:

```text
OFR-001 → handover active sessions before shift close.
OFR-002 → Cancel/Void reason is optional.
OFR-003 → minimum asset quantity scope is defined.
OFR-004 → monthly review is read-only.
OFR-005 → offline does not elevate permissions.
```

Remaining later technical design detail: determine which protected operations require live central verification in `RestrictedOffline`.

---

# 34. Placement

Recommended filename:

```text
FUNCTIONAL_REQUIREMENTS_R1.md
```

Suggested later folder:

```text
docs/02_Requirements/
```

---

# 35. Next

This file is already paired with `NON_FUNCTIONAL_REQUIREMENTS_R1.md`, `USER_STORIES_R1.md`, and `ACCEPTANCE_CRITERIA_R1.md`.

Current next step:

```text
RBAC / IA / UX Flows   ✅
        ↓
74-screen UI correction   ⏭️ NEXT
        ↓
UI Review + Design System
        ↓
System Analysis
```
