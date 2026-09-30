# +90 PS — R1 Physical Data Dictionary (DB-01 + DB-01.1)

Status: DB-01.1 FINANCIAL + BUSINESS DAY DESIGN COMPLETED / READY FOR REVIEW; no table exists yet. Companion [physical design](DATABASE_DESIGN_R1.md) defines authority/status, and [query/index plan](INDEX_QUERY_PLAN_R1.md) defines candidate index IDs. Source shorthand: BE = actual Domain slice; BR = approved Business Rule; TD = TECHNICAL_DECISIONS_R1 section. Q-BIZ-01/02 and Q-REPORT-01 are resolved by the Product Owner on 2026-09-26.

Each row below is one proposed physical column. SQLite type modifiers such as BLOB(16) and TEXT CHECK length<=N are design constraints to express in provider DDL/EF mapping; SQLite affinity alone does not enforce them. All local UTC integers are Unix epoch milliseconds. SQL Server datetime2(3) values are UTC by application contract. Monetary bigint/INTEGER values are EGP piasters, duration bigint/INTEGER values milliseconds. N/Y in nullable, PK, mutable and sensitive are explicit; C means a composite PK member. Unique lists candidate IDs when part of a unique key; Index membership includes PK/UX/IX candidates. “—” means none. “Application” generation occurs outside pure Domain. Unless explicitly stated, Default is none: future commands supply all required data.

String bounds are proposed to keep operational names/codes reasonable: 160 for Business/Branch/Employee names, 80 for Console/Audit action, 120 for asset name, 300 for expense description, 500 for optional prose reason/note, 16–80 for controlled codes. JSON is intentionally unbounded for versioned payloads/context, not generic names. Validate existing Domain/Application inputs against these limits before BE-09 migrations. Sensitive means restrict access/redact output, not that storage contains a raw secret. For a one-provider table, the other provider's type cell is only a mapping comparison; the local/central matrix determines where a physical column is actually deployed.

