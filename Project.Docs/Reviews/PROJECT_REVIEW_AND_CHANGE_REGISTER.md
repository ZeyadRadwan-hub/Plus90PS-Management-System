# +90 PS — Project Review & Change Register

> **REPO-01 status note:** This dated register preserves earlier review findings and proposed actions; it is not a fresh audit of every item below. Current implementation status is [CURRENT_STATE.md](CURRENT_STATE.md), and approved R1 rules take precedence over old examples. This worktree tracks rendered diagram PNGs only; editable sources and the proposed design-pack files must be obtained/verified before any source/export claim. R1 discounts are out of scope; invoice-before-payment and one Match per Session are resolved, while paid-invoice cash reversal and exact Pause eligibility remain open.

**Version:** 2.0 · **Date:** 2026-09-22 · **Scope:** Whole project; detailed immediate actions for R1, future impact on R2/R3.  
**Status:** Open tracking register, **not** a claim that fixes have been implemented.  
**Why this file exists:** One place to see precisely what we agreed to revisit, what is already inconsistent, what has yet to be designed, where to make each change, and how to verify it before coding. Never mark an item `Done` merely because it appears in a document, diagram, or screenshot.

## 0. مكان الملف داخل تقسيمتك الفعلية + طريقة استخدامه

**ضع هذا الملف تحديدًا:** `PLUS NINETY PS/Project.Docs/Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md`.

**تصحيح حالة المصدر في REPO-01:** `DATABASE_DESIGN_R1.md` المشار إليه في حزمة التصميم القديمة **غير متتبع في هذا الـworktree**؛ لا تنشئ مجلد Database أو ملفًا بديلًا من التخمين. مجلد الإصدار الموجود فعلًا هو `Project.Docs/Releases/Release 1/`. `Project.Digrams/R1/` يحوي PNGs متتبعة فقط، وليس مصادر `.drawio` قابلة للتعديل هنا.

```text
PLUS NINETY PS/
├── AboutZeyad.md                         # التعارف وأسلوب التعاون، ليس مصدر قواعد المنتج
├── README.md                              # نقطة الدخول، تُحدَّث لاحقًا
├── README(1).md                           # نسخة قديمة؟ تُقارن قبل اختيار المرجع
├── AGENTS.md                              # لم يُنشأ بعد، نجهزه عند تسليم Codex
├── Project.Docs/
│   ├── Discovery/                         # Discovery، AS-IS/TO-BE، بعد تحديد النسخ الحالية
│   ├── Governance/                        # BUSINESS_RULES، DECISIONS، CHANGELOG بعد التسوية
│   ├── Planning/                          # ROADMAP و milestones
│   ├── Releases/
│   │   ├── R1/
│   │   │   ├── Database/
│   │   │   │   └── DATABASE_DESIGN_R1.md   # نسخة التصميم الجاري استكمالها
│   │   │   ├── UX_UI/                      # UI_REVIEW_R1, DESIGN_SYSTEM_R1, UX_FLOWS_R1...
│   │   │   ├── Architecture/               # تحليلات ومواصفات ستُكتب
│   │   │   ├── API/                        # مواصفات ستُكتب
│   │   │   ├── Security/                   # مواصفات ستُكتب
│   │   │   ├── Offline_Sync/               # مواصفات ستُكتب
│   │   │   ├── Testing/                    # خطط ستُكتب
│   │   │   └── Deployment_Operations/      # خطط ستُكتب
│   │   ├── R2/
│   │   └── R3/
│   ├── Requirements/                      # BRD/PRD/FR/NFR/Stories/AC؛ حافظ على أسماء نسخك الفعلية
│   ├── Reviews/
│   │   └── PROJECT_REVIEW_AND_CHANGE_REGISTER.md  # THIS FILE
│   └── MASTER.md                          # مرجع عام بعد توحيد النسخ
├── Project.Digrams/
│   └── R1/
│       ├── 01_Use_Cases/
│       ├── 02_Activity_Diagrams/
│       ├── 03_Sequence_Diagrams/
│       ├── 04_State_Diagrams/
│       ├── 05_C4_Architecture/
│       ├── 06_Deployment/
│       ├── 07_Data_Model/
│       └── 08_Data_Flow/
├── UI/                                    # ملفات الـUI المرجعية القديمة + تصميم WPF لاحقًا
├── Meta/                                  # optional working notes; not a second requirements authority
├── Comments/
└── Pictures/
```

> **مهم:** تقسيمة المجلدات الفرعية داخل `Releases/R1` **اقتراح لتنظيم الملفات الجديدة فقط** لا ادعاء أنها موجودة عندك. أسماء الملفات المذكورة بالجدول قد تكون داخل مجلدات أخرى في جهازك؛ ابحث عن اسم الملف ثم انقل *النسخة الصحيحة بعد المقارنة*، ولا تعمل نسخًا مكررة بنفس الاسم في أكثر من مكان. هذا الملف لا ينشئ أي مجلد عندك تلقائيًا.

### Codex: ترتيب المراجع عند وجود تعارض

1. **قرارات المستخدم المعتمدة المسجلة في أحدث `DECISIONS.md` والـBusiness Rules الموحدة المعتمدة** بعد مرحلة Reconciliation؛ حتى ذلك الحين اعتمد القواعد المثبتة في §1 و§10 من هذا السجل لتحديد مواضع التعارض، لا تعتمد نسخة قديمة باسم `Final`.
2. مواصفات R1 المعتمدة بعد مراجعتها (`FR/NFR/AC`, Database/API/Security/Offline-Sync)، ثم **الـUX/UI المصححة في §3 و§10** وليس الرسم القديم إذا خالف التغيير المعتمد.
3. `.drawio` ولقطات الشاشة **مراجع شكلية/تصميمات مبدئية**؛ لا تجعل `ERD` تخترع سياسة محاسبية ولا تجعل `PNG` قديم يغير Workflow.
4. أي بند `DESIGN OPEN` أو `TBD` أو تناقض بلا قرار: **اسأل زياد، ولا تؤلف Business Rule ولا تعتبر مقترحنا قرارًا نهائيًا**.

**الحالات:** `CONFIRMED GAP` تعارض موثق؛ `DESIGN OPEN` تفصيل لم يُعتمد (ليس بالضرورة خطأ)؛ `VISUAL OPEN` واجهة مقصودة موثقة لكن صورة قديمة لم تُعدّل؛ `FUTURE` عمل لم يبدأ. `P0` يُحسم قبل أول كود مؤثر؛ `P1` قبل تنفيذ الجزء/إطلاقه؛ `P2` تنظيم/وضوح. كل بنود هذا السجل `Open` إلى أن يثبت تنفيذ تعديلها فعليًا؛ الموافقة على *المتطلب* لا تعني تنفيذ *الكود/الصور*.

## 1. Freeze what is already decided — do NOT reopen by accident

- R1 is **Windows WPF desktop**, ASP.NET Core API, branch-local SQLite, central SQL Server, **EF Core Code First**. Branch operational writes are local-first online and offline; background sync reaches SQL Server **through API**, not directly from WPF.
- R1 has **no** customer accounts/history/bookings, website/mobile, products, café, discounts, or new peripheral hardware. R2 expands customer/bookings, R3 adds advanced features. No extra R1 functionality is introduced just because older files or screenshots show it.
- Cashier/Manager/Owner permissions, personal employee PIN on shared branch PC; no Cashier access to Manager/Owner administration. PIN temporary/security locks and subscription/license locks are distinct.
- Open / fixed-hours / match behavior, no automatic fixed-session stop; pause is free; store original session opener and current responsible employee separately; handover before ending a shift with active sessions.
- Time billing: seconds round *up* to whole billable minute, then if **1–3 minutes** remain before the next 15-minute mark, advance to that mark; never round down. Money: agreed EGP rounding followed by upward-only possible adjustment to next multiple of 5 when difference is 1 or 2. Do not replace with a generic `Math.Round` or historical older wording.
- Invoice Cancel/Void requires authorized Manager/Owner action and retains history; **reason optional**. Paid-invoice accounting treatment still needs design; optional reason does not imply deleting payments.
- User-requested final UI behavior is **documented as intended**, but *not actually applied to every supplied screenshot*. The original ZIP is not proof that every approved change is visually implemented.
- No product code, live DB, API, WPF implementation, deployment, or real QA has been verified as delivered by the **current design workflow**. An older `CODE_REVIEW.md` describes a *different historical code snapshot*; do not equate it with a reviewed current implementation.

