# +90 PS — Documentation reconciliation (2026-09-30)

Scope: approved documentation reconciliation only. No code, EF models, migrations, SQL schema, databases or physical indexes were changed. HOLD remains. Sources: inspected uploaded ZIP; owner messages directly visible in the current conversation. This is not a complete old-chat export.

BusinessDay start-month/year attribution and explicit shared next-day boundary are approved owner requirements. Half-open intervals and atomic transition ordering are engineering conventions to finalize in the future workflow, not an additional owner quote. Manager and no-close-with-running-Session answers are preserved literally in DECISION_REQUESTS_R1.md. Paused is unfinished; ordinary application Exit specifics remain a later contract.

Latest completed backend: BE-08. Latest completed DB design: DB-01.1, Ready for Review / not implemented in ZIP. Earlier contextual chat reports approval; the full verbatim report is unavailable, so no physical-design sign-off is fabricated. Tests: latest reported 206 passed / 0 failed, independently 206 static cases; not run here. Next backend: BE-09, not authorized. Q-OPS-01 remains open; no Owner permission inheritance or hourly formula is invented.

## Changed existing files

- `Project.Docs/Governance/CHANGELOG.md`
- `Project.Docs/Governance/DECISIONS.md`
- `Project.Docs/Governance/DECISION_REQUESTS_R1.md`
- `Project.Docs/Governance/PROJECT_CONTEXT.md`
- `Project.Docs/Governance/TECHNICAL_DECISIONS_R1.md`
- `Project.Docs/Releases/Release 1/DATABASE_DESIGN_R1.md`
- `Project.Docs/Releases/Release 1/DATA_DICTIONARY_R1.md`
- `Project.Docs/Releases/Release 1/INDEX_QUERY_PLAN_R1.md`
- `Project.Docs/Requirements/ACCEPTANCE_CRITERIA_R1(2).md`
- `Project.Docs/Requirements/BRD_R1(2).md`
- `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`
- `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`
- `Project.Docs/Requirements/PRD_R1(2).md`
- `Project.Docs/Requirements/RBAC_MATRIX_R1.md`
- `Project.Docs/Reviews/CURRENT_STATE.md`
- `Project.Docs/Reviews/DOCUMENT_RECONCILIATION_R1.md`
- `Project.Docs/Reviews/PRE_CODE_GATE_R1.md`
- `README.md`

## Exact old/current wording

### Change 001 — `Project.Docs/Governance/DECISION_REQUESTS_R1.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Owner clarifications — 2026-09-30 (documentation only)

These are requirement decisions, not implementation authorization or DB-01.1 physical-design sign-off. The wider gate stays HOLD; BE-09 is not authorized.

- **Start Day / End Day actor:** the owner answered “1.المدير و يعني ايه RBAC”. Manager is the confirmed role. Do not silently infer Owner role inheritance or Cashier permission. RBAC means Role-Based Access Control; future service/API enforcement must check permissions and scope.
- **No close with an unfinished Session:** “2.مينفعش ال System يتقفل و فيه Session شغاله”. In the End Day discussion, closing the BusinessDay must be blocked while any Session is unfinished, including a paused Session; pause is not completion. No automatic completion or Shift handover satisfies this guard. Exact WPF normal-exit handling is a later UX/engineering contract; crash/power-loss recovery remains required.
- **Calendar attribution — Q-REPORT-01 clarification:** “لو بدأ 30 او حتي قبل الشهر الجديد بي 5 دقائق يتحسب للشهر الي بدأ فيه”. The entire BusinessDay belongs to the calendar date/month/year of its Start Day instant, interpreted in the Branch reporting timezone (R1 Africa/Cairo), regardless of its End Day date. Example: 30 September 23:55 → 1 October belongs wholly to September. Revenue and Expenses still follow their assigned BusinessDayId; Payment may have a different day from Invoice issuance. Do not split a BusinessDay at midnight or a month/year edge.
- **Explicit next-day transition:** the owner requested “نهايه اليوم الي فات هو بدايه اليوم الجديد”. At an explicit transition from one BusinessDay to the next, old EndedAtUtc = new StartedAtUtc, using one shared transition instant. This is not automatic midnight closure/opening. As an engineering interval convention, use [start, end) and an ordered, atomic local transition so there is neither overlap nor double attribution at the boundary; finalize its command/transaction contract in an authorized workflow task. A standalone End Day does not silently create a new day.

These statements replace the older provisional start-label policy and unanswered Manager/End Day wording in the current documents. Only Q-OPS-01 remains in the required owner-ticket table. Owner inheritance, the complete hourly charge formula and other future workflow contracts are not invented by this amendment.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 002 — `Project.Docs/Governance/DECISION_REQUESTS_R1.md`

OLD:

````text
branch timezone defaults to Africa/Cairo and serves calendar display/period labels.
````

CURRENT:

````text
branch timezone defaults to Africa/Cairo and serves calendar display/period labels derived from StartedAtUtc. The entire BusinessDay belongs to its local start date/month/year, including any later receipts or expenses assigned to that day; no midnight/month-edge split. See the 2026-09-30 owner clarifications below.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 003 — `README.md`

