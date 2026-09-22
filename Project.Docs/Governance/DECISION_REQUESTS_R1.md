# +90 PS R1 — Final approval requests (only genuine blockers)

**Status: answers requested from Zeyad; neither assistant nor Codex may quietly choose a business rule.** The items marked FIXED below come from the latest explicit project decisions, but this file does not overwrite the user's existing DECISIONS.md until the original repository copies are reconciled. Technical options are examples, not default approval.

## Already FIXED — don't ask these again

R1 Windows WPF, branch SQLite first (online/offline), ASP.NET Core API→central SQL Server, EF Core Code First; no R1 discounts/customers/bookings/product/cafe/web/mobile; employee PIN on shared PC; protected Manager/Owner invoice Cancel with OPTIONAL reason and non-destructive history; first 5 failed PINs 20-second lock+warning/event and another five SecurityLocked separate from license; offline lease >10 paid days max72h, <=10 days paid remaining+10h grace; free pause; positive seconds ceil whole minute then if 1–3 minutes to next quarter-hour round UP, never down; money .49/.50 rounding to whole EGP then only 1/2 EGP upward to next 5; fixed-hour expiry alert only; Match fixed price with branch-configured duration and manual staff finish; opened-by preserved and active-session responsibility handover before shift closes; assets quantities not products; monthly closing read-only review. These do NOT imply any specific cash reversal or employee offline admin permission.

## Required decision tickets

| ID | Missing answer | Why it matters | Suggested choices to discuss (NOT chosen) | Blocks |
|---|---|---|---|---|
| Q-BIZ-01 | If Manager/Owner cancels an invoice **after cash was received**, does cashier return actual cash, record a correction without refund, or use another approved policy? When is Cancel vs Void allowed? | Accurate revenue, cash audit and Payment schema | explicit refund record / reversal entry / manager-only adjustment; no destructive deletion | Finance schema, paid cancellation API/tests/reports |
| Q-BIZ-02 | On finishing a Session, may an Invoice be Issued without immediately receiving cash; can payment happen later/partially? | One payment vs multiple and atomic transaction boundary | always immediate one full cash payment / allow unpaid and later payment / partial cash | Invoice/Payment cardinality, completion API |
| Q-BIZ-03 | For Match, can one Session include multiple matches/extensions, and what if staff ends early or after configured duration? | Price, duration snapshot, events and final billing | one match = one Session / several matches counted explicitly | Match schema/calc/UAT |
| Q-ORG-01 | Is Owner tied to a specific Business with many Branches? Can a single Employee work in >1 Branch; is one shared Windows PC always one branch? | Branch/owner identity model | one business→many branches, staff belongs to one branch / explicit cross-branch grants | Branch/employee data model and RBAC |
| Q-OFF-01 | Which employee/role changes, PIN resets, price/category edits are allowed while offline? How quickly should central revocation take effect when device remains disconnected? | Theft/revocation and cache expiry | require online for sensitive grants, use cached signed rights only / limited offline authorized edits | Auth, ownership, sync, admin UI |
| Q-SEC-01 | Scope/reset of failed-PIN counts and trusted device recovery; which engineer recovery procedure is acceptable when completely offline? | Prevent bypass by reboot, device cloning, clock rollback | per account+device durable counters, centrally authenticated online reset, expiring signed offline challenge | Security/lease/recovery implementation |
| Q-DATA-01 | Is storing exact amounts as integer piasters accepted, including branch-configurable price precision? What is the business timezone/day boundary for reports? | Money precision, reports and provider mapping | integer piasters + UTC events + explicit branch reporting timezone | DB physical types, report contract |
| Q-OPS-01 | Which hosting/provider, actual Windows/.NET supported targets and safe branch/central backup destinations and key custodian? | Deployment, migrations, DR and real RTO test | choose after budget/device check | Deployment/rollout (may be after first independent domain slice) |

## Engineering proposals requiring design review, not user to invent code

`UUID` storage in SQLite (`TEXT` or `BLOB`), central inbox receipt/operation-key scope, session durable timing/anti-clock-rollback method, provider-specific migrations, error model, typed DTOs, secret storage, operational audit retention and outbox eviction policy. Codex may propose but cannot declare production-safe without tests and explicit approval of architecture/security reviewer (Zeyad's review for the product decision).

## Recording an answer

`Ticket ID | User decision in exact terms | approved date | affected rules/docs/diagrams/tests | reviewer | status`. Never convert silence or “يلا” into consent for irreversible money/security behavior. Answering Q-BIZ-01 doesn't imply automatic refunds unless explicitly stated.
