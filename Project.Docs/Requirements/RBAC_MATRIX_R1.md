# +90 PS — Release 1 RBAC Matrix

**Document Type:** Role-Based Access Control / UX Permission Baseline  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Baseline  
**UI Baseline:** `+90PS_UI.zip` — 37 English screens + 37 Arabic screens  
**Last Updated:** 2026-09-10

## 1. Purpose

This document defines the Release 1 business/UX permission boundaries for the three official application roles:

- Owner
- Manager
- Cashier

It exists to prevent the UI from treating a Cashier as a Manager/Owner and to give the UI, later Security Design, API authorization, and testing one consistent permission baseline.

Hiding a button is not a security boundary. The final application/API must enforce the same permission rules.

## 2. Core Role Model

### Cashier

The Cashier is the primary day-to-day session operator.

Core responsibilities:

- Login with a personal PIN.
- View console availability.
- Start eligible gaming sessions.
- Choose Single / Multi.
- Choose the approved billing/session option.
- Pause / Resume.
- Complete sessions.
- Create the resulting invoice.
- Record cash payment.
- View operational session/invoice history needed for the job.
- View the Cashier's own current-shift information.
- Switch user / logout.

The Cashier must not receive Manager/Owner administration merely because the same desktop application is being used.

### Manager

The Manager is the branch-administration role.

Core responsibilities:

- Manage branch consoles/devices.
- Manage branch pricing.
- Manage employees according to permission.
- Reset employee PINs where authorized.
- Manage employee shifts.
- Create/manage expenses.
- Manage basic branch assets/equipment quantities.
- View branch reports.
- Authorize protected branch actions.
- Cancel/Void invoices through a protected flow.
- Approve/perform session responsibility transfer.

### Owner

The Owner has ownership-level access within the authorized ownership scope.

Core responsibilities:

- Perform authorized Manager-level administration.
- View authorized business/branch information.
- Manage higher-level access where allowed.
- Review reports across the authorized ownership scope.
- Perform supported security/subscription/recovery actions.
- Access protected Owner-level operations.

Owner access must still respect branch/ownership isolation.

## 3. Permission Legend

| Symbol | Meaning |
|---|---|
| ✅ | Allowed by the current R1 baseline |
| 👁 | View-only / limited visibility |
| 🔐 | Protected action; explicit authorization required |
| ❌ | Not granted to this role by default |
| — | Not a normal function for this role |

## 4. R1 Capability Matrix