OLD:

````text
through BE-04
````

CURRENT:

````text
through BE-08
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 004 — `README.md`

OLD:

````text
Implemented: an ASP.NET Core Web API foundation with development Swagger and health endpoint, billable-time rules, session timing lifecycle, immutable session terms/pricing snapshot, money-rounding rules, and automated tests.
````

CURRENT:

````text
Implemented: an ASP.NET Core Web API foundation with development Swagger and health endpoint, billable-time and money-rounding rules, session timing/terms/pricing snapshots, pricing catalog, Branch/GameConsole foundation, Session aggregate core, Business/Employee/Role business scope, and automated tests.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 005 — `README.md`

OLD:

````text
Not implemented: the full Session aggregate, hourly charge formula, pricing management,
````

CURRENT:

````text
Not implemented: durable Session workflows, the full hourly charge formula, application-level pricing management,
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 006 — `README.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
DB-01 / DB-01.1 are documentation-only database designs, completed / ready for review; no database or migration is implemented. BE-09 is the next planned backend slice and is not authorized. Latest reported tests: 206 passed, 0 failed; this ZIP inspection independently counts 206 static xUnit cases but does not execute them. Current BusinessDay reporting uses the Branch-local start date/month/year; Manager Start/End and unfinished-Session closing guards are requirement decisions, not running features. See the linked current state and decision requests.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 007 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
BE-00 through BE-06 now
````

CURRENT:

````text
BE-00 through BE-08 now
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 008 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
Unpaid Cancel/Void distinction remains a narrow open question.
````

CURRENT:

````text
R1 has Cancel only, no separate Void action/state. Each Invoice has zero or one positive full Cash Payment, which may be received after issuance; no partial/installment payments.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 009 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
- UI target modifications in `UX_UI/UI_IMPLEMENTATION_OVERRIDE_R1.md` OVERRIDE old PNG control behavior; original images are visual/style references only.
````

CURRENT:

````text
- Approved requirements and current UX review/flows/design-system documents govern future UI behavior; original images are visual/style references only. `UI_IMPLEMENTATION_OVERRIDE_R1.md` is not present in this ZIP. Available references are `Project.Docs/Reviews/UI_REVIEW_R1(1).md`, `Project.Docs/Planning/UX_FLOWS_R1(2).md`, and `Project.Docs/Requirements/DESIGN_SYSTEM_R1(1).md`.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 010 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
Source-tree inventory and true repository code state have NOT been verified in this design-only deliverable.
````

CURRENT:

````text
The uploaded source-tree inventory has been inspected: BE-00 through BE-08 Domain/API foundation exists; DB-01/DB-01.1 are documentation only. The ZIP does not provide a usable Git object database or verified original Git status/HEAD, and this inspection does not claim a fresh test run.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 011 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
3. Approved R1 UX target override, plus specific up-to-date requirements;
````

CURRENT:

````text
3. Approved R1 UX requirements and the available review/flow/design-system files;
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 012 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
5. Editable diagram sources; diagrams and images do not independently create business policies.
````

CURRENT:

````text
5. Tracked diagram PNGs are references, not business authority; editable diagram sources are absent from this ZIP.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 013 — `Project.Docs/Governance/PROJECT_CONTEXT.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Current BusinessDay / reporting baseline — 2026-09-30

Manager is confirmed for explicit Start Day / End Day; no Owner inheritance is inferred. End Day is blocked by any unfinished Session, including Paused. One open BusinessDay per Branch; no midnight/restart automatic close/start. A complete BusinessDay belongs to its Branch-local start date/month/year (Africa/Cairo), even if it starts five minutes before a new month. At an explicit transition, previous EndedAtUtc equals next StartedAtUtc. Daily = one BusinessDay; longer reports = Monthly, selected six calendar months, Yearly. Revenue follows successful Payment.BusinessDayId, Expenses their recording BusinessDayId; Profit = Revenue − Expenses. Source: owner clarifications in DECISION_REQUESTS_R1.md. These requirements do not implement or authorize the workflows.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 014 — `Project.Docs/Governance/TECHNICAL_DECISIONS_R1.md`

OLD:

````text
Reporting uses explicit Branch reporting timezone configuration; the exact timezone and business-day cutoff still require an owner answer (`Q-REPORT-01`).
````

CURRENT:

````text
Q-REPORT-01 is resolved: explicit Start Day / End Day, not a midnight cutoff; R1 reporting timezone Africa/Cairo. The entire BusinessDay is labeled and selected by StartedAtUtc converted to its Branch reporting timezone, including its month/year. At an explicit next-day transition, previous EndedAtUtc equals next StartedAtUtc. See the owner clarifications dated 2026-09-30 in DECISION_REQUESTS_R1.md; these are requirements, not implemented reporting code.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 015 — `Project.Docs/Reviews/CURRENT_STATE.md`

OLD:

````text
Current State (2026-09-26)
````

CURRENT:

````text
Current State (2026-09-30 documentation reconciliation)
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 016 — `Project.Docs/Reviews/CURRENT_STATE.md`

