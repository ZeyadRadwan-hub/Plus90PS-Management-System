# +90 PS R1 — Final approval requests (only genuine blockers)

**Status: only the questions still listed below need answers.** The fixed decisions below override older open-question wording in `DECISIONS.md`; neither assistant nor Codex may quietly choose a remaining business rule. Technical options are examples, not default approval.

## Already FIXED — don't ask these again

R1 Windows WPF, branch SQLite first (online/offline), ASP.NET Core API→central SQL Server, EF Core Code First; no R1 discounts/customers/bookings/product/cafe/web/mobile; Cash-only payment method; Invoice may be issued before Payment; employee PIN on shared PC; protected Manager/Owner invoice Cancel with OPTIONAL reason and non-destructive history; first 5 failed PINs 20-second lock+warning/event and another five SecurityLocked separate from license; offline lease >10 paid days max72h, <=10 days paid remaining+10h grace; free pause; positive seconds ceil whole minute then if 1–3 minutes to next quarter-hour round UP, never down; money .49/.50 rounding to whole EGP then only 1/2 EGP upward to next 5; fixed-hour expiry alert only, never auto-stop; one Match per Session, with a second Match requiring a new Session, fixed Match price with branch-configured reference duration and manual staff finish, never timer auto-completion; opened-by preserved and active-session responsibility handover before shift closes; assets quantities not products; monthly closing read-only review. These do NOT imply any specific cash reversal, partial-payment policy, or employee offline admin permission.

## Required decision tickets

| ID | Missing answer | Why it matters | Suggested choices to discuss (NOT chosen) | Blocks |
|---|---|---|---|---|
| Q-BIZ-01 | If Manager/Owner cancels an invoice **after cash was received**, does cashier return actual cash, record a correction without refund, or use another approved policy? When is Cancel vs Void allowed? | Accurate revenue, cash audit and Payment schema | explicit refund record / reversal entry / manager-only adjustment; no destructive deletion | Finance schema, paid cancellation API/tests/reports |
| Q-BIZ-02 | Invoice-before-Payment is approved. Are partial cash payments supported, and what settlement/cardinality rules apply after issuance? | Payment cardinality and later settlement contract | one full later cash payment / partial cash payments | Payment schema/API/tests; does not block issuing an invoice before payment |
| Q-ORG-01 | Is Owner tied to a specific Business with many Branches? Can a single Employee work in >1 Branch; is one shared Windows PC always one branch? | Branch/owner identity model | one business→many branches, staff belongs to one branch / explicit cross-branch grants | Branch/employee data model and RBAC |
| Q-OFF-01 | Which employee/role changes, PIN resets, price/category edits are allowed while offline? How quickly should central revocation take effect when device remains disconnected? | Theft/revocation and cache expiry | require online for sensitive grants, use cached signed rights only / limited offline authorized edits | Auth, ownership, sync, admin UI |
| Q-SEC-01 | Scope/reset of failed-PIN counts and trusted device recovery; which engineer recovery procedure is acceptable when completely offline? | Prevent bypass by reboot, device cloning, clock rollback | per account+device durable counters, centrally authenticated online reset, expiring signed offline challenge | Security/lease/recovery implementation |
| Q-DATA-01 | Is storing exact amounts as integer piasters accepted, including branch-configurable price precision? What is the business timezone/day boundary for reports? | Money precision, reports and provider mapping | integer piasters + UTC events + explicit branch reporting timezone | DB physical types, report contract |
| Q-OPS-01 | Which hosting/provider, actual Windows/.NET supported targets and safe branch/central backup destinations and key custodian? | Deployment, migrations, DR and real RTO test | choose after budget/device check | Deployment/rollout (may be after first independent domain slice) |

**Resolved ticket Q-BIZ-03:** exactly one Match per Session; a second Match needs a new Session. Match uses captured Match pricing, is not billed by elapsed time, and is completed by staff rather than a timer. The configured duration is a reference, not automatic completion. Invoice-before-Payment is also resolved within Q-BIZ-02; only the narrowed settlement question above remains open.

## Engineering proposals requiring design review, not user to invent code

`UUID` storage in SQLite (`TEXT` or `BLOB`), central inbox receipt/operation-key scope, session durable timing/anti-clock-rollback method, provider-specific migrations, error model, typed DTOs, secret storage, operational audit retention and outbox eviction policy. Codex may propose but cannot declare production-safe without tests and explicit approval of architecture/security reviewer (Zeyad's review for the product decision).

## Recording an answer

`Ticket ID | User decision in exact terms | approved date | affected rules/docs/diagrams/tests | reviewer | status`. Never convert silence or “يلا” into consent for irreversible money/security behavior. Answering Q-BIZ-01 doesn't imply automatic refunds unless explicitly stated.