## 2. Immediate issues in documents / project source of truth

| ID · Priority · Status | File(s) / location | Exactly what is wrong or unfinished | Required correction / completion test |
|---|---|---|---|
| DOC-01 · P0 · CONFIRMED GAP | `MASTER.md`, `MASTER(1).md`, `R1_OPERATIONAL_MVP.md`, `R1_OPERATIONAL_MVP(1).md`, `R1_ACCEPTANCE.md`, `ROADMAP.md`, `DISCOVERY*.md`, `AS_IS.md` | Older text includes **R1 discounts**, discount authorization/acceptance, or unresolved rules that have since been decided. | During **final reconciliation**, remove R1 discount *requirements*, test rows, workflow/ERD/API assumptions; move genuinely intended discount scope to appropriate future release without accidentally deleting unrelated valid R1 content. Update any index/backlinks. |
| DOC-02 · P0 · CONFIRMED GAP | `DECISIONS.md`, copies, `SHARED_RULES.md`, merged `BUSINESS_RULES(1)(1).md`, old planning docs | Some O-01…O-16 questions are still labelled `open` even though subsequent user decisions resolved seconds, pause, money, fixed expiry, shift handover, reason optional, PIN lock, license/offline lease, etc. Other issues really **remain** unresolved. | Rewrite decision register with per-item `Resolved / Open / Partially Resolved`, dated decision and source; **do not reopen resolved rules**; retain genuine outstanding decisions (e.g. cash reversal accounting, exact permissions for offline administration) with gate/owner. |
| DOC-03 · P0 · CONFIRMED GAP | Older Master, Release 1 docs, `R1_ACCEPTANCE`, `DECISIONS` | Invoice Cancel/Void reason sometimes described as required/conditional or mixed with a discount/override reason. | Normalize **invoice cancellation reason = optional** everywhere, distinguishing separate actions that may require a reason; update validation, DTO design, UX, tests. Preserve cancellation actor/time and audit history. |
| DOC-04 · P0 · CONFIRMED GAP | Older requirement/architecture snippets, `MASTER*`, `BUSINESS_RULES*`, `R1_OPERATIONAL_MVP*` | Some diagrams/snippets may imply online WPF writes directly to API/SQL Server with fallback SQLite. | Normalize **same local-first write path** for branch operations in online and offline conditions; central write via API/sync; do not rewrite central-only queries/configuration as locally authored writes. |
| DOC-05 · P0 · CONFIRMED GAP | `CODE_REVIEW.md`, `README*.md`, `MASTER*.md`, any status dashboard | Legacy code review describes an older `net8.0` API, controllers and entities. Later planning says current product has no delivered code. A flat “0% code” and “reviewed existing code” are not the same statement. | Label old `CODE_REVIEW.md` **historical snapshot / provenance unknown for current repository**; on Codex handoff inspect the *actual Git repo* and inventory existing code/migrations/data before reuse, deletion, or migration. No claim of build/test success without running it. |
| DOC-06 · P0 · DESIGN OPEN | All duplicate `.md`, `(1)` copies and `PLUS90PS_*` doc packs | Multiple copies / “Final” labels make it unclear which edition wins, especially `UI_REVIEW_R1.md` vs `PLUS90PS_FINAL_UI_DOCS_2026-09-10/UI_REVIEW_R1.md`, `BUSINESS_RULES.md` vs merged file, and old vs new UX/IA. | Choose **one canonical file per subject**, preserve old editions under `Archive/`, add a small source-of-truth index; use latest agreed decisions rather than filename or “Final” header as approval proof. Do not bulk overwrite originals. |
| DOC-07 · P1 · CONFIRMED GAP | `BUSINESS_RULES(1)(1).md` (merged from `SHARED_RULES` + `VALIDATION`), older cross-file links | Cross-file links still point at former standalone filenames, old paths and dated validation claims. “Validation succeeded” describes *old documentation checks*, not current diagrams/code. | Repair relative links after final folder move; preserve validation evidence with its original **date/scope**; do not call it current runtime test proof. |
| DOC-08 · P1 · DESIGN OPEN | `BRD_R1`, `PRD_R1`, `FUNCTIONAL_REQUIREMENTS_R1`, `NON_FUNCTIONAL_REQUIREMENTS_R1`, `USER_STORIES_R1`, `ACCEPTANCE_CRITERIA_R1` | Baseline exists, but not yet cross-checked line-by-line against latest license/PIN policies, billing edge cases, offline handover, role visibility, and new database/API contract. | At final reconciliation make traceability table Rule → FR/NFR → User Story → AC → UX → Diagram → Test. Patch **only inconsistencies/gaps**, not rewrite approved scope from scratch. |
| DOC-09 · P1 · DESIGN OPEN | `ROADMAP.md`, `FIRST_SLICE.md`, `PROJECT_LIFECYCLE.md`, `FLOW_AND_STEPS.md`, `R1_ACCEPTANCE.md` | Earlier roadmap assumes an old code slice and marks features/milestones differently from current docs-first pre-code gate and R1 no-discount policy. | Update milestone sequence to **design → reconciliation → pre-code approval → Codex slices → tests → pilot**; keep real scope and proven coverage, correct old discounts, old open questions, and stale progress claims. |
| DOC-10 · P1 · FUTURE | `PROJECT_CONTEXT.md`, `CURRENT_STATE.md`, `AGENTS.md`, root `AboutZeyad.md`, `README.md`, `CHANGELOG.md` | Codex has no guaranteed view into all chat decisions or this exact working state. `AboutZeyad.md` is profile/collaboration context, **not** R1 source of truth. | At handoff create project context, up-to-date progress, root AGENTS with mandatory read order and non-invention rules, a decision log and versioned files in repo. Keep personal info limited to what Zeyad wants Codex to see. |

## 3. UX/UI: docs describe **intended** corrected state; supplied ZIP is **not** corrected everywhere

**Grounding:** latest intended spec: `PLUS90PS_FINAL_UI_DOCS_2026-09-10/UI_REVIEW_R1.md`, `.../DESIGN_SYSTEM_R1.md`, `PLUS90PS_R1_UX_DOCS_2026-09-10/UX_FLOWS_R1.md`, `.../INFORMATION_ARCHITECTURE_R1.md`. ZIP `+90PS_UI.zip` still contains **37 EN + 37 AR images including `36_Settings_Security.png`**. The status check below is about **screenshots**, not an assertion that product functionality exists.

