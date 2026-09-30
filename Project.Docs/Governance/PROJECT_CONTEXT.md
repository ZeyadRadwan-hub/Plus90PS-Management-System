# +90 PS — Project Context for Codex and reviewers

**Document origin:** Release 1 pre-code design snapshot. BE-00 through BE-08 now have explicitly approved narrow code slices; see [CURRENT_STATE.md](../Reviews/CURRENT_STATE.md). The BE-06 [technical baseline](TECHNICAL_DECISIONS_R1.md) is documented, not implemented Database/Auth/Sync. Never read old `Final` as implemented. Developer/user: Zeyad; collaboration preferences are in root `AboutZeyad.md` (share only facts he explicitly wants in repo, and keep personal details out of public repositories).

## Product

Multi-branch-capable PlayStation shop management product, initial single shared Windows PC per branch, personal employee PIN and explicit branch/Owner/Manager/Cashier permissions. **R1:** operational sessions/PS4/PS5/Single/Multi/Open/Fixed Hours/Match/pricing/invoice/cash/shift/expenses/quantity-based internal assets/reports. Offline-first: WPF→local SQLite transaction (+outbox) online/offline; background sync→authorized ASP.NET Core API→central SQL Server. EF Core Code First is approved implementation approach **not yet implemented**. R2 customers/history/bookings/deposits; R3 advanced later. R1 no web/mobile/customer login/food/drink/product sales/discounts/new peripherals.

## Binding rules

- Billable positive seconds ceil minute, then round UP to next quarter hour only if 1–3 minutes remain; free Pause only for Open/Fixed, never Match; future Invoice shows total paused duration; no minimum. EGP .00–.49 drop fraction, .50–.99 up whole EGP, then move UP to next multiple of five only if +1 or +2 EGP.
- Fixed Hours expiry = alert only; Match = fixed branch price/defined duration and employee-ended; mode frozen during session.
- Owner/Manager can start/end shift for selected employee; handover before closing when sessions active, original opener retained.
- Protected Manager/Owner Cancel applies only to eligible unpaid Invoices, with optional reason and historical retention; an Invoice with successful recorded Payment cannot be cancelled. R1 has Cancel only, no separate Void action/state. Each Invoice has zero or one positive full Cash Payment, which may be received after issuance; no partial/installment payments. Monthly closing is read-only financial review.
- First 5 PIN failures→warning+20s temporary lock/security event; another five→SecurityLocked/engineer process. Separate subscription lock. Offline paid lease >10d→max72h; <=10d→remaining paid time+10h; no local time tampering extension.
- Approved requirements and current UX review/flows/design-system documents govern future UI behavior; original images are visual/style references only. `UI_IMPLEMENTATION_OVERRIDE_R1.md` is not present in this ZIP. Available references are `Project.Docs/Reviews/UI_REVIEW_R1(1).md`, `Project.Docs/Planning/UX_FLOWS_R1(2).md`, and `Project.Docs/Requirements/DESIGN_SYSTEM_R1(1).md`.

## How we reached this state

Original broad roadmap contains multiple old/duplicated .md versions and outdated discounts/settings. Current newer R1 requirements and business decisions supersede those **for conflicting behaviors only**, not wholesale entire documents. Zeyad authored all requested R1 Use Case/Activity/Sequence/State/C4/Deployment/Logical ERD/DFD diagrams. Pictures of ERD/DFD have readability/contract gaps: targeted diagram edits after related designs. The previous `PROJECT_REVIEW_AND_CHANGE_REGISTER.md` tracks named corrections. The uploaded source-tree inventory has been inspected: BE-00 through BE-08 Domain/API foundation exists; DB-01/DB-01.1 are documentation only. The ZIP does not provide a usable Git object database or verified original Git status/HEAD, and this inspection does not claim a fresh test run. Current pack is a reviewable **draft**, not a branch deployment or signed-off API/schema/security plan.

## Reference priority BEFORE reconciliation

1. Explicit latest user-approved decisions listed above and `Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md` (when placed in repo).
2. Latest validated governance decisions and R1 business/acceptance rules; check duplicates/old versions, never blindly trust filename `Final`.
3. Approved R1 UX requirements and the available review/flow/design-system files; old PNGs only styling reference if they contradict target behavior.
4. This design pack: technical drafts where marked Proposed/TBD; any unresolved business question **blocks dependent coding**.
5. Tracked diagram PNGs are references, not business authority; editable diagram sources are absent from this ZIP.

`CURRENT_STATE.md` lists the gate/next steps. Codex should inventory actual Git repo read-only before asserting existing code, services, migrations, secrets or DB data; do not delete/rewrite without owner approval.

## Current BusinessDay / reporting baseline — 2026-09-30

Manager is confirmed for explicit Start Day / End Day; no Owner inheritance is inferred. End Day is blocked by any unfinished Session, including Paused. One open BusinessDay per Branch; no midnight/restart automatic close/start. A complete BusinessDay belongs to its Branch-local start date/month/year (Africa/Cairo), even if it starts five minutes before a new month. At an explicit transition, previous EndedAtUtc equals next StartedAtUtc. Daily = one BusinessDay; longer reports = Monthly, selected six calendar months, Yearly. Revenue follows successful Payment.BusinessDayId, Expenses their recording BusinessDayId; Profit = Revenue − Expenses. Source: owner clarifications in DECISION_REQUESTS_R1.md. These requirements do not implement or authorize the workflows.
