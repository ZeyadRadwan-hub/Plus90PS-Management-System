# +90 PS — Current State (2026-09-25)

**Status:** BE-00 backend foundation, BE-01 billable-time calculation, BE-02 pure Domain session timing/pause lifecycle, and BE-03 immutable session terms/pricing snapshot exist. BE-01.5 configures a conventional controller-based ASP.NET Core Web API with development-only Swagger UI, and BE-01.6 simplifies Solution Explorer. BE-03 does not implement the full Session aggregate, pricing management, final money calculation, persistence/database, financial modules, authentication, sync, or WPF frontend. The wider R1 pre-code gate remains on HOLD.

| Area | True status |
|---|---|
| Scope/business/UX R1 | Major documents and target UX drafted/approved at requirement level; older duplicate docs still need reconciliation |
| Diagrams | User says all requested diagram types were drawn. Logical ERD and DFD submitted as screenshots; visual/contract review actions remain |
| Database | Local/central ownership map in existing `DATABASE_DESIGN_R1.md`; extended draft, detailed inventory and open physical schema decisions in this pack |
| Architecture/API/Security/Offline-sync | Controller-based API startup and development Swagger UI are configured; business API routes and security method remain proposed, not formal approved contracts |
| Tests/hosting/DR | Health, development Swagger, billable-time, session-timing, and session-terms tests pass; no deployment or restoration validated; provider/machine details not confirmed |
| UI | Approved target corrections documented as intended. Supplied ZIP still old 37 EN + 37 AR with obsolete Screen 36; actual WPF UI not verified/implemented |
| Legacy code | Repository inventory before BE-00 found no existing .NET projects or database implementation in this worktree; old `CODE_REVIEW.md` may describe a different snapshot |
| Codex | BE-00, BE-01, BE-01.5, BE-01.6, BE-02, and BE-03 Session Types & Pricing Context Domain explicitly approved / implemented; BE-04 and later slices remain unauthorized |

**Next:** Resolve the relevant open decisions and approve a separate later slice before any full Session, pricing management, final billing, or persistence implementation.
