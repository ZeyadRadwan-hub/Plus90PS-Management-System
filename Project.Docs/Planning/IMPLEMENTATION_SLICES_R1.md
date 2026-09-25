# R1 — Proposed Codex implementation slices AFTER approved gate

**NOT an order to code now.** Start only after `Reviews/PRE_CODE_GATE_R1.md` is explicitly marked approved by Zeyad; preserve actual repo/data. Scope each slice with exact paths, acceptance tests and rollback; no shotgun multi-release implementation.

| Slice | Proposed content | Must be approved beforehand |
|---|---|---|
| C0 read-only | Inspect git status/tree, actual .NET solution, migration/db state, backup/restore risks; report discrepancies with docs; no edits | Zeyad authorizes Codex repo access; safe even before implementation gate if read-only |
| C1 foundation | domain state/value contracts, local/central EF Core Code First plans, provider-specific migrations and test harness, explicit branch/device identity contracts | DB dictionary, ownership, security architecture and approval for files/migrations |
| C2 local branch operations | SQLite transaction+outbox, Console/Pricing/Session events, duration rules, free pause and fixed alert with local recovery tests | Session time/money/pricing schema and local permissions |
| C3 financial/shift | Complete/invoice/cash, protected cancel, payment/reversal semantics, selected employee shift+handover, expenses/assets | Open Q-BIZ-01 and narrowed Q-BIZ-02 plus affected states/tests; Q-BIZ-03 is resolved (one Match per Session) |
| C4 API/central sync | Server auth+branch scoping, central SQL schema, idempotent receipt, replay/ack/conflict and signed reference config | API/Security/Sync contract, offline admin decisions |
| C5 WPF | EN/AR MVVM screens implementing **intended corrected UI**, not old PNG mismatches | UX target override and relevant slice use cases |
| C6 operations/pilot | hosting config, protected secrets, migrations, backup/restore, monitoring/UAT, branch deployment | Q-OPS-01, security review, DR verification |

**Every slice exit:** changed code/DB paths, named approved requirement IDs, tests with actual outputs, real migration safeguards, security/offline effects, updated CURRENT_STATE and CHANGELOG. Do not mark R1 Complete from generated diagrams, README or a successful isolated unit test.
