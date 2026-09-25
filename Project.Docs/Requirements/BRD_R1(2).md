# +90 PS — Release 1 Business Requirements Document (BRD)

**Document Type:** Business Requirements Document  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Draft Baseline  
**Last Updated:** 2026-09-09
**Primary Inputs:** AS-IS, TO-BE Process, Release 1 Business Rules  
**Purpose:** Define what the business needs Release 1 to achieve before product, UX, architecture, database, API, security, and implementation details are finalized.

---

# 1. Executive Summary

+90 PS Release 1 is the first operational version of a management system for PlayStation gaming shops.

The business problem being solved is the heavy dependence on manual session tracking, manual timing, manual pricing, manual arithmetic, and disconnected operational records.

Release 1 must provide a dependable daily operational system that allows a real branch to:

```text
Authenticate Staff
      ↓
See Console Availability
      ↓
Start / Manage Gaming Sessions
      ↓
Calculate Charges Correctly
      ↓
Create Invoice
      ↓
Record Cash Payment
      ↓
Record Expenses
      ↓
Produce Operational Reports
      ↓
Continue Core Operation Offline
      ↓
Synchronize When Connectivity Returns
```

Release 1 is deliberately not the full final product. Customer accounts, customer history, full booking, customer-facing web/mobile, advanced transfers, advanced accounting, and other later capabilities remain outside this release.

---

# 2. BRD Purpose

This BRD answers:

> What must Release 1 achieve for the business?

It does not answer:

```text
Which database tables exist?
Which API routes exist?
Which C# classes exist?
Which WPF controls exist?
Which exact cryptographic algorithms are used?
```

Those belong to later artifacts.

The required chain is:

```text
AS-IS
  ↓
TO-BE
  ↓
Business Rules
  ↓
BRD  ← this file
  ↓
PRD
  ↓
Functional / Non-Functional Requirements
  ↓
UX / System Analysis
  ↓
Architecture / Data / API / Security
  ↓
Testing / Implementation
```

---

# 3. Business Background

The PlayStation shop operation depends on time-sensitive, money-sensitive activities.

A typical customer visit includes:

```text
Customer Arrives
      ↓
Employee Finds Available Console
      ↓
Selects Device / Mode / Pricing Method
      ↓
Starts Play
      ↓
Tracks Time / Match
      ↓
Customer Finishes
      ↓
Employee Calculates Charge
      ↓
Cash Is Collected
```

In the current manual process, employee attention and manual calculations create avoidable operational risk.

Release 1 exists to turn the core process into a structured, persistent, auditable workflow.

---

# 4. Business Problem Statement

The current operating model creates the following core problems:

```text
Manual Session Tracking
+
Manual Time Tracking
+
Manual Arithmetic
+
Manual Pricing Lookup
+
Weak Recovery After Unexpected Interruption
+
Limited Central Visibility
+
No Reliable Offline/Sync Product Behavior
```

These problems can cause:

- Billing mistakes.
- Lost or unclear session state.
- Difficulty knowing which console is available.
- Weak accountability for who opened/changed an operation.
- Difficulty producing reliable revenue/expense/profit reporting.
- Operational interruption when Internet is unavailable if the product is designed online-only.
- Data inconsistency if offline and online behavior are implemented as unrelated workflows.

---

# 5. Business Vision

The Release 1 vision is:

> A real PlayStation shop can depend on +90 PS for its core daily operation, including sessions, billing, cash records, expenses, basic reporting, offline continuation, recovery, and synchronization.

The longer-term product should be able to evolve toward multiple branches and additional clients without forcing Release 1 to include every future feature.

---

# 6. Release 1 Business Goals

## BG-001 — Reduce Manual Session Management

Move the core session lifecycle from manual tracking into a structured system workflow.

## BG-002 — Reduce Billing Errors

Automate the approved timing and money calculation rules.

## BG-003 — Make Console Availability Visible

Employees must be able to determine which consoles are available for a new session.

## BG-004 — Improve Operational Accountability

The system must preserve who performed important operations.

## BG-005 — Create Reliable Financial Records

