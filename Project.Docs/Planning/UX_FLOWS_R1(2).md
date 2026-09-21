# +90 PS — Release 1 UX Flows

**Document Type:** UX Flow Specification  
**Release:** Release 1 — Operational MVP  
**Status:** Pre-Code Baseline  
**UI Baseline:** `+90PS_UI.zip` — 37 English screens + 37 Arabic screens  
**Last Updated:** 2026-09-10

## 1. Purpose

This document connects the approved Release 1 business behavior to the current UI screen set.

It defines:

- Entry and authentication.
- Cashier session operation.
- Open / Hours / Match selection.
- Pause / Resume / Fixed expiry.
- Session handover.
- Invoice and Cancel/Void behavior.
- Shift administration.
- Management flows.
- Reports.
- Settings.
- Back navigation.
- Arabic/English parity.

## 2. Global UX Rules

1. Cashier must not be presented as Manager/Owner.
2. The active user, branch, and role context must remain clear.
3. Available consoles start through a single `START` action.
4. `Single/Multi` selection belongs in the Session Start flow, not directly on an available-device card.
5. Billing/session choice is made inside Screen 05.
6. `Match` must be available.
7. `Hours` reveals a duration input.
8. Fixed duration expiry alerts; it does not auto-stop.
9. Cancel/Void is protected and its reason is optional.
10. Financial history is preserved.
11. Shift management and Employee Management are separate.
12. Every non-root flow has Back/Close/Cancel navigation.
13. Offline operation never elevates permissions.
14. Arabic and English behavior is identical.

## 3. Authentication Flow

### Primary screens

```text
01 Login
02 Login Lockout
03 Dashboard
```

### Flow

```text
Open +90 PS
    ↓
01 — Login
    ↓
Enter personal Employee PIN
    ↓
Validate identity + branch + role + permission + applicable authority
    ↓
Success?
 ┌──────No────────────────────────┐
 ↓                                │
Failed attempt                    │
 ↓                                │
Threshold reached?                │
 ↓                                │
02 — Login Lockout / security flow│
                                  │
Yes                               │
 ↓                                │
03 — Role-appropriate Dashboard
```

The normal employee login is PIN-based. Windows login is not the application identity.

## 4. Dashboard → Start Session

### Screen

`03_Dashboard`

### Available device behavior

```text
Available Device
      ↓
START
      ↓
05 — Session Start
```

Do not use direct `Single` / `Multi` buttons on the available-device card.

Do not show `Add Order` because customer food/product ordering is outside R1.

## 5. Session Start Flow

### Screen

`05_Session_Start`

### Required fields

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

### Behavior

```text
Billing = Open Time
    ↓
Hourly + Open
    ↓
No intended end time

Billing = Hours
    ↓
Hourly + Fixed
    ↓
Show "Hours" input below Billing
    ↓
Cashier enters intended hours (for example 2 or 3)

Billing = Match
    ↓
Match pricing
    ↓
No hourly amount calculation
```

### Start validation

```text
START
  ↓
Validate console availability
  ↓
Validate selected mode
  ↓
Validate applicable price
  ↓
If Hours → validate intended duration
  ↓
Persist session start
  ↓
06 — Active Session Details
```

## 6. Open Time Flow

```text
03 Dashboard
   ↓ START
05 Session Start
   ↓ Billing = Open Time
START
   ↓
06 Active Session Details
   ↓
Customer plays
   ├── Pause
   ├── Resume
   └── Complete
          ↓
08 Session End / Invoice
```

Open Time has no automatic end.

## 7. Fixed Hours Flow

```text
03 Dashboard
   ↓ START
05 Session Start
   ↓ Billing = Hours
Enter intended hours
   ↓
START
   ↓
06 Active Session Details
   ↓
Time reaches intended duration
   ↓
09 Fixed Time Alert
   ↓
Employee acknowledges / returns to session
   ↓
06 Active Session Details
```

The alert does not automatically complete the session.

The employee still chooses when to complete it.

## 8. Match Flow

```text
03 Dashboard
   ↓ START
05 Session Start
   ↓ Billing = Match
Select Mode = Single / Multi
   ↓
START
   ↓
06 Active Session Details
   ↓
Players finish match
   ↓
Cashier completes match
   ↓
Use captured Match price
   ↓
08 Session End / Invoice
```

Match price is not calculated from elapsed time.

## 9. Pause / Resume Flow

```text
06 Active Session Details
   ↓
Pause
   ↓
Persist pause timestamp/state
   ↓
Paused
   ↓
Resume
   ↓
Persist resume timestamp/state
   ↓
Active
```

Paused time is excluded from billable active duration according to the approved R1 rule.

## 10. Complete Session → Invoice → Cash

```text
06 Active Session Details
   ↓
Complete / Stop & Invoice
   ↓
Calculate approved charge
   ↓
08 Session End Invoice
   ↓
Confirm invoice
   ↓
Record Cash payment
   ↓
Persist local financial completion
   ↓
Return to Dashboard / Invoice Details
```