OLD:

````text
tests pass; no deployment or restoration validated;
````

CURRENT:

````text
tests were last reported as 206 passed / 0 failed; static inspection finds 206 cases, with no fresh execution here; no deployment or restoration validated;
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 017 — `Project.Docs/Reviews/CURRENT_STATE.md`

OLD:

````text
Supplied ZIP still old 37 EN + 37 AR with obsolete Screen 36; actual WPF UI not verified/implemented
````

CURRENT:

````text
Supplied ZIP contains both old 37 EN + 37 AR sets with obsolete Screen 36 and a second 36 EN + 36 AR set without it; this inventory does not certify all visual corrections; WPF is not implemented
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 018 — `Project.Docs/Reviews/CURRENT_STATE.md`

OLD:

````text
Start Day/End Day permission and the effect of active Sessions on End Day still need workflow approval before those behaviors are implemented, and Q-OPS-01 deployment/provider details remain open for rollout.
````

CURRENT:

````text
The owner has confirmed Manager Start Day/End Day, no closing with unfinished Sessions, whole-day calendar attribution to the Branch-local start month/year, and a shared boundary for an explicit next-day transition (2026-09-30). These business choices are resolved; workflow command/RBAC/transaction contracts still need implementation review. Q-OPS-01 deployment/provider details remain open for rollout.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 019 — `Project.Docs/Reviews/CURRENT_STATE.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Verification limits and documentation-only changes

Latest completed backend slice: **BE-08**. Latest completed DB documentation slice: **DB-01.1 — COMPLETED / READY FOR REVIEW, NOT IMPLEMENTED**. Next planned backend slice: **BE-09 — database/EF Core foundation, NOT AUTHORIZED**. Latest reported total: **206 passed / 0 failed**; independent source counting finds 94 Fact cases + 112 InlineData cases = 206, not a fresh runtime result. The unavailable original Git worktree prevents verifying current HEAD/status. Required owner ticket still open: **Q-OPS-01**. Owner role inheritance for day actions and future detailed implementation contracts are not inferred.

The 2026-09-30 reconciliation updates current requirements/governance/database-design prose only. It adds no production code, EF model, migration, database or physical index and does not lift HOLD. Superseded wording and its replacement/source are preserved in DOCUMENT_RECONCILIATION_2026-09-30.md; Info.md retains the original supplied file snapshots and an explicitly incomplete available-chat archive.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 020 — `Project.Docs/Reviews/PRE_CODE_GATE_R1.md`

OLD:

````text
Q-BIZ-01/02 and Q-REPORT-01 resolved in DB-01.1; Start Day/End Day permission and active-Session End Day behavior need future workflow approval; Q-OPS-01 hosting remains open for rollout
````

CURRENT:

````text
Q-BIZ-01/02 and Q-REPORT-01 resolved; 2026-09-30 owner clarification confirms Manager day actions, no End Day with unfinished Sessions, whole-day start-month/year attribution and a shared explicit-transition boundary. Workflow contracts remain to implement/review; Q-OPS-01 hosting remains open for rollout
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 021 — `Project.Docs/Releases/Release 1/DATABASE_DESIGN_R1.md`

OLD:

````text
An authorized employee explicitly starts and ends the day; exact Cashier/Manager/Owner permission is deferred to RBAC.
````

CURRENT:

````text
An authorized Manager explicitly starts and ends the day, confirmed by the owner on 2026-09-30; do not infer Cashier permission or Owner inheritance. Future RBAC/service/API enforcement remains unimplemented.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 022 — `Project.Docs/Releases/Release 1/DATABASE_DESIGN_R1.md`

OLD:

````text
Whether End Day is allowed while Sessions remain active, blocks, hands over, or completes them is **not decided**; this does not block the BusinessDays table and must not be inferred from the separate Shift-close handover rule.
````

CURRENT:

````text
End Day is blocked while any Session is unfinished (Active or Paused); pause does not mean completion. Do not auto-complete or transfer Sessions to bypass this guard. This owner-confirmed End Day requirement is separate from Shift-close handover. Exact normal application Exit handling is a later UX/engineering contract, with crash recovery still required.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 023 — `Project.Docs/Releases/Release 1/DATABASE_DESIGN_R1.md`

OLD:

````text
A BusinessDay crossing a calendar-period edge needs an explicit display/period-attribution convention in later report/UI review; provisional design groups by its local Start Day label, never splits cash across midnight. This is a technical/report presentation detail, not the old Q-REPORT-01 day-boundary blocker.
````

CURRENT:

````text
The owner approved whole-day attribution by the Branch-local Start Day label on 2026-09-30: a day starting 30 September 23:55 and ending 1 October belongs wholly to September; a day starting 31 December and ending 1 January belongs to the starting year. Use StartedAtUtc interpreted in Branches.TimeZoneId, not EndedAtUtc or individual receipt timestamps, to select the day IDs. At an explicit transition to the next day, previous EndedAtUtc equals next StartedAtUtc; use one shared UTC transition instant and the engineering half-open interval convention [start, end). Finalize atomic transition/operation ordering in an authorized workflow task. No automatic midnight/day creation follows.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 024 — `Project.Docs/Releases/Release 1/DATABASE_DESIGN_R1.md`

OLD:

````text
Clarify End Day while Sessions are active before coding that workflow (it does not block schema design); review provisional cross-period BusinessDay labeling and Shift/day relationship if needed;
````

CURRENT:

````text
The 2026-09-30 Manager, unfinished-Session End Day guard, start-month/year attribution and shared-boundary choices are resolved; review their concrete RBAC/command/atomic transition contracts without reopening those business choices. Do not infer Owner inheritance or a one-Shift-to-one-day relationship;
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 025 — `Project.Docs/Releases/Release 1/DATA_DICTIONARY_R1.md`

OLD:

````text
Exact RBAC deferred
````

CURRENT:

````text
Manager confirmed 2026-09-30; no inferred Owner inheritance
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 026 — `Project.Docs/Releases/Release 1/DATA_DICTIONARY_R1.md`

OLD:

````text
Not derived from midnight
````

CURRENT:

````text
Explicit start; Branch-local start date/month/year selects whole BusinessDay
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 027 — `Project.Docs/Releases/Release 1/DATA_DICTIONARY_R1.md`

OLD:

````text
Null exactly while open; when set >= StartedAtUtc
````

CURRENT:

````text
Null exactly while open; when set >= StartedAtUtc; explicit next-day transition shares this instant
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 028 — `Project.Docs/Releases/Release 1/DATA_DICTIONARY_R1.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## BusinessDay constraints clarified — 2026-09-30

