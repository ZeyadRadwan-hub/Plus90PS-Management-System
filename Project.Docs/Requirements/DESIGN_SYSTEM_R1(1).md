# +90 PS — Release 1 Design System

**Document Type:** Final UI Design System Baseline  
**Release:** Release 1 — Operational MVP  
**Status:** Approved Design-System Baseline for Implementation Planning  
**Visual Source:** `+90PS_UI.zip`  
**Behavior Source:** Final R1 UI Review + current R1 requirements  
**Last Updated:** 2026-09-10

---

# 1. Purpose

This document captures the reusable visual and interaction rules of the approved +90 PS Release 1 desktop UI.

The goal is to preserve the existing visual identity when the UI is later implemented in WPF and when additional R1 states/screens are created.

This design system must **not redesign the product**. New implementation work must look and behave like the approved UI baseline.

---

# 2. Design Principles

## DS-001 — Preserve the Existing +90 PS Identity

The current dark gaming/operations visual language is the baseline.

Do not replace it with a generic Windows, Material, Fluent, Bootstrap, or web-dashboard look.

## DS-002 — Operational Clarity

The Cashier must be able to identify:

- Console state.
- Session state.
- Time.
- Billing mode.
- Amount.
- Primary action.

quickly and without unnecessary navigation.

## DS-003 — Role-Aware UI

A visible control must respect the current user's role/permission.

The design must not make a Cashier look like an Owner/Manager.

## DS-004 — Same Visual Language Everywhere

Buttons, cards, inputs, tables, dialogs, navigation, badges, and status treatments use one consistent component language across all screens.

## DS-005 — Arabic/English Parity

English and Arabic are one design system with mirrored direction, not separate products.

---

# 3. Reference Screen Size

The supplied approved UI screenshots use a desktop canvas of:

```text
1600 × 900 px
```

This size is the high-fidelity visual reference.

Implementation may adapt to supported desktop window sizes, but the proportions, hierarchy, density, and visual relationships must remain consistent with the reference screens.

---

# 4. Color System

The following palette is taken from the current UI screenshots and should be treated as the visual baseline.

## Core dark surfaces

| Token | Reference HEX | Usage |
|---|---|---|
| `Color.App.Background` | `#0B0000` | Main application background |
| `Color.Sidebar.Background` | `#090000` | Sidebar/navigation background |
| `Color.Surface.Primary` | `#100001` | Main panels/cards |
| `Color.Surface.Secondary` | `#150102` | Secondary dark surface |
| `Color.Surface.Elevated` | `#190203` | Elevated/card/form surfaces |
| `Color.Surface.Deep` | `#140102` | Nested/secondary controls |
| `Color.Border.DarkRed` | `#32060A` | Dark red separators/borders |

## Brand/action colors

| Token | Reference HEX | Usage |
|---|---|---|
| `Color.Brand.Primary` | `#F20D12` | Primary red actions, active accents, key highlights |
| `Color.Brand.Bright` | `#FF3A3E` | Bright red highlight/hover/emphasis where present |

## Text

| Token | Reference HEX | Usage |
|---|---|---|
| `Color.Text.Primary` | `#F4EEEE` | Primary readable text |
| `Color.Text.Secondary` | `#B4A7A7` | Secondary labels / muted text |
| `Color.Text.Muted` | `#6E5E5E` | Lower-emphasis text |

## Status colors

| Token | Reference HEX | Usage |
|---|---|---|
| `Color.Status.Success` | `#1E9847` | Successful/saved/positive confirmation |
| `Color.Status.Available` | `#13723C` | Available/operational positive state where used |

The design should not add unrelated bright colors unless a later approved semantic state requires them.

---

# 5. Color Usage Rules

## Primary Red

Use `#F20D12` for:

- Main primary actions.
- Active navigation indicator.
- Selected emphasis.
- Important brand accents.

Do not paint every control red. Red remains strongest when the dark UI provides contrast.

## Dark Surfaces

Use the existing hierarchy of nearly-black and burgundy surfaces to separate:

```text
App background
Sidebar
Card
Form panel
Modal
Input
Table section
```

without introducing light theme surfaces.

