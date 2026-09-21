# +90 PS — Release 1 UI Review

**Document Type:** Final UI Review / Release 1 UI Baseline  
**Release:** Release 1 — Operational MVP  
**Status:** Approved UI Review Baseline for the Next Analysis Stage  
**Visual Source:** `+90PS_UI.zip`  
**Review Basis:** Current R1 requirements + current UI visual language + the latest approved UI corrections  
**Last Updated:** 2026-09-10

---

# 1. Purpose

This document reviews and freezes the intended Release 1 UI behavior and screen scope before System Analysis and technical design begin.

The current `+90PS_UI.zip` is the visual reference. For this review, the latest approved corrections are treated **as if they are already applied to the final UI**. The purpose is therefore not to request another visual redesign pass, but to document the UI state that the project will use going forward.

This document does not define database tables, API routes, WPF classes, or implementation details.

---

# 2. Final UI Direction

The final Release 1 UI keeps the existing +90 PS visual identity unchanged:

- Same dark black/burgundy visual language.
- Same red primary accent.
- Same typography style.
- Same button style.
- Same card style.
- Same sidebar structure and visual density.
- Same English/Arabic mirrored visual direction.
- Same desktop-first screen proportions.

Only the approved content, permission, navigation, and workflow corrections described in this file are considered changed.

---

# 3. Authentication Review

## 3.1 Normal employee login

The normal Release 1 employee login is **Personal PIN-based authentication**.

The final Login flow is:

```text
Open +90 PS
    ↓
Employee enters personal PIN
    ↓
System identifies Employee + Role + Branch + Permissions
    ↓
Dashboard / allowed workspace
```

A normal employee **Username + Password screen is not required**.

Desktop versus Website does not determine whether authentication must use Username/Password. Authentication is a product/security decision. The approved R1 requirements explicitly use a personal employee PIN on the shared branch PC.

Therefore:

```text
Normal R1 employee login = Personal PIN
Username + Password        = Not part of the normal employee UI
```

If a future device/branch activation or platform administration flow needs separate credentials, that must be specified later as a separate protected setup flow. It must not be invented inside the normal employee Login screen.

**Final Review Result:** `KEEP PIN LOGIN`

---

# 4. Role-Aware UI Review

Official R1 roles:

```text
Owner
Manager
Cashier
```

The UI must not present the Cashier as if the Cashier is an Owner or Manager.

## Cashier UI focus

The Cashier UI is centered on daily operation:

- Dashboard / console availability.
- Start Session.
- Live Sessions.
- Pause / Resume.
- Complete Session.
- Invoice/payment workflow.
- Operational history allowed to the Cashier.
- Own current shift information where applicable.
- Normal user settings.

## Manager / Owner UI

Administrative areas are available only according to role/permission:

- Devices / Consoles.
- Pricing.
- Employees.
- Roles & Permissions.
- Assets.
- Expenses.
- Shift administration.
- Reports.
- Protected Invoice Cancel/Void.
- Protected Session responsibility transfer.

**Final Review Result:** `ROLE-AWARE NAVIGATION REQUIRED`

---

# 5. Sidebar and Icon Consistency

The Dashboard and Sessions icons must follow the same icon-color rules as the rest of the sidebar.

Final rule:

```text
Inactive navigation item → same neutral/muted icon color used by the other inactive items
Active navigation item   → primary red active treatment
```

Dashboard and Sessions must not use a permanently different icon color from other navigation items unless they are the currently active destination.

The active state can continue using the existing red highlight/bar/background treatment from the current visual design.

**Final Review Result:** `VISUAL CONSISTENCY FIXED`

---

# 6. Screen 03 — Dashboard

The Dashboard remains the main operational console-grid screen.

## Removed

`Add Order` is removed from Release 1.

Reason: R1 does not include food, drink, cafeteria, or product-ordering workflows.

## Available console action

An available console displays one primary action:

```text
START
```

The card must not ask the employee to choose `Single` or `Multi` directly on the Dashboard card.

Final flow:

```text
Available Console
      ↓
START
      ↓
Screen 05 — Session Start
```

## Active console

An active console keeps the existing operational visual language and shows only actions that are valid for the active session and current user's permission.

**Final Review Result:** `APPROVED WITH START FLOW`

---

# 7. Screen 05 — Session Start

The visual style of Screen 05 stays unchanged, but its input logic is finalized as follows.

## Required structure

```text
Console / Device
Mode
Billing
Conditional Hours Field
Primary Start Action
```

## Mode

```text
Single
Multi
```

## Billing

Billing is a Drop Down List with:

```text
Open Time
Hours
Match
```

### Open Time

Starts an Hourly/Open session with no intended fixed end time.

### Hours

When `Hours` is selected, an input appears directly below Billing.