Valid session completion, invoice, and cash payment information must become persistent business records.

## BG-006 — Support Operational Reporting

The business must be able to review revenue, expenses, and profit over required periods.

## BG-007 — Continue Core Operation Without Internet

Internet failure must not force the branch back to a separate manual process for approved core operations.

## BG-008 — Recover After Interruption

An unexpected application or power interruption must not reset a persisted active session to zero.

## BG-009 — Protect Sensitive Actions

Administrative, financial, security, and subscription-sensitive actions require controlled authorization and audit behavior.

## BG-010 — Support Product Growth

Release 1 must establish a business foundation that can evolve into later releases without requiring the business model to be reinvented.

---

# 7. Stakeholders

## 7.1 Owner

Business interests:

- Reliable branch operation.
- Financial visibility.
- Control over branch access and authorized management.
- Subscription/commercial control.
- Recovery and support ability.
- Future multi-branch visibility.

## 7.2 Manager

Business interests:

- Operate/manage the branch.
- Manage consoles and pricing.
- Manage users according to permission.
- Start/end shifts.
- Create expenses.
- View branch reports.
- Authorize protected actions.

## 7.3 Cashier

Business interests:

- Fast login.
- Fast console selection.
- Fast session start.
- Accurate timing.
- Simple pause/resume/complete workflow.
- Correct final amount.
- Clear local/offline/save status.

## 7.4 Customer

The customer is not an authenticated Release 1 system user.

Customer interests include:

- Correct billing.
- Fast service.
- Clear handling of pause/fixed/match behavior.

## 7.5 System Operator / Engineer

Operational interests include:

- Security recovery.
- Support.
- Subscription/entitlement administration where applicable.
- Central system health/recovery.

---

# 8. Release 1 Scope — In Scope

Release 1 includes the business capability to support:

```text
Staff Authentication / PIN
Owner / Manager / Cashier Roles
Branch Context and Isolation
Console Management
PS4 / PS5
Single / Multi
Hourly Pricing
Match Pricing
Open Sessions
Fixed Sessions
Pause / Resume
Session Completion
Pricing Snapshot
Time Calculation
Money Calculation / Rounding
Invoices
Cash Payments
Basic Shifts
Expenses
Basic Asset/Equipment Quantity Tracking where retained
Daily / Weekly / Monthly / Yearly Reporting
Revenue / Expenses / Profit
Offline Core Operation
Synchronization
Security Lock / Recovery
Subscription / Offline License Enforcement
Audit of Sensitive Actions
Backup / Recovery Requirements
```

---

# 9. Release 1 Scope — Out of Scope

Release 1 excludes:

```text
Customer Accounts
Customer History
Full Booking / Reservation System
Booking Deposits
Customer Login
Customer Website
Mobile Application
Public Website as R1 Client
Food Sales
Drink Sales
Cafeteria
General Product Sales
Printer Integration
Barcode Integration
Dedicated POS Hardware Integration
Membership
Advanced Inter-Branch Transfers
Advanced Accounting Integration
Advanced Customer Analytics
```

Discounts are currently deferred from Release 1.

---

# 10. Business Operating Model

The normal Release 1 branch day is expected to follow this business pattern:

```text
Manager/Owner Starts Shift
        ↓
Employees Authenticate Individually
        ↓
Console Availability Is Visible
        ↓
Customer Requests Service
        ↓
Cashier Selects Console / Mode / Pricing
        ↓
Session Starts
        ↓
Session Is Persisted
        ↓
Pause / Resume if Needed
        ↓
Session Completes
        ↓
Charge Is Calculated
        ↓
Invoice Is Created
        ↓
Cash Payment Is Recorded
        ↓
Data Is Available for Reporting
        ↓
Synchronization Occurs in Background
        ↓
Manager/Owner Ends Shift
```

Internet availability must not create a different core business process.

---

# 11. Business Requirements — Authentication and Access

## BRQ-AUTH-001

The business requires every employee using the system to have a distinguishable application identity.

## BRQ-AUTH-002

The business requires personal PIN-based authentication for employees.

## BRQ-AUTH-003