| Table | Column | Business meaning | Domain type | SQLite type | SQL Server type | Nullable? | PK? | FK? | Unique? | Default? | Generated? | Mutable? | Sync direction | Sensitive? | Index membership | Notes/source |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Businesses | Id | Business identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | C→L | N | PK_Businesses | BE-08; BR-BRANCH-007 |
| Businesses | Name | Business display name | string max 160 | TEXT CHECK length<=160 | nvarchar(160) | N | N | — | — | none | — | Y | C→L | N | — | Limit 160 display characters; BE-08; BR-BRANCH-007 |
| Businesses | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | C→L | N | — | Regenerate on update; BE-08; BR-BRANCH-007 |
| Branches | Id | Branch identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | C→L | N | PK_Branches | BE-08; BR-BRANCH-007; Q-REPORT-01 |
| Branches | BusinessId | Owning Business | Guid | BLOB(16) | uniqueidentifier | N | N | Businesses.Id | — | none | — | N | C→L | N | — | BE-08; BR-BRANCH-007; Q-REPORT-01 |
| Branches | Name | Branch display name | string max 160 | TEXT CHECK length<=160 | nvarchar(160) | N | N | — | — | none | — | Y | C→L | N | — | Limit 160 display characters; BE-08; BR-BRANCH-007; Q-REPORT-01 |
| Branches | TimeZoneId | Reporting/display timezone | string max 64 | TEXT CHECK length<=64 | nvarchar(64) | N | N | — | — | Africa/Cairo | — | Y | C→L | N | — | IANA ID; R1 provisioning/default value; day boundaries remain explicit; Q-REPORT-01 |
| Branches | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | C→L | N | — | Timezone config changes require version; BE-08; Q-REPORT-01 |
| BusinessDays | Id | Explicit operational day identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_BusinessDays | New DB-01.1 shared table; Start Day |
| BusinessDays | BranchId | Owning Branch and open-day uniqueness scope | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | L14/C15 open-only | none | — | N | L→C | N | L14, C15, C16 | NO ACTION; one open row per Branch |
| BusinessDays | StartedByEmployeeId | Authorized employee who started day | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | L→C | Y | — | Manager confirmed 2026-09-30; no inferred Owner inheritance |
| BusinessDays | StartedAtUtc | Explicit Start Day instant | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | L15, C16 | Explicit start; Branch-local start date/month/year selects whole BusinessDay |
| BusinessDays | EndedByEmployeeId | Authorized employee who ended day | Guid | BLOB(16) | uniqueidentifier | Y | N | Employees.Id | — | none | — | Y | L→C | Y | — | Null exactly while open |
| BusinessDays | EndedAtUtc | Explicit End Day instant | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | L→C | N | L14, C15 | Null exactly while open; when set >= StartedAtUtc; explicit next-day transition shares this instant |
| BusinessDays | VersionToken | Optimistic concurrency token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | L→C | N | — | Regenerate when End Day closes row |
| Employees | Id | Business-wide employee identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | C→L | N | PK_Employees | BE-08; Q-ORG-01 |
| Employees | BusinessId | Owning Business | Guid | BLOB(16) | uniqueidentifier | N | N | Businesses.Id | — | none | — | N | C→L | N | C06 | No Employee.BranchId; BE-08; Q-ORG-01 |
| Employees | Name | Employee display name | string max 160 | TEXT CHECK length<=160 | nvarchar(160) | N | N | — | — | none | — | Y | C→L | Y | — | Personal data; BE-08; Q-ORG-01 |
| Employees | Role | Owner 1 Manager 2 Cashier 3 | enum numeric | INTEGER | int | N | N | — | — | none | — | Y | C→L | N | C06 | Validate known role; BE-08; Q-ORG-01 |
| Employees | IsActive | Business-wide activation | bool | INTEGER 0/1 | bit | N | N | — | — | none | — | Y | C→L | N | C06 | BE-08; Q-ORG-01 |
| Employees | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | C→L | N | — | BE-08; Q-ORG-01 |
| GameConsoles | Id | Console identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | C→L | N | PK_GameConsoles | BE-06; BR-CONSOLE-001/003 |
| GameConsoles | BranchId | Owning Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | C→L | N | C07 | BE-06; BR-CONSOLE-001/003 |
| GameConsoles | Name | Console display label | string max 80 | TEXT CHECK length<=80 | nvarchar(80) | N | N | — | — | none | — | Y | C→L | N | C07 | Not unique; BE-06; BR-CONSOLE-001/003 |
| GameConsoles | Generation | PS4 or PS5 numeric | enum numeric | INTEGER | int | N | N | — | — | none | — | Y | C→L | N | — | Validate enum; BE-06; BR-CONSOLE-001/003 |
| GameConsoles | IsActive | Configuration enabled | bool | INTEGER 0/1 | bit | N | N | — | — | none | — | Y | C→L | N | C07 | Not occupancy; BE-06; BR-CONSOLE-001/003 |
| GameConsoles | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | C→L | N | — | BE-06; BR-CONSOLE-001/003 |
| PricingEntries | Id | Persistence row identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | C→L | N | PK_PricingEntries | Not a new Domain field; BE-05; BR-PRICE-001/005/009 |
| PricingEntries | BranchId | Price owning Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | L01, C01 | none | — | N | C→L | N | L01, C01 | BE-05; BR-PRICE-001/005/009 |
| PricingEntries | ConsoleGeneration | PS4 or PS5 key | enum numeric | INTEGER | int | N | N | — | L01, C01 | none | — | N | C→L | N | L01, C01 | BE-05; BR-PRICE-001/005/009 |
| PricingEntries | PlayMode | Single or Multi key | enum numeric | INTEGER | int | N | N | — | L01, C01 | none | — | N | C→L | N | L01, C01 | BE-05; BR-PRICE-001/005/009 |
| PricingEntries | PricingMethod | Hourly or Match key | enum numeric | INTEGER | int | N | N | — | L01, C01 | none | — | N | C→L | N | L01, C01 | BE-05; BR-PRICE-001/005/009 |
| PricingEntries | PricePiasters | Current configured price | decimal EGP | INTEGER piasters | bigint piasters | N | N | — | — | none | — | Y | C→L | N | — | Positive and exact minor units; BE-05; BR-PRICE-001/005/009 |
| PricingEntries | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | C→L | N | — | Snapshot never rewritten; BE-05; BR-PRICE-001/005/009 |
| Sessions | Id | Session identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_Sessions | BE-07; TD §3-4 |
| Sessions | BranchId | Operational Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | C04, C05 | BE-07; TD §3-4 |
| Sessions | BusinessDayId | Day on which Session started | Guid | BLOB(16) | uniqueidentifier | N | N | BusinessDays.Id | — | none | — | N | L→C | N | — | Same Branch required; no index until a day-scoped Session query is approved |
| Sessions | ConsoleId | Played Console | Guid | BLOB(16) | uniqueidentifier | N | N | GameConsoles.Id | — | none | — | N | L→C | N | — | BE-07; TD §3-4 |
| Sessions | OpenedByEmployeeId | Original opener | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | L→C | Y | — | Never replace on handover; BE-07; TD §3-4 |
| Sessions | CurrentResponsibleEmployeeId | Current responsible employee | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | Y | L→C | Y | — | Same-Business scope; BE-07; TD §3-4 |
| Sessions | SessionType | Open Fixed Match numeric | enum numeric | INTEGER | int | N | N | — | — | none | — | N | L→C | N | — | BE-07; TD §3-4 |
| Sessions | ConsoleGeneration | Captured generation | enum numeric | INTEGER | int | N | N | — | — | none | — | N | L→C | N | — | Snapshot; BE-07; TD §3-4 |
| Sessions | PlayMode | Captured mode | enum numeric | INTEGER | int | N | N | — | — | none | — | N | L→C | N | — | Snapshot; BE-07; TD §3-4 |
| Sessions | PricingMethod | Captured method | enum numeric | INTEGER | int | N | N | — | — | none | — | N | L→C | N | — | Snapshot; BE-07; TD §3-4 |
| Sessions | CapturedPricePiasters | Captured price | decimal EGP | INTEGER piasters | bigint piasters | N | N | — | — | none | — | N | L→C | N | — | Do not reference current price for history; BE-07; TD §3-4 |
| Sessions | StartedAtUtc | Actual start | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | L06, C04 | BE-07; TD §3-4 |
| Sessions | CompletedAtUtc | Actual completion | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | L→C | N | L07, C05 | Null until Completed; BE-07; TD §3-4 |
| Sessions | State | Active Paused Completed numeric | enum numeric | INTEGER | int | N | N | — | — | none | — | Y | L→C | N | L06, C04 | State-time CHECK later; BE-07; TD §3-4 |
| Sessions | FixedDurationMs | Intended Fixed duration | TimeSpan | INTEGER ms | bigint ms | Y | N | — | — | none | — | N | L→C | N | — | Only Fixed; BE-07; TD §3-4 |
| Sessions | MatchReferenceDurationMs | Match reference duration | TimeSpan | INTEGER ms | bigint ms | Y | N | — | — | none | — | N | L→C | N | — | Not auto-end; BE-07; TD §3-4 |
| Sessions | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | L→C | N | — | BE-07; TD §3-4 |
| Sessions | AggregateRevision | Event sequence head | long | INTEGER | bigint | N | N | — | — | none | — | Y | L→C | N | — | Separate from VersionToken; BE-07; TD §3-4 |
| SessionEvents | Id | Event identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_SessionEvents | TD §3-4; BR-RECOVERY-002 |
| SessionEvents | SessionId | Parent Session | Guid | BLOB(16) | uniqueidentifier | N | N | Sessions.Id | L02, C02 | none | — | N | L→C | N | L02, C02 | TD §3-4; BR-RECOVERY-002 |
| SessionEvents | BranchId | Branch scope for replay | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | — | TD §3-4; BR-RECOVERY-002 |
| SessionEvents | Revision | Per-Session event order | long | INTEGER | bigint | N | N | — | L02, C02 | none | — | N | L→C | N | L02, C02 | Starts at 1; TD §3-4; BR-RECOVERY-002 |
| SessionEvents | EventType | Start Pause Resume Complete Transfer | string max 32 | TEXT CHECK length<=32 | nvarchar(32) | N | N | — | — | none | — | N | L→C | N | — | Versioned event vocabulary; TD §3-4; BR-RECOVERY-002 |
| SessionEvents | OccurredAtUtc | Actual event time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | — | TD §3-4; BR-RECOVERY-002 |
| SessionEvents | ActorEmployeeId | Acting employee if applicable | Guid | BLOB(16) | uniqueidentifier | Y | N | Employees.Id | — | none | — | N | L→C | Y | — | Null for system action; TD §3-4; BR-RECOVERY-002 |
| SessionEvents | DetailsJson | Structured nonsecret event detail | versioned JSON | TEXT | nvarchar(max) | Y | N | — | — | none | — | N | L→C | Y | — | Schema version in event payload; TD §3-4; BR-RECOVERY-002 |
| ConsoleOccupancies | ConsoleId | Occupied Console and PK | Guid | BLOB(16) | uniqueidentifier | N | Y | GameConsoles.Id | PK | none | — | N | Local | N | PK_ConsoleOccupancies | One occupancy per Console; TD §4; BR-CONSOLE-004 |
| ConsoleOccupancies | SessionId | Current occupying Session | Guid | BLOB(16) | uniqueidentifier | N | N | Sessions.Id | L03 | none | — | N | Local | N | L03 | Unique; TD §4; BR-CONSOLE-004 |
| ConsoleOccupancies | AcquiredAtUtc | Occupancy start evidence | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Local | N | — | Technical row removed on complete; TD §4; BR-CONSOLE-004 |
| Invoices | Id | Invoice identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_Invoices | BR-INVOICE-001/010/011; Q-BIZ-01 |
| Invoices | BranchId | Issuing Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | C08 | BR-INVOICE-001/010/011; Q-BIZ-01 |
| Invoices | BusinessDayId | Day Invoice was issued | Guid | BLOB(16) | uniqueidentifier | N | N | BusinessDays.Id | — | none | — | N | L→C | N | L08, C08 | Same Branch; never rewritten when paid later |
| Invoices | SessionId | Completed Session | Guid | BLOB(16) | uniqueidentifier | N | N | Sessions.Id | L04, C03 | none | — | N | L→C | N | L04, C03 | One R1 Invoice per Session; no reissue workflow; BR-INVOICE-003 |
| Invoices | IssuedByEmployeeId | Issuing employee | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | L→C | Y | — | BR-INVOICE-001/010/011; Q-BIZ-01 |
| Invoices | IssuedAtUtc | Issue time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | L08, C08 | BR-INVOICE-001/010/011; Q-BIZ-01 |
| Invoices | AmountPiasters | Final invoice amount | decimal EGP | INTEGER piasters | bigint piasters | N | N | — | — | none | — | N | L→C | N | — | Formula not defined here; BR-INVOICE-001/010/011; Q-BIZ-01 |
| Invoices | TotalPausedDurationMs | Paused time displayed | TimeSpan | INTEGER ms | bigint ms | N | N | — | — | none | — | N | L→C | N | — | Zero for non-paused; BR-INVOICE-001/010/011; Q-BIZ-01 |
| Invoices | CancelledAtUtc | Protected Cancel instant | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | L→C | N | — | Null while issued; nonnull when cancelled; Q-BIZ-01 |
| Invoices | CancelledByEmployeeId | Protected Cancel actor | Guid | BLOB(16) | uniqueidentifier | Y | N | Employees.Id | — | none | — | Y | L→C | Y | — | Null while issued; required when cancelled; Q-BIZ-01 |
| Invoices | CancelReason | Optional Cancel reason | string max 500 | TEXT CHECK length<=500 | nvarchar(500) | Y | N | — | — | none | — | Y | L→C | Y | — | Null permitted even when cancelled; Q-BIZ-01 |
| Invoices | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | L→C | N | — | BR-INVOICE-001/010/011; Q-BIZ-01 |
| Payments | Id | Cash receipt identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_Payments | BR-PAY-001/002; Q-BIZ-02 |
| Payments | BranchId | Receiving Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | C10 | BR-PAY-001/002; Q-BIZ-02 |
| Payments | BusinessDayId | Day Cash was received | Guid | BLOB(16) | uniqueidentifier | N | N | BusinessDays.Id | — | none | — | N | L→C | N | L10, C10 | Same Branch; may differ from Invoice.BusinessDayId |
| Payments | InvoiceId | Invoice fully settled | Guid | BLOB(16) | uniqueidentifier | N | N | Invoices.Id | L09, C09 | none | — | N | L→C | N | L09, C09 | One full Cash Payment per Invoice, no partial; Q-BIZ-02 |
| Payments | ReceivedByEmployeeId | Cash receiving employee | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | L→C | Y | — | BR-PAY-001/002; Q-BIZ-02 |
| Payments | AmountPiasters | Full Cash settlement | decimal EGP | INTEGER piasters | bigint piasters | N | N | — | — | none | — | N | L→C | N | — | >0 and equals Invoice.AmountPiasters transactionally; Q-BIZ-02 |
| Payments | PaidAtUtc | Receipt time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | L10, C10 | BR-PAY-001/002; Q-BIZ-02 |
| Shifts | Id | Shift identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_Shifts | BR-SHIFT-001/003/005 |
| Shifts | BranchId | Shift Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | C14 | BR-SHIFT-001/003/005 |
| Shifts | StartedByEmployeeId | Administrative starter | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | L→C | Y | — | BR-SHIFT-001/003/005 |
| Shifts | StartedAtUtc | Start time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | — | BR-SHIFT-001/003/005 |
| Shifts | EndedByEmployeeId | Administrative closer | Guid | BLOB(16) | uniqueidentifier | Y | N | Employees.Id | — | none | — | Y | L→C | Y | — | Null while open; BR-SHIFT-001/003/005 |
| Shifts | EndedAtUtc | End time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | L→C | N | C14 | Null while open; BR-SHIFT-001/003/005 |
| Shifts | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | L→C | N | — | No payroll; BR-SHIFT-001/003/005 |
| Expenses | Id | Expense identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_Expenses | BR-EXP-001/003; Q-REPORT-01 |
| Expenses | BranchId | Expense Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | C11 | BR-EXP-001/003; Q-REPORT-01 |
| Expenses | BusinessDayId | Day Expense was recorded | Guid | BLOB(16) | uniqueidentifier | N | N | BusinessDays.Id | — | none | — | N | L→C | N | L11, C11 | Same Branch; drives day expense/profit report |
| Expenses | AmountPiasters | Expense amount | decimal EGP | INTEGER piasters | bigint piasters | N | N | — | — | none | — | N | L→C | N | — | Positive; BR-EXP-001/003; Q-REPORT-01 |
| Expenses | CategoryCode | Optional category label | string max 32 | TEXT CHECK length<=32 | nvarchar(32) | Y | N | — | — | none | — | Y | L→C | N | — | No elaborate accounting taxonomy; BR-EXP-001/003; Q-REPORT-01 |
| Expenses | Description | Expense description | string max 300 | TEXT CHECK length<=300 | nvarchar(300) | N | N | — | — | none | — | Y | L→C | Y | — | Bounded 300; BR-EXP-001/003; Q-REPORT-01 |
| Expenses | Note | Optional repair/context note | string max 500 | TEXT CHECK length<=500 | nvarchar(500) | Y | N | — | — | none | — | Y | L→C | Y | — | Bounded 500; BR-EXP-001/003; Q-REPORT-01 |
| Expenses | CreatedByEmployeeId | Recording employee | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | L→C | Y | — | BR-EXP-001/003; Q-REPORT-01 |
| Expenses | OccurredAtUtc | Expense time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C | N | L11, C11 | BR-EXP-001/003; Q-REPORT-01 |
| Expenses | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | L→C | N | — | BR-EXP-001/003; Q-REPORT-01 |
| BranchAssets | Id | Quantity row identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C | N | PK_BranchAssets | BR-ASSET-002/005 |
| BranchAssets | BranchId | Owning Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | L→C | N | C12 | BR-ASSET-002/005 |
| BranchAssets | CategoryCode | Controller Accessory Equipment Asset | string max 24 | TEXT CHECK length<=24 | nvarchar(24) | N | N | — | — | none | — | Y | L→C | N | — | Controlled R1 categories; BR-ASSET-002/005 |
| BranchAssets | Name | Equipment display name | string max 120 | TEXT CHECK length<=120 | nvarchar(120) | N | N | — | — | none | — | Y | L→C | N | — | Not unique; BR-ASSET-002/005 |
| BranchAssets | Quantity | Nonnegative count | int | INTEGER | int | N | N | — | — | none | — | Y | L→C | N | — | No serial tracking; BR-ASSET-002/005 |
| BranchAssets | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | L→C | N | — | BR-ASSET-002/005 |
| AuditEvents | Id | Audit identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | L→C or central | N | PK_AuditEvents | BR-AUDIT-001/004; TD §6 |
| AuditEvents | BusinessId | Business scope | Guid | BLOB(16) | uniqueidentifier | N | N | Businesses.Id | — | none | — | N | L→C or central | N | — | BR-AUDIT-001/004; TD §6 |
| AuditEvents | BranchId | Branch scope if applicable | Guid | BLOB(16) | uniqueidentifier | Y | N | Branches.Id | — | none | — | N | L→C or central | N | C13 | Null for business-wide central action; BR-AUDIT-001/004; TD §6 |
| AuditEvents | ActorEmployeeId | Actor if person | Guid | BLOB(16) | uniqueidentifier | Y | N | Employees.Id | — | none | — | N | L→C or central | Y | — | Null for system actor; BR-AUDIT-001/004; TD §6 |
| AuditEvents | Action | Stable action name | string max 80 | TEXT CHECK length<=80 | nvarchar(80) | N | N | — | — | none | — | N | L→C or central | N | — | No secrets; BR-AUDIT-001/004; TD §6 |
| AuditEvents | EntityType | Target kind | string max 64 | TEXT CHECK length<=64 | nvarchar(64) | N | N | — | — | none | — | N | L→C or central | N | — | BR-AUDIT-001/004; TD §6 |
| AuditEvents | EntityId | Target identity if applicable | Guid | BLOB(16) | uniqueidentifier | Y | N | — | — | none | — | N | L→C or central | N | — | Polymorphic target no FK; BR-AUDIT-001/004; TD §6 |
| AuditEvents | OccurredAtUtc | Audit time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | L→C or central | N | L12, C13 | BR-AUDIT-001/004; TD §6 |
| AuditEvents | Reason | Optional protected-action reason | string max 500 | TEXT CHECK length<=500 | nvarchar(500) | Y | N | — | — | none | — | N | L→C or central | Y | — | BR-AUDIT-001/004; TD §6 |
| AuditEvents | BeforeJson | Redacted before context | versioned JSON | TEXT | nvarchar(max) | Y | N | — | — | none | — | N | L→C or central | Y | — | Never secrets; BR-AUDIT-001/004; TD §6 |
| AuditEvents | AfterJson | Redacted after context | versioned JSON | TEXT | nvarchar(max) | Y | N | — | — | none | — | N | L→C or central | Y | — | Never secrets; BR-AUDIT-001/004; TD §6 |
| OutboxOperations | LocalSequence | Monotonic local processing key | long | INTEGER | bigint | N | Y | — | PK | none | SQLite rowid | N | Local→API | N | PK_OutboxOperations, L13 | Only local integer identity exception; TD §8; BR-SYNC-001/005 |
| OutboxOperations | OperationId | Stable retry identity | Guid | BLOB(16) | uniqueidentifier | N | N | — | L05 | none | Application | N | Local→API | N | L05 | Unique; TD §8; BR-SYNC-001/005 |
| OutboxOperations | BranchId | Origin Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | AggregateType | Aggregate kind | string max 64 | TEXT CHECK length<=64 | nvarchar(64) | N | N | — | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | AggregateId | Aggregate identity | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | AggregateRevision | Ordered aggregate revision | long | INTEGER | bigint | N | N | — | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | OperationType | Versioned operation name | string max 80 | TEXT CHECK length<=80 | nvarchar(80) | N | N | — | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | SchemaVersion | Payload schema version | int | INTEGER | int | N | N | — | — | none | — | N | Local→API | N | — | Positive; TD §8; BR-SYNC-001/005 |
| OutboxOperations | OccurredAtUtc | Business occurrence time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | PayloadJson | Immutable canonical payload | versioned JSON | TEXT | nvarchar(max) | N | N | — | — | none | — | N | Local→API | Y | — | Sensitive business data possible; TD §8; BR-SYNC-001/005 |
| OutboxOperations | PayloadHash | Immutable SHA-256 payload digest | byte[32] | BLOB(32) | binary(32) | N | N | — | — | none | — | N | Local→API | Y | — | 32 bytes; TD §8; BR-SYNC-001/005 |
| OutboxOperations | Status | Pending Syncing Synced Rejected Conflict | enum numeric | INTEGER | int | N | N | — | — | none | — | Y | Local→API | N | L13 | Persist numeric stable codes after status design; retry remains Pending with AttemptCount; TD §8; BR-SYNC-001/005 |
| OutboxOperations | AttemptCount | Worker attempts | int | INTEGER | int | N | N | — | — | none | — | Y | Local→API | N | — | Nonnegative; TD §8; BR-SYNC-001/005 |
| OutboxOperations | NextAttemptAtUtc | Next due attempt | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | Local→API | N | L13 | Null when terminal; TD §8; BR-SYNC-001/005 |
| OutboxOperations | LastErrorCode | Last retry/rejection code | string max 48 | TEXT CHECK length<=48 | nvarchar(48) | Y | N | — | — | none | — | Y | Local→API | N | — | No secret/detail; TD §8; BR-SYNC-001/005 |
| OutboxOperations | CreatedAtUtc | Local commit time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Local→API | N | — | TD §8; BR-SYNC-001/005 |
| OutboxOperations | AcknowledgedAtUtc | Central receipt time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | Local→API | N | — | Null until accepted; TD §8; BR-SYNC-001/005 |
| InboxReceipts | BranchId | Idempotency scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | Branches.Id | PK | none | — | N | API central | N | PK_InboxReceipts | TD §8; BR-SYNC-004 |
| InboxReceipts | OperationId | Operation key and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | — | PK | none | — | N | API central | N | PK_InboxReceipts | TD §8; BR-SYNC-004 |
| InboxReceipts | PayloadHash | Accepted canonical digest | byte[32] | BLOB(32) | binary(32) | N | N | — | — | none | — | N | API central | Y | — | Different hash is conflict; TD §8; BR-SYNC-004 |
| InboxReceipts | ResultCode | Stored accepted result code | string max 48 | TEXT CHECK length<=48 | nvarchar(48) | N | N | — | — | none | — | N | API central | N | — | TD §8; BR-SYNC-004 |
| InboxReceipts | ResultJson | Stored replay result | versioned JSON | TEXT | nvarchar(max) | Y | N | — | — | none | — | N | API central | Y | — | No secret tokens; TD §8; BR-SYNC-004 |
| InboxReceipts | ReceivedAtUtc | Server receipt time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | API central | N | — | TD §8; BR-SYNC-004 |
| InboxReceipts | AppliedAtUtc | Atomic effect completion time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | API central | N | — | Receipt/effect same transaction; TD §8; BR-SYNC-004 |
| Devices | Id | Provisioned Device identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Application | N | Central→binding | N | PK_Devices | TD §6-7 |
| Devices | BranchId | Bound Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | Central→binding | N | — | Reprovision creates new DeviceId/binding, never edit BranchId in place; TD §6-7 |
| Devices | CredentialHash | One-way device credential digest | byte[32] | BLOB(32) | binary(32) | N | N | — | — | none | — | Y | Central→binding | Y | — | Raw 256-bit secret outside DB; TD §6-7 |
| Devices | SecurityVersion | Central auth/security version | long | INTEGER | bigint | N | N | — | — | none | — | Y | Central→binding | N | — | TD §6-7 |
| Devices | IsRevoked | Revocation state | bool | INTEGER 0/1 | bit | N | N | — | — | none | — | Y | Central→binding | N | — | TD §6-7 |
| Devices | ProvisionedAtUtc | Provision time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Central→binding | N | — | TD §6-7 |
| Devices | RevokedAtUtc | Revocation time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | Central→binding | N | — | TD §6-7 |
| Devices | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Central→binding | N | — | TD §6-7 |
| LocalDeviceBindings | DeviceId | Local installed Device | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Provisioning | N | Provisioning local | N | PK_LocalDeviceBindings | Matches central Devices.Id; TD §6; Q-ORG-01 |
| LocalDeviceBindings | BranchId | Sole bound Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | Provisioning local | N | — | No in-place branch swap; TD §6; Q-ORG-01 |
| LocalDeviceBindings | ProvisionedAtUtc | Local provision time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Provisioning local | N | — | TD §6; Q-ORG-01 |
| LocalDeviceBindings | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Provisioning local | N | — | Raw secret in DPAPI-protected store outside SQLite; TD §6; Q-ORG-01 |
| PinVerifiers | EmployeeId | Credential owner and PK | Guid | BLOB(16) | uniqueidentifier | N | Y | Employees.Id | PK | none | — | N | Central | Y | PK_PinVerifiers | TD §6 |
| PinVerifiers | Salt | Per-credential random salt | byte[16] | BLOB(16) | binary(16) | N | N | — | — | none | — | Y | Central | Y | — | Minimum 16 bytes; TD §6 |
| PinVerifiers | DerivedHash | PBKDF2 derived verifier | byte[32] | BLOB(32) | binary(32) | N | N | — | — | none | — | Y | Central | Y | — | No plaintext PIN/pepper; TD §6 |
| PinVerifiers | AlgorithmCode | Verifier algorithm ID | string max 32 | TEXT CHECK length<=32 | nvarchar(32) | N | N | — | — | none | — | Y | Central | N | — | PBKDF2-HMAC-SHA256 baseline; TD §6 |
| PinVerifiers | WorkFactor | Iteration count | int | INTEGER | int | N | N | — | — | none | — | Y | Central | N | — | Baseline 600000; TD §6 |
| PinVerifiers | CredentialVersion | PIN reset version | long | INTEGER | bigint | N | N | — | — | none | — | Y | Central | N | — | TD §6 |
| PinVerifiers | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Central | N | — | Central pepper outside SQL; TD §6 |
| OfflinePinVerifiers | BranchId | Verifier Branch scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | Branches.Id | PK | none | — | N | Provisioning local | Y | PK_OfflinePinVerifiers | TD §6 |
| OfflinePinVerifiers | DeviceId | Verifier Device scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | LocalDeviceBindings.DeviceId | PK | none | — | N | Provisioning local | Y | PK_OfflinePinVerifiers | TD §6 |
| OfflinePinVerifiers | EmployeeId | Verifier Employee and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | Employees.Id | PK | none | — | N | Provisioning local | Y | PK_OfflinePinVerifiers | TD §6 |
| OfflinePinVerifiers | Salt | Device-specific random salt | byte[16] | BLOB(16) | binary(16) | N | N | — | — | none | — | Y | Provisioning local | Y | — | TD §6 |
| OfflinePinVerifiers | DerivedHash | Device-bound verifier | byte[32] | BLOB(32) | binary(32) | N | N | — | — | none | — | Y | Provisioning local | Y | — | TD §6 |
| OfflinePinVerifiers | AlgorithmCode | Verifier algorithm ID | string max 32 | TEXT CHECK length<=32 | nvarchar(32) | N | N | — | — | none | — | Y | Provisioning local | N | — | TD §6 |
| OfflinePinVerifiers | WorkFactor | Iteration count | int | INTEGER | int | N | N | — | — | none | — | Y | Provisioning local | N | — | TD §6 |
| OfflinePinVerifiers | CredentialVersion | PIN reset version | long | INTEGER | bigint | N | N | — | — | none | — | Y | Provisioning local | N | — | TD §6 |
| OfflinePinVerifiers | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Provisioning local | N | — | Offline pepper outside SQLite under DPAPI; TD §6 |
| PinAttemptStates | BranchId | Attempt scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | Branches.Id | PK | none | — | N | Local | Y | PK_PinAttemptStates | TD §6; BR-PIN-001/002 |
| PinAttemptStates | DeviceId | Attempt scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | LocalDeviceBindings.DeviceId | PK | none | — | N | Local | Y | PK_PinAttemptStates | TD §6; BR-PIN-001/002 |
| PinAttemptStates | EmployeeId | Attempt scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | Employees.Id | PK | none | — | N | Local | Y | PK_PinAttemptStates | TD §6; BR-PIN-001/002 |
| PinAttemptStates | CycleNumber | First or second cycle | int | INTEGER | int | N | N | — | — | none | — | Y | Local | N | — | TD §6; BR-PIN-001/002 |
| PinAttemptStates | FailuresInCycle | Failures in current cycle | int | INTEGER | int | N | N | — | — | none | — | Y | Local | N | — | TD §6; BR-PIN-001/002 |
| PinAttemptStates | TemporaryLockedUntilUtc | 20-second lock expiry | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | Local | N | — | TD §6; BR-PIN-001/002 |
| PinAttemptStates | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Local | N | — | Persist across restart; TD §6; BR-PIN-001/002 |
| DeviceSecurityStates | BranchId | State scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | Branches.Id | PK | none | — | N | Local | N | PK_DeviceSecurityStates | TD §6-7; BR-PIN-003 |
| DeviceSecurityStates | DeviceId | State scope and PK part | Guid | BLOB(16) | uniqueidentifier | N | C | LocalDeviceBindings.DeviceId | PK | none | — | N | Local | N | PK_DeviceSecurityStates | TD §6-7; BR-PIN-003 |
| DeviceSecurityStates | SecurityVersion | Signed recovery version | long | INTEGER | bigint | N | N | — | — | none | — | Y | Local | N | — | TD §6-7; BR-PIN-003 |
| DeviceSecurityStates | IsSecurityLocked | Security lock only | bool | INTEGER 0/1 | bit | N | N | — | — | none | — | Y | Local | N | — | Separate from license lock; TD §6-7; BR-PIN-003 |
| DeviceSecurityStates | LastTrustedUtc | Clock rollback defense floor | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | Local | N | — | TD §6-7; BR-PIN-003 |
| DeviceSecurityStates | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Local | N | — | TD §6-7; BR-PIN-003 |
| OfflineAuthorities | AuthorityId | Signed grant identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Central | N | Central→local | N | PK_OfflineAuthorities | TD §7; BR-SUB-003 |
| OfflineAuthorities | BranchId | Authorized Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | DeviceId | Authorized Device | Guid | BLOB(16) | uniqueidentifier | N | N | LocalDeviceBindings.DeviceId | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | GrantVersion | Signed format version | int | INTEGER | int | N | N | — | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | KeyId | Public verification key selector | string max 64 | TEXT CHECK length<=64 | nvarchar(64) | N | N | — | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | IssuedAtUtc | Grant issue time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | OfflineAllowedUntilUtc | Signed expiry | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | SecurityVersion | Signed security state version | long | INTEGER | bigint | N | N | — | — | none | — | N | Central→local | N | — | TD §7; BR-SUB-003 |
| OfflineAuthorities | SignedEnvelope | Signed grant payload/signature | signed bytes | BLOB | varbinary(max) | N | N | — | — | none | — | N | Central→local | Y | — | No private key; TD §7; BR-SUB-003 |
| UsedRecoveryChallenges | ChallengeId | Consumed challenge identity | Guid | BLOB(16) | uniqueidentifier | N | Y | — | PK | none | Local | N | Local | N | PK_UsedRecoveryChallenges | Reject reuse; TD §7; BR-PIN-006 |
| UsedRecoveryChallenges | BranchId | Challenge Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | Local | N | — | TD §7; BR-PIN-006 |
| UsedRecoveryChallenges | DeviceId | Challenge Device | Guid | BLOB(16) | uniqueidentifier | N | N | LocalDeviceBindings.DeviceId | — | none | — | N | Local | N | — | TD §7; BR-PIN-006 |
| UsedRecoveryChallenges | SecurityVersion | Matched lock version | long | INTEGER | bigint | N | N | — | — | none | — | N | Local | N | — | TD §7; BR-PIN-006 |
| UsedRecoveryChallenges | ConsumedAtUtc | Consumption time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | Local | N | — | TD §7; BR-PIN-006 |
| ApiSessionTokens | TokenHash | Opaque token SHA-256 and PK | byte[32] | BLOB(32) | binary(32) | N | Y | — | PK | none | API | N | API central | Y | PK_ApiSessionTokens | Plaintext token never stored; TD §6 |
| ApiSessionTokens | EmployeeId | Authenticated employee | Guid | BLOB(16) | uniqueidentifier | N | N | Employees.Id | — | none | — | N | API central | Y | — | TD §6 |
| ApiSessionTokens | BranchId | Authorized Branch | Guid | BLOB(16) | uniqueidentifier | N | N | Branches.Id | — | none | — | N | API central | N | — | TD §6 |
| ApiSessionTokens | DeviceId | Authorized Device | Guid | BLOB(16) | uniqueidentifier | N | N | Devices.Id | — | none | — | N | API central | N | — | TD §6 |
| ApiSessionTokens | SecurityVersion | Authorization version | long | INTEGER | bigint | N | N | — | — | none | — | N | API central | N | — | TD §6 |
| ApiSessionTokens | CreatedAtUtc | Token issue time | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | API central | N | — | TD §6 |
| ApiSessionTokens | ExpiresAtUtc | Short-lived expiry | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | N | N | — | — | none | — | N | API central | N | — | About 15 minutes baseline; TD §6 |
| SubscriptionStates | BranchId | Subscription scope and PK | Guid | BLOB(16) | uniqueidentifier | N | Y | Branches.Id | PK | none | — | N | Central | N | PK_SubscriptionStates | TD §7; BR-SUB-001/007 |
| SubscriptionStates | PaidThroughUtc | Paid period end | DateTimeOffset UTC | INTEGER epoch-ms | datetime2(3) UTC | Y | N | — | — | none | — | Y | Central | N | — | Null if no paid period yet; commercial source later review; TD §7; BR-SUB-001/007 |
| SubscriptionStates | IsSuspended | Central suspension | bool | INTEGER 0/1 | bit | N | N | — | — | none | — | Y | Central | N | — | TD §7; BR-SUB-001/007 |
| SubscriptionStates | VersionToken | Optimistic token | Guid | BLOB(16) | uniqueidentifier | N | N | — | — | none | Application | Y | Central | N | — | No local editable subscription; TD §7; BR-SUB-001/007 |
## Conditional constraints and scope notes