Manager is the confirmed Start/End Day role; Owner inheritance is not inferred. End Day requires no unfinished Active/Paused Session. Calendar attribution uses StartedAtUtc in the Branch reporting timezone, Africa/Cairo for R1: all financial rows assigned to that BusinessDay aggregate in its starting month/year, even across a boundary. No extra persisted month field, report-total table or new index is introduced by this clarification. At an explicit next-day transition, previous EndedAtUtc = next StartedAtUtc; finalize atomic command ordering later. See DECISION_REQUESTS_R1.md and the companion physical design. Existing proposed constraints/mappings remain design-only and require review.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 029 — `Project.Docs/Releases/Release 1/INDEX_QUERY_PLAN_R1.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Approved period-attribution policy — 2026-09-30

Q-DAY-02 and Q-REPORT-MONTH-01/6M-01/YEAR-01 select BusinessDay IDs by StartedAtUtc using a Branch-local calendar range converted to UTC bounds. This is now the owner-approved whole-day start-date/month/year policy, not a provisional convention. A 30 September 23:55 → 1 October day is wholly selected by September. Aggregate Payments and Expenses by the selected BusinessDayIds; do not filter those rows again by the calendar receipt/expense timestamp and accidentally split a day. Invoice issuance may precede its receipt BusinessDay.

At an explicit next-day transition the previous End equals next Start; engineering intervals use [start, end), with transaction ordering reviewed later. This does not add a query, table or index candidate. QUERY FIRST → INDEX SECOND remains binding: each non-PK index needs an actual query/constraint/join/ordering/plan reason; provider-native SQLite B-Tree and SQL Server B+Tree-style plans require later measurement. Table scans can be correct for small tables or wide result sets. Existing candidate counts remain 15 SQLite / 16 SQL Server; no physical index has been created.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 030 — `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`

OLD:

````text
An authorized Start Day opens a Branch BusinessDay, and an explicit End Day closes it.
````

CURRENT:

````text
An authorized Manager Start Day opens a Branch BusinessDay, and an explicit authorized Manager End Day closes it (owner confirmation 2026-09-30).
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 031 — `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`

OLD:

````text
Do not infer Start Day/End Day permissions from Shift permissions; the authorization matrix requires its own approval.
````

CURRENT:

````text
Manager Start Day/End Day is confirmed independently of Shift permissions; do not silently infer Owner inheritance. End Day is blocked while any Session is unfinished, including Paused; no automatic completion/handover bypasses the guard.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 032 — `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`

OLD:

````text
Whether active Sessions block End Day is not decided here.
````

CURRENT:

````text
The approved unfinished-Session End Day guard is defined in BR-DAY-002.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 033 — `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`

OLD:

````text
## BR-DAY-003 — Operational Attribution
Sessions, Invoices, Payments and Expenses record their respective BusinessDay. Invoice issuance and later Cash receipt may belong to different BusinessDays. The approved unfinished-Session End Day guard is defined in BR-DAY-002.
````

CURRENT:

````text
## BR-DAY-003 — Operational Attribution
Sessions, Invoices, Payments and Expenses record their respective BusinessDay. Invoice issuance and later Cash receipt may belong to different BusinessDays. The approved unfinished-Session End Day guard is defined in BR-DAY-002.

## BR-DAY-004 — Shared Explicit Transition Boundary
At an explicit transition from a finished BusinessDay to the next, previous EndedAtUtc equals next StartedAtUtc, using one shared instant. This does not create automatic midnight closure/opening or an implicit new day after a standalone End Day. The later transaction contract must preserve one open day, ordering and no double attribution at the boundary.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 034 — `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`

OLD:

````text
That timezone is for display and calendar-period selection; stored instants remain UTC. No stored report totals are required.
````

CURRENT:

````text
The full BusinessDay belongs to its start date/month/year in that timezone, even if it ends in a later month/year. Select day IDs by StartedAtUtc; never split their financial rows at midnight. Stored instants remain UTC. No stored report totals are required.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 035 — `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`

OLD:

````text
An authorized Start Day action shall create
````

CURRENT:

````text
An authorized Manager Start Day action shall create
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 036 — `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`

OLD:

````text
An explicit authorized End Day action shall capture
````

CURRENT:

````text
An explicit authorized Manager End Day action shall capture
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 037 — `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`

OLD:

````text
Start Day/End Day authorization and the effect of active Sessions on End Day need separate workflow approval; do not infer them from Shift rules.
````

CURRENT:

````text
Manager Start Day/End Day is confirmed; do not infer Owner inheritance. End Day shall be rejected while any Session is unfinished, including Paused, without automatically completing or handing over Sessions.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 038 — `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`

OLD:

````text
## FR-DAY-003
Sessions, Invoices, Payments and Expenses shall be associated with the BusinessDay of their respective operation. An Invoice and its later Payment may have different BusinessDayIds. Manager Start Day/End Day is confirmed; do not infer Owner inheritance. End Day shall be rejected while any Session is unfinished, including Paused, without automatically completing or handing over Sessions.
````

CURRENT:

````text
## FR-DAY-003
Sessions, Invoices, Payments and Expenses shall be associated with the BusinessDay of their respective operation. An Invoice and its later Payment may have different BusinessDayIds. Manager Start Day/End Day is confirmed; do not infer Owner inheritance. End Day shall be rejected while any Session is unfinished, including Paused, without automatically completing or handing over Sessions.

## FR-DAY-004
An explicit transition to the next BusinessDay shall use one shared UTC instant for old EndedAtUtc and new StartedAtUtc and preserve the one-open-day invariant. No automatic midnight/new-day action is implied; the later authorized workflow shall define atomic operation ordering.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 039 — `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`

OLD:

````text
Monthly, selected six-calendar-month and Yearly reports shall select BusinessDays by Branch timezone calendar labels (R1 default Africa/Cairo), while stored instants remain UTC.
````

CURRENT:

````text
Monthly, selected six-calendar-month and Yearly reports shall select each complete BusinessDay by the date/month/year of StartedAtUtc in the Branch reporting timezone (R1 Africa/Cairo), regardless of EndedAtUtc. A day beginning five minutes before a month/year boundary shall belong wholly to its starting period, without splitting its assigned receipts/expenses. Stored instants remain UTC.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 040 — `Project.Docs/Requirements/ACCEPTANCE_CRITERIA_R1(2).md`

OLD:

````text
### AC-US-DAY-001-D — Authorization Not Inferred

Start Day/End Day permissions and whether active Sessions block End Day require separate workflow approval; Shift permissions and Shift close rules do not silently decide either question.
````

CURRENT:

````text
### AC-US-DAY-001-D — Manager Day Actions

**Given** an active Manager in authorized Business/Branch scope<br>
**When** an otherwise valid explicit Start Day or End Day is requested<br>
**Then** Manager is the confirmed permitted role, subject to workflow validation<br>
**And** Cashier permission or Owner inheritance is not silently inferred from Shift rules.

### AC-US-DAY-001-E — Block End with Unfinished Sessions

**Given** a Branch has an open BusinessDay and at least one unfinished Active or Paused Session<br>
**When** End Day is requested<br>
**Then** End Day is rejected and the same BusinessDay remains open<br>
**And** no Session is silently completed, transferred or erased.

### AC-US-DAY-001-F — Shared Explicit Transition Instant

**Given** all Sessions are completed and an authorized explicit transition to the next day is requested<br>
**When** that transition commits successfully<br>
**Then** previous EndedAtUtc equals next StartedAtUtc, using one instant<br>
**And** one open day remains, with no overlap or double attribution at the shared boundary<br>
**And** midnight or restart alone never triggers the transition.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 041 — `Project.Docs/Requirements/ACCEPTANCE_CRITERIA_R1(2).md`

OLD:

````text
**Then** the BusinessDays are grouped by the corresponding Branch-local calendar labels while stored instants remain UTC.
````

CURRENT:

````text
**Then** each complete BusinessDay is selected by the Branch-local date/month/year of StartedAtUtc, regardless of its ending period; stored instants remain UTC.

### AC-US-REPORT-001-F — Cross-Month Start Attribution

**Given** a BusinessDay starts 30 September at 23:55 in Africa/Cairo and ends 1 October<br>
**When** September and October reports are viewed<br>
**Then** that complete BusinessDay, including all successful Payments and recorded Expenses assigned to it, is included in September only<br>
**And** no portion is moved to October merely because its receipt timestamp or End Day occurs in October.

### AC-US-REPORT-001-G — Cross-Year Start Attribution

**Given** a BusinessDay starts 31 December in Africa/Cairo and ends 1 January<br>
**When** either calendar Year is selected<br>
**Then** the complete BusinessDay belongs to the starting year only<br>
**And** an Invoice issued on an earlier BusinessDay still contributes no revenue until its successful Payment is recorded in the appropriate receipt BusinessDay.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 042 — `Project.Docs/Requirements/RBAC_MATRIX_R1.md`

OLD:

````text
Cancel/Void
````

CURRENT:

````text
Cancel
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 043 — `Project.Docs/Requirements/RBAC_MATRIX_R1.md`

OLD:

````text
cancelled/void-related
````

CURRENT:

````text
cancelled
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 044 — `Project.Docs/Requirements/RBAC_MATRIX_R1.md`

OLD:

````text
| Cancel invoice | ❌ | 🔐 | 🔐 | Reason is optional |
````

CURRENT:

````text
| Cancel eligible unpaid invoice | ❌ | 🔐 | 🔐 | Optional reason; paid invoice cannot Cancel; no Void/refund |
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 045 — `Project.Docs/Requirements/RBAC_MATRIX_R1.md`

OLD:

````text
| End shift for employee | ❌ | ✅ | ✅ | Active sessions must be handed over first |
````

CURRENT:

````text
| End shift for employee | ❌ | ✅ | ✅ | Active sessions must be handed over first |
| Start BusinessDay | ❌ | ✅ | TBD | Manager confirmed 2026-09-30; no automatic Owner inheritance |
| End BusinessDay | ❌ | ✅ | TBD | No unfinished Active/Paused Session; explicit action |
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 046 — `Project.Docs/Requirements/RBAC_MATRIX_R1.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Current permission / implementation boundary — 2026-09-30