The system must know the active employee identity during operational actions.

## BRQ-AUTH-004

Authentication must support approved offline operation.

## BRQ-AUTH-005

The business must not depend on retrieving readable old PINs from the database.

## BRQ-AUTH-006

Forgotten PIN handling must use authorized reset to a new PIN.

## BRQ-AUTH-007

Repeated invalid PIN attempts must trigger the approved security-lock workflow.

## BRQ-AUTH-008

The business requires controlled recovery of SecurityLocked branches both online and offline.

---

# 12. Business Requirements — Roles and Permissions

## BRQ-RBAC-001

Release 1 requires Owner, Manager, and Cashier business roles.

## BRQ-RBAC-002

Administrative capability may be assigned to an Owner or an authorized Manager; a separate “Admin” role is not required by the business.

## BRQ-RBAC-003

Cashiers must not gain management permissions by changing the UI/client request.

## BRQ-RBAC-004

Branch/ownership access boundaries must apply regardless of which screen or request is used.

## BRQ-RBAC-005

The Release 1 role-permission baseline used by UX is defined in `RBAC_MATRIX_R1.md`. Later Security/RBAC Design must enforce the same business boundaries through trusted authorization controls.

## BRQ-RBAC-006

The Cashier experience must not expose management capabilities as if the Cashier were a Manager or Owner. Administrative screens/actions are shown only to roles/permissions authorized for them.

---

# 13. Business Requirements — Consoles

## BRQ-CONSOLE-001

The business requires consoles to belong to branches.

## BRQ-CONSOLE-002

Release 1 must support PS4 and PS5 console types.

## BRQ-CONSOLE-003

Employees must be able to determine whether a console is available for a new session.

## BRQ-CONSOLE-004

The system must prevent a second simultaneous active session on an already occupied console.

## BRQ-CONSOLE-005

Authorized management must be able to maintain console configuration/status.

---

# 14. Business Requirements — Pricing

## BRQ-PRICE-001

The business requires branch-specific pricing.

## BRQ-PRICE-002

The business requires independent pricing by console type.

## BRQ-PRICE-003

The business requires independent pricing by Single/Multi mode.

## BRQ-PRICE-004

The business requires Hourly and Match pricing methods.

## BRQ-PRICE-005

The system must support independent valid price combinations across PS4/PS5 × Single/Multi × Hourly/Match.

## BRQ-PRICE-006

A session must not start when no valid price exists for the selected combination.

## BRQ-PRICE-007

Cashiers must not be able to change branch pricing.

## BRQ-PRICE-008

Authorized pricing changes must be traceable.

## BRQ-PRICE-009

A running session must preserve the price applicable when it began.

---

# 15. Business Requirements — Session Lifecycle

## BRQ-SESSION-001

Authorized employees must be able to start a session on an available console.

## BRQ-SESSION-002

The system must record who opened the session.

## BRQ-SESSION-003

The system must preserve the selected console, branch, mode, pricing method, and pricing snapshot.

## BRQ-SESSION-004

The system must support Open hourly sessions.

## BRQ-SESSION-005

The system must support Fixed sessions with an intended duration.

## BRQ-SESSION-006

The system must support Match sessions.

## BRQ-SESSION-007

The system must support Pause and Resume.

## BRQ-SESSION-008

Pause time must not be billed.

## BRQ-SESSION-009

Multiple Pause/Resume cycles must remain correctly represented.

## BRQ-SESSION-010

A Fixed session reaching its intended duration must generate a prominent alert and must not automatically force the session to stop.

## BRQ-SESSION-011

Match completion is employee-driven and Match charge is not calculated from elapsed match time.

## BRQ-SESSION-012

Changing Single/Multi during an existing session pricing context is not allowed; the current context must be closed and a new valid context started.

---

# 16. Business Requirements — Time and Billing

## BRQ-TIME-001

Actual timestamps must be retained with enough precision for audit and recovery.

## BRQ-TIME-002

Billing is minute-based even if seconds are stored internally.

## BRQ-TIME-003

Any positive seconds beyond a completed minute move the billable base to the next minute.