- Sessions: CompletedAtUtc is NULL exactly while not Completed, non-NULL when Completed; FixedDurationMs is positive only for Fixed, MatchReferenceDurationMs positive only for Match, and Open has neither. The exact provider CHECK expressions and event replay mapping need BE-09 review. No MatchCount or hourly charge formula.
- BusinessDays: Open is EndedAtUtc=NULL, with no duplicate state column. Provider CHECK: (EndedAtUtc IS NULL AND EndedByEmployeeId IS NULL) OR (EndedAtUtc IS NOT NULL AND EndedByEmployeeId IS NOT NULL AND EndedAtUtc >= StartedAtUtc). L14/C15 enforce one open day per Branch. A closed day is historical. VersionToken changes on End Day. No midnight auto-close or auto-open after restart.
- Invoices: UNIQUE SessionId (L04/C03). Provider CHECK: (CancelledAtUtc IS NULL AND CancelledByEmployeeId IS NULL AND CancelReason IS NULL) OR (CancelledAtUtc IS NOT NULL AND CancelledByEmployeeId IS NOT NULL AND CancelledAtUtc >= IssuedAtUtc). Reason remains optional. Cancel state derives from cancellation fields; Paid derives from existence of Payment. No duplicate StatusCode. A successful Payment blocks Cancel through future local/central transaction and concurrency checks, not a cross-table CHECK. Cancel never refunds/reverses cash.
- Payments: UNIQUE InvoiceId (L09/C09) means at most one full Cash receipt. AmountPiasters > 0 and must equal Invoice.AmountPiasters, verified in the future local and central transaction, not a cross-table CHECK. No partial settlement, remaining balance, or speculative payment method column.
- Reporting: stored instants remain UTC. Explicit BusinessDays, not wall-clock midnight, define Daily. Payment.BusinessDayId represents cash receipt day, independently from Invoice.BusinessDayId; Expense.BusinessDayId represents recording day. Branches.TimeZoneId defaults to Africa/Cairo for calendar labels/range interpretation. Monthly, selected six-calendar-month and Yearly aggregates use selected BusinessDays; no report tables.
- FKs are NO ACTION/RESTRICT, including BusinessDays.BranchId/StartedByEmployeeId/nullable EndedByEmployeeId and Sessions/Invoices/Payments/Expenses.BusinessDayId. Same-Branch/same-Business cross-row checks and composite security FKs require provider review. No historical/audit cascade. Shifts.BusinessDayId is deliberately absent pending an approved shift/day association.
- Unique candidate IDs L01/C01 price combination, L02/C02 SessionId+Revision, L03 occupancy SessionId, L04/C03 Invoice.SessionId, L05 Outbox OperationId, L09/C09 Payment.InvoiceId, L14/C15 one open BusinessDay per Branch; ConsoleOccupancies.ConsoleId is PK, and InboxReceipts(BranchId,OperationId) is composite PK. No uniqueness on names.
- No raw PIN, PIN pepper, raw DeviceCredential, private ECDSA key or plaintext API token is a column. Device secret and offline pepper are protected outside databases; central pepper/key are protected outside SQL Server. Password/verifier hashes, token hashes, signed grants, financial rows and personal data remain sensitive even without raw secrets.

## BusinessDay constraints clarified — 2026-09-30

Manager is the confirmed Start/End Day role; Owner inheritance is not inferred. End Day requires no unfinished Active/Paused Session. Calendar attribution uses StartedAtUtc in the Branch reporting timezone, Africa/Cairo for R1: all financial rows assigned to that BusinessDay aggregate in its starting month/year, even across a boundary. No extra persisted month field, report-total table or new index is introduced by this clarification. At an explicit next-day transition, previous EndedAtUtc = next StartedAtUtc; finalize atomic command ordering later. See DECISION_REQUESTS_R1.md and the companion physical design. Existing proposed constraints/mappings remain design-only and require review.