## Success Green

Use green only for semantic success/availability, for example:

- Saved successfully.
- Available device.
- Positive operational status.

Do not use green as a generic decorative accent.

---

# 6. Typography

The exact font-family metadata cannot be recovered reliably from raster screenshots alone, so implementation must preserve the **same approved typography source/family used to create the current UI** rather than substituting a new family based only on this document.

Visual typography characteristics that are part of the baseline:

- Strong condensed/impact-style uppercase English headings.
- Clean compact body/interface text.
- Strong numeric emphasis for timers and amounts.
- Arabic typography with clear compact dashboard readability.
- High contrast against dark backgrounds.

Semantic type roles:

```text
Brand / Product Title
Page Title
Section Title
Card Title
Body
Label
Caption
Button Label
Table Header
Table Cell
Timer / Numeric Emphasis
Money / Amount Emphasis
Status / Badge Text
```

Rules:

- Keep the same current font appearance.
- Do not replace headings with a soft rounded consumer font.
- Timer and money values must remain highly scannable.
- Arabic must not be forced into an English font fallback that damages readability.

---

# 7. Layout System

## Desktop Structure

The standard application layout uses:

```text
Sidebar Navigation
+
Main Content Area
+
Top/Page Header
+
Cards / Tables / Forms / Dialogs
```

English layout:

```text
Sidebar → Left
Content → Right
```

Arabic layout:

```text
Content ← Left
Sidebar ← Right
```

The Arabic version mirrors direction while preserving component size, hierarchy, and behavior.

## Density

The application is an operational desktop system, so the design intentionally uses compact information density.

Do not convert it into oversized mobile-style cards or excessive whitespace.

---

# 8. Sidebar Navigation Component

The sidebar keeps the current dark background, icon + label pattern, active red treatment, grouped navigation, and user/role area.

## Icon color rule

```text
Inactive item → common neutral/muted icon treatment
Active item   → primary red active treatment
```

Dashboard and Sessions use the same rule as every other navigation item. They do not receive permanent special icon colors.

## Permission rule

Navigation is role-aware.

Cashier does not receive Manager/Owner administration merely because those routes exist in the application.

---

# 9. Back Navigation Component

A common `Back` control is part of the final system navigation language.

Requirements:

- Same style across all relevant screens.
- Positioned consistently within the page/header structure.
- Uses the existing icon/button visual language.
- English direction points back according to LTR navigation.
- Arabic is mirrored for RTL.
- Returns to the previous valid UI context.
- Never bypasses role or authentication checks.

Root/entry contexts must handle Back safely rather than creating invalid navigation.

---

# 10. Button System

The current button visual language is retained.

Required semantic button families:

```text
Primary
Secondary
Neutral / Tertiary
Success / Confirm
Danger / Protected
Icon Button
Back
```

## Primary Button

Use for the main action on a screen, for example:

- START.
- Save.
- Confirm.
- Login.

Visual baseline:

- Bright red primary fill/accent.
- High-contrast light text.
- Same current corner radius and compact height.
- Strong uppercase/compact label style where used in the current UI.

## Secondary Button

Uses dark/burgundy surfaces and borders consistent with the current design.

## Danger / Protected

Cancel/Void and protected destructive-like actions must be visually distinguishable from ordinary primary actions, while still remaining inside the same red/dark design family.

## States

Every reusable button supports:

```text
Default
Hover
Pressed
Focused
Disabled
Loading (when needed)
```

---

# 11. Console / Device Card Component

The console card is one of the core R1 components.

It must preserve the current card shape, dark surface, state emphasis, typography hierarchy, and action placement.

## Available state

Shows:

- Device identity/name.
- PS4 / PS5 type where applicable.
- Available state.
- One primary action: `START`.

It must **not** contain direct `Single` and `Multi` start buttons.

## Active state

Shows the existing relevant session information such as:

- Device.
- Session state.
- Timer/duration.
- Mode/Billing context where appropriate.
- Current allowed operational actions.

`Add Order` is not a Release 1 card action.

---

# 12. Session Start Form Component

Screen 05 defines the standard session-start pattern.

## Fields