The UI must not show a successful financial completion before required local persistence succeeds.

## 11. Session Transfer Flow

### Screen

`07_Session_Transfer`

### Normal case

```text
Active session belongs to Cashier A
       ↓
Handover required
       ↓
Authorized Manager/Owner
       ↓
07 Session Transfer
       ↓
Select receiving Cashier B
       ↓
Confirm
       ↓
OpenedBy remains Cashier A
CurrentResponsibleUser becomes Cashier B
       ↓
Audit/trace
```

The original opener must remain historically visible.

## 12. Session History Flow

### Screen

`10_Session_History`

```text
Sessions
  ↓
Session History
  ↓
Filter / inspect history
```

History must include records related to operations that were later cancelled/voided. They must not disappear simply because a linked financial record was cancelled.

## 13. Invoice Browsing Flow

```text
11 Invoices All
    ↓ select invoice
12 Invoice Details
    ↓
Back → 11 Invoices All
```

Cashier may inspect normal authorized invoice history.

Protected Cancel/Void is not a normal Cashier action.

## 14. Invoice Cancel/Void Flow

### Screens

```text
12 Invoice Details
13 Invoice Cancel
14 Invoices Cancelled
```

### Flow

```text
12 Invoice Details
    ↓
Authorized Manager/Owner selects Cancel/Void
    ↓
Permission check
    ↓
13 Invoice Cancel
    ↓
Reason field (Optional)
    ↓
Confirm
    ↓
Mark Cancelled/Voided
    ↓
Preserve original record/history
    ↓
Update reporting treatment
    ↓
14 Cancelled Invoices / 12 Invoice Details
```

The flow must succeed with a blank reason when every other requirement is valid.

There is no unrestricted destructive invoice deletion.

## 15. Shift Overview Flow

### Screens

```text
15 Shift Current
16 Shift Start
17 Shift End
18 Shift History
```

Shift management is an operational employee-work-period function and is separate from `Management > Employees`.

### Cashier

```text
Shift
  ↓
View own current shift only
```

### Manager / Owner

```text
Shift
  ↓
See relevant branch employees / shifts
  ├── Start selected employee shift
  ├── End selected employee shift
  ├── View current shifts
  └── View shift history
```

## 16. Start Shift for an Employee

```text
Manager/Owner opens Shift
   ↓
Select employee
   ↓
16 Shift Start
   ↓
Confirm
   ↓
Persist Branch + Employee + Starting Actor + Start Time
   ↓
Shift Open
```

## 17. End Shift with No Active Sessions

```text
Manager/Owner selects employee
   ↓
17 Shift End
   ↓
No active sessions assigned?
   ↓ Yes
Confirm End Shift
   ↓
Persist End Time
   ↓
Shift Closed
```

## 18. End Shift with Active Sessions

```text
Manager/Owner selects End Shift for Cashier A
       ↓
System detects active sessions
       ↓
Select receiving Cashier B
       ↓
07 Session Transfer / handover flow
       ↓
Transfer all required active-session responsibility
       ↓
OpenedBy remains original opener
       ↓
Close Cashier A shift
```

The remaining session responsibility is attributed to the receiving Cashier.

## 19. Device Management Flow

### Screens

```text
19 Management Devices
20 Device Add/Edit
21 Device Delete/Deactivate Confirm
```

```text
Manager/Owner
   ↓
19 Devices
   ├── Add → 20
   ├── Edit → 20
   └── Deactivate/protected removal → 21
```

Cashier does not receive this management flow.

## 20. Pricing Management Flow

### Screen

`22_Management_Pricing`

```text
Manager/Owner
   ↓
Pricing
   ↓
Select branch pricing context
   ↓
Set/update PS4/PS5 + Single/Multi + Hourly/Match price
   ↓
Validate
   ↓
Save
   ↓
Audit/trace
```

A price change must not retroactively change a running session's captured price snapshot.

## 21. Employee Management Flow

### Screens

```text
23 Management Employees
24 Employee Add/Edit
25 Roles & Permissions
```

```text
Authorized Manager/Owner
   ↓
Employees
   ├── Add/Edit employee
   ├── Activate/Deactivate where supported
   ├── Reset PIN through authorized flow
   └── Roles & Permissions if permission allows
```

This is not the same as the Shift employee list.

The Shift area manages work periods; Employee Management manages employee records/access.

## 22. Asset Management Flow

### Screens

```text
26 Management Assets
27 Asset Add/Edit
```

```text
Manager/Owner
   ↓
Assets
   ↓
Choose internal asset/equipment category
   ↓
View / add / update branch quantity
   ↓
Save
```

R1 assets are internal equipment tracking, not customer product-sales inventory.

A dedicated per-controller damage lifecycle is not required. Repair cost may be recorded as an Expense.

## 23. Expense Flow

### Screens

```text
28 Management Expenses
29 Expense Add/Edit
```