| ID · Priority · Status | Existing screenshot / spec | Mismatch and implementation requirement |
|---|---|---|
| UI-01 · P0 · VISUAL OPEN | `03_Dashboard`, Cashier navigation | Cashier screenshot still shows Manager/Owner-level navigation (Management/Reports). Hide unauthorized routes/controls **and** enforce permissions in use cases/API; do not rely on hiding alone. |
| UI-02 · P1 · VISUAL OPEN | Dashboard/Sessions sidebar | Inactive Dashboard and Sessions icon colors differ from inactive nav icon style. Bring to shared inactive token; active red only. |
| UI-03 · P0 · VISUAL OPEN | `03_Dashboard` | Remove `Add Order` (product/food out of R1); available-console card has a **single START**, not Single/Multi selection. START opens Session Start. |
| UI-04 · P0 · VISUAL OPEN | `05_Session_Start` | Add/implement Session Mode Single/Multi and Billing dropdown `Open Time / Hours / Match`; Hours reveals duration field (e.g. 2,3); Match uses match price/configured duration; fixed-hour expiry **alert only**, not auto-stop. |
| UI-05 · P1 · VISUAL OPEN | Cross-screen navigation | Global Back/Close/Cancel is not consistently visible in screenshots. Implement safe navigation preserving role/login/branch context; do not bypass protected routes. |
| UI-06 · P1 · VISUAL OPEN | `10_Session_History`, invoice screens | History must show cancelled/void-related events/financial status where allowed; no silent disappearance of cancelled invoices. |
| UI-07 · P0 · VISUAL OPEN | `15–18_Shift*`, `23_Management_Employees*` | Shift page needs relevant employee list and Manager/Owner Start/End for **selected employee**, with active-session handover before close. Employee Management remains a *separate* module. |
| UI-08 · P1 · VISUAL OPEN | `13_Invoice_Cancel*` | Reason field can exist, but must be explicitly **optional**, not block cancel when empty; protected authorization and audit still required. |
| UI-09 · P1 · VISUAL OPEN | `35_Settings_General` | Branch name display is **read-only**, no branch-name save action; remove/keep absent `Default Start Page` option. Settings only expose ordinary user preferences. |
| UI-10 · P0 · VISUAL OPEN | `36_Settings_Security` | ZIP still contains and navigation references screen 36 with editable lock-policy settings; approved R1 **removes it from normal user-facing UI entirely**. Security policy is not an editable end-user setting; keep protected recovery separately. Do not describe screenshot 36 as already deleted. |
| UI-11 · P1 · DESIGN OPEN | `30–34_Reports*`, EN/AR | Final behavior requires Daily/Weekly/Monthly/Yearly filter, Statistics vs Details, and **monthly closing as read-only review**. Ensure two languages, permission gates, saved/unsynced states and error feedback are equivalent in actual WPF, not only in prose. |
| UI-12 · P1 · DESIGN OPEN | `DESIGN_SYSTEM_R1.md`, ZIP/Figma | Raster screenshots do **not prove exact original font metadata or HEX values**; documented reference tokens are implementation approximations unless confirmed against editable Figma source. Do not mislabel guessed tokens “pixel-perfect verified”. Keep approved dark/burgundy identity and AR mirroring. |
| UI-13 · P2 · FUTURE | Source screenshots vs intended docs | **No new PNG redesign requested.** During WPF implementation use corrected UX/UI docs + original visual identity as the baseline; if a precise visual ambiguity blocks implementation, ask Zeyad rather than copying an old unwanted control. |

## 4. Diagrams: all authored ≠ all validated, and not all need redrawing

**User reported authoring all requested diagrams externally.** This tracked worktree currently contains rendered PNGs only, not editable `.drawio`/PlantUML sources. Obtain and preserve real editable sources if available; do not fabricate or recreate diagrams solely for document count. Update **only impacted diagrams** after design decisions or for readable output.

| ID · Priority · Status | Diagram(s) | Exact check/correction |
|---|---|---|
| DIA-01 · P1 · DESIGN OPEN | R1 Logical ERD | Once data dictionary is finalized, update changed fields/FKs/cardinalities: session timing/snapshots; paid-invoice reversal; license/employee security; local vs central sync receipts. Current ERD is logical, **not** an approved physical SQL Server/SQLite schema. |
| DIA-02 · P1 · CONFIRMED GAP | DFD Level 0 (screenshot submitted) | Labels and arrows are overlapping; reorder external entities and use short **data names** with legible inbound/outbound arrows. Keep one process boundary and the same actors; no new system redesign. |
| DIA-03 · P1 · DESIGN OPEN | DFD Level 1 (screenshot submitted) | Re-layout unreadable, overlapping names. Check that Level-0 exchanges for **Cashier, Manager, Owner, Engineer** balance with Level 1; clearly distinguish local save/outbox, API/sync, central SQL, and local-vs-central reports. Do not imply direct WPF→SQL Server central write. |
| DIA-04 · P1 · DESIGN OPEN | C4 Level 1/2/3, technical flow | Ensure context does not show Sequence lifelines, sync uses **API**, branch ops remain local-first, roles/security vs system components are distinguished. Update after component responsibilities are finalized, not before. |
| DIA-05 · P1 · DESIGN OPEN | Sequence diagrams | After API contracts and transaction boundaries, align retries, acks, rejection, handover, protected invoice cancellation and authentication with final requests/responses. Initial sequence pictures alone do not prove real API behavior. |
| DIA-06 · P1 · DESIGN OPEN | Session/Invoice/PIN/License/Sync State diagrams | Verify allowed transitions match final business rules, including pause timing, paid invoice cancel, security lock vs license lock, retry vs conflict; do **not** force independent security/license dimensions into one false state machine. |
| DIA-07 · P1 · DESIGN OPEN | Deployment Diagram | Reconcile with actual chosen hosting, backups, restore, branch PC/Windows support and secure key handling when deployment design is ready. Do not draw unapproved extra branch hardware. |
| DIA-08 · P2 · DESIGN OPEN | All diagrams | Check PlantUML renders without warning, legibility at normal zoom, consistent names, source+export present, labels/directions/cardinality; revisions must be traceable to a real change. |

## 5. Database Design — current **first active phase**, before writing Code First models

The historical pack's `DATABASE_DESIGN_R1.md` (not tracked in this worktree), Section 01, reportedly proposed ownership for 18 logical entities; its **TBD/Proposed is not accepted automatically**. Obtain and verify that source before relying on its content. Design the **local SQLite and central SQL Server models explicitly**, even if many C# domain types are shared. Required decisions and checks:

| ID · Priority · Status | Target | Work / acceptance check |
|---|---|---|
| DB-01 · P0 · DESIGN OPEN | `DATABASE_DESIGN_R1.md` Section 01 | Approve or revise Local/Central/Shared ownership for every entity. Special decisions: offline employee create/PIN reset/role change, branch vs central pricing authority, asset-category creation, centrally issued license/role revisions. Document cached version/expiry and revocation behavior. |
| DB-02 · P0 · DESIGN OPEN | `Branch` and ownership model | Define full field-level data dictionary, IDs and branch/owner relationships; tenant/branch isolation must be expressible, centrally enforced and safe under offline cache. `Branch` being connected to many tables is **not itself an ERD error**. |
| DB-03 · P0 · DESIGN OPEN | `Employee`, `Role`, `Permission`, `RolePermission` | Define PK/FK, composite uniqueness, branch scope, secure PIN verifier (not plaintext), per-user state, revocation and offline rights; reconcile globally defined roles with ownership/branch authorization. Avoid storing sensitive secrets in audit/outbox payload. |
| DB-04 · P0 · DESIGN OPEN | `Console` | Define branch-local human-readable name uniqueness if desired, active/inactive, valid console types and rules preventing concurrent active sessions on the same console. Historic sessions must survive deactivation. |
| DB-05 · P0 · DESIGN OPEN | `Pricing` | Define uniqueness/effective intervals by Branch + ConsoleType + Single/Multi + Hourly/Match, whether amount is per hour vs per match, match duration and versioning; prevent ambiguous overlapping active prices. |
| DB-06 · P0 · DESIGN OPEN | `Session` + `SessionEvent` | Determine durable time-accounting data for multiple free pauses/restarts and correct billable calculation; timestamps in UTC; appropriate status transitions; Open/Fixed/Match vs pricing-method representation; save full **pricing snapshot** incl. relevant match duration/version, not just `PriceSnapshot`. Fixed expiry warning only, no automatic completion. |
| DB-07 · P0 · DESIGN OPEN | Session responsibility/Shift | Preserve `OpenedByEmployeeId` forever; transfer `CurrentResponsibleEmployeeId` under authorized handover and record events/attribution. Specify Shift Start/End actors and links; close only after transferring active sessions. |
| DB-08 · P0 · DESIGN OPEN | `Invoice` / `Payment` | One accepted invoice per completed session and R1 cash flow, status definitions, optional reason, authorized cancel/void, immutable history; explicitly design effects of **cancel after cash received** (reversal/adjustment/refund record vs no cash refund). Do not infer payment hard-delete or silently alter revenue. Payment cardinality and settlement timing need validation. |
| DB-09 · P0 · DESIGN OPEN | Time/money storage | Agree `Guid`/SQL Server `uniqueidentifier`/SQLite UUID representation, UTC `datetime` handling, EGP minor-unit/decimal precision strategy, exact rounding/version, calculation tests at thresholds. Logical `UUID` ≠ a type named `UUID` in SQL Server. |
| DB-10 · P0 · DESIGN OPEN | `SyncOperation` and central inbox | Clarify local outbox vs central deduplication **not identical mirrored tables**, stable OperationId unique per branch/scope as appropriate, atomic local save+outbox and central data+receipt, retries/out-of-order/dependency/conflict states, purge/retention, authenticated origin. |
| DB-11 · P0 · DESIGN OPEN | `BranchLicense`, PIN security | Store central paid-expiry, signed cached offline lease and anti-time-tamper evidence; implement approved >10-days-left / <=10-days-left lease rules without trusting editable SQLite fields; `SecurityLocked` independent from subscription lock; define offline recovery provenance. |
| DB-12 · P1 · DESIGN OPEN | `AuditEvent`, `SessionEvent` | Separate business events from security/audit; distinguish nullable system actor vs employee actor, append-only retention, local/central event origin, correlation IDs and non-secret payload policies. |
| DB-13 · P1 · DESIGN OPEN | `Expense`, `AssetCategory`, `BranchAsset`, reports | Define authority, quantity constraints (>=0), non-sales equipment semantics, optional notes, expense types and time/branch boundaries; revenue/expenses/profit query definitions; monthly closing **no financial write**. |
| DB-14 · P0 · DESIGN OPEN | All entities → Data Dictionary | For **each column** define purpose, C# type, SQL Server type, SQLite mapping, NULL/default, PK/FK, Unique/Check, index, read/write owner, delete/deactivate, history, sync behavior, retention. Do not code based on diagram labels alone. |
| DB-15 · P0 · DESIGN OPEN | Schema + Code First migration planning | Plan EF Core `DbContext` boundaries, configuration, provider-specific migrations and SQLite limitations, initial schema, seed/branch bootstrap, development vs production migration approvals, backups + rollback strategy. **No SQL/C# generation until explicit pre-code approval.** |
| DB-16 · P1 · DESIGN OPEN | Concurrency / recovery | Define active-console uniqueness under races, branch PC data-recovery/SQLite integrity and outbox replay, version/concurrency controls, failed local commit UI; central conflict must **preserve** financial evidence. |
| DB-17 · P1 · DESIGN OPEN | Relationship verification | Validate every actual FK (`Invoice.SessionId`, `Payment.InvoiceId`, shift actors, session opener/responsible, audit actor, etc.), optionality and delete behavior. Never cascade-delete historic financial/audit records because an employee/branch/console is disabled. |

## 6. System Analysis / Architecture / API / Security / Offline-Sync — still to author, not “bad finished docs”

| ID · Priority · Status | Deliverable | What must be specified before Codex |
|---|---|---|
| ARC-01 · P0 · FUTURE | `SYSTEM_ANALYSIS_R1.md` | Use-case boundaries, domain invariants, actor/permissions matrix, error and recovery cases, responsibility handover and report scope; link each to existing requirements/diagrams. |
| ARC-02 · P0 · FUTURE | `ARCHITECTURE_R1.md` + ADRs | WPF/MVVM modules, common billing domain used locally/centrally, API trust boundary, SQLite owner, sync worker, central DB, lifecycle, DI, cancellation/error behavior. Confirm what is shared C# vs provider-specific. |
| ARC-03 · P0 · FUTURE | `API_CONTRACT_R1.md`, `openapi.yaml` | Endpoints, DTOs, validation/response codes, auth, explicit branch scope, pagination/report queries, versioning, stable operation IDs, idempotent sync/acks/conflicts, protected cancellation; never expose EF entities as public API by default. |
| SEC-01 · P0 · FUTURE | `SECURITY_DESIGN_R1.md`, threat model | PIN rate limits, first five failure warning + 20-second temporary lock, next five `SecurityLocked`, engineer recovery online/offline; secure verification and auditing; distinguish security and license locks. No user-editable settings for those constants. |
| SEC-02 · P0 · FUTURE | RBAC enforcement and branch/ownership isolation | Server authorization independent of UI, effective role/permission revocation, employee reset, Manager/Owner protected actions, local offline rights expiry, device enrollment/keys and signed lease; audit and tests. |
| SYNC-01 · P0 · FUTURE | `OFFLINE_SYNC_DESIGN_R1.md` | One local-first command path, transaction/outbox format, per-entity ownership, safe sequencing, retries/timeouts/acks, central receipts, reject/conflict workflow, stale cached rights, status UI, reinstallation/replay. Never silent last-write-wins for finance/security. |
| SYNC-02 · P0 · FUTURE | Sync failure scenarios matrix | Network down before local commit, after local commit, before server commit, after server commit before ack, duplicate request, out-of-order, bad auth/lease, revoked employee, conflicting price, local/central restore. Define expected durable state for each. |

## 7. Testing, deployment, operations and later releases

| ID · Priority · Status | Area | Finish / verify |
|---|---|---|
| QA-01 · P0 · FUTURE | Test Plan / Cases / UAT | Cover every R1 acceptance criterion with unit/integration/API/UI/E2E and real-branch pilot; cases for billable-minute edges, money rounds, Match/Fixed/Pause, protected cancel, handover, security lock, offline/replay and branch isolation. No “tests passed” before running actual tests. |
| QA-02 · P1 · FUTURE | Performance / reliability tests | Define test dataset and test environment; measure branch recovery and central API/DB behavior under representative load; avoid inventing proof from documented RTO/RPO targets. |
| OPS-01 · P0 · FUTURE | Hosting / Deployment / Rollback | Select and document actual central hosting and supported Windows/.NET/provider versions, environments, encrypted secrets, onboarding, EF migrations/rollback, desktop update package, logging/health monitoring. |
| OPS-02 · P0 · FUTURE | Backup / Disaster Recovery | Test local SQLite backups about every 30m, central nightly FULL / 6h differential / 5min transaction logs, off-host retention and restore procedure; branch RTO <=5m, central RTO <=4h, central RPO <=5m are **targets to verify**, not achieved results. |
| OPS-03 · P1 · FUTURE | Runbooks / training / release gates | Incident/recovery steps, onboarding, cashier/manager guide, security unlock, failed sync, data integrity and cancellation adjustments, pilot feedback and release checklist. |
| R2-01 · P1 · FUTURE | `R2_EXPANDED_MVP.md` and future R2 design | Only **after R1 baseline exists**, derive R2 customer/account/history/booking/deposit/late-arrival/migration/booking-sync work. Correct any old R1 dependencies without adding bookings to R1. |
| R3-01 · P2 · FUTURE | `R3_FULL_PRODUCT.md` | Revisit advanced multi-branch, reports, asset transfer, packages/automation **only when R3 discovery starts**; don't promise that planning ideas are built. |
| HANDOFF-01 · P0 · FUTURE | Pre-Code Gate / Codex | Review resolved P0 decisions, canonical docs/diagrams/data contracts, current repository, tests/backups plan; Zeyad expressly approves implementation. Then deliver `AGENTS.md`, project/state files, ordered prompts. Codex implements approved **small vertical slices**, not invents scope/business rules. |

