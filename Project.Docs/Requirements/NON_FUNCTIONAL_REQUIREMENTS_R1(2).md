# +90 PS — Release 1 Non-Functional Requirements

**Document Type:** Non-Functional Requirements Specification  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Baseline  
**Last Updated:** 2026-09-09
**Primary Inputs:** `BRD_R1.md`, `PRD_R1.md`, `BUSINESS_RULES_R1.md`  
**Purpose:** Define the quality attributes and operational constraints Release 1 must satisfy.

---

# 1. Requirement Format

Requirements use `NFR-<AREA>-NNN`.

This file avoids inventing numeric performance targets that have not yet been approved. Agreed measurable thresholds are recorded; other thresholds remain TBD.

---

# 2. Reliability

## NFR-REL-001
A successfully committed local business operation shall remain available after normal application restart.

## NFR-REL-002
An active session shall not depend solely on in-memory UI timer state.

## NFR-REL-003
The system shall recover persisted active/paused sessions after restart without resetting their original start time.

## NFR-REL-004
A failed central sync shall not invalidate an already successful local commit solely because Internet connectivity is unavailable.

## NFR-REL-005
Retrying synchronization shall not create duplicate accepted financial effects.

## NFR-REL-006
The system shall prefer visible error/conflict state over silent data loss.

---

# 3. Data Integrity

## NFR-DATA-001
Financial records shall preserve business consistency appropriate to the final database design.

## NFR-DATA-002
The product shall not show a successful financial completion before required local persistence succeeds.

## NFR-DATA-003
Invoice/payment/session identifiers and sync operation identities shall support deduplication/idempotency.

## NFR-DATA-004
A price change shall not retroactively alter the captured price snapshot of a running session.

## NFR-DATA-005
Paused time shall not be counted twice.

## NFR-DATA-006
A console shall not accept two active sessions through concurrent normal operation.

---

# 4. Offline Resilience

## NFR-OFFLINE-001
The branch shall support approved core operation when Internet connectivity is unavailable.

## NFR-OFFLINE-002
Online/offline connectivity shall not cause separate authoritative business-creation models for core branch operations.

## NFR-OFFLINE-003
The UI shall clearly communicate local-save and synchronization states.

## NFR-OFFLINE-004
The product shall tolerate connection loss before, during, or after synchronization.

## NFR-OFFLINE-005
A lost server acknowledgement shall be recoverable without duplicate business effect.

## NFR-OFFLINE-006
Offline operation shall never elevate a user's role or permissions. Any protected action unavailable because current central verification cannot be performed shall fail closed and communicate that limitation clearly.

---

# 5. Recoverability

## NFR-REC-001 — Branch RTO
After branch power/device availability returns, the operational recovery target is:

```text
≤ 5 Minutes
```

## NFR-REC-002 — Central RTO
Current Release 1 target for central API + SQL Server:

```text
≤ 4 Hours
```

## NFR-REC-003 — Central RPO
Current central target:

```text
≤ 5 Minutes
```

## NFR-REC-004
The system shall support recovery testing for branch restart, local persistence, central backup restore, and sync replay scenarios.

## NFR-REC-005
Security/subscription locks shall not destroy operational history.

---

# 6. Backup

## NFR-BACKUP-001
The branch design shall support automatic local SQLite backup approximately every 30 minutes.

## NFR-BACKUP-002
Release 1 shall not require purchase of an external SSD, NAS, or UPS.

## NFR-BACKUP-003
Central backup planning baseline:

```text
Full Backup            → Nightly
Differential Backup    → Every 6 Hours
Transaction Log Backup → Every 5 Minutes
```

## NFR-BACKUP-004
Central backup design should avoid making the live database server the only failure domain protecting recoverability.

---

# 7. Security

## NFR-SEC-001
PINs shall not be stored as readable plaintext.

## NFR-SEC-002
Authentication and authorization shall be distinct controls.

## NFR-SEC-003
Authorization shall be enforced beyond merely hiding UI controls.

## NFR-SEC-004
Branch/ownership isolation shall be enforced by trusted application/backend controls.

## NFR-SEC-005
Sensitive actions shall be auditable.