Manager Start/End BusinessDay is an owner-confirmed requirement, separate from Shift permissions. Owner inheritance is unspecified, not an implied grant. Protected Cancel applies only to eligible unpaid Invoices; paid invoices cannot Cancel; R1 has no Void or automatic refund/reversal. Employee belongs to Business and is deactivated Business-wide; branch scope must be checked separately. Sensitive role/PIN/pricing administration is online-only under TECHNICAL_DECISIONS_R1.md. This matrix is requirements, not implemented PIN/RBAC enforcement.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 047 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
The distinction between Cancel and Void for unpaid states remains open.
````

CURRENT:

````text
R1 has one Cancel action for eligible unpaid Invoices, no separate Void action/state. Each Invoice has zero or one positive full Cash Payment equal to its final amount, never partial/installments.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 048 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
Protected cancellation/void requires authorization and remains traceable.
````

CURRENT:

````text
Protected Cancel of an eligible unpaid Invoice requires Manager/Owner authorization and remains traceable; a paid Invoice cannot Cancel.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 049 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
Cancel/Void
````

CURRENT:

````text
Cancel
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 050 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
through BE-04
````

CURRENT:

````text
through BE-08
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 051 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
Cancel/Void
````

CURRENT:

````text
Cancel
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 052 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
Release 1 must support Weekly reporting.
````