## 8. Work order; what exactly gets updated when

1. **NOW — Database Design:** review Section 01 storage/ownership mapping, then `Branch` → all entities' Data Dictionary → relationship/constraint/financial/timing design → Code First migration strategy. As decisions become approved, mark relevant DB rows; patch the ERD **only where necessary**.
2. **System Analysis + Architecture + API + Security + Offline-Sync:** write detailed contracts and scenario matrices, patch **affected** Sequence/C4/State/DFD/Deployment diagrams only after their actual design is fixed.
3. **QA + Deployment/Operations planning:** create verifiable test/deployment/restore/UAT plans; no production/test-success claim.
4. **Final reconciliation:** run DOC-01…09 across every *canonical* R1 requirement, UX/UI, diagram, decision, acceptance and milestone file. Remove R1 discounts; record optional cancel reason, latest time/money/PIN/license/offline rules; repair old links/versions; check no contradictory source of truth.
5. **Pre-Code Gate:** audit each P0 issue as resolved or explicitly blocked with an owner; confirm correct actual repository, user approval. **Only then announce “Codex starts now”** and start EF Core Code First and application implementation.
6. **During implementation:** reconcile *verified actual changes* in `CURRENT_STATE.md`, `CHANGELOG.md`, tests and the impacted spec; do not retroactively mark diagrams/screenshots as corrected until their editable sources/rendered outputs were actually updated.

## 9. Acceptance rules for this register

- A row moves to `Resolved` only with **actual file/diagram path, concrete change or approved decision, reviewer/date, and matching acceptance check**; for runtime claims, add actual test evidence.
- A `DESIGN OPEN` row is **not** an established bug and a proposed solution is **not** approved until Zeyad accepts the relevant business decision.
- The user has finished drawing the diagrams; that accomplishment stands. The separate legibility/contract checks above are *targeted review*, not a request to recreate all diagrams or edit PNGs now.
- The intended UI changes documented “as if applied” **must be implemented in WPF**; do not tell Codex the old ZIP is an already-fixed screenshot pack.
- This register is deliberately comprehensive for **known, observed, and previously named follow-up items**; an unseen source tree or future execution can reveal additional issues. Add them here with evidence rather than asserting an impossible guarantee of “every future bug.”


---

## 10. تعليمات تغيير محددة — أين تبحث وماذا تكتب بالضبط

الجدول أعلاه هو الـIssue Tracker الكامل؛ هذا القسم يعطي **نصوصًا مرجعية قابلة للنسخ** للمُصلح/ Codex. المقصود تعديل **الأقسام الموجودة** بعد العثور عليها، لا لصق النص في كل ملف عشوائيًا. بعض الملفات لم تُفحص نسخة جهازك الحية؛ لذا *موضع البحث* هو اسم القسم/العبارة وليس رقم سطر غير موثوق. احتفظ بنسخة سابقة في Archive، وسجل diff.