| Capability | Cashier | Manager | Owner | Notes |
|---|:---:|:---:|:---:|---|
| Login with personal PIN | ✅ | ✅ | ✅ | Personal application identity |
| Switch user / logout | ✅ | ✅ | ✅ | Shared branch PC |
| View dashboard / console availability | ✅ | ✅ | ✅ | Content/actions remain role-specific |
| View live sessions | ✅ | 👁 | 👁 | Manager/Owner may supervise; operational controls stay permission-based |
| Start session | ✅ | — | — | Primary Cashier operation; do not assume inheritance unless explicitly granted later |
| Choose Single / Multi | ✅ | — | — | Session-start flow |
| Choose Open Time / Hours / Match | ✅ | — | — | UI billing/session selection |
| Pause / Resume session | ✅ | — | — | Cashier operation |
| Complete session | ✅ | — | — | Cashier operation |
| Create invoice from valid completion | ✅ | — | — | Part of Cashier completion flow |
| Record cash payment | ✅ | — | — | R1 payment method = Cash |
| View session history | ✅ | ✅ | ✅ | Scope follows branch/ownership authorization |
| View cancelled/void-related history | ✅ | ✅ | ✅ | Historical records are not deleted |
| View invoices | ✅ | ✅ | ✅ | Scope follows authorization |
| Cancel/Void invoice | ❌ | 🔐 | 🔐 | Reason is optional |
| Physically delete financial invoice history | ❌ | ❌ | ❌ | Non-destructive financial history |
| Request session transfer/handover | ✅ | ✅ | ✅ | Approval/perform step is protected |
| Approve/perform session responsibility transfer | ❌ | 🔐 | 🔐 | Preserve original opener |
| View own current shift | ✅ | ✅ | ✅ | Cashier limited to own operational shift view |
| Start shift for employee | ❌ | ✅ | ✅ | Manager/Owner selects employee |
| End shift for employee | ❌ | ✅ | ✅ | Active sessions must be handed over first |
| View shift history / all employees | ❌ | ✅ | ✅ | Separate from Employee Management |
| Add/Edit/Activate/Deactivate console | ❌ | ✅ | ✅ | Branch administration |
| Change pricing | ❌ | ✅ | ✅ | Cashier must not change pricing |
| Manage employees | ❌ | ✅ | ✅ | According to branch/ownership permission |
| Reset employee PIN | ❌ | 🔐 | 🔐 | Old readable PIN is never shown |
| Edit role/permission configuration | ❌ | 🔐 | 🔐 | High-sensitivity administration |
| Manage asset/equipment quantities | ❌ | ✅ | ✅ | Internal equipment, not product stock |
| Create/manage expenses | ❌ | ✅ | ✅ | Cashier cannot create expenses |
| View reports | ❌ | ✅ | ✅ | Manager = branch scope; Owner = authorized ownership scope |
| Monthly review | ❌ | 👁 | ✅ | Review only; no financial-state mutation |
| Change branch name from normal Settings | ❌ | ❌ | ❌ | Branch name is read-only in normal user Settings |
| Change normal user-facing language | ✅ | ✅ | ✅ | English/Arabic parity |
| Edit security-policy constants from Settings | ❌ | ❌ | ❌ | Security policy is not a normal editable Settings page |
| SecurityLocked recovery | ❌ | — | 🔐 | Authorized recovery flow |
| Subscription/entitlement administration | ❌ | ❌ | 🔐 | Ownership/platform-sensitive |
| Operate offline within existing permission | ✅ | ✅ | ✅ | Offline never elevates permissions |

## 5. Session Responsibility / Shift Handover

When a Cashier with active sessions needs to leave or a Manager/Owner ends that Cashier's shift:

```text
Cashier A has active sessions
        ↓
Manager/Owner selects End Shift / Handover
        ↓
Select receiving Cashier B
        ↓
Transfer active session responsibility
        ↓
OpenedBy remains Cashier A
CurrentResponsibleUser becomes Cashier B
        ↓
Record handover/audit
        ↓
Close Cashier A shift
```

The remaining operational responsibility after the handover is attributed to the receiving Cashier.

## 6. Invoice Cancel/Void

```text
Cashier direct Cancel/Void          ❌
Authorized Manager/Owner            ✅
Reason field                        Optional
Physical delete                     ❌
Historical trace                    Required
```

If a reason is entered, it is preserved with the cancellation/audit information. An empty reason does not by itself block an otherwise authorized Cancel/Void.

## 7. Offline Permission Rule

Offline mode must keep the same role boundary:

```text
Cashier offline → Cashier permissions only
Manager offline → Manager permissions only
Owner offline   → Owner permissions only
```

Offline mode must never be used to bypass role restrictions.

## 8. Current UI Screen Access Baseline

This table maps the current `+90PS_UI.zip` screen set to the intended R1 role boundary. It does not mean the current screenshots are already fully permission-correct.

