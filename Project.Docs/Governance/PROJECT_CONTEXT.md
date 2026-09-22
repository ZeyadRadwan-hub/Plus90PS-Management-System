# +90 PS — Project Context for Codex and reviewers

**Current phase:** Release 1 pre-code design. Never read old `Final` as implemented. Developer/user: Zeyad; collaboration preferences are in root `AboutZeyad.md` (share only facts he explicitly wants in repo, and keep personal details out of public repositories).

## Product

Multi-branch-capable PlayStation shop management product, initial single shared Windows PC per branch, personal employee PIN and explicit branch/Owner/Manager/Cashier permissions. **R1:** operational sessions/PS4/PS5/Single/Multi/Open/Fixed Hours/Match/pricing/invoice/cash/shift/expenses/quantity-based internal assets/reports. Offline-first: WPF→local SQLite transaction (+outbox) online/offline; background sync→authorized ASP.NET Core API→central SQL Server. EF Core Code First is approved implementation approach **not yet implemented**. R2 customers/history/bookings/deposits; R3 advanced later. R1 no web/mobile/customer login/food/drink/product sales/discounts/new peripherals.

## Binding rules

- Billable positive seconds ceil minute, then round UP to next quarter hour only if 1–3 minutes remain; free Pause; no minimum. EGP .00–.49 drop fraction, .50–.99 up whole EGP, then move UP to next multiple of five only if +1 or +2 EGP.
- Fixed Hours expiry = alert only; Match = fixed branch price/defined duration and employee-ended; mode frozen during session.
- Owner/Manager can start/end shift for selected employee; handover before closing when sessions active, original opener retained.
- Protected Manager/Owner invoice Cancel/Void with optional reason; history kept; paid-invoice cash adjustment remains OPEN. Monthly closing is read-only financial review.
- First 5 PIN failures→warning+20s temporary lock/security event; another five→SecurityLocked/engineer process. Separate subscription lock. Offline paid lease >10d→max72h; <=10d→remaining paid time+10h; no local time tampering extension.
- UI target modifications in `UX_UI/UI_IMPLEMENTATION_OVERRIDE_R1.md` OVERRIDE old PNG control behavior; original images are visual/style references only.

## How we reached this state

Original broad roadmap contains multiple old/duplicated .md versions and outdated discounts/settings. Current newer R1 requirements and business decisions supersede those **for conflicting behaviors only**, not wholesale entire documents. Zeyad authored all requested R1 Use Case/Activity/Sequence/State/C4/Deployment/Logical ERD/DFD diagrams. Pictures of ERD/DFD have readability/contract gaps: targeted diagram edits after related designs. The previous `PROJECT_REVIEW_AND_CHANGE_REGISTER.md` tracks named corrections. Source-tree inventory and true repository code state have NOT been verified in this design-only deliverable. Current pack is a reviewable **draft**, not a branch deployment or signed-off API/schema/security plan.

## Reference priority BEFORE reconciliation

1. Explicit latest user-approved decisions listed above and `Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md` (when placed in repo).
2. Latest validated governance decisions and R1 business/acceptance rules; check duplicates/old versions, never blindly trust filename `Final`.
3. Approved R1 UX target override, plus specific up-to-date requirements; old PNGs only styling reference if they contradict target behavior.
4. This design pack: technical drafts where marked Proposed/TBD; any unresolved business question **blocks dependent coding**.
5. Editable diagram sources; diagrams and images do not independently create business policies.

`CURRENT_STATE.md` lists the gate/next steps. Codex should inventory actual Git repo read-only before asserting existing code, services, migrations, secrets or DB data; do not delete/rewrite without owner approval.