Example:

```text
Billing: Hours
Hours:   2
```

or:

```text
Billing: Hours
Hours:   3
```

The entered value represents the intended number of hours for the Fixed session.

When the intended duration finishes, the system alerts the employee; it does not automatically force-stop the session.

### Match

`Match` is visible as a Billing option.

Match uses the applicable Match price and is not billed from elapsed Hourly time.

**Final Review Result:** `APPROVED BILLING DROPDOWN = OPEN TIME / HOURS / MATCH`

---

# 8. Global Back Navigation

A consistent Back control is part of the final Release 1 UI.

The Back action returns the user to the previous valid UI context.

Examples:

```text
Session Start      → Back → previous Dashboard/console context
Session Details    → Back → previous Sessions context
Invoice Details    → Back → Invoices
Shift action       → Back → Shift
Management form    → Back → parent Management page
Report detail      → Back → Reports
Settings subpage   → Back → Settings / previous valid screen
```

The Login screen remains the entry screen, and Back must never be used to bypass authentication or permissions.

The Back control must use the existing button/icon visual language rather than introducing a new visual style.

**Final Review Result:** `GLOBAL NAVIGATION REQUIREMENT APPROVED`

---

# 9. Screen 10 — Session History

Session History must preserve visibility of relevant historical operations that were later associated with Cancelled/Voided financial records.

The user must be able to distinguish cancelled history instead of having it disappear.

Recommended visible status treatment within the existing table/badge language:

```text
Completed
Cancelled / Voided
```

No historical financial/session record is physically removed from the UI history simply because it was cancelled.

**Final Review Result:** `CANCELLED HISTORY INCLUDED`

---

# 10. Invoice Cancel/Void UI

Cancel/Void remains a protected Manager/Owner action.

The reason field is **Optional**.

Final behavior:

```text
Authorized Cancel/Void
       ↓
Optional Reason
       ↓
Confirm
       ↓
Invoice remains historically traceable
```

The UI must not display the reason field as required.

Examples of acceptable UI wording:

```text
Reason (Optional)
سبب الإلغاء (اختياري)
```

**Final Review Result:** `OPTIONAL REASON`

---

# 11. Shift UI

Shift administration is separate from `Management > Employees`.

The existing Employees management feature remains in the Management area and is **not removed**.

## Shift area purpose

The Shift area manages employee work periods.

For an authorized Manager/Owner, the Shift page shows the relevant employees and their shift state.

The Manager/Owner can:

```text
View employees and current shift status
Start Shift for selected employee
End Shift for selected employee
View Shift History
```

## Ending a shift with active sessions

If Cashier A has active sessions when the Manager/Owner ends the shift:

```text
End Shift for Cashier A
       ↓
Active sessions detected
       ↓
Select receiving Cashier B
       ↓
Transfer current responsibility
       ↓
Preserve original opening Cashier
       ↓
End Cashier A shift
```

The active session responsibility after transfer is associated with the receiving Cashier according to the approved handover rule.

**Final Review Result:** `SHIFT EMPLOYEE LIST + START/END APPROVED`

---

# 12. Management > Employees

`Management > Employees` remains a separate feature.

It is used for employee administration, for example:

- Employee records.
- Add/Edit where authorized.
- Role assignment according to permission.
- Employee state/access management.
- PIN reset through the protected flow.

It must not be merged into Shift.

```text
Employees Management = Employee/account administration
Shift                 = Employee work-period operation
```

**Final Review Result:** `KEEP AS SEPARATE MODULE`

---

# 13. Screen 35 — General Settings

The final General Settings page contains only reasonable user-facing settings.

## Branch Name

Branch Name is visible as context, but it is **Read Only** in normal Settings.

It must not appear as a normal editable text field.

## Remove Default Start Page

The setting that lets a user choose what page opens first in the system is removed.

The system uses the approved product navigation/start behavior rather than allowing each user to change the initial page from Settings.

Therefore the following is not part of final R1 Settings:

```text
Default Start Page
Open Dashboard First
Choose Initial System Page
```

## Allowed Settings Direction

General Settings should contain only ordinary user-facing preferences that do not change business, security, pricing, branch identity, financial, or permission rules.

Examples of acceptable categories within the current visual style:

- Language.
- Approved notification/sound preferences.
- Other non-critical personal display/interaction preferences only if explicitly included in the UI.

**Final Review Result:** `USER-FACING SETTINGS ONLY`

---

# 14. Screen 36 — Settings Security

Screen 36 is removed from the normal final Release 1 UI.

Security-policy constants such as lock thresholds, lock durations, or other protected security rules:

- Must not be editable by ordinary users.
- Must not appear as normal Settings fields.
- Must not be exposed merely because a Manager or Owner can open Settings.

