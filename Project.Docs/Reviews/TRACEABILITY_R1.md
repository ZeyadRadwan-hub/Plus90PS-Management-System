# R1 — Requirements-to-design-to-test starter matrix (NOT final reconciliation)

**Status:** Draft cross-references by topic; exact FR/AC document IDs must be read/diffed in the canonical repo before declaring coverage complete. Don't invent completed tests.

| Rule/feature | Requirement source topic | Design owner in pack | Diagram(s) to re-check | Test IDs / verification |
|---|---|---|---|---|
| PIN and separate lease | BRD R1 auth/offline; RBAC matrix | `Security/SECURITY_DESIGN_R1.md` | State/PIN/License, Sequence auth, C4 | T-PIN-01, T-LEASE-01 |
| Start/pause/fixed/match/time/money | BRD R1 §§14–17, BUSINESS_RULES latest | `Architecture/SYSTEM_ANALYSIS_R1.md`, `Database/*` | Activity, Session State, ERD | T-BILL-01..06, T-SESSION-01..03 |
| Complete/cash/invoice, cancel | BRD R1 §18; UI cancel target | `Database/*`, `API/*`, `Offline_Sync/*` | ERD, Session→Invoice→Payment Sequence | T-FIN-01..03; paid cancel BLOCKED |
| Shifts/handover | Latest approved shift UX/rules | `Architecture/*`, `UX_UI/UI_IMPLEMENTATION_OVERRIDE_R1.md` | Shift Activity/Sequence, ERD | T-SHIFT-01/02 |
| Offline/local-first and central dedup | BRD R1 §22, DB storage map | `Offline_Sync/OFFLINE_SYNC_DESIGN_R1.md` | C4, DFD Level1, Sync Sequence | T-SYNC-01/02, T-FIN-01/02 |
| Role/branch permissions | BRD R1 auth/report, RBAC Matrix | `Security/*`, `Architecture/*` | Use Cases, DFD | T-RBAC-01 |
| EN/AR fixed UI target | UI Review/UX Flows/Design System + Override | `UX_UI/UI_IMPLEMENTATION_OVERRIDE_R1.md` | UI reference/screens only | T-UI-01 |
| Reports and monthly review | BRD R1 §21 | `API/*`, `Testing/*` | Reporting Activity/DFD | T-REPORT-01 |
| Backups and recovery | NFR R1 and agreed targets | `Deployment_Operations/*` | Deployment | T-DR-01 |

**Required final verification:** each actual active FR/AC ID maps to one or more test IDs, each P0 business/security decision maps to data/API/UX if relevant; obsolete R1 discount IDs remain retired with no re-use; paths/refs in the actual repo resolve; no test claims pass without executed evidence.
