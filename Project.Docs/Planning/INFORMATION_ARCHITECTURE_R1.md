# +90 PS — Release 1 Information Architecture

**Document Type:** Information Architecture / Role-Based Navigation Specification  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code UI/UX Baseline  
**UI Baseline:** `+90PS_UI.zip` — 37 English screens + 37 Arabic screens  
**Last Updated:** 2026-09-10

## 1. Purpose

This document defines how Release 1 information and screens are organized inside the desktop application.

It answers:

- What are the main navigation areas?
- Which role sees which area?
- Which screens belong together?
- What is a root page versus a modal/detail/action page?
- How should the English and Arabic versions mirror each other?
- Which current ZIP screens are not part of the approved final user-facing architecture?

This document does not define database tables, API routes, or implementation classes.

## 2. Global Navigation Model

The application is one product with role-aware navigation.

Approved R1 top-level areas:

```text
Dashboard
Sessions
Invoices
Shift
Management
Reports
Settings
```

`Management` contains the administrative areas that must not appear as normal Cashier capabilities.

### Management children

```text
Devices / Consoles
Pricing
Employees
Roles & Permissions
Assets
Expenses
```

There is no normal R1 navigation for:

```text
Customers
Customer Accounts
Customer History
Bookings
Food / Drinks / Cafeteria
Product Sales
Membership
Discount Management
Technical Sync Center
API / Database / Outbox Administration
```

## 3. Cashier Information Architecture

Cashier navigation should be operational and short.

```text
Dashboard
Sessions
  ├── Live Sessions
  └── Session History
Invoices
  ├── All / Relevant Invoices
  └── Invoice Details
Shift
  └── My Current Shift (view only where applicable)
Settings
  ├── General user-facing settings
  └── Language
```

The Cashier must not receive normal navigation to:

- Device management.
- Pricing management.
- Employee management.
- Roles & Permissions.
- Asset management.
- Expense management.
- Full Shift administration.
- Reports.
- Security-policy editing.
- Owner-level subscription/recovery functions.

## 4. Manager Information Architecture

Manager navigation is branch-administrative.

```text
Dashboard
Sessions
  ├── Live Sessions
  └── Session History
Invoices
  ├── All Invoices
  ├── Invoice Details
  └── Cancelled Invoices
Shift
  ├── Current / Employee Shifts
  ├── Start Shift
  ├── End Shift
  └── Shift History
Management
  ├── Devices
  ├── Pricing
  ├── Employees
  ├── Roles & Permissions (only if permission allows)
  ├── Assets
  └── Expenses
Reports
  ├── Statistics
  ├── Details
  ├── Revenue
  ├── Expenses
  └── Shifts
Settings
  ├── General user-facing settings
  └── Language
```

The Manager remains branch-scoped.

## 5. Owner Information Architecture

Owner may use the Manager areas where authorized and also has ownership-level functions.

```text
Dashboard / Authorized Business Overview
Sessions / Invoices / Shift
Management
Reports
Settings
Ownership-level Security Recovery
Subscription / Entitlement Administration
```

The current 37-screen UI pack does not yet provide complete dedicated Owner-level subscription/recovery screens. Those requirements must be covered before the R1 UI is declared complete.

## 6. Dashboard Architecture

### Screen 03 — Dashboard

The Dashboard is the operational console grid and must remain the fastest path to session start.

For an available device, the main action is:

```text
START
```

The device card must not make the user choose `Single` or `Multi` directly on the card.

Expected flow:

```text
Available Device
      ↓
START
      ↓
Screen 05 — Session Start
```

For an active device, the card may expose only valid active-session actions.

`Add Order` is not part of R1 because food/product ordering is outside scope.

## 7. Session Start Information Structure

### Screen 05 — Session Start

The session-start form should contain:

```text
Device
Mode
Billing
Conditional Duration
```

### Mode

```text
Single
Multi
```

### Billing dropdown

```text
Open Time
Hours
Match
```

Behavior:

- `Open Time` = Hourly/Open session.
- `Hours` = Hourly/Fixed intended duration.
- `Match` = Match pricing.
- If `Hours` is selected, a duration field appears below Billing so the Cashier can enter the intended number of hours (for example 2 or 3).
- Match must not be calculated from elapsed time.
- Fixed Hours expiry alerts the employee; it does not force automatic stop.

## 8. Session Area

```text
Sessions
  ├── Screen 04 — Live Sessions
  ├── Screen 05 — Session Start
  ├── Screen 06 — Active Session Details
  ├── Screen 07 — Session Transfer
  ├── Screen 08 — End / Invoice
  ├── Screen 09 — Fixed Time Alert
  └── Screen 10 — Session History
```

Session History must preserve and expose relevant cancelled/void-related historical records instead of making them disappear.

## 9. Invoice Area

```text
Invoices
  ├── Screen 11 — All Invoices
  ├── Screen 12 — Invoice Details
  ├── Screen 13 — Cancel/Void
  └── Screen 14 — Cancelled Invoices
```

Rules:

- Cashier may view invoices needed for normal operation.
- Cancel/Void is protected for authorized Manager/Owner.
- Reason is optional.
- Cancelled/Voided records stay historically visible.
- There is no unrestricted destructive financial delete.

## 10. Shift Area

Shift administration is separate from Employee Management.

### Employee Management

`Management > Employees` manages employee records.

### Shift

`Shift` manages operational work periods.

The Shift area for Manager/Owner must be able to display the employees relevant to the branch and allow:

```text
Start Shift for selected employee
End Shift for selected employee
View current/open shifts
View shift history
```

If an employee being ended still owns active sessions:

```text
End Shift
   ↓
Active Sessions?
   ↓ Yes
Select receiving Cashier
   ↓
Transfer responsibility
   ↓
Close Shift
```

The Cashier may see the Cashier's own current-shift information but must not receive full shift administration.

## 11. Management Area

### Devices

```text
19 Management Devices
20 Device Add/Edit
21 Device Delete/Deactivate Confirm
```

### Pricing

```text
22 Management Pricing
```

### Employees

```text
23 Management Employees
24 Employee Add/Edit
```

This remains separate from the Shift employee list.

### Roles & Permissions

```text
25 Roles & Permissions
```

This is a protected administration area and must not appear as a Cashier capability.

### Assets

```text
26 Management Assets
27 Asset Add/Edit
```

Asset tracking is internal branch equipment/quantity tracking, not product-sales inventory.

### Expenses

```text
28 Management Expenses
29 Expense Add/Edit
```

Cashier must not create/manage expenses.

## 12. Reports Area

Current report screens:

```text
30 Reports Statistics
31 Reports Details
32 Reports Revenue
33 Reports Expenses
34 Reports Shifts
```

The Reports experience must support:

```text
Daily
Weekly
Monthly
Yearly
```

And the explicit display mode:

```text
Statistics
Details
```

Core business views include Revenue, Expenses, Profit, and Shift reporting as defined by R1 requirements.

Monthly Review is review-only. It does not close or mutate financial records.

## 13. Settings Area

Settings must contain only normal user-facing options.

### Screen 35 — General Settings

- Branch name is displayed read-only.
- Business/security policy constants must not be editable here.
- Keep only reasonable user-facing settings.

### Screen 36 — Settings Security

This screen exists in the current ZIP baseline, but it is not part of the approved final normal user-facing information architecture.

Security thresholds/policies such as PIN-attempt limits and lock timings must not be editable through an ordinary Settings page.

### Screen 37 — Language

Language selection is valid user-facing Settings.

English and Arabic must provide the same functionality with LTR/RTL mirroring.

## 14. Global Back Navigation

Every non-root detail/action/modal flow must provide a clear Back/Close/Cancel path to the immediately previous valid screen.

Examples:

```text
Invoice Details → Back → Invoices
Session History → Back → Sessions
Device Add/Edit → Back → Device Management
Report Details → Back → Reports
Settings Language → Back → Settings
```

Back navigation must not bypass permission checks.

## 15. Current Screen Inventory

| # | Screen | Area | Final IA Status |
|---:|---|---|---|
| 01 | Login | Authentication | Keep |
| 02 | Login Lockout | Authentication | Keep |
| 03 | Dashboard | Dashboard | Keep; revise actions |
| 04 | Sessions Live | Sessions | Keep |
| 05 | Session Start | Sessions | Keep; revise Billing flow |
| 06 | Session Active Details | Sessions | Keep |
| 07 | Session Transfer | Sessions | Keep; protected |
| 08 | Session End Invoice | Sessions/Invoices | Keep |
| 09 | Session Fixed Time Alert | Sessions | Keep |
| 10 | Session History | Sessions | Keep; include cancelled history |
| 11 | Invoices All | Invoices | Keep |
| 12 | Invoice Details | Invoices | Keep |
| 13 | Invoice Cancel | Invoices | Keep; protected; optional reason |
| 14 | Invoices Cancelled | Invoices | Keep |
| 15 | Shift Current | Shift | Keep; role-specific |
| 16 | Shift Start | Shift | Keep; Manager/Owner |
| 17 | Shift End | Shift | Keep; Manager/Owner |
| 18 | Shift History | Shift | Keep; Manager/Owner |
| 19 | Management Devices | Management | Keep |
| 20 | Device Add/Edit | Management | Keep |
| 21 | Device Delete Confirm | Management | Keep; review deactivate semantics later |
| 22 | Management Pricing | Management | Keep |
| 23 | Management Employees | Management | Keep |
| 24 | Employee Add/Edit | Management | Keep |
| 25 | Roles & Permissions | Management | Keep; protected |
| 26 | Management Assets | Management | Keep |
| 27 | Asset Add/Edit | Management | Keep |
| 28 | Management Expenses | Management | Keep |
| 29 | Expense Add/Edit | Management | Keep |
| 30 | Reports Statistics | Reports | Keep |
| 31 | Reports Details | Reports | Keep |
| 32 | Reports Revenue | Reports | Keep |
| 33 | Reports Expenses | Reports | Keep |
| 34 | Reports Shifts | Reports | Keep |
| 35 | Settings General | Settings | Keep; branch name read-only |
| 36 | Settings Security | Settings | Remove from normal final UI |
| 37 | Settings Language | Settings | Keep |

## 16. Arabic / English Parity

The EN and AR folders are one product, not two separate feature sets.

Requirements:

- Same screen coverage.
- Same permission rules.
- Same actions.
- Same validation.
- Same state transitions.
- Same navigation meaning.
- Arabic mirrors the layout RTL.
- English remains LTR.

## 17. UI Alignment Notes Before Final UI Approval

The current ZIP is the visual baseline, but the next UI correction pass must align it with this IA:

- Dashboard: remove `Add Order`.
- Dashboard available-device action: use `Start`, not direct `Single/Multi`.
- Session Start: Billing dropdown = Open Time / Hours / Match; Hours reveals duration input.
- Keep Match visible.
- Add Back navigation throughout.
- Session History: preserve/show cancelled history.
- Shift: Manager/Owner can start/end shifts for selected employees; keep Employees Management separate.
- Cashier must not receive Manager/Owner administration.
- Screen 35 branch name must not be editable.
- Screen 36 must not remain as normal editable user Settings.
- Keep the existing design language; IA changes behavior/structure, not the visual identity.