Security recovery remains a separate protected workflow where required; it is not the same thing as editing security-policy values.

**Final Review Result:** `REMOVE FROM NORMAL UI`

---

# 15. Screen 37 — Language

Language remains a valid user-facing setting.

```text
English → LTR
Arabic  → RTL
```

Changing language must not change permissions, business behavior, financial behavior, or available features.

**Final Review Result:** `KEEP`

---

# 16. Reports Review

R1 Reports remain Manager/Owner functionality according to authorization.

The final report UI supports:

```text
Daily
Weekly
Monthly
Yearly
```

And keeps the explicit presentation distinction:

```text
Statistics
Details
```

Core reporting remains centered on:

- Revenue.
- Expenses.
- Profit.
- Shifts / operational detail where defined.

Monthly Review is review-only and does not mutate or close financial records.

**Final Review Result:** `KEEP / ROLE-PROTECTED`

---

# 17. Assets Review

Assets remain internal branch equipment tracking, not customer product inventory.

The visual direction supports quantity-based management for approved equipment categories.

The UI must not introduce:

- Product sales stock.
- Customer shopping inventory.
- Dedicated controller damage lifecycle.

Repair cost can be handled through Expenses as already defined in R1 requirements.

**Final Review Result:** `KEEP AS INTERNAL QUANTITY-BASED ASSETS`

---

# 18. English / Arabic Parity

The final UI exists in matching English and Arabic versions.

Every approved correction in this review applies to both languages:

- Same screen capability.
- Same role permission.
- Same fields.
- Same buttons/actions.
- Same statuses.
- Same workflow.
- Same validation meaning.
- Mirrored layout direction only.

No feature may exist only in English or only in Arabic.

---

# 19. Final Active Screen Baseline

The original UI pack contains numbered screens 01–37 in both languages. For the approved final UI interpretation:

- Screen 36 (`Settings Security`) is retired from normal user-facing UI.
- Screen numbering is kept as a traceability reference; renumbering is not required for documentation.
- All other screens remain subject to the role and workflow corrections documented here.

Key final screen decisions:

| Screen | Final decision |
|---|---|
| 01 Login | Personal PIN; no normal Username/Password form |
| 02 Login Lockout | Keep security state |
| 03 Dashboard | Remove Add Order; available console uses START |
| 04 Sessions Live | Keep |
| 05 Session Start | Billing dropdown = Open Time / Hours / Match; Hours reveals Hours input |
| 06 Active Details | Keep within Cashier operational permission |
| 07 Session Transfer | Protected Manager/Owner handover |
| 08 Session End Invoice | Keep |
| 09 Fixed Time Alert | Keep; alert only, no auto-stop |
| 10 Session History | Include cancelled/void-related history |
| 11–14 Invoices | Keep; Cancel/Void protected; reason optional |
| 15–18 Shift | Show relevant employees; Manager/Owner Start/End shifts; handover active sessions |
| 19–29 Management | Keep, role-protected; Employees remains separate from Shift |
| 30–34 Reports | Keep, Manager/Owner authorized access |
| 35 General Settings | Branch name read-only; remove default start-page option |
| 36 Settings Security | Remove from normal final UI |
| 37 Language | Keep |

---

# 20. Final UI Approval Checklist

- [x] Existing visual identity preserved.
- [x] Employee authentication defined as Personal PIN.
- [x] No normal Username/Password employee login added.
- [x] Owner / Manager / Cashier role boundaries respected.
- [x] Dashboard and Sessions icon color behavior normalized with the sidebar system.
- [x] `Add Order` removed from R1 Dashboard.
- [x] Available device action is `START`.
- [x] Single/Multi moved to Session Start flow.
- [x] Billing is a dropdown with Open Time / Hours / Match.
- [x] Hours reveals intended-hours input.
- [x] Match is represented.
- [x] Back navigation is a system-wide navigation rule.
- [x] Cancelled/Voided history remains visible.
- [x] Shift employee operations are separate from Employee Management.
- [x] Manager/Owner can Start/End authorized employee shifts.
- [x] Active-session handover occurs before closing the outgoing Cashier's shift.
- [x] Invoice Cancel/Void reason is optional.
- [x] Branch name is read-only in General Settings.
- [x] Default Start Page setting removed.
- [x] Settings Security screen removed from normal UI.
- [x] Settings limited to normal user-facing preferences.
- [x] English/Arabic behavior remains equivalent.

---

# 21. UI Review Conclusion

For project-planning purposes, Release 1 UI is now treated as **reviewed against the latest approved corrections**.

No further UI-edit stage is required by this document.

Any later UI change should come from a newly approved requirement, UX issue, implementation constraint, UAT finding, or Release 2/Release 3 scope change rather than silently changing the R1 baseline.