```text
Manager/Owner
   ↓
Expenses
   ↓
Add/Edit allowed expense information
   ↓
Amount + Category/Description + optional note
   ↓
Save with Branch + Actor + Timestamp
```

Cashier must not create expenses.

## 24. Reports Flow

### Screens

```text
30 Reports Statistics
31 Reports Details
32 Reports Revenue
33 Reports Expenses
34 Reports Shifts
```

```text
Authorized Manager/Owner
       ↓
Reports
       ↓
Choose period:
Daily / Weekly / Monthly / Yearly
       ↓
Choose presentation:
Statistics / Details
       ↓
Open relevant report:
Revenue / Expenses / Profit / Shifts
```

Monthly Review is a review-only flow. It must not lock a period or mutate financial records.

## 25. General Settings Flow

### Screen

`35_Settings_General`

```text
Settings
   ↓
General
```

Rules:

- Branch name is visible but read-only.
- Keep only normal user-facing settings.
- Do not expose security-policy constants as editable fields.
- Back returns to the previous valid screen.

## 26. Security Settings Screen

### Current ZIP screen

`36_Settings_Security`

This screen is present in the current ZIP, but it must not be part of the approved normal user-facing flow.

PIN thresholds, security lock timings, and similar security-policy constants are not ordinary user settings.

Security recovery is a separate protected security flow, not a normal editable Settings page.

## 27. Language Flow

### Screen

`37_Settings_Language`

```text
Settings
   ↓
Language
   ↓
Choose English / Arabic
   ↓
Apply
   ↓
Reload/mirror UI direction
```

English = LTR.  
Arabic = RTL.

The feature set and permissions must remain identical after language change.

## 28. Global Back Flow

Every non-root page/action must offer a clear Back/Close/Cancel route.

Examples:

```text
05 Session Start → Back/Cancel → 03 Dashboard
06 Active Session Details → Back → 04 Live Sessions / previous valid screen
07 Session Transfer → Back/Cancel → previous session/shift screen
08 End Invoice → Back/Cancel where valid → 06 Active Session Details
10 Session History → Back → Sessions
12 Invoice Details → Back → 11 Invoices
13 Invoice Cancel → Cancel/Back → 12 Invoice Details
16/17 Shift action → Back/Cancel → Shift
20/21 Device action → Back/Cancel → 19 Devices
24 Employee action → Back/Cancel → 23 Employees
27 Asset action → Back/Cancel → 26 Assets
29 Expense action → Back/Cancel → 28 Expenses
31-34 Report details → Back → Reports
35/37 Settings → Back → previous Settings/root screen
```

Back navigation must never bypass role/permission checks.

## 29. Offline Flow

```text
Internet unavailable
      ↓
Check valid offline authority
      ↓
Allowed?
 ┌────No──────────────┐
 ↓                    │
Blocking/restricted   │
state                 │
                      │
Yes                   │
 ↓                    │
Continue only role-authorized local operation
 ↓
Persist locally
 ↓
Show clear saved/pending state
 ↓
Synchronize later
```

Offline mode does not turn Cashier into Manager/Owner.

## 30. UI States Still Required for Final R1 Coverage

Before UI approval, important flows must account for the applicable states:

```text
Loading
Empty
Saving
Saved
Offline
Service unavailable
Permission denied
Validation error
Sync pending
Sync failed/conflict
Security temporary lock
SecurityLocked
LicenseExpired
SubscriptionSuspended
```

These states should use user-facing language, not infrastructure terminology.

## 31. Arabic / English Parity

Every flow in this file applies to both:

```text
+90PS_UI/EN/
+90PS_UI/AR/
```

The Arabic version mirrors direction and layout but must not change:

- Permission.
- Required fields.
- Session behavior.
- Financial behavior.
- Shift behavior.
- History.
- Settings scope.
- Error/lock meaning.

## 32. Current UI Alignment Checklist

The next UI correction pass should verify:

- [ ] Dashboard uses `START` for an available device.
- [ ] `Add Order` is removed.
- [ ] Session Start includes Mode = Single/Multi.
- [ ] Billing is a dropdown with Open Time / Hours / Match.
- [ ] Hours reveals duration input.
- [ ] Match is visible.
- [ ] Back navigation exists throughout.
- [ ] Session History includes cancelled/void-related historical records.
- [ ] Shift area allows Manager/Owner to Start/End selected employee shifts.
- [ ] Shift employee list is separate from Management > Employees.
- [ ] Cashier does not receive Management/Reports/Expense/Pricing privileges.
- [ ] Invoice Cancel/Void is Manager/Owner protected.
- [ ] Cancel/Void reason is optional.
- [ ] Screen 35 branch name is read-only.
- [ ] Screen 36 is not part of normal editable Settings.
- [ ] Settings remain normal user-facing settings.
- [ ] English and Arabic behavior match.
- [ ] Existing visual identity is preserved while these behavior/structure changes are applied.