CURRENT:

````text
Release 1 must support a selected six-calendar-month reporting range; Weekly is not an R1 report.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 053 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
Daily / Weekly / Monthly / Yearly Reports
````

CURRENT:

````text
Daily / Monthly / Selected Six-Month / Yearly Reports
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 054 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
CancelOrVoidInvoice
````

CURRENT:

````text
CancelEligibleUnpaidInvoice
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 055 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
- Protected cancel/void requires authorization.
````

CURRENT:

````text
- Protected Cancel requires Manager/Owner authorization and an eligible unpaid Invoice; a paid Invoice cannot Cancel. No Void or automatic refund/reversal exists in R1.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 056 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
- Cancelled/void records remain traceable.
````

CURRENT:

````text
- Cancelled records remain traceable and are not destructively deleted.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 057 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
protected invoice cancel/void
````

CURRENT:

````text
protected eligible unpaid invoice Cancel
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 058 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
Create Invoice
      ↓
Record Payment
      ↓
Persist
      ↓
Show Completed
````

CURRENT:

````text
Create Invoice
      ↓
Persist Invoice locally with required Audit/Outbox
      ↓
Show Invoice issued (unpaid unless a separate Payment commits)
      ↓
Record optional full Cash Payment now or later
      ↓
Persist Payment locally with required Audit/Outbox
      ↓
Show Paid only after Payment commit
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 059 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
Daily
Weekly
Monthly
Yearly
````

CURRENT:

````text
Daily (one BusinessDay)
Monthly
Selected six calendar months
Yearly
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 060 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
Remaining later design detail: Security/RBAC will define which protected actions require live central verification in `RestrictedOffline`.
````

CURRENT:

````text
The approved TECHNICAL_DECISIONS_R1.md baseline already makes sensitive role/PIN/pricing administration online-only; offline does not elevate roles. Concrete RBAC/API/RestrictedOffline contracts, enforcement and security tests remain unimplemented.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 061 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
- Cash is the only Release 1 payment method. An invoice may be issued before cash is received; Payment is a separate step.
````