| الملف/المجموعة (فتش داخل النسخة الفعلية لديك) | القسم أو النص الذي تبحث عنه | المشكلة المؤكدة أو المطلوب التحقق منه | النص المعتمد/التصرف عند الإصلاح |
|---|---|---|---|
| `Project.Docs/MASTER.md` وأي `MASTER(1).md` أو master داخل `Releases` | `Confirmed Scope`, `Release 1 — Operational MVP`, `Scope`, `Architecture`, `Current status` | نسخة موروثة تسرد R1 Discounts وأحيانًا توحي بكتابة مركزية مباشرة؛ قد تتعامل مع نماذج مقترحة كأنها منفذة. | استخدم **R1 = WPF + local SQLite first + background sync → authenticated ASP.NET Core API → central SQL Server. No R1 discounts/website/mobile/bookings/customer accounts/food/products. Implementation = NOT VERIFIED until repository inspection. Code First = approved approach, NOT code written.** افصل R2/R3 في أقسامهما. |
| `Project.Docs/Releases/Release 1/R1_OPERATIONAL_MVP.md` (historical) | `3. Release 1 Scope Summary` + `Cashier/Manager permissions`, `Invoices`, `Acceptance`, `Project Flow` | `Allowed predefined discounts` و`Manager-authorized manual discounts` قديمان، وتظهر خصومات في صلاحيات وخطط/اختبارات متفرقة. | R1 discount references are superseded, not current requirements. Keep **Manager/Owner protected Invoice Cancel/Void** separate; Local-first remains the current path. |
| `Project.Docs/Discovery/DISCOVERY*.md`, `AS_IS.md` | `R1 Scope`, `Discount Requirement`, `Main Modules`, `Success Criteria`, `AS-IS` | ملف Discovery الموروث يذكر خصومات كأنها ضمن R1؛ وقد يخلط توثيق الوضع القديم مع متطلبات الإصدار المستقبلي. | احتفظ بالوصف التاريخي للواقع AS-IS **إن كان واقعيًا**، لكن انقل/احذف الخصومات من **TO-BE/R1 target scope** مع توضيح `Not in R1`. لا تعيد كتابة الوقائع التاريخية كأنها خطأ تقني. |
| `Project.Docs/Discovery/TO_BE_PROCESS.md` أو النسخة الحالية | `Invoice`, `Cash`, `Shift`, `Offline` | يلزم فحص عدم وجود خصومات أو حسم قبل رد المركز أو إغلاق وردية بموظف مسؤول عن جلسات مفتوحة. | **Completion → SQLite transaction (Session+Invoice+Cash Payment if collected+Outbox+required events) → local success → async API sync**؛ `End Shift` مع جلسات نشطة يتطلب handover مصرحًا قبل الإغلاق. |
| `Project.Docs/Governance/BUSINESS_RULES(1)(1).md` أو الملف الموحد الفعلي | sections `Billing`, `Financial`, `PIN`, `License`, `Offline`, `Invoice Cancel`, cross-links | هو مرجع القواعد المدمج وليس مبررًا لإعادة فتح الحسم؛ بعض روابط `SHARED_RULES.md`/`VALIDATION.md` أو ملخصات نسخ قديمة قد تكون غير صالحة. | ثبّت نص `Business Rules` في §11 هنا *بدون توليد صيغة رياضية مخالفة*، أصلح روابط النسخ بعد نقلها، واحتفظ بوصف `VALIDATION` كفحص Docs قديم لا إثبات Code/QA. لا تجعل النسخ القديمة تساوي هذا المرجع تلقائيًا. |
| `Project.Docs/Governance/DECISIONS.md` وأي `(1)` | كل `O-01`…`O-16` / `Open questions` | بعض المسائل التي حُسمت ما زالت `Open` في نسخ قديمة. | عيّن لكل مسألة `Resolved / Partially resolved / Open` مع القرار الفعلي والتاريخ؛ ثبّت pause/free, 3min rule, Money, PIN 5+5, lease 10d, reason optional, shift handover؛ **لا تختلق** تفصيل استرجاع كاش بعد إلغاء فاتورة مدفوعة أو صلاحيات تعديل الموظف Offline. |
| `Project.Docs/Requirements/BRD_R1.md`, `PRD_R1.md` | `Scope`, `Out of Scope`, `Cashier`, `Admin`, `Billing`, `Invoice`, `Shift` | يلزم ضمان توافقها مع آخر UI/Business Rules، وليس إثباتًا أنها كلها غلط. | Patch فقط إن وُجد تعارض: R1 لا خصومات، خصم الواجهة القديمة لا ينشئ متطلبًا؛ الـCashier لا يرى Management/Reports؛ Cancel optional reason; Fixed expiry alert only; Match سعر/مدة معتمدين؛ Local-first. |
| `Project.Docs/Requirements/FUNCTIONAL_REQUIREMENTS_R1.md` | auth/role, session create, invoice, shift, reports, settings, sync | التحقق أن كل Behavior مصحح له FR وأن FR قديم لا يطلب شيء خارج النطاق. | اربط `Screen 03 START → Screen 05 Mode+Billing`; `Hours` يُظهر duration؛ `Cancel reason nullable`; `Manager/Owner controls employee shifts with handover`; history keeps cancelled records; reports periods and read-only review; auth/API permission gates, local transaction + sync. لا تضف رقم FR بلا اتساق مع الموجود. |
| `Project.Docs/Requirements/NON_FUNCTIONAL_REQUIREMENTS_R1.md` | durability, recovery, security, performance, offline | قد تختلف أهداف الاستعادة عن الأدلة التنفيذية؛ لا تعتبر SLA/RTO محققة لمجرد كتابتها. | نوثّق الـ**targets**: local backup ~30 min؛ central FULL nightly, differential 6h, log backup 5min؛ branch RTO ≤5m, central RTO ≤4h؛ أي RPO مستنتج من جدول النسخ ليس دليلًا متحققًا؛ الاختبار والتشغيل لاحقًا. لا تمنح offline lease عبر تغيير ساعة الجهاز. |
| `Project.Docs/Requirements/USER_STORIES_R1.md`, `ACCEPTANCE_CRITERIA_R1.md` | Stories/AC المرتبطة بـDashboard, Shift, Cancel, PIN, License, Reports, offline | `Done` في مستندات التصميم لا يساوي اختبار واجهة شغالة؛ احتمالية حالات ناقصة. | حدد AC/Tests للأزرار والـpermission والوضعين EN/AR والـ`Pending/Synced/Conflict`, الرفع المتكرر, انقطاع الكهرباء, Cashier لا يصل إلى الإدارة من deep links, Cancel مع سبب فارغ. لا تعيد ترقيم كل القصص لمجرد التعديل. |
| `Project.Docs/Requirements/R1_ACCEPTANCE.md` | الصفان **`R1-15` و`R1-16`** تحديدًا، و`R1-13`/`R1-14`, جدول milestone | R1-15/16 were discount proposals. | IDs R1-15/16 are now marked **Removed from R1 scope — superseded**, not reused; R1-13/14 preserve invoice-before-payment and the separate Cash step. |
| `Project.Docs/Planning/ROADMAP.md`, `FIRST_SLICE.md`, `PROJECT_LIFECYCLE.md`, `FLOW_AND_STEPS.md`, `Steps R1.txt` إن وُجد | `milestones`, `M1-M4`, code gates, discount slices, current progress | قد تعرض مراحل Coding قبل إغلاق التصميم أو اختبارات غير منفذة كأنها مكتملة. | **Database design → Architecture/API/Security/Offline sync → QA/deployment plans → final reconciliation → pre-code gate → Codex small slices → tests → pilot**. صَحّح *الحالة الفعلية* لا مجرد التاريخ. |
| `Project.Docs/Reviews/CODE_REVIEW.md` إن نقلته للمجلد | أول العنوان/`Status`, أي `API implemented`, `build passed` | مراجعة تاريخية لكود/Repo سابق، لا دليل على تنفيذ النظام الحالي. | رأس واضح **Historical snapshot; current repo not audited.** عند التسليم يفتح Codex Git repo الحالي ويعمل inventory فعلي قبل اختيار reuse/replace؛ ممنوع حذف كود سابق دون مراجعة. |
| `README.md`, `README(1).md`, `Project.Docs/MASTER.md` | `Getting Started`, links, `Current status` | احتمال رابط نسخ قديمة أو إشارة إلى feature منتهية وهي مجرد UX/diagram. | اربط إلى **هذا السجل → أحدث Business Rules/Decision Log → R1 approved requirements/UX → DB/API/Security/Sync**. قل **Diagrams authored, Database Design ongoing, no verified deployed R1**. لا تخلط README(1) بأحدث README دون diff. |
| `Project.Docs/Governance/CHANGELOG.md` | changelog entries | توثيق إعادة كتابة Design قد يُقرأ كأنه feature coded. | استخدم وسوم `DESIGN_APPROVED`, `DOC_UPDATED`, `DIAGRAM_UPDATED`, `IMPLEMENTED`, `TESTED`; لا تضع `IMPLEMENTED` من غير repo commit + tests. |
| `Project.Docs/Releases/R2/` / `R2_EXPANDED_MVP.md`; `R3_FULL_PRODUCT.md` | R2/R3 Scope | نسخ قديمة قد تعرض فكرة مخطط لها كأنها جاهزة/ضمن R1. | R2 لاحق بعد R1 working baseline لعمل Customers/History/Bookings/Deposits؛ R3 later advanced scope. تعديل R2/R3 فقط **إن تلوث النطاق** لا إعادة تخطيطها الآن. |
| `Project.Docs/Planning/INFORMATION_ARCHITECTURE_R1.md` | Cashier nav, Management children, `There is no normal R1 navigation` | المصطلح `Discount Management` يظهر ضمن **قائمة المنع بالفعل**، وليس خطأ يتطلب حذفه؛ انتبه للـnegation. | اترك قسم الاستبعاد؛ التحقق من أن Cashier لا تظهر له management/reports وأن Employees management منفصل عن Shift. |
| `Project.Docs/Reviews/UI_REVIEW_R1(1).md` | `Purpose`, `Final Active Screen Baseline`, `Final UI Approval Checklist` | نص `as if already applied` وعلامات `[x]` تعني الموافقة على **التصميم المقصود**؛ قد يفسرها Codex خطأً كأن PNG/واجهة WPF اتعدلت بالفعل. | Approved target UI spec, not an audit of actual ZIP/WPF; old screenshots remain. `[x]` is design acceptance only, not implemented. |
| `Project.Docs/Requirements/DESIGN_SYSTEM_R1(1).md` | Color tokens/font specs + `Sources` | Raster screenshots do not prove exact Figma tokens. | Verify against the real editable design source before claiming pixel-perfect fonts/colors; preserve the approved visual identity. |
| `Project.Docs/Planning/UX_FLOWS_R1(2).md` and `Project.Docs/Requirements/RBAC_MATRIX_R1.md` | Shift, Dashboard→Start, Cancel, Settings, Back | Approved design does not mean WPF was implemented. | Shift Start/End for selected employee by authorized Manager/Owner; handover active sessions; Employee Management separate. |
| `UI/` و`Pictures/` + `+90PS_UI.zip` | PNG names in §12 | ZIP قديم لا يساوي Final screens. | **احتفظ بالـPNG كمرجع Visual فقط.** لا تعدّل PNG/ترسلها على أنها fixed. عند بناء WPF نفّذ كل التصحيحات مع نفس visual style؛ لا تنشئ شاشة 36 للمستخدم العادي. |

### نص موحد يضاف تحت مصدر الحقيقة في الـMaster/Requirements عند المصالحة (بعد اعتماد النسخ)

```md
> R1 implementation contract (current): Windows WPF desktop, personal employee PIN on a
> shared branch PC, role-aware navigation and enforced authorization; local-first SQLite
> writes both online/offline, durable outbox, ASP.NET Core API, central SQL Server.
> EF Core Code First is the approved implementation approach; design is not yet code.
> R1 excludes discounts, customer accounts/history/bookings, website/mobile,
> product/food/drink sales and new peripheral integrations. Session time/amount
> calculations, shift handover, Invoice Cancel/Void and license/PIN behavior follow
> the latest approved Business Rules/Decision Log. The corrected UI is a TARGET
> SPECIFICATION; old PNGs are only a visual reference, not an implemented screen set.
```

## 11. نص قواعد العمل التي لا يجوز للكود القديم تغييرها

