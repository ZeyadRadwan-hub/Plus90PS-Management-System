# سجل التغييرات

> **Historical documentation changelog.** Entries below describe an older planning-copy reconciliation, not the present repository implementation or today's open R1 decisions. Match is now one per Session with employee completion (no timer auto-completion); see [DECISION_REQUESTS_R1.md](DECISION_REQUESTS_R1.md) and [CURRENT_STATE.md](../Reviews/CURRENT_STATE.md).

تغييرات هذه المهمة منفذة في النسخة المستقلة، ولا تعني تنفيذ البرنامج. كل الأقسام غير المعدلة محفوظة نصيًا من الأصل. نطاق R1 الأصلي محفوظ؛ لا إضافة AI أو موقع كاشير أو حذف وظيفة.

| التغيير | السابق | الحالي | السبب/الملفات |
|---|---|---|---|
| الوقت | Open والتقريب مفتوحان أو بدائل عامة | قاعدة المستخدم مع أسئلة التفاصيل | AS_IS، RELEASE_1، RELEASE_3، SHARED_RULES |
| الفصل بين الواقع والمطلوب | احتمال نسبة قاعدة البرنامج للمحل | تمييز AS-IS وTO-BE | AS_IS، DISCOVERY_BRIEF |
| الماتش | تعريف قطعي كمدة ثابتة | القدرة محفوظة؛ الإنهاء يحتاج قرارًا | MASTER، RELEASE_1، RELEASE_3، DISCOVERY_BRIEF |
| الحفظ | مساران Online/Offline | Local-first للنواة + API sync | ملفات المعمارية وSHARED_RULES |
| الترتيب | Sync/اختبارات متأخرة | M1/M2 مبكرًا واختبارات كل جزء | ROADMAP، ملفا التدفق، MASTER، RELEASE_1، RELEASE_2 |
| الملكية | قواعد مكررة | مرجع مشترك وإحالات لكل إصدار | الملفات الستة |
| تغطية النطاق | قوائم دون معالم دقيقة | 30 مجموعة قدرات وقبول | R1_ACCEPTANCE |
| جاهزية الكود | غير محددة | فحص مصدر وحدود إعادة استخدام | CODE_REVIEW |