```text
Device
Mode
Billing
Hours (conditional)
```

## Mode control

```text
Single
Multi
```

Use the existing segmented/choice visual language.

## Billing control

Billing is a Drop Down List:

```text
Open Time
Hours
Match
```

## Conditional Hours input

When:

```text
Billing = Hours
```

show the Hours numeric field directly below Billing.

Examples accepted by the intended UI concept:

```text
2
3
```

When Billing is not Hours, the field is not shown.

## Match

Match is a first-class visible Billing option and must not be omitted.

---

# 13. Input System

Reusable input families:

```text
PIN Input
Text Input
Numeric Input
Dropdown / ComboBox
Search Input
Read-Only Field
Optional Textarea / Reason
Date / Period control where required
```

States:

```text
Default
Focused
Filled
Read Only
Disabled
Validation Error
```

Inputs keep the current dark/burgundy background, subdued border, light text, and red focus/accent direction.

---

# 14. PIN Input

The Login PIN input is a dedicated authentication component.

Requirements:

- Personal employee PIN.
- Masked/secure entry.
- Strong focus visibility.
- Clear error state.
- Compatible with temporary lock flow.
- No Username field in normal employee Login.
- No Password field in normal employee Login.

Desktop versus Web does not change this product authentication rule.

---

# 15. Dropdown Component

The dropdown must visually match existing form controls.

For Session Start, Billing values are exactly:

```text
Open Time
Hours
Match
```

The component must support keyboard/mouse desktop operation and clear selected-state visibility during implementation.

---

# 16. Read-Only Field Component

Read-only values must look intentionally non-editable rather than merely appearing as a broken input.

Primary R1 example:

```text
Settings → Branch Name
```

Branch Name is context information and is not editable from normal Settings.

---

# 17. Tables

Tables keep the current compact dark management/reporting style.

Common features:

- Clear header row.
- Dark row surfaces.
- Readable light text.
- Muted secondary metadata.
- Status badges.
- Row actions only when the current role has permission.

Primary table areas:

- Session History.
- Invoices.
- Shift History.
- Devices.
- Employees.
- Assets.
- Expenses.
- Reports Details.

---

# 18. Status Badge System

Status must use text plus visual treatment; do not rely on color alone.

Relevant R1 statuses include, where applicable:

```text
Available
Active
Paused
Completed
Cancelled / Voided
Saved
Offline
Pending
Error
Locked
```

Session History must visibly support Cancelled/Voided-related history.

---

# 19. Dialog / Modal System

Dialogs keep the current centered dark/burgundy panel treatment with strong page dimming/visual separation.

Common modal/action patterns:

- Confirm action.
- Cancel/Void invoice.
- Session transfer.
- Shift start/end.
- Delete/Deactivate confirmation.
- Error/alert.

Primary action and Cancel/Back placement should remain consistent.

---

# 20. Fixed Session Alert

The Fixed session expiry alert uses the existing high-visibility centered alert language.

Requirements:

- Identifies affected session/device.
- Strong visual priority.
- Does not imply that the session has automatically ended.
- Employee acknowledges and returns to the active-session workflow.

---

# 21. Session History Design Rule

Session History preserves historical visibility.

Cancelled/Voided-related records must use the normal table + status-badge system rather than being removed.

History design must make status scannable without changing the overall table style.

---

# 22. Shift Components

Shift has its own operational employee list and is not the same component as Employee Management.

## Shift employee row/card

Displays enough information to identify:

- Employee.
- Role where useful.
- Current shift status.
- Start/End action for authorized Manager/Owner.

## Employee Management row/card

Used for employee account/record administration.

These two concepts may share visual table/card components but must not share the same purpose or permissions.

---

# 23. Reports Components

Reports use the current dark analytics/table visual style.

Required period choices:

```text
Daily
Weekly
Monthly
Yearly
```

Required presentation choice:

```text
Statistics
Details
```

Common reporting components:

- Summary cards.
- Statistics/chart area where present.
- Details table.
- Period selector.
- Report-category navigation.

Monthly Review is display/review only and must not use destructive/closing-state visual semantics.

---

# 24. Settings Design Rules