**استخدم هذه الجمل كـimplementation invariants عند المصالحة، لا كبديل عن قواعد العمل التفصيلية الكاملة.**

- **R1 sessions:** `Open Time / Hours / Match`؛ `Hours` قيمة ساعات مقصودة ويصدر عند انتهائها **تنبيه فقط** دون auto-stop؛ `Match` سعر ثابت ومدته المعتمدة حسب الفرع؛ Single/Multi اختيار بداية الجلسة لا يتحول أثناء Active.
- **Time:** احسب الزمن القابل للفوترة بعد استبعاد Pause المجاني؛ لو فيه ثوانٍ ارفع الناتج لأقرب دقيقة صحيحة، ثم إذا **باقي 1 أو 2 أو 3 دقائق** فقط للوصول للربع ساعة التالي ارفع للربع؛ باقي الحالات اترك عدد الدقائق كما هو؛ **لا تقرب لأسفل**. اختبر 2:20→2:20 و2:27→2:30 و2:42→2:45 و2:59→3:00. لا تجعل UI timer tick هو حدث حفظ دائم.
- **Money:** بعد الحساب بالجنيه: الكسور `.00–.49` تهبط للجنيه الصحيح، `.50–.99` تصعد للجنيه التالي، ثم **إن الفرق إلى مضاعف الـ5 الأعلى يساوي 1 أو 2 جنيه** صعّد إليه؛ وإلا اترك الناتج الصحيح. لا تقرب ماليًا لأسفل إلى مضاعف 5 (102≠100).
- **Shift:** Manager/Owner يستطيع Start/End لوردية **موظف مختار**؛ إذا بقيت جلسات نشطة عند نهاية الوردية، يتم نقل المسؤولية لموظف مخوّل قبل الإغلاق، مع حفظ `OpenedByEmployeeId` وعدم إعادة نسبته تلقائيًا للمستلم.
- **Invoice:** protected Cancel/Void by authorized Manager/Owner؛ `CancellationReason` **Optional**، مع actor/time/audit وحفظ التاريخ، والفواتير الملغاة تُستبعد من الإيراد الفعال وفق تعريف التقرير النهائي؛ **تفاصيل مصير Payment النقدي بعد إلغاء فاتورة مدفوعة ما زالت قرارًا مفتوحًا**، لا تُنشئ Refund تلقائيًا ولا تحذف Payment.
- **PIN:** أول 5 إدخالات PIN خاطئة → تحذير + قفل مؤقت 20 ثانية + security event؛ خمس إضافية بعد دورة القفل → `SecurityLocked` واتصال بالمهندس زياد وفق recovery policy. هذا **غير** Subscription lock. لا تعرض threshold/editing في Settings.
- **Subscription lease:** إذا المتبقي من الاشتراك المدفوع **أكثر من 10 أيام** فلا تتجاوز السلطة Offline **72 ساعة** من تحقق معتمد؛ إذا المتبقي **10 أيام أو أقل** فلا تتجاوز **المتبقي من المدفوع + 10 ساعات grace** وفق قاعدتنا المعتمدة؛ لا يسمح بتعديل الساعة محليًا لمد الصلاحية. خطط توقيع السلطة والتحقق الآمن لاحقًا قبل التنفيذ.
- **Data integrity:** `SQLite transaction` تحفظ الحالة المالية والسجل والـOutbox معًا قبل رسالة Saved؛ idempotent sync via API; never SQL Server direct WPF writes; any failed/unknown ack is retried with **same ID**؛ financial/security conflicts محفوظة للمراجعة دون overwrite صامت.
- **Assets & reports:** assets معدات داخلية بالكمية لكل فرع، بلا بيع منتجات ولا lifecycle تلف لكل controller؛ Daily/Weekly/Monthly/Yearly، Statistics vs Details، monthly closing **review-only** بدون تغيير مالي.

## 12. Frontend implementation contract — مُعتمد كأنه مُنفَّذ في الـDocs، ويجب تنفيذه فعلًا في WPF

**إلى Codex:** اعتبر الحالات أدناه **الصورة المقصودة النهائية لمنتج R1** عند كتابة الواجهة، حتى لو ملف PNG قديم يخالفها. هذا **لا يعني** أن PNG نفسه عُدّل أو أن WPF موجودة. حافظ على visual identity الأصلية (dark black/burgundy + red accent + desktop proportions + EN LTR/AR RTL). بعد تنفيذ كل بند أثبت behavior + screenshots من WPF، ولا تعلّم `VISUAL OPEN` = `Resolved` قبل الاختبار.

| ID | الصور القديمة/المسار المرجعي | الخطأ المرئي أو الناقص | الـWPF النهائي المطلوب تحديدًا / Test |
|---|---|---|---|
| FE-01 | `UI/.../01_Login.png` | PIN ظاهر بالفعل؛ لا يُعد هذا بندًا ناقصًا في الـPNG، لكن UI ليس تنفيذًا. | شاشة دخول الموظف Personal PIN؛ لا تفرض username/password للموظف العادي. تحقق من هوية الموظف وفرعه وصلاحياته وقفل الـPIN. |
| FE-02 | `03_Dashboard.png`, Sidebar لكل شاشة | لقطة Cashier فيها `Management`/`Reports` وقد يستطيع رؤية عناصر لا يملكها. | إذا Cashier: لا navigation/commands إدارية أو تقارير محمية؛ Manager/Owner يظهر له المسموح فقط؛ خدمات/Backend تفرض الصلاحية أيضًا حتى مع direct route. |
| FE-03 | `03_Dashboard.png`, `04_Sessions_Live.png` وسائر Sidebar | Dashboard/Sessions inactive icons بألوان غير متسقة مع البقية. | نفس muted/inactive color لباقي icons؛ red selected state فقط للعنصر الحالي. |
| FE-04 | `03_Dashboard.png` available device card | `Add Order` موجود وSingle/Multi على بطاقة Dashboard بدل Start واحدة. | احذف `Add Order` بالكامل من R1؛ جهاز Available → زر `START` **واحد فقط** → Screen 05. لا تختَر Mode في Dashboard. |
| FE-05 | `05_Session_Start.png` | لا يوجد Billing dropdown بثلاث قيم أو Hours condition بالشكل المطلوب. | Mode = Single/Multi؛ Billing dropdown `Open Time / Hours / Match`؛ `Hours` يكشف Hours numeric input (مثل 2 أو 3)؛ `Match` يستخدم fixed match price/configured duration؛ اختيار غير صالح يمنع Start. Fixed hours → alert عند expiry لا auto-stop. |
| FE-06 | جميع الشاشات غير الرئيسية | Back/Cancel/Close غير منتظم؛ احتمال ضياع السياق. | navigation آمنة وموحّدة للعودة بدون ضياع غير مقصود لفرع/role/قيمة مدخلة، وحماية route وبيانات غير محفوظة حيث يلزم. |
| FE-07 | `10_Session_History.png`; `11–14_Invoice*.png` | تاريخ جلسات يقتصر على completed، وبعض cancellation history غير ظاهر في سياق الجلسة. | عرض سجلات الحالة المرتبطة بالإلغاء/void حسب الدور؛ وجود فاتورة ملغاة لا يمحو جلسة أو payment history؛ authorization على التفاصيل. |
| FE-08 | `15_Shift_Current.png`..`18_Shift_History.png` | لا employee list ولا اختيار شخص في Start/End ولا handover واضح عند end. | Shift UI فيها قائمة موظفين مناسبة للصلاحية وManager/Owner يستطيع Start/End للموظف **المختار**؛ عند جلسات نشطة لا ينتهي Shift قبل authorized handover لكل مسؤولية حالية. يحتفظ system بالـoriginal opener. |
| FE-09 | `23_Management_Employees.png`, `24_Employee_Add_Edit.png` | احتمال الخلط بين Shift UI وEmployee Management. | تبقى Employees CRUD/role management **وحدة منفصلة** عن Shift administration؛ Cashier لا يحصل على صلاحية إدارة الموظفين أو ورديات بقية الناس. |
| FE-10 | `13_Invoice_Cancel.png` | حقل reason موجود لكن مش مصرح أنه Optional في الصورة. | label `Reason (Optional)`/`السبب (اختياري)`، الحقل الفارغ لا يمنع protected cancel، reason إن وُجد يُحفظ، actor/time/audit دائمًا؛ قرار paid-invoice handling محجوز للتصميم المالي. |
| FE-11 | `35_Settings_General.png` | Branch Name يبدو قابلًا للتعديل وله `SAVE CHANGES`; معايير Settings قد تختلط بالتهيئة الإدارية. | Branch Name **read-only** وسلوك Save لا يغيّره؛ لا Default Start Page حتى لو ورد في نسخة قديمة؛ إعدادات شخصية عادية فقط. |
| FE-12 | `36_Settings_Security.png` + Settings navigation | الصورة القديمة موجودة حتى الآن وفيها adjustable lock rules؛ إزالتها موثقة فقط. | **لا تبنِ Screen 36 كواجهة User Settings في R1، ولا رابط لها**؛ سياسات lock ثابتة بالتصميم، recovery محفوظة في مسار محمي منفصل عند الموافقة. لا تطلب حذف الصورة المرجعية من أرشيف UI لمجرد retirement. |
| FE-13 | `37_Settings_Language.png` | موجودة فعليًا ضمن الصور القديمة (ليست بندًا ناقصًا). | English→LTR / Arabic→RTL؛ نفس الحقول/الأزرار والpermissions والـvalidations بلا اختلاف Features. |
| FE-14 | `30_Reports_Statistics.png`..`34_Reports_Shifts.png` | يجب ربط التقارير المكتوبة ببيانات حقيقية وبالصلاحيات، لا يكفي وجود tabs مرسومة. | Daily/Weekly/Monthly/Yearly؛ `Statistics` vs `Details`، Revenue/Expenses/Profit + shifts المعتمدة؛ Manager/Owner فقط؛ Monthly Closing = review-only بدون mutation؛ اقصِ cancelled invoices من active revenue بما لا يمحو تاريخها. |
| FE-15 | `26_Management_Assets.png`, `27_Asset_Add_Edit.png`, `28–29_Expense*.png` | الصور وحدها لا تثبت data model. | assets quantity-based internal equipment per branch، no shop stock/product sales/per-controller damage records. Expense controls حسب الدور وValidation. |
| FE-16 | status/errors لجميع الشاشات | PNG لا يثبت offline saved state, pending sync, conflict/error handling, save confirmations. | أظهر `Saved locally` مختلفًا عن `Synced centrally`، Offline/Retry/Conflict متى يلزم، امنع نجاح UI لو local commit فشل، امنع كشف بيانات/أزرار غير مصرّح بها. نفس المعنى EN/AR. |