Example:

```text
1:17:00 → 1:17
1:17:40 → 1:18
```

## BRQ-TIME-004

The approved quarter-hour rule must be applied after minute conversion.

## BRQ-TIME-005

The quarter-hour rule must not round billing duration downward.

## BRQ-TIME-006

Release 1 has no minimum charge.

---

# 17. Business Requirements — Money Rounding

## BRQ-MONEY-001

The customer-facing final amount must be whole EGP.

## BRQ-MONEY-002

Amounts below 50 piasters remain at the current whole EGP; 50 piasters or more move to the next whole EGP.

## BRQ-MONEY-003

After whole-EGP rounding, an amount may move upward to the next multiple of 5 only when the increase is 1 or 2 EGP.

## BRQ-MONEY-004

The multiple-of-five rule must never reduce the amount.

---

# 18. Business Requirements — Invoice and Cash Payment

## BRQ-FIN-001

A valid completed gaming operation must produce the required invoice record.

## BRQ-FIN-002

Release 1 records cash payments.

## BRQ-FIN-003

Payment must be associated with the relevant invoice/financial operation.

An invoice may be issued before Payment; cash receipt is a separate business step. Release 1 supports Cash only. The cash refund/reversal effect of cancelling a paid invoice remains unresolved.

## BRQ-FIN-004

Financial completion must not be shown as successful before required persistence succeeds.

## BRQ-FIN-005

Retry/synchronization must not duplicate an invoice or cash payment.

## BRQ-FIN-006

Financial records must not be freely destructively deleted.

## BRQ-FIN-007

Protected cancellation/void requires authorization and remains traceable.

## BRQ-FIN-008

A Cancel/Void reason field is optional. An authorized cancellation must not be blocked only because no reason was entered; if a reason is supplied, it is preserved in the audit/history context.

---

# 19. Business Requirements — Shifts

## BRQ-SHIFT-001

An authorized Manager or Owner starts a shift.

## BRQ-SHIFT-002

An authorized Manager or Owner ends a shift.

## BRQ-SHIFT-003

The business requires shift records to preserve branch, actor, start time, and end time where applicable.

## BRQ-SHIFT-004

If active sessions are still assigned to the Cashier whose shift is being closed, an authorized Manager/Owner must transfer those active sessions to another authorized Cashier before the shift is closed.

## BRQ-SHIFT-005

The original session opener remains preserved for history, while current responsibility changes to the receiving Cashier.

## BRQ-SHIFT-006

After handover, the remaining operational/session responsibility is attributed to the receiving Cashier; the handover itself remains auditable.

---

# 20. Business Requirements — Expenses and Assets

## BRQ-EXP-001

Only authorized Manager/Owner users create expenses.

## BRQ-EXP-002

Expenses must be branch-scoped.

## BRQ-EXP-003

Expense records must preserve amount, description/category, actor, timestamp, and branch.

## BRQ-EXP-004

Controller/equipment repair may be represented as an expense with a note rather than requiring a dedicated damage-management workflow.

## BRQ-ASSET-001

Release 1 includes basic internal equipment tracking and does not treat it as product-sales stock.

## BRQ-ASSET-002

The approved minimum model is branch-scoped quantity tracking for Controllers, Accessories, Equipment, and Assets. Consoles remain managed through Console Management.

## BRQ-ASSET-003

Authorized Manager/Owner users can maintain the approved equipment quantity records. A dedicated per-unit damage lifecycle is not required; repair cost can be recorded as an Expense.

## BRQ-ASSET-004

Advanced inter-branch asset transfer is outside Release 1.

---

# 21. Business Requirements — Reporting

## BRQ-REPORT-001

Release 1 must support Daily reporting.

## BRQ-REPORT-002

Release 1 must support Weekly reporting.

## BRQ-REPORT-003

Release 1 must support Monthly reporting.

## BRQ-REPORT-004

Release 1 must support Yearly reporting.

## BRQ-REPORT-005

Release 1 must provide operational Revenue, Expenses, and Profit views.

## BRQ-REPORT-006

