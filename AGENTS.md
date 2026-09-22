# +90 PS — Codex agent instructions

**STOP BEFORE CODING:** `Project.Docs/Reviews/PRE_CODE_GATE_R1.md` currently says **HOLD**. You may inventory repo and review docs in read-only mode if asked, but MUST NOT generate/modify production code, EF Models, DbContext, migrations, SQL schema, backend, WPF, seeds or scripts that mutate user data until Zeyad explicitly approves a defined implementation slice and gate is updated. Do not interpret existence of this file as coding authorization.

## Read order

1. `AboutZeyad.md` — desired collaboration/teaching style, NOT business rules; do not copy personal details into generated source/logs.
2. `Project.Docs/Governance/PROJECT_CONTEXT.md`, `Project.Docs/Reviews/CURRENT_STATE.md`, `Project.Docs/Reviews/PRE_CODE_GATE_R1.md`.
3. `Project.Docs/Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md` if placed in repo; `Project.Docs/Governance/DECISION_REQUESTS_R1.md`; latest canonical approved Business Rules/DECISIONS and current R1 FR/NFR/AC.
4. `Project.Docs/Releases/R1/{Database,Architecture,API,Security,Offline_Sync,UX_UI,Testing,Deployment_Operations}/*` as needed. Draft/Proposed/TBD are **not** approvals.
5. `Project.Digrams/R1/` editable diagrams; UI images are styling baseline only where they contradict `UI_IMPLEMENTATION_OVERRIDE_R1.md`.

## Non-negotiable principles

- R1 Windows WPF + branch-local SQLite + API + central SQL Server, EF Core Code First **after gate**. Same branch local-first path online/offline, durable outbox; no direct WPF→central SQL Server writes. Server independently authorizes branch/owner scope and validates financial events.
- Respect exact latest billing, free pause, shift handover, invoice Cancel reason optional and history, PIN/security/lease rules. Do **NOT** invent paid invoice cash reversal or offline role/pricing administration. R1 does not include discounts, Customer/Booking, website/mobile, product/cafe sales or extra peripherals.
- Old screenshots (37 EN + 37 AR, incl retired 36 Security Settings) are NOT corrected UI. Actual WPF implements `UX_UI/UI_IMPLEMENTATION_OVERRIDE_R1.md` and approved style; role guards require service/API enforcement.
- Do not guess what existing code or data is in the repo. Before any modifying action: run read-only repo inventory, report relevant files/migrations/DB and `git status`, propose a narrow diff and tests, seek user consent. Never delete existing data or rewrite unrelated user work.
- Return short, useful Egyptian Arabic explanations with English technical terms; explain decisions so Zeyad learns, no unnecessary lengthy repetition. Ask once in a consolidated list when an essential business rule is unknown.
- On approved code slice: implement only explicit scope; run relevant tests; show actual failures/success with evidence; update docs and `CURRENT_STATE.md` only when verified. Never claim R1/UI/diagrams/QA complete based on authored docs or PNGs.