## NFR-SEC-006
Normal logs/audit output shall not expose plaintext PINs, passwords, private signing keys, or connection secrets.

## NFR-SEC-007
Offline security unlock shall not use a permanent universal master PIN.

## NFR-SEC-008
Offline security recovery shall be one-time/challenge-specific/branch-specific according to later cryptographic design.

## NFR-SEC-009
PIN recovery shall not bypass subscription enforcement.

## NFR-SEC-010
The subscription/offline-license design shall resist simple local editing/tampering.

## NFR-SEC-011
Changing Windows clock backward shall not extend valid offline license authority.

## NFR-SEC-012
Central-client communication carrying sensitive/business data shall use protected transport such as HTTPS in production.

---

# 8. Auditability

## NFR-AUDIT-001
Sensitive actions shall be attributable to a user/actor where applicable.

## NFR-AUDIT-002
Audit records shall carry sufficient context to investigate important business/security changes.

## NFR-AUDIT-003
Audit records shall be append-oriented/non-destructive in product behavior.

## NFR-AUDIT-004
Audit and diagnostic logging shall be treated as separate concerns where appropriate.

---

# 9. Performance

## NFR-PERF-001
Cashier-critical interactions shall be designed for fast daily operation.

Examples:

```text
Login
Console View
Start Session
Pause / Resume
Complete
Invoice / Payment
```

## NFR-PERF-002
The application shall avoid unnecessary per-second database writes for the visible session timer.

## NFR-PERF-003
The displayed timer shall be derived from timestamps/events rather than requiring constant database updates.

## NFR-PERF-004
Synchronization shall operate in the background and shall not unnecessarily block core cashier operation.

## NFR-PERF-005
Report queries shall be designed to remain usable as operational data grows.

## NFR-PERF-006
Numeric response-time targets are TBD until expected branch hardware, dataset size, network conditions, and hosting environment are benchmarked.

No unapproved latency SLA is claimed here.

---

# 10. Usability

## NFR-UX-001
The cashier workflow shall minimize unnecessary steps for frequent operations.

## NFR-UX-002
The UI shall clearly show console availability.

## NFR-UX-003
The UI shall clearly communicate:

```text
Online
Offline
Saved Locally
Pending Sync
Syncing
Synced
Sync Failed / Conflict
```

## NFR-UX-004
The UI shall clearly distinguish:

```text
SecurityLocked
SubscriptionSuspended
LicenseExpired
Maintenance
```

## NFR-UX-005
Fixed-session expiry shall use a prominent alert identifying the affected console/session.

## NFR-UX-006
Error messages shall not falsely imply success.

## NFR-UX-007
The product shall be usable on a shared branch PC by multiple employees without requiring separate Windows accounts.

## NFR-UX-008
The UI shall present role-appropriate navigation and actions so a Cashier is not visually treated as a Manager/Owner. Hidden/disabled presentation is a usability aid only; trusted authorization remains mandatory.

## NFR-UX-009
Where Cancel/Void exposes a reason input, the UI shall make the optional nature of that field clear and shall not imply that a reason is mandatory.

---

# 11. Maintainability

## NFR-MAINT-001
Business rules shall be implemented so they can be tested independently from UI presentation where practical.

## NFR-MAINT-002
The codebase shall separate major concerns sufficiently to support future Web/Mobile clients without duplicating authoritative business rules.

## NFR-MAINT-003
Configuration shall be separated from source code where appropriate.

## NFR-MAINT-004
Secrets shall not be committed into source control.

## NFR-MAINT-005
Database schema evolution shall use controlled versioned migrations after implementation begins.

## NFR-MAINT-006
Release builds shall be versionable and repeatable.

---

# 12. Scalability

## NFR-SCALE-001
The business/domain model shall not assume that the product can only ever have one branch.

## NFR-SCALE-002
Branch-scoped data shall include sufficient branch context for multi-branch growth.

## NFR-SCALE-003
The central API/database design shall consider future growth beyond the first branch.

## NFR-SCALE-004
Future Web/Mobile clients should be able to use the central product boundary without requiring a duplicated backend business model.

