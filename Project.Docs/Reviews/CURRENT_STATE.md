# +90 PS — Current State (2026-09-25)

**Status:** BE-00 backend foundation, BE-01 billable-time calculation, BE-02 pure Domain session timing/pause lifecycle, BE-03 immutable session terms/pricing snapshot, BE-04 pure Domain money rounding, and BE-05 pure Domain pricing catalog exist. BE-01.5 configures a conventional controller-based ASP.NET Core Web API with development-only Swagger UI, and BE-01.6 simplifies Solution Explorer. REPO-01 repository/documentation reconciliation is implemented. BE-05 selects an applicable price for one already-selected branch context and creates the existing immutable snapshot; it does not implement a Branch entity or pricing management. The full Session aggregate, hourly charge formula, pricing management, persistence/database, invoices/payments, authentication, sync, and WPF frontend are not implemented. The wider R1 pre-code gate remains on HOLD.

| Area | True status |
|---|---|
| Scope/business/UX R1 | Major documents and target UX drafted/approved at requirement level; REPO-01 marks historical material and resolves the identified stale decisions, while missing design-pack sources still need recovery/review |
| Diagrams | Rendered PNGs are tracked under `Project.Digrams/R1/`; no editable `.drawio`/PlantUML source is tracked in this worktree. Logical ERD and DFD visual/contract review actions remain |
| Database | No `DATABASE_DESIGN_R1.md` or `DATA_DICTIONARY_R1.md` is tracked in this worktree. Earlier pack/manifest describes proposed drafts, not present files; physical schema and SQLite/SQL Server mapping remain open |
| Architecture/API/Security/Offline-sync | Controller-based API startup and development Swagger UI are configured; business API routes and security method remain proposed, not formal approved contracts |
| Tests/hosting/DR | Health, development Swagger, billable-time, session-timing, session-terms, money-rounding, and pricing-catalog tests pass; no deployment or restoration validated; provider/machine details not confirmed |
| UI | Approved target corrections documented as intended. Supplied ZIP still old 37 EN + 37 AR with obsolete Screen 36; actual WPF UI not verified/implemented |
| Legacy code | Inventory before BE-00 found no existing .NET projects or database implementation here; `CODE_REVIEW.md` is explicitly a historical review of another older snapshot |
| Codex | BE-00, BE-01, BE-01.5, BE-01.6, BE-02, BE-03, BE-04, and BE-05 explicitly approved / implemented; REPO-01 documentation reconciliation implemented. BE-06 and later slices remain unauthorized |

**Next:** Resolve the relevant open decisions and approve a separate later slice before any full Session, hourly charge formula, pricing management, invoice/payment workflow, or persistence implementation.