**ملاحظة عن screenshots:** الـZIP المرجعي الذي فُحص في جلسة التصميم يحتوي **37 EN + 37 AR** وبينهما `36_Settings_Security.png`. التعديلات `FE-02…12` أعلاه **لم تُطبَّق على الصور القديمة بشكل كامل**. لا تقدّم صور ZIP على أنها صور الواجهة بعد التصحيح. لا حاجة لتوليد PNGs جديدة من تلقاء نفسك: المطلوب تطبيق النتيجة في WPF عند بوابة الكود فقط.

## 13. عناوين ومواضع تعديل الرسومات الحالية — لا إعادة لكل الرسومات

| الملف داخل `Project.Digrams/R1/` (ابحث عن ملفاتك الحقيقية `.drawio`/`.puml`) | أين التعديل المقصود | بعد أي مرحلة؟ | كيف تعرف أنه انتهى؟ |
|---|---|---|---|
| `07_Data_Model/Logical Release 1 ERD.drawio` | `Session`, `SessionEvent`, `Pricing`, `Invoice`, `Payment`, `Shift`, `Employee.SecurityState`, `BranchLicense`, `SyncOperation`; optionality/cardinality/delete rules | Data Dictionary + Financial/Security/Sync design | الحقول والـFK والـSnapshots تكفي للدومين، ولا يفرض الرسم physical table mirroring؛ النسخة المحفوظة source+export متطابقة. |
| `08_Data_Flow/R1_DFD_Level0_Context.drawio` | `Cashier`, `Manager`, `Owner`, `System Operator/Engineer` arrows and data labels | الآن أو في final diagrams pass | one-process context كما هو، inbound/outbound named data، لا labels متداخلة عند قراءة الرسم. |
| `08_Data_Flow/R1_DFD_Level1.drawio` | محاذاة flows لكل Actors مع Level 0؛ فصل `Local SQLite` و`Outbox` عن `Sync/API` وعن `Central SQL Server`؛ report source | بعد Architecture/Sync | لا مسار مباشر WPF/Sync→central DB، لا تبديل write path Online/Offline، تقرير branch-local vs central معلوم. |
| `03_Sequence_Diagrams/*` | completion atomics; same OperationId retries/acks; cancel protected; shift handover; PIN recovery | بعد API/Security/Sync | الطلبات/الاستجابات تطابق approved API contracts ولا يضيع حدث/Payment مع network failure. |
| `04_State_Diagrams/*` | Session/Pause, Invoice paid/cancel, PIN SecurityLocked vs lease state, Sync Pending/Conflict | بعد rules/contracts | لا transitions غير مصرح بها أو model يجمع security/license independent states في state واحد مضلل. |
| `05_C4_Architecture/*` | components and correct async branch-local first/API sync | Architecture final | Level 1/2/3 architecture لا lifelines sequence mistake ولا direct central DB write. |
| `06_Deployment/*` | chosen real host, backups/recovery, machine support | بعد deployment plan | لا تصف الاستضافة أو backup restore المنفذة قبل وجود اختبار. |
| `01_Use_Cases/*`, `02_Activity_Diagrams/*` | تحديث **المتأثر فقط** عند اكتشاف تعارض مثبت في actor/permission/shift/Invoice/Local-first | بعد related decisions | لا إضافات Scope ولا إعادة إنتاج كل الرسم من الصفر. |

## 14. Handoff contract لـCodex (يتنفذ فقط بعد إغلاق التصميم)

قبل كتابة أي كود يقرأ Codex **بالترتيب**: `AboutZeyad.md` لأسلوب التعاون → root `AGENTS.md` حين يُنشأ → `Project.Docs/Reviews/PROJECT_REVIEW_AND_CHANGE_REGISTER.md` → canonical Master/Business Rules/Decisions → Requirements/UX → Database/Architecture/API/Security/Offline-Sync → diagrams ذات الصلة؛ ويقوم بـ`git status` وrepo inventory. **أي مسودة `DESIGN OPEN` أو عقد غير موجود تمنع تنفيذ جزء يعتمد عليها**؛ لا تعني منع كتابة أي كود مستقبلًا بعد موافقة زياد على Slice مستقل مكتمل التفاصيل.

**Codex لا يصل تلقائيًا لهذا الملف من مجرد وجوده في ChatGPT.** زياد ينزله ويحطه في الـRepository الفعلي ويعمل commit. هذه النسخة سجل ملاحظات وتعليمات، **ليست تنفيذًا آليًا للتعديلات في الملفات المذكورة**. لا تعتبر بندًا مغلقًا قبل تحقق `diff + review + test` المناسب له.

### نموذج متابعة لكل بند في هذا السجل

```text
Issue ID: 
Status: Open / Resolved / Deferred (approved)
Changed file path:
Changed section/commit:
Decision or business approval:
Verification (document diff / render / test):
Reviewer and date:
```