| # | Screen | Cashier | Manager | Owner | Permission note |
|---:|---|:---:|:---:|:---:|---|
| 01 | Login | ✅ | ✅ | ✅ | PIN entry |
| 02 | Login Lockout | ✅ | ✅ | ✅ | Security state |
| 03 | Dashboard | ✅ | ✅ | ✅ | Role-specific actions |
| 04 | Sessions Live | ✅ | 👁 | 👁 | Cashier operates; admin roles may supervise |
| 05 | Session Start | ✅ | — | — | Cashier operation |
| 06 | Session Active Details | ✅ | 👁 | 👁 | Operational actions permission-based |
| 07 | Session Transfer | ❌ | 🔐 | 🔐 | Protected approval/perform step |
| 08 | Session End Invoice | ✅ | — | — | Cashier completion/payment flow |
| 09 | Session Fixed Time Alert | ✅ | 👁 | 👁 | No automatic stop |
| 10 | Session History | ✅ | ✅ | ✅ | Include cancelled/void-related history |
| 11 | Invoices All | ✅ | ✅ | ✅ | Scope-limited |
| 12 | Invoice Details | ✅ | ✅ | ✅ | Cancel action hidden from Cashier |
| 13 | Invoice Cancel | ❌ | 🔐 | 🔐 | Optional reason |
| 14 | Invoices Cancelled | ✅ | ✅ | ✅ | Historical visibility |
| 15 | Shift Current | 👁 | ✅ | ✅ | Cashier sees own shift only |
| 16 | Shift Start | ❌ | ✅ | ✅ | Start shift for selected employee |
| 17 | Shift End | ❌ | ✅ | ✅ | End selected employee shift |
| 18 | Shift History | ❌ | ✅ | ✅ | All authorized employees/shifts |
| 19 | Management Devices | ❌ | ✅ | ✅ | Administration |
| 20 | Device Add/Edit | ❌ | ✅ | ✅ | Administration |
| 21 | Device Delete/Deactivate Confirm | ❌ | 🔐 | 🔐 | Prefer deactivate where appropriate |
| 22 | Management Pricing | ❌ | ✅ | ✅ | Administration |
| 23 | Management Employees | ❌ | ✅ | ✅ | Keep separate from Shift employee list |
| 24 | Employee Add/Edit | ❌ | ✅ | ✅ | Administration |
| 25 | Roles & Permissions | ❌ | 🔐 | 🔐 | High-sensitivity |
| 26 | Management Assets | ❌ | ✅ | ✅ | Quantity-based internal assets |
| 27 | Asset Add/Edit | ❌ | ✅ | ✅ | Quantity-based |
| 28 | Management Expenses | ❌ | ✅ | ✅ | Administration |
| 29 | Expense Add/Edit | ❌ | ✅ | ✅ | Administration |
| 30 | Reports Statistics | ❌ | ✅ | ✅ | Statistics mode |
| 31 | Reports Details | ❌ | ✅ | ✅ | Details mode |
| 32 | Reports Revenue | ❌ | ✅ | ✅ | Period/branch scoped |
| 33 | Reports Expenses | ❌ | ✅ | ✅ | Period/branch scoped |
| 34 | Reports Shifts | ❌ | ✅ | ✅ | Period/branch scoped |
| 35 | Settings General | ✅ | ✅ | ✅ | User-facing only; branch name read-only |
| 36 | Settings Security | ❌ | ❌ | ❌ | Must not be a normal user-facing editable page |
| 37 | Settings Language | ✅ | ✅ | ✅ | English/Arabic selection |

## 9. UI Alignment Rules for the Next UI Pass

The final UI must:

- Never expose all management pages/actions merely because a Cashier is logged in.
- Keep Management, Expenses, Pricing, Employees, Assets, Reports, protected Shift administration, and Roles/Permissions behind the correct role/permission.
- Keep Invoice Cancel/Void protected.
- Keep the Cancel/Void reason optional.
- Keep Screen 36 security policy outside normal editable Settings.
- Keep normal Settings simple and user-facing.
- Keep Arabic and English role behavior identical.
- Enforce the same permission rules even if a screen is reached through Back navigation or a direct internal route.

## 10. Source Documents

This RBAC baseline is aligned with the current R1 documentation set:

- `BUSINESS_RULES_R1.md`
- `BRD_R1.md`
- `PRD_R1.md`
- `FUNCTIONAL_REQUIREMENTS_R1.md`
- `NON_FUNCTIONAL_REQUIREMENTS_R1.md`
- `USER_STORIES_R1.md`
- `ACCEPTANCE_CRITERIA_R1.md`
- `TO_BE_PROCESS.md`
- Current user-approved decisions
- Current `+90PS_UI.zip` screen baseline