## سجل كل قسم عُدّل
- C001: [MASTER.md](MASTER.md) — # 44. Offline-First Architecture: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C002: [MASTER.md](MASTER.md) — # 45. Online Mode: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C003: [MASTER.md](MASTER.md) — # 46. Offline Mode: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C004: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 53. Online Architecture: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C005: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 54. Offline Architecture: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C006: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 115. Technical Flow — Session: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C007: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 80. Offline Architecture: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C008: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 81. Online Flow: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C009: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 82. Offline Flow: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C010: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 233. Final Offline Flow: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C011: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 25. Offline Requirement: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C012: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 26. Online Requirement: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C013: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 34. High-Level Architecture: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C014: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 38. First Release Offline Flow: Unify branch operational writes and remove connectivity-based dual write paths (ADR-01).
- C015: [MASTER.md](MASTER.md) — # 68. Sequence Diagram — Start Session: Align start-session sequence with atomic local write and asynchronous sync.
- C016: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 78. Sequence Diagram — Start Session: Align start-session sequence with atomic local write and asynchronous sync.
- C017: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 26. Hourly Pricing: Replace open/alternative rounding policy with latest explicit user instruction; preserve remaining questions.
- C018: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 25. Hourly Pricing: Replace open/alternative rounding policy with latest explicit user instruction; preserve remaining questions.
- C019: [MASTER.md](MASTER.md) — # 25. Match-Based Pricing: Distinguish proposed fixed duration from observed timer use and unresolved completion semantics.
- C020: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 27. Match Pricing: Distinguish proposed fixed duration from observed timer use and unresolved completion semantics.
- C021: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 28. Match Business Rule: Distinguish proposed fixed duration from observed timer use and unresolved completion semantics.
- C022: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 26. Match Pricing: Distinguish proposed fixed duration from observed timer use and unresolved completion semantics.
- C023: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 11. Match Pricing: Distinguish proposed fixed duration from observed timer use and unresolved completion semantics.
- C024: [MASTER.md](MASTER.md) — # 166. Recommended First Development Order: Move local operation, sync and verification early without reducing Release 1 scope.
- C025: [MASTER.md](MASTER.md) — # 167. Recommended Release 1 Development Slices: Move local operation, sync and verification early without reducing Release 1 scope.
- C026: [MASTER.md](MASTER.md) — # 178. Recommended Next Work Item: Move local operation, sync and verification early without reducing Release 1 scope.
- C027: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 157. Release 1 Development Order: Move local operation, sync and verification early without reducing Release 1 scope.
- C028: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 158. Release 1 Milestones: Move local operation, sync and verification early without reducing Release 1 scope.
- C029: [MASTER.md](MASTER.md) — # 0. Purpose of This Document: Define documentation ownership and provenance.
- C030: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 0. How to Use This File: Keep release-specific source of truth without duplicating shared policy.
- C031: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 185. Open Decisions Before Final Implementation: Replace stale open items with staged decision register and trace old IDs.
- C032: [AS_IS.md](DISCOVERY(1).md) — # 6. Determine Session Duration: Apply actual user clarification on Open; avoid inventing automatic match end.
- C033: [AS_IS.md](DISCOVERY(1).md) — # 8. During the Session: Add described manual timer behavior.
- C034: [AS_IS.md](DISCOVERY(1).md) — # 32. AS-IS Business Rules Discovered: Separate observed operation from desired software behavior.
- C035: [AS_IS.md](DISCOVERY(1).md) — # 33. Important Questions Still Open: Close answered questions while preserving genuine discovery gaps.
- C036: [AS_IS.md](DISCOVERY(1).md) — # 35. Transition to TO-BE: Replace linear documentation gate with just-in-time first slice.
- C037: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 44. Current Discovery Status: State accurate planning vs implementation status.
- C038: [DISCOVERY_BRIEF.md](DISCOVERY(1).md) — # 45. Approval State: Avoid equating legacy assistant suggestions with user approval.
- C039: [MASTER.md](MASTER.md) — # 176. Final Current Source of Truth: Centralize authority, preserve roadmap boundaries and distinguish inherited facts.
- C040: [RELEASE_1.md](R1_OPERATIONAL_MVP.md) — # 184. Final Release 1 Source of Truth: Remove duplicated baseline and qualify candidate models.
- C041: [MASTER.md](MASTER.md) — # 30. Refunds: Centralize Release 2 policy rather than duplicate it.
- C042: [MASTER.md](MASTER.md) — # 168. Release 2 Development Slices: Clarify customer reporting vs advanced analytics boundary.
- C043: [RELEASE_2.md](R2_EXPANDED_MVP.md) — # 159. Release 2 Milestones: Apply early sync policy to incremental Release 2 without expanding scope.
- C044: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 36. Booking Deposit: Replace repeated Release 2 policy with release-owner reference.
- C045: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 38. Late Arrival Policy: Replace repeated Release 2 policy with release-owner reference.
- C046: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 39. Late Arrival Calculation: Replace repeated Release 2 policy with release-owner reference.
- C047: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 66. Deposit Financial Integrity: Replace repeated Release 2 policy with release-owner reference.
- C048: [RELEASE_3.md](R3_FULL_PRODUCT.md) — # 231. Final End-to-End Product Flow: Align final flow with local-first and optional customer context.

السجل التفصيلي الآلي [changes.json](changes.json) يحفظ النص الكامل السابق والجديد لكل قسم. [original_sources.zip](original_sources.zip) يحفظ الملفات الثمانية الأصلية بأسمائها وبصماتها في manifest.json. الإضافات الجديدة مثل خريطة القبول لم يكن لها ملف مقابل سابق.