CURRENT:

````text
- Cash is the only Release 1 payment method. An invoice may be issued before cash is received; Payment is a separate step. Each Invoice has zero or one full positive Cash Payment equal to its final amount; no partial/installments. Payment belongs to the BusinessDay in which cash is actually received, which may differ from Invoice issuance.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 062 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## BusinessDay / reporting clarification — 2026-09-30

Manager is confirmed for explicit Start Day / End Day; no Owner inheritance is inferred. End Day must be blocked with unfinished Active/Paused Sessions. Daily = one explicit BusinessDay; Monthly, selected six calendar months and Yearly select the entire day by its Branch-local start date/month/year (R1 Africa/Cairo), even if it starts five minutes before a new month and ends in the next. Revenue = successful Payments by receipt BusinessDay; Expenses = recording BusinessDay; operational Profit = Revenue − Expenses. At an explicit next-day transition, previous EndedAtUtc equals next StartedAtUtc. Midnight/restart never close or create a day automatically. These requirements supersede older report/cancellation wording and are not implementation evidence. See DECISION_REQUESTS_R1.md, current BR/FR/AC and CURRENT_STATE.md.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 063 — `Project.Docs/Requirements/PRD_R1(2).md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## BusinessDay / reporting clarification — 2026-09-30

Manager is confirmed for explicit Start Day / End Day; no Owner inheritance is inferred. End Day must be blocked with unfinished Active/Paused Sessions. Daily = one explicit BusinessDay; Monthly, selected six calendar months and Yearly select the entire day by its Branch-local start date/month/year (R1 Africa/Cairo), even if it starts five minutes before a new month and ends in the next. Revenue = successful Payments by receipt BusinessDay; Expenses = recording BusinessDay; operational Profit = Revenue − Expenses. At an explicit next-day transition, previous EndedAtUtc equals next StartedAtUtc. Midnight/restart never close or create a day automatically. These requirements supersede older report/cancellation wording and are not implementation evidence. See DECISION_REQUESTS_R1.md, current BR/FR/AC and CURRENT_STATE.md.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 064 — `Project.Docs/Governance/DECISIONS.md`

OLD:

````text
ownership and deployment/reporting details remain open in the current decision requests.
````

CURRENT:

````text
ownership and reporting choices are resolved in Q-ORG-01 / Q-REPORT-01, including the 2026-09-30 start-month rule; Q-OPS-01 deployment details remain open. The table body below is historical and may contain superseded open-question wording.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 065 — `Project.Docs/Governance/DECISIONS.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Current cross-reference — 2026-09-30

