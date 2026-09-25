# +90 PS R1 — PRE-CODE GATE

**CURRENT STATE: HOLD for other business implementation.** BE-00 Backend Foundation and BE-01 Billable Time Domain Rules are the only approved implementation slices. This is a completed **checklist template and draft design package**, not completion of every design decision. No persistence, session, money, authentication, sync, or frontend work is unlocked.

| Scope | Authorization |
|---|---|
| BE-00 Backend Foundation | APPROVED FOR IMPLEMENTATION — 2026-09-25 |
| BE-01 Billable Time Domain Rules | APPROVED / IMPLEMENTED — unit tested |
| Database implementation | NOT AUTHORIZED YET |
| EF Core persistence | NOT AUTHORIZED YET |
| Authentication | NOT AUTHORIZED YET |
| Employees/RBAC | NOT AUTHORIZED YET |
| Console/Pricing | NOT AUTHORIZED YET |
| Sessions | NOT AUTHORIZED YET |
| Invoices/Payments | NOT AUTHORIZED YET |
| Offline Sync | NOT AUTHORIZED YET |
| WPF Frontend | NOT AUTHORIZED TO CODEX |

## What exists as draft outputs of this package

- Database storage mapping, field-by-field current ERD inventory and Code First principles; proposed physical mapping, not final schema.
- System analysis and architecture/transaction boundaries.
- Candidate API contract **skeleton** (NOT approved OpenAPI), security/threat model and sync failure matrix.
- QA/UAT plan and deployment/backup/DR targets (NOT test results or live backups).
- Approved intended WPF corrections (old screenshots NOT modified) and targeted diagram reconciliation plan (editable draw.io NOT modified).
- Document reconciliation plan, decision requests, Codex handoff files.

## Formal gate checklist

| Gate check | Status now | Evidence required to change to Done |
|---|---|---|
| User approves true product decisions Q-BIZ-01..Q-OPS-01 relevant to first implementation slice | BLOCKED | Written answers, owner/date, updated canonical rules and dependent docs |
| Database dictionary: exact fields/types/FK/NULL/unique, local-central mapping, provider-specific plan | DRAFT | Signed-off dictionary & schema design; SQLite/SQL Server differences tested later |
| Session/financial state and cash cancellation are consistent | BLOCKED | Explicit amounts/payment flow, rounding/version/paid-cancel model + acceptance tests |
| API endpoint/DTO/error/retry/OpenAPI contract reviewed | DRAFT | Complete versioned contract with matching auth and state schemas |
| PIN/device/lease/recovery and branch ownership security reviewed | BLOCKED | Threat model and signed authority/revocation policy approved |
| Offline-sync event/receipt/ordering/conflict resolution design reviewed | DRAFT | Stable keys, failure matrix, safe correction path, local/central transactions specified |
| UI implementation contract accepted as target, screenshots labeled old | DOC DONE / IMPLEMENTATION NOT STARTED | UI reviewer consent; later WPF EN/AR and RBAC tests |
| Diagrams consistent with approved design and readable | PARTIAL | Editable sources fixed only where affected, exports reviewed |
| Duplicate/legacy documents reconciled in actual repo | NOT DONE | canonical index + diffs + archived old versions + links checked |
| Real Git repository inventory incl. any existing code/migrations/data | BE-00 INVENTORY DONE | Clean `codex/r1-backend` worktree before BE-00; no existing .NET projects, database code, migrations, or data found in this worktree |
| Zeyad explicitly authorizes Codex coding slices | BE-00 and BE-01 ONLY APPROVED | Direct narrow task authorizations; all other slices remain on HOLD |

## Safe next order

1. Zeyad resolves genuinely unanswered business questions **once** (do not reopen already fixed policies).
2. Revise affected dictionary/architecture/API/security/sync/test documents and approve a small initial implementation slice. Determine if any open question can be legitimately deferred without touching that slice.
3. Compare with **actual** files in `PLUS NINETY PS/` and carry out reconciliation; edit only necessary diagrams, never declare old screenshots fixed.
4. Inspect actual repository without changing code; protect existing assets/data, set tests and migration plan.
5. Record explicit approval: `APPROVED BY ZEYAD | date | allowed first slice | accepted unresolved items scoped out`. **Only then** say «هنا يبدأ Codex» and give it implementation instructions.

**Important:** The assistant may prepare and review design documents before this approval. The instruction “اعملهم كلهم” to complete pre-code paperwork is **not** authorization to invent missing business rules, operate on the user's local repo, or start implementation.