Revenue must be derived from valid financial records as operations occur, not created only at month-end.

## BRQ-REPORT-007

The Owner may use an optional monthly review process.

## BRQ-REPORT-008

The Release 1 monthly review is review/reporting only. It does not lock a period or change invoice/payment/financial state.

## BRQ-REPORT-009

Report visibility must respect branch/ownership authorization.

---

# 22. Business Requirements — Offline Operation

## BRQ-OFFLINE-001

Approved core operation must continue without Internet access while valid offline security/subscription authority remains available.

## BRQ-OFFLINE-002

The branch must not return to a separate manual workflow simply because Internet is unavailable.

## BRQ-OFFLINE-003

Core local business success must be based on persistent local save, not temporary UI state.

## BRQ-OFFLINE-004

The business must distinguish local save success from central synchronization success.

## BRQ-OFFLINE-005

Approved offline capability must include the core session → invoice → cash workflow.

## BRQ-OFFLINE-006

Offline operation must preserve the same role/permission boundaries used online; offline mode must not elevate a Cashier, Manager, or Owner.

## BRQ-OFFLINE-007

Security/RBAC Design may require live central verification for specific protected operations. That technical dependency must not be interpreted as a new business role or as permission escalation.

---

# 23. Business Requirements — Synchronization

## BRQ-SYNC-001

Locally committed operations requiring central storage must be synchronized later.

## BRQ-SYNC-002

Synchronization must be retryable.

## BRQ-SYNC-003

Retry must not create duplicate business effects.

## BRQ-SYNC-004

Lost acknowledgement after successful server processing must be safely recoverable.

## BRQ-SYNC-005

Central rejection/conflict must be visible and traceable.

## BRQ-SYNC-006

Conflicts must not silently delete local financial evidence.

---

# 24. Business Requirements — Security Lock

## BRQ-SEC-001

Five failed PIN attempts trigger a 20-second temporary lock, warning sound, and security event.

## BRQ-SEC-002

A second set of five failed attempts after the first lock triggers SecurityLocked.

## BRQ-SEC-003

SecurityLocked blocks normal use but does not delete business data.

## BRQ-SEC-004

Authorized online recovery must be supported.

## BRQ-SEC-005

Authorized offline challenge-response recovery must be supported without a permanent master PIN.

## BRQ-SEC-006

PIN recovery must not bypass subscription/license locks.

---

# 25. Business Requirements — Subscription and Offline License

## BRQ-SUB-001

A shop must not be able to avoid subscription enforcement indefinitely by disconnecting Internet access.

## BRQ-SUB-002

Subscription verification occurs at suitable online opportunities, including startup, reconnection, periodic operation, and at least daily while online.

## BRQ-SUB-003

Successful verification must provide/refresh verifiable offline authority.

## BRQ-SUB-004

If more than 10 paid days remain at successful verification, the maximum offline lease is 72 hours.

## BRQ-SUB-005

If 10 days or less remain, offline allowance equals remaining subscription time + 10 hours payment grace.

## BRQ-SUB-006

When offline authority expires without successful renewal, the system enters LicenseExpired and operational use is locked even while offline.

## BRQ-SUB-007

When central suspension is received, the system enters SubscriptionSuspended.

## BRQ-SUB-008

Subscription/License locks must preserve business data.

## BRQ-SUB-009

Changing Windows time backward must not extend offline subscription authority.

---

# 26. Business Requirements — Recovery and Continuity

## BRQ-REC-001

The system must not rely only on RAM or a visible timer to preserve an active session.

## BRQ-REC-002

Important session events must be persistent.

## BRQ-REC-003

The system does not need to save timer progress every second.

## BRQ-REC-004

After an application crash or power interruption, persisted active sessions must be recoverable without resetting their original start state.

## BRQ-REC-005

Release 1 must support local backup behavior without requiring extra branch hardware as a condition of use.

## BRQ-REC-006

Central backup/recovery planning must support the approved RPO/RTO targets.

Current targets:

```text
Central RPO        ≤ 5 minutes
Branch RTO         ≤ 5 minutes after power/device availability
Central API/DB RTO ≤ 4 hours
```