## NFR-SCALE-005
No specific 40-branch performance SLA is claimed until infrastructure sizing/load testing is performed.

---

# 13. Availability

## NFR-AVL-001
Loss of Internet connectivity shall not make approved core branch operations unavailable while valid offline authority exists.

## NFR-AVL-002
Loss of the central API shall not automatically erase or invalidate locally committed branch operations.

## NFR-AVL-003
Central unavailability shall be observable through product status/monitoring behavior.

## NFR-AVL-004
The branch shall be able to use local operational data needed for approved offline workflows.

---

# 14. Compatibility

## NFR-COMPAT-001
Release 1 is Windows desktop-first through WPF.

## NFR-COMPAT-002
The exact minimum supported Windows version is TBD before Deployment Design.

## NFR-COMPAT-003
The exact .NET runtime version is finalized during Architecture/Development Foundation.

## NFR-COMPAT-004
The product shall not require a public Website or Mobile client for Release 1 operation.

---

# 15. Observability and Logging

## NFR-OBS-001
Production logging shall support investigation of:

```text
Errors
Warnings
Sync Failures
Database Failures
API Failures
Authentication Failures
Unexpected States
```

## NFR-OBS-002
The central environment shall expose health information for at least API Health, Database Connectivity, and Critical Background Processing.

## NFR-OBS-003
The branch environment shall expose/communicate Local Database Health and Sync Status.

## NFR-OBS-004
Logging shall support trace/correlation identifiers where needed for end-to-end diagnosis.

---

# 16. Testability

## NFR-TEST-001
Billing/time rules shall be testable with deterministic inputs.

## NFR-TEST-002
Money-rounding rules shall be testable independently.

## NFR-TEST-003
Offline/reconnect/lost-ack scenarios shall be testable.

## NFR-TEST-004
PIN lock/recovery behavior shall be testable.

## NFR-TEST-005
Subscription/offline-license expiry behavior shall be testable without relying only on real-time waiting in automated tests.

## NFR-TEST-006
Power/app restart recovery scenarios shall have reproducible tests.

## NFR-TEST-007
Authorization/branch-isolation behavior shall have negative tests.

---

# 17. Data Privacy / Exposure

## NFR-PRIV-001
The system shall collect only operational/staff data required for R1 business functions.

## NFR-PRIV-002
R1 shall not introduce customer-account/history data simply because later releases may need it.

## NFR-PRIV-003
Sensitive credential material shall be minimized and protected.

---

# 18. Deployment and Update Readiness

## NFR-DEPLOY-001
The desktop application shall support repeatable installation/configuration.

## NFR-DEPLOY-002
Branch-specific configuration shall be separable from application binaries where practical.

## NFR-DEPLOY-003
The update strategy shall preserve the ability to roll back/recover from a failed update.

## NFR-DEPLOY-004
The first release does not require a sophisticated global updater if it would block R1, but design must not make future managed updates impossible.

---

# 19. Operational Support

## NFR-OPS-001
Operational runbooks shall be produced before production for at least:

```text
API Down
SQL Server Down
Branch Offline
Sync Failure
SQLite / Local DB Problem
PIN Security Lock
Subscription Lock
Backup Restore
```

## NFR-OPS-002
Support procedures shall not require deleting business data as the default recovery action.

## NFR-OPS-003
The system shall provide enough diagnostic state to distinguish local-save failure from sync failure.

---

# 20. Open Non-Functional Decisions

```text
ONFR-001 Exact supported Windows versions
ONFR-002 Exact .NET runtime baseline
ONFR-003 Hosting provider/topology
ONFR-004 Central backup storage destination
ONFR-005 Numeric cashier-operation latency targets
ONFR-006 Expected branch data volumes / retention
ONFR-007 Exact technical list of protected actions that require live central verification in RestrictedOffline
```

---

# 21. Placement

Recommended filename:

```text
NON_FUNCTIONAL_REQUIREMENTS_R1.md
```

Suggested later folder:

```text
docs/02_Requirements/
```

---

# 22. Next

`USER_STORIES_R1.md` and `ACCEPTANCE_CRITERIA_R1.md` are already present and reference the requirements baseline.

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