Settings contains only ordinary user-facing preferences.

## Keep

- General non-critical user preferences.
- Language.
- Approved notification/sound preferences where included.

## Read Only

- Branch Name.

## Remove

- Default Start Page / choose first page.
- Editable security-policy constants.
- Security lock thresholds/timers as normal settings.
- Pricing/business-rule configuration that belongs elsewhere.
- Permission configuration that belongs in Roles & Permissions.

## Screen 36

`Settings Security` is not part of the normal approved user-facing Settings navigation.

---

# 25. Language / RTL System

English and Arabic share the same components.

## English

```text
Direction = LTR
Sidebar   = Left
```

## Arabic

```text
Direction = RTL
Sidebar   = Right
```

Mirroring applies to:

- Sidebar.
- Back direction.
- Page alignment.
- Form flow.
- Table alignment where appropriate.
- Dialog action placement where appropriate.

Do not mirror business meaning or change feature order semantically.

---

# 26. Icon System

Icons use one coherent line/compact style matching the current UI.

Rules:

- Same visual weight across sidebar icons.
- Same inactive color behavior.
- Same active red behavior.
- Do not make Dashboard or Sessions permanently brighter/different.
- Icon meaning must remain recognizable at desktop sidebar size.
- Icons do not replace text labels for primary navigation.

---

# 27. Feedback and User-Facing Messages

Use short operational wording.

Examples:

```text
Data Saved
تم حفظ البيانات

No Internet Connection
لا يوجد اتصال بالإنترنت

Service temporarily unavailable
الخدمة غير متاحة مؤقتًا

Could not save changes. Try again or contact support.
تعذر حفظ التغييرات. حاول مرة أخرى أو تواصل مع الدعم.
```

Normal user UI should avoid technical infrastructure language such as:

```text
SQLite
SQL Server
Outbox
API exception
Sync protocol
Database transaction
```

unless shown inside a future authorized technical/support tool.

---

# 28. Permission-Aware Component States

The design system supports three UI outcomes for a permission-sensitive action:

```text
Visible + Enabled
Visible + Disabled with clear reason (when useful)
Hidden from normal navigation/action surface
```

Which treatment is used depends on UX context, but the UI must never imply that a Cashier is allowed to perform Manager/Owner administration.

Security enforcement remains outside visual design and must later be implemented in trusted application/backend authorization.

---

# 29. Financial Record Design Rule

Financial records use non-destructive visual semantics.

For Invoice Cancel/Void:

- Protected Manager/Owner action.
- Reason input is optional.
- Cancelled status remains visible.
- History remains accessible.
- No generic permanent Delete action for financial history.

---

# 30. Component Consistency Checklist

All implementation screens must reuse the same design language for:

- [x] Sidebar.
- [x] Navigation active state.
- [x] Back control.
- [x] Primary red button.
- [x] Secondary dark button.
- [x] Inputs.
- [x] Dropdowns.
- [x] Read-only fields.
- [x] Tables.
- [x] Status badges.
- [x] Cards.
- [x] Modals.
- [x] Session device cards.
- [x] Shift employee rows/cards.
- [x] Reports controls.
- [x] Settings controls.
- [x] English/Arabic mirrored layout.

---

# 31. Final Design-System Freeze

The Release 1 visual identity is considered frozen at this stage.

Implementation must preserve:

```text
Dark black/burgundy palette
Primary red identity
Current typography appearance
Current compact desktop density
Current button/card/form visual language
Current sidebar visual language
Arabic/English mirroring
```

The following behavioral corrections are part of the final design baseline even if the original raster ZIP still visually contains the previous state:

```text
PIN login only for normal employees
Unified navigation icon color rules
Dashboard START action
No Add Order
Session Billing = Open Time / Hours / Match
Conditional Hours input
Global Back navigation
Cancelled history visibility
Shift employee Start/End administration
Employees Management remains separate
Settings Branch Name read-only
No Default Start Page setting
No normal Settings Security screen
Role-aware access
Optional invoice-cancel reason
```

Any future visual change must be tied to an approved requirement, implementation constraint, usability finding, UAT issue, or later release scope.