---

# 27. Business Requirements — Audit

## BRQ-AUDIT-001

Sensitive actions must be traceable.

## BRQ-AUDIT-002

Audit should identify actor, branch, action, time, affected record, and reason/before-after where appropriate.

## BRQ-AUDIT-003

Plain PINs, passwords, and signing/connection secrets must not appear in normal audit output.

---

# 28. Business Information Requirements

Release 1 requires the business process to be able to preserve information such as:

```text
Branch Context
User Identity
Role / Permission Context
Console
Console Type
Session
Session State
Opening Employee
Responsible Employee where applicable
Actual Start / Pause / Resume / Complete Times
Pricing Snapshot
Mode
Pricing Method
Calculated Charge
Invoice
Cash Payment
Shift
Expense
Report Period
Audit Event
Sync Status
Security Lock State
Subscription / Offline License State
```

This is not the ERD. Exact entities, fields, relationships, indexes, keys, and storage mapping will be defined later.

---

# 29. Business Constraints

## BC-001 — Historical Code Baseline

At this document's original pre-code date, Release 1 implementation had not started. This is historical status, not the current repository state: BE-00 through BE-04 now contain tested foundation and Domain code. See [CURRENT_STATE.md](../Reviews/CURRENT_STATE.md); the full Release 1 product is not implemented.

## BC-002 — Desktop-First R1

Release 1 is focused on branch desktop operation. Public web/mobile clients are later scope.

## BC-003 — Cash Payment

Release 1 financial payment scope is cash.

## BC-004 — No Required Extra Branch Hardware

The product must not require purchase of external SSD/NAS/UPS/special backup hardware as a condition for core Release 1 use.

## BC-005 — Offline Is a Core Requirement

Offline operation is not an optional enhancement added after the online system.

## BC-006 — Security and Financial Integrity Are Core

Security, authorization, persistence, and financial integrity must be designed before Release 1 is considered ready for production.

---

# 30. Assumptions

Current working assumptions include:

- A branch has a Windows PC capable of running the Release 1 desktop application.
- Employees use individual application identities/PINs.
- Internet may be unreliable or temporarily unavailable.
- The business needs cash-session operation to continue during approved offline periods.
- Central infrastructure will eventually receive synchronized branch data.
- Later releases will extend rather than replace stable Release 1 behavior.

Assumptions that become false must be changed through the project decision/change process rather than silently implemented around.

---

# 31. Dependencies

Release 1 business success depends on later design/implementation of:

```text
UX/UI suitable for fast cashier operation
Reliable local persistence
Accurate billing logic
Authorization / branch isolation
Synchronization / idempotency
Central availability and recovery
Backup / restore
Audit
Testing / UAT
Deployment / support
```

These are dependencies of the business requirements, not claims that those systems already exist.

---

# 32. Business Risks

## RISK-001 — Billing Error

Incorrect time or money calculation directly affects customers and branch revenue.

Mitigation direction:

```text
Formal Rules
Automated Tests
Acceptance Cases
UAT
```

## RISK-002 — Offline/Sync Data Conflict

Poor offline design may create duplicates, missing data, or conflicting financial state.

Mitigation direction:

```text
Local-first policy
Stable operation identity
Idempotency
Visible conflicts
Recovery tests
```

## RISK-003 — Unauthorized Access

A cashier or unauthorized user may attempt management/financial actions.

Mitigation direction:

```text
Authentication
Authorization
Branch isolation
Audit
Security tests
```

## RISK-004 — Power/Application Interruption

Unexpected shutdown may disrupt active sessions.

Mitigation direction:

```text
Persistent events
Recovery design
Crash/power test scenarios
```

## RISK-005 — Subscription Bypass Through Offline Mode

A shop may intentionally disconnect Internet to avoid subscription enforcement.

Mitigation direction:

```text
Offline lease
Expiry
Trusted verification
Clock rollback protection
```

## RISK-006 — Scope Creep

Adding R2/R3 features to Release 1 can delay operational delivery and increase complexity.

Mitigation direction:

```text
Release boundaries
Change control
Traceability
```