The old table above is preserved as history, not reopened requirements. Current R1: Cancel-only eligible unpaid Invoice; zero/one full Cash Payment; Business-wide Employee; explicit Manager BusinessDay actions with no unfinished-Session close; complete day attributed to its local start month/year; shared boundary at an explicit next-day transition. Sources: DECISION_REQUESTS_R1.md, TECHNICAL_DECISIONS_R1.md and the current R1 Business Rules. Q-OPS-01 remains the required unresolved owner ticket.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 066 — `Project.Docs/Reviews/DOCUMENT_RECONCILIATION_R1.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## Reconciliation actually applied — 2026-09-30

The older table above records a historical patch plan, including old decisions and references to absent files. Current governance, README, BRD/PRD, BR/FR/AC/RBAC, physical DB design, dictionary and query plan have now been reconciled in this extracted copy. The exact OLD / CURRENT / SOURCE register is DOCUMENT_RECONCILIATION_2026-09-30.md. The original supplied ZIP and original text snapshots in Info.md remain historical evidence. No production code, migration, database, physical index or gate authorization changed.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 067 — `Project.Docs/Governance/CHANGELOG.md`

OLD:

````text
(No corresponding current clarification.)
````

CURRENT:

````text
## 2026-09-30 — Current documentation reconciliation

Updated README/PROJECT_CONTEXT from stale BE-04/BE-06 state to BE-08; kept DB-01.1 completed / ready for review / not implemented. Reconciled Cancel-only, zero/one full Cash Payment, current report periods and actual UI inventory. Recorded owner-confirmed Manager BusinessDay actions, no End Day with unfinished Sessions, full day in its start month/year and shared explicit next-day boundary. Preserved old history and exact old/current snippets in ../Reviews/DOCUMENT_RECONCILIATION_2026-09-30.md. Latest reported/static count remains 206; no fresh test execution, production-code changes or gate lift.
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 068 — `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`

OLD:

````text
through BE-04
````

CURRENT:

````text
through BE-08
````

SOURCE:

2026-09-30 owner clarification in DECISION_REQUESTS_R1.md; current BE-08 source inventory and DB-01.1 decision/design documents. Superseded exact wording retained above and in the original supplied ZIP / Info.md original file snapshots.

### Change 069 — `Project.Docs/Requirements/BRD_R1(2).md`

OLD:

````text
Daily / Weekly / Monthly / Yearly Reporting
````

CURRENT:

````text
Daily / Monthly / Selected Six-Month / Yearly Reporting
````

SOURCE:

Current verified ZIP inventory, DB-01.1 decision/design documents and the 2026-09-30 owner clarification. Original wording preserved above and in the input ZIP.

### Change 070 — `Project.Docs/Planning/IMPLEMENTATION_SLICES_R1.md`

OLD:

````text
Complete/invoice/cash, protected cancel, payment/reversal semantics, selected employee shift+handover, expenses/assets
````

CURRENT:

````text
Complete/invoice/full Cash, protected unpaid Cancel only (no Void/refund/reversal), explicit BusinessDay, selected employee shift+handover, expenses/assets
````

SOURCE:

Current verified ZIP inventory, DB-01.1 decision/design documents and the 2026-09-30 owner clarification. Original wording preserved above and in the input ZIP.

### Change 071 — `Project.Docs/Planning/IMPLEMENTATION_SLICES_R1.md`

OLD:

````text
Open Q-BIZ-01 and narrowed Q-BIZ-02 plus affected states/tests; Q-BIZ-03 is resolved (one Match per Session)
````

CURRENT:

````text
Q-BIZ-01/02/03 and Q-REPORT-01 are resolved; 2026-09-30 Manager/closing/start-month/shared-boundary rules apply. Concrete workflow states/contracts/tests and explicit slice authorization remain required
````

SOURCE:

Current verified ZIP inventory, DB-01.1 decision/design documents and the 2026-09-30 owner clarification. Original wording preserved above and in the input ZIP.

### Change 072 — `Project.Docs/Planning/IMPLEMENTATION_SLICES_R1.md`

OLD:

````text
UX target override and relevant slice use cases
````

CURRENT:

````text
Current approved UX review/flow/design-system requirements and relevant use cases; proposed UI_IMPLEMENTATION_OVERRIDE_R1.md is absent
````

SOURCE:

Current verified ZIP inventory, DB-01.1 decision/design documents and the 2026-09-30 owner clarification. Original wording preserved above and in the input ZIP.

### Change 073 — `AGENTS.md`

OLD:

````text
The earlier proposed Database/Architecture/API/Security/Offline_Sync/UX_UI/Testing/Deployment_Operations design-pack files are not tracked in this worktree.
````

CURRENT:

````text
The earlier proposed design pack is not fully tracked here; the current DATABASE_DESIGN_R1.md, DATA_DICTIONARY_R1.md and INDEX_QUERY_PLAN_R1.md exist under Release 1 as DB-01/DB-01.1 designs ready for review, not implemented. The missing Architecture/API/Security/Offline_Sync/UX_UI/Testing/Deployment_Operations source documents must not be reconstructed from memory.
````

SOURCE:

Current verified ZIP inventory, DB-01.1 decision/design documents and the 2026-09-30 owner clarification. Original wording preserved above and in the input ZIP.

## Archive update

Info.md Part B is reconciled to the current owner decisions. Part A retains the earlier available messages and adds the latest visible owner message and three progress responses, with an explicit gap for an unavailable previous delivery. It remains an incomplete old-chat archive. All original source snapshots and original file hashes are preserved unchanged.

## Final byte-preservation verification

```json
{
  "changed_existing_markdown_files": 20,
  "unchanged_original_files": 1829,
  "unchanged_Project_Code_files": 1600,
  "added_markdown_files": [
    "Info.md",
    "Project.Docs/Reviews/DOCUMENT_RECONCILIATION_2026-09-30.md"
  ],
  "original_total_files": 1849,
  "fresh_tests_run": false,
  "static_test_count": 206,
  "production_code_modified": false,
  "gate": "HOLD"
}
```

Existing changed Markdown paths:

- `AGENTS.md`
- `Project.Docs/Governance/CHANGELOG.md`
- `Project.Docs/Governance/DECISIONS.md`
- `Project.Docs/Governance/DECISION_REQUESTS_R1.md`
- `Project.Docs/Governance/PROJECT_CONTEXT.md`
- `Project.Docs/Governance/TECHNICAL_DECISIONS_R1.md`
- `Project.Docs/Planning/IMPLEMENTATION_SLICES_R1.md`
- `Project.Docs/Releases/Release 1/DATABASE_DESIGN_R1.md`
- `Project.Docs/Releases/Release 1/DATA_DICTIONARY_R1.md`
- `Project.Docs/Releases/Release 1/INDEX_QUERY_PLAN_R1.md`
- `Project.Docs/Requirements/ACCEPTANCE_CRITERIA_R1(2).md`
- `Project.Docs/Requirements/BRD_R1(2).md`
- `Project.Docs/Requirements/BUSINESS_RULES_R1(1).md`
- `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1(2).md`
- `Project.Docs/Requirements/PRD_R1(2).md`
- `Project.Docs/Requirements/RBAC_MATRIX_R1.md`
- `Project.Docs/Reviews/CURRENT_STATE.md`
- `Project.Docs/Reviews/DOCUMENT_RECONCILIATION_R1.md`
- `Project.Docs/Reviews/PRE_CODE_GATE_R1.md`
- `README.md`
