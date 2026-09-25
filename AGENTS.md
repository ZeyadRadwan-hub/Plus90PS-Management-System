# +90 PS — Codex agent instructions

**STOP BEFORE CODING:** `Project.Docs/Reviews/PRE_CODE_GATE_R1.md` currently says **HOLD**. You may inventory repo and review docs in read-only mode if asked, but MUST NOT generate/modify production code, EF Models, DbContext, migrations, SQL schema, backend, WPF, seeds or scripts that mutate user data until Zeyad explicitly approves a defined implementation slice and gate is updated. Do not interpret existence of this file as coding authorization.

## Read order

1. `AboutZeyad.md` — desired collaboration/teaching style, NOT business rules; do not copy personal details into generated source/logs.
2. `Project.Docs/Governance/PROJECT_CONTEXT.md`, `Project.Docs/Reviews/CURRENT_STATE.md`, `Project.Docs/Reviews/PRE_CODE_GATE_R1.md`.
3. `Project.Docs/Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md` if placed in repo; `Project.Docs/Governance/DECISION_REQUESTS_R1.md`; latest canonical approved Business Rules/DECISIONS and current R1 FR/NFR/AC.
4. The tracked Release 1 folder is `Project.Docs/Releases/Release 1/`; its `R1_OPERATIONAL_MVP.md` is a historical planning snapshot. The earlier proposed Database/Architecture/API/Security/Offline_Sync/UX_UI/Testing/Deployment_Operations design-pack files are not tracked in this worktree. Do not invent their contents or treat Draft/Proposed/TBD as approval.
5. `Project.Digrams/R1/` currently contains rendered PNGs, not tracked editable `.drawio`/PlantUML sources. Old UI images are styling references only; the proposed `UI_IMPLEMENTATION_OVERRIDE_R1.md` is not tracked here. Available UX references include `Project.Docs/Reviews/UI_REVIEW_R1(1).md`, `Project.Docs/Planning/UX_FLOWS_R1(2).md`, and `Project.Docs/Requirements/DESIGN_SYSTEM_R1(1).md`; report gaps rather than guessing.

## Non-negotiable principles

- R1 Windows WPF + branch-local SQLite + API + central SQL Server, EF Core Code First **after gate**. Same branch local-first path online/offline, durable outbox; no direct WPF→central SQL Server writes. Server independently authorizes branch/owner scope and validates financial events.
- Respect exact latest billing, free pause, shift handover, invoice Cancel reason optional and history, PIN/security/lease rules. Do **NOT** invent paid invoice cash reversal or offline role/pricing administration. R1 does not include discounts, Customer/Booking, website/mobile, product/cafe sales or extra peripherals.
- Old screenshots (37 EN + 37 AR, incl retired 36 Security Settings) are NOT corrected UI. The intended UI corrections in the approved requirements/change register govern future WPF; the proposed `UI_IMPLEMENTATION_OVERRIDE_R1.md` is absent from this tracked worktree. Role guards require service/API enforcement.
- Do not guess what existing code or data is in the repo. Before any modifying action: run read-only repo inventory, report relevant files/migrations/DB and `git status`, propose a narrow diff and tests, seek user consent. Never delete existing data or rewrite unrelated user work.
- Return short, useful Egyptian Arabic explanations with English technical terms; explain decisions so Zeyad learns, no unnecessary lengthy repetition. Ask once in a consolidated list when an essential business rule is unknown.
- On approved code slice: implement only explicit scope; run relevant tests; show actual failures/success with evidence; update docs and `CURRENT_STATE.md` only when verified. Never claim R1/UI/diagrams/QA complete based on authored docs or PNGs.