---

# 33. Business Success Criteria

Release 1 satisfies the BRD when a real branch can demonstrate the business journey:

```text
Employee Authenticates
      ↓
Correct Branch Context Is Applied
      ↓
Available Console Is Selected
      ↓
Valid Session Starts
      ↓
Session Persists
      ↓
Pause / Resume Works
      ↓
Session Completes
      ↓
Approved Time Rules Produce Correct Duration
      ↓
Approved Money Rules Produce Correct Amount
      ↓
Invoice Exists
      ↓
Cash Payment Is Recorded
      ↓
Financial Data Appears in Reports
      ↓
Internet Failure Occurs
      ↓
Approved Core Operation Continues
      ↓
Internet Returns
      ↓
Pending Work Synchronizes Without Duplication
      ↓
Application/Power Interruption Is Recoverable
      ↓
Security and Subscription Controls Behave Correctly
```

---

# 34. Business Acceptance Principles

The business will not consider Release 1 acceptable merely because screens open or CRUD operations work.

Release 1 must demonstrate:

```text
Correctness
Reliability
Persistence
Offline Continuity
Financial Integrity
Security
Recoverability
Traceability
Usability for Daily Operation
```

---

# 35. Business Decisions Closed for the UX Gate

The previously open R1 business items needed before UX are now resolved:

```text
OBD-001 Shift Close with Active Sessions
→ Transfer active sessions to another authorized Cashier before closing the shift.

OBD-002 Cancel/Void Reason
→ Reason is optional. Protected authorization and historical traceability remain mandatory.

OBD-003 Basic Asset Scope
→ Branch-scoped quantity tracking for Controllers / Accessories / Equipment / Assets; no product-sales behavior, no detailed damage lifecycle, no advanced transfer.

OBD-004 Monthly Closing
→ Monthly review is review/reporting only and does not mutate financial state.

OBD-005 Offline Permission Meaning
→ Offline does not elevate role permissions. Exact operations that technically require live central verification are deferred to Security/RBAC Design.
```

No unresolved business decision in this list blocks UX flow definition or the next UI correction pass.

---

# 36. Traceability

The next artifacts should trace BRD requirements to the Business Rules.

Example:

```text
BR-TIME-003
      ↓
BRQ-TIME-003
      ↓
PRD Feature
      ↓
Functional Requirement
      ↓
Acceptance Criteria
      ↓
Test Case
```

Traceability must also work in the opposite direction so a future feature can be shown to have a valid business origin.

---

# 37. Non-Goals of This BRD

This BRD does not finalize:

```text
Screen Designs
Wireframes
API Routes
DTOs
Database Tables
ERD
C4
Sequence Diagrams
Encryption Algorithms
PIN Hash Algorithm
Offline License Token Format
Sync Payloads
Retry Backoff
SQL Indexes
Deployment Provider
```

Those are later artifacts.

---

# 38. Placement

Recommended filename:

```text
BRD_R1.md
```

Current flat project layout:

```text
+90PS/
├── DISCOVERY(1).md
├── TO_BE_PROCESS.md
├── BUSINESS_RULES_R1.md
├── BRD_R1.md
├── R1_OPERATIONAL_MVP.md
├── R1_ACCEPTANCE.md
├── DECISIONS.md
└── ...
```

If folders are introduced later without renaming the file:

```text
docs/
└── 02_Requirements/
    ├── BUSINESS_RULES_R1.md
    └── BRD_R1.md
```

---

# 39. Next Step

The BRD chain through Acceptance Criteria is now present. The current UX-preparation sequence is:

```text
BRD / PRD / Requirements / Stories / Acceptance   ✅
RBAC_MATRIX_R1.md                                  ✅
INFORMATION_ARCHITECTURE_R1.md                     ✅
UX_FLOWS_R1.md                                     ✅
      ↓
Correct current 74-screen UI                       ⏭️ NEXT
      ↓
UI_REVIEW_R1.md
      ↓
DESIGN_SYSTEM_R1.md
      ↓
System Analysis
```

No code or runtime implementation is claimed by this document.
