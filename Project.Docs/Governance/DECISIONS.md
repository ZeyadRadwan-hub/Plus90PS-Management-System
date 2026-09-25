# سجل القرارات والأسئلة المفتوحة

> **Historical question register (2026-09-06).** The table below preserves the original questions and is not the current open-decision list. For current blockers use [DECISION_REQUESTS_R1.md](DECISION_REQUESTS_R1.md). BE-06 owner decisions supersede old questions: Pause/Resume applies to Open/Fixed, not Match; a successfully paid Invoice cannot be cancelled; eligible unpaid cancellation remains protected, optional-reason, historical. Invoices may precede Cash Payment; one Match per Session, employee completion, Fixed expiry alert only, and no R1 discounts remain approved. Technical storage/security/sync decisions are documented—not implemented—in [TECHNICAL_DECISIONS_R1.md](TECHNICAL_DECISIONS_R1.md).

**Previously open IDs now resolved for R1:** O-01 (minute/quarter time rounding), O-02 (free Pause and multiple intervals, now limited to Open/Fixed), O-03 (no minimum and approved money rounding; production prices remain separate), O-04 (one Match per Session, staff completion and no timer), O-05 (Fixed alert, no auto-stop), O-06 (R1 discounts removed; eligible unpaid Cancel reason optional; paid Invoice cannot be cancelled), O-07 (authorized shift handover), and O-08 (basic internal-asset quantities). O-10/O-11 technical offline/security baselines are now approved; ownership and deployment/reporting details remain open in the current decision requests. Do not reopen resolved parts merely because historical question text remains below.

قرارات U المؤكدة وADR الهندسية في [SHARED_RULES](BUSINESS_RULES(1).md). الجدول لا يعيد فتحها. صاحب القرار التشغيلي: المستخدم بعد التحقق من المحل؛ صاحب التفصيل الهندسي: المطور ضمن المتطلبات. لا يلزم حسم أسئلة R2/R3 لبدء R1.

