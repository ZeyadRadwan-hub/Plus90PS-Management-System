# +90 PS — Pre-code design pack: what to copy and where

**Created:** 2026-09-22. **Status:** complete draft PACK of current pre-code phases; NOT an approved Code First schema, final executable OpenAPI, closed historical-doc reconciliation, fixed diagrams, generated app, or green code gate.

1. The outer archive has folder `PLUS NINETY PS/` and contains **new/continuing design docs, the existing change register, and AGENTS.md**. Extract into a **review copy of your repo root** first and compare file names; do not blindly overwrite your actual sources. You already have `AboutZeyad.md` (keep it); this archive also includes the unchanged prior `Project.Docs/Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md` as context; compare before replacing any newer copy.
2. Continuing file `Project.Docs/Releases/R1/Database/DATABASE_DESIGN_R1.md` includes the old Section 01 with added Sections 02–05; compare your on-disk version before replacing it.
3. Newly written docs are organized under `Project.Docs/Releases/R1/{Database,Architecture,API,Security,Offline_Sync,Testing,Deployment_Operations,UX_UI}`; governance docs under `Project.Docs/Governance/`, review docs under `Project.Docs/Reviews/`, milestone plan under `Project.Docs/Planning/`; `AGENTS.md` is at repo root.
4. `Project.Digrams` spelling is kept EXACTLY as Zeyad's current folder name; do not rename it accidentally. No `.drawio` or PNG was modified and no new code/DB was created.
5. Put only the project/collaboration information you're comfortable sharing with Codex in `AboutZeyad.md`. If the repo is public, review that file before committing. Never commit PINs, API keys, private signing keys, branch DB files or backups.
6. Review open decisions, update dependent design docs, reconcile canonical copies and targeted diagrams, then ask Zeyad for explicit approval. `PRE_CODE_GATE_R1.md` is **HOLD** until that happens. A request to create design files is NOT permission to start coding.

## Key paths inside your actual tree

`PLUS NINETY PS/AGENTS.md`  
`PLUS NINETY PS/Project.Docs/Governance/{PROJECT_CONTEXT.md, DECISION_REQUESTS_R1.md}`  
`PLUS NINETY PS/Project.Docs/Reviews/{PROJECT_REVIEW_AND_CHANGE_REGISTER.md, CURRENT_STATE.md, PRE_CODE_GATE_R1.md, DOCUMENT_RECONCILIATION_R1.md, TRACEABILITY_R1.md, INSTALL_AND_README_PRECODE_R1.md}`  
`PLUS NINETY PS/Project.Docs/Releases/R1/Database/{DATABASE_DESIGN_R1.md, DATA_DICTIONARY_R1.md}`  
`PLUS NINETY PS/Project.Docs/Releases/R1/Architecture/{SYSTEM_ANALYSIS_R1.md, ARCHITECTURE_R1.md, DIAGRAM_RECONCILIATION_R1.md}`  
`PLUS NINETY PS/Project.Docs/Releases/R1/API/API_CONTRACT_R1.md`  
`PLUS NINETY PS/Project.Docs/Releases/R1/Security/SECURITY_DESIGN_R1.md`  
`PLUS NINETY PS/Project.Docs/Releases/R1/Offline_Sync/OFFLINE_SYNC_DESIGN_R1.md`  
`PLUS NINETY PS/Project.Docs/Releases/R1/Testing/TEST_STRATEGY_R1.md`  
`PLUS NINETY PS/Project.Docs/Releases/R1/Deployment_Operations/DEPLOYMENT_OPERATIONS_R1.md`  
`PLUS NINETY PS/Project.Docs/Releases/R1/UX_UI/UI_IMPLEMENTATION_OVERRIDE_R1.md`  
`PLUS NINETY PS/Project.Docs/Planning/IMPLEMENTATION_SLICES_R1.md`

## Source of truth warning

This pack is a **multi-phase draft with explicit blocking questions**. It does not update duplicate `MASTER(1).md`/`R1_OPERATIONAL_MVP(1).md`/`README(1).md`, old UI ZIP, or draw.io sources. Reconcile changes only after checking actual copies and their status. For pending invoice cash reversal, staff permissions offline, ownership model and lease recovery, see `DECISION_REQUESTS_R1.md` rather than letting Codex improvise.
