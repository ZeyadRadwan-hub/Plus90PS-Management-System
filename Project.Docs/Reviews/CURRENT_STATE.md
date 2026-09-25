# +90 PS — Current State (2026-09-25)

**Status:** BE-00 backend foundation, BE-01 billable-time calculation, BE-02 pure Domain session timing/pause lifecycle, BE-03 immutable session terms/pricing snapshot, BE-04 pure Domain money rounding, BE-05 pure Domain pricing catalog, and BE-06 pure Domain Branch/GameConsole identity and activation foundation exist. BE-01.5 configures controller-based ASP.NET Core Web API startup with development-only Swagger UI; BE-01.6 simplifies Solution Explorer. REPO-01 reconciliation is implemented. BE-06 also documents the owner-approved R1 technical baseline in [TECHNICAL_DECISIONS_R1.md](../Governance/TECHNICAL_DECISIONS_R1.md). It does not implement ConsoleOccupancy, full Session, pricing management, hourly charge formula, EF Core/databases, PIN/auth/security, Invoice/Payment/Cancel, Outbox/sync, or WPF. The wider R1 pre-code gate remains on HOLD for unapproved work.

| Area | True status |
|---|---|
| Scope/business/UX R1 | Major documents and target UX drafted/approved at requirement level; REPO-01 marks historical material and resolves the identified stale decisions, while missing design-pack sources still need recovery/review |
| Diagrams | Rendered PNGs are tracked under `Project.Digrams/R1/`; no editable `.drawio`/PlantUML source is tracked in this worktree. Logical ERD and DFD visual/contract review actions remain |
| Database | Approved provider/storage baseline is documented; no EF Core contexts, migrations, SQLite/SQL Server database, `DATABASE_DESIGN_R1.md`, or `DATA_DICTIONARY_R1.md` is tracked. Physical schema remains a later design/implementation task |
| Architecture/API/Security/Offline-sync | Technical baseline approved/documented; only API startup and development Swagger are coded. No business API, authentication/security, offline authority, Outbox/Inbox, or sync implementation exists |
| Tests/hosting/DR | Health, development Swagger, billable-time, session-timing, session-terms, money-rounding, pricing-catalog, Branch, and GameConsole tests pass; no deployment or restoration validated; provider/machine details not confirmed |
| UI | Approved target corrections documented as intended. Supplied ZIP still old 37 EN + 37 AR with obsolete Screen 36; actual WPF UI not verified/implemented |
| Legacy code | Inventory before BE-00 found no existing .NET projects or database implementation here; `CODE_REVIEW.md` is explicitly a historical review of another older snapshot |
| Codex | BE-00, BE-01, BE-01.5, BE-01.6, BE-02, BE-03, BE-04, BE-05, and BE-06 explicitly approved / implemented; REPO-01 implemented. BE-07 and later slices remain unauthorized |

**Next:** Resolve remaining partial-payment, unpaid Cancel/Void distinction, ownership/cross-branch staffing, reporting timezone/day cutoff, and deployment/provider choices as relevant; approve each later slice explicitly before any full Session, hourly charge formula, pricing management, invoice/payment workflow, or persistence implementation.
