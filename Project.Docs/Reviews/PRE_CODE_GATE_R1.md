# +90 PS R1 — PRE-CODE GATE

**CURRENT STATE: HOLD for other business implementation.** BE-00 through BE-08 are the explicitly approved narrow slices listed below; REPO-01 is implemented. BE-06 records the approved [R1 technical baseline](../Governance/TECHNICAL_DECISIONS_R1.md); BE-07 adds only pure Domain Session aggregate core. BE-08 adds only pure Domain Business, Branch.BusinessId, Employee/EmployeeRole, and active same-Business scope; Q-ORG-01 is resolved. Device replacement/reprovisioning and query-driven B-Tree/B+Tree indexing are approved **documentation only**. A documented technical decision is **not** implemented Database/Auth/Sync code or a globally approved physical schema. No PIN/authentication, RBAC permission enforcement, role changes, Employee management API, Device implementation, ConsoleOccupancy, persistence/indexes, responsibility transfer, hourly charge formula, pricing management, invoice/payment workflow, sync, or frontend work is unlocked.

| Scope | Authorization |
|---|---|
| BE-00 Backend Foundation | APPROVED FOR IMPLEMENTATION — 2026-09-25 |
| BE-01 Billable Time Domain Rules | APPROVED / IMPLEMENTED — unit tested |
| BE-02 Session Timing & Pause Lifecycle | APPROVED / IMPLEMENTED — unit tested; timing only |
| BE-03 Session Types & Pricing Context Domain | APPROVED / IMPLEMENTED — unit tested; terms and pricing snapshot only |
| BE-04 Money Rounding Domain Rules | APPROVED / IMPLEMENTED — unit tested; supplied amount only |
| BE-05 Pricing Matrix Domain | APPROVED / IMPLEMENTED — unit tested; immutable entries/catalog and existing snapshot only |
| BE-06 Branch & Console Domain Foundation | APPROVED / IMPLEMENTED — unit tested; supplied IDs, Branch, GameConsole configuration/activation only |
| BE-07 Session Aggregate Core | APPROVED / IMPLEMENTED — unit tested; pure Domain identity, terms, timing lifecycle, Pause eligibility, captured pricing only |
| BE-08 Business, Employee & Role Scope Domain Foundation | APPROVED / IMPLEMENTED — unit tested; Business, Branch.BusinessId, Employee/EmployeeRole, activation and same-Business scope only |
| R1 technical architecture baseline | APPROVED / DOCUMENTED — future design direction, **not** Database/Auth/Sync implementation |
| Device replacement/reprovisioning and query-driven B-Tree/B+Tree indexing policy | APPROVED / DOCUMENTED — future design only; no Device or database index code |
| REPO-01 Repository & Documentation Reconciliation | IMPLEMENTED — documentation/repository status only; no business code |
| Database implementation | NOT AUTHORIZED YET |
| EF Core persistence | NOT AUTHORIZED YET |
| Authentication | NOT AUTHORIZED YET |
| PIN/Auth/RBAC enforcement, role changes, Employee management API | NOT AUTHORIZED YET |
| Device entity/provisioning | NOT AUTHORIZED YET |
| Database indexes | NOT AUTHORIZED YET |
| ConsoleOccupancy / Console persistence / Pricing management | NOT AUTHORIZED YET |
| Session persistence, events, occupancy, and responsibility transfer | NOT AUTHORIZED YET |
| Invoices/Payments | NOT AUTHORIZED YET |
| Offline Sync | NOT AUTHORIZED YET |
| WPF Frontend | NOT AUTHORIZED TO CODEX |

## Historical design-pack outputs (not all tracked here)

- The older pack manifest names Database/Data Dictionary, Architecture/System Analysis, API contract, Security, Offline Sync, Testing, Deployment, and UI Override drafts. Those eleven named files are **not tracked in this worktree**; do not infer their contents from the manifest.
- Available requirements and planning documents discuss local-first architecture, Code First direction, and candidate data/API/security/sync design, but none constitutes an approved physical schema or deployed contract.
- QA/UAT plan and deployment/backup/DR targets (NOT test results or live backups).
- Approved intended WPF corrections (old screenshots NOT modified) and targeted diagram reconciliation plan. This worktree tracks rendered PNG diagrams only; editable `.drawio`/PlantUML sources are not present here.
- Document reconciliation plan, decision requests, Codex handoff files.

## Formal gate checklist

| Gate check | Status now | Evidence required to change to Done |
|---|---|---|
| User approves true product decisions relevant to later slices | PARTIAL | Pause eligibility and paid-invoice prohibition resolved in BE-06; Q-ORG-01 ownership/staffing resolved in BE-08; partial Payment, unpaid Cancel/Void, reporting boundary, and hosting remain open where relevant |
| Database dictionary: exact fields/types/FK/NULL/unique, local-central mapping, provider-specific plan | DRAFT | Signed-off dictionary & schema design; SQLite/SQL Server differences tested later |
| Session/financial state and cash cancellation are consistent | PARTIAL | Paid Invoice cannot be cancelled; future full workflow must define partial Payment and eligible unpaid Cancel/Void states and test them |
| API endpoint/DTO/error/retry/OpenAPI contract reviewed | DRAFT | Complete versioned contract with matching auth and state schemas |
| PIN/device/lease/recovery and branch ownership security reviewed | BASELINE APPROVED / IMPLEMENTATION NOT STARTED | Technical baseline recorded; concrete threat review, ownership grants, provisioning, keys, and adversarial tests remain |
| Offline-sync event/receipt/ordering/conflict resolution design reviewed | BASELINE APPROVED / IMPLEMENTATION NOT STARTED | Technical baseline recorded; concrete schema/contracts, failure tests, and rollout still remain |
| UI implementation contract accepted as target, screenshots labeled old | DOC DONE / IMPLEMENTATION NOT STARTED | UI reviewer consent; later WPF EN/AR and RBAC tests |
| Diagrams consistent with approved design and readable | PARTIAL | Editable sources are not tracked in this worktree; obtain/inspect real sources before claiming source/export reconciliation |
| Duplicate/legacy documents reconciled in actual repo | REPO-01 PARTIAL | Current-vs-historical status and identified contradictions reconciled; missing design-pack files and old historical links still require source recovery/review |
| Real Git repository inventory incl. any existing code/migrations/data | BE-00 INVENTORY DONE | Clean `codex/r1-backend` worktree before BE-00; no existing .NET projects, database code, migrations, or data found in this worktree |
| Zeyad explicitly authorizes Codex coding slices | BE-00 through BE-08 narrow slices approved as listed above | Direct narrow task authorizations; BE-09, DB-01, and later remain on HOLD |

## Safe next order

1. Zeyad resolves genuinely unanswered business questions **once** (do not reopen already fixed policies).
2. Revise affected dictionary/architecture/API/security/sync/test documents and approve a small initial implementation slice. Determine if any open question can be legitimately deferred without touching that slice.
3. Compare with **actual** files in `PLUS NINETY PS/` and carry out reconciliation; edit only necessary diagrams, never declare old screenshots fixed.
4. Inspect actual repository without changing code; protect existing assets/data, set tests and migration plan.
5. Record explicit approval: `APPROVED BY ZEYAD | date | allowed first slice | accepted unresolved items scoped out`. **Only then** say «هنا يبدأ Codex» and give it implementation instructions.

**Important:** The assistant may prepare and review design documents before this approval. The instruction “اعملهم كلهم” to complete pre-code paperwork is **not** authorization to invent missing business rules, operate on the user's local repo, or start implementation.