| ID | السؤال الحقيقي المتبقي | النوع/صاحب القرار | بوابة الحسم | العمل المستقل المسموح |
|---|---|---|---|---|
| O-01 | **RESOLVED R1:** billable time uses whole minutes (positive remainder rounds up), then the approved upward quarter-hour threshold. | تشغيل/المستخدم | مغلق للنطاق الحالي | BE-01 tested; no second-by-second billing |
| O-02 | **RESOLVED R1:** Pause is free, multiple intervals are summed once, and only Open/Fixed may Pause/Resume; Match may not. | تشغيل/المستخدم | مغلق للنطاق الحالي | Full Session enforcement later; BE-02 timing mechanism unchanged |
| O-03 | **RESOLVED in part:** no minimum charge; EGP rounding approved; storage uses integer piasters with configured values at most two decimals. Production price amounts remain a separate decision. | تشغيل/المستخدم | أسعار الإنتاج لاحقًا | BE-04 rounds only a supplied amount; storage baseline documented, not implemented |
| O-04 | **RESOLVED R1:** one Match per Session, second Match needs new Session, fixed captured Match price, employee completion, no timer auto-completion. | تشغيل/المستخدم | مغلق للنطاق الحالي | BE-03 captures terms; full aggregate later |
| O-05 | **RESOLVED R1:** Fixed intended duration triggers an alert only, never automatic stop. Extension/early-exit workflow details remain later design. | تشغيل/المستخدم | تفاصيل UI لاحقًا | BE-03 captures planned duration |
| O-06 | [Historical] أنواع الخصومات وحدودها وترتيب الحساب والسبب المطلوب للإلغاء؟ | تشغيل/المستخدم | Superseded for R1: no discounts; eligible unpaid Cancel reason optional; paid Invoice cannot be cancelled | لا تنفيذ خصومات R1؛ unpaid Cancel/Void distinction remains a separate narrow question |
| O-07 | **RESOLVED R1:** authorized Manager/Owner starts/ends selected employee shift; active assigned sessions need authorized handover before close, retaining original opener. | تشغيل/المستخدم | مغلق للنطاق الحالي | implementation later |
| O-08 | **RESOLVED R1:** branch-scoped internal quantities for Controllers/Accessories/Equipment/Assets; repair is Expense, not product inventory or per-unit damage lifecycle. | تشغيل/المستخدم | مغلق للنطاق الحالي | implementation later |
| O-09 | بداية يوم العمل وفترة التقارير وتعريف الإيراد المسدد/غير المسدد؟ | تشغيل/المستخدم | قبل تقارير M3 | بيانات اختبار للتجميع |
| O-10 | **TECHNICAL BASELINE APPROVED R1:** core actions offline; sensitive administration online-only; bounded signed authority and periodic central refresh. Ownership/first-device rollout details still require later review. | تشغيل وأمان/المستخدم والمطور | Technical baseline closed; rollout later | Design documented, not implemented |
| O-11 | **TECHNICAL BASELINE APPROVED R1:** PIN verifier, durable per-branch/device/employee attempts, device identity, signed one-time recovery, distinct SecurityLocked. | أمان/المطور | Technical baseline closed; implementation later | No auth/security code in BE-06 |
| O-12 | **RESOLVED in part:** Single/Multi cannot change in-place; captured pricing stays stable. Console transfer procedure remains open. | تشغيل/المستخدم | نقل الجهاز لاحقًا | جلسات بإعداد ثابت ولقطة سعر |
| O-13 | سياسة الحجز دون اتصال وتعارض المورد والوقت، وتوحيد رقم الهاتف؟ | تشغيل/المستخدم | قبل حجز R2 | R1 بالكامل |
| O-14 | نسبة العربون، معالجة المصادرة والإلغاء، تداخل حدّي 10/15 دقيقة؟ | تشغيل/المستخدم | قبل مالية R2 | R1 بالكامل |
| O-15 | مزود الاستضافة والتحديث ومكان النسخ الاحتياطي وRPO/RTO ومقاييس الأداء؟ | تشغيل وهندسة/المستخدم والمطور | قبل خروج M4 | تنفيذ استعادة محلية تجريبية |
| O-16 | هل قاعدة بيانات الكود الحالي فيها بيانات حقيقية؟ الأجهزة المستهدفة وإصدارات Windows/.NET؟ | تحقق/المستخدم والمطور | قبل ترحيل أو تثبيت M0/M4 | فحص المصدر بلا اتصال أو تعديل قاعدة البيانات |

## تعارضات حُسمت أو كُشفت

- C-01: ملف AS-IS القديم يترك Open مجهولًا؛ حُسم من U-02.
- C-02: «أقرب علامة» قد تُفهم تقريبًا لأسفل؛ عُوّضت بقاعدة للأعلى عند بقاء ≤3 دقائق.
- C-03: المساعد القديم نسب سياسة البرنامج للتشغيل اليدوي؛ فُصل AS-IS عن TO-BE.
- C-04: الحفظ المباشر Online مقابل Local-first؛ حُسم هندسيًا ADR-01 لعمليات الفرع الأساسية.
- C-05: الأوفلاين والاختبارات متأخران رغم كونهما أساسيين؛ عدّل ترتيب M1/M2 وكل جزء.
- C-06: [Historical status] Match duration/timer question was open in O-04; current R1 uses a configured reference duration, exactly one Match per Session, fixed Match price, and employee completion without timer auto-stop.
- C-07: مبيعات المنتجات والعضويات/الاشتراكات الموجودة بالكود لا توسع R1؛ راجع CODE_REVIEW.
- C-08: تكرار قواعد R2 في R3/العام؛ مرجع السياسة R2. صفة Confirmed في مستند سابق لا تعني تحققًا جديدًا.

## آلية الإقفال

تُضاف إجابة السؤال ومصدرها وتاريخها، وتُحدّث القاعدة واختبارات القبول ومهام الجزء المتأثر معًا. لا يعتبر سكوت المستخدم اعتمادًا لسياسة مالية. الأسئلة لا تمنع المهام المستقلة المذكورة. خيارات الباقات والتحويلات المتقدمة تبقى ضمن مستند R3 عند بدء الإصدار، ولا تُفرض الآن.
