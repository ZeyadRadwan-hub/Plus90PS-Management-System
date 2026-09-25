# سجل القرارات والأسئلة المفتوحة

> **Historical question register (2026-09-06).** The table below preserves the original questions and is not the current open-decision list. For current blockers use [DECISION_REQUESTS_R1.md](DECISION_REQUESTS_R1.md). Approved R1 decisions: invoices may exist before cash payment; one Match per Session (another Match starts a new Session), Match pricing is fixed and completion is employee-driven, never timer-driven; Fixed expiry alerts without auto-stop; discounts are outside R1; payment method is Cash only. Pause eligibility by SessionType and paid-invoice cash refund/reversal remain unresolved. The hourly charge formula, persistence/schema, and other later design details are not approved by this note.

**Previously open IDs now resolved for R1:** O-01 (minute/quarter time rounding), O-02 (free pause and multiple intervals, but not type eligibility), O-03 (no minimum and approved money rounding; test-price and storage precision remain separate), O-04 (one Match per Session, staff completion and no timer), O-05 (Fixed alert, no auto-stop), O-06 (R1 discounts removed; Cancel reason optional, while paid-invoice cash effect stays open), O-07 (authorized shift handover), and O-08 (basic internal-asset quantities). O-10, O-11, and O-12 retain only their narrower unresolved security/operational details. Do not reopen the resolved parts merely because their original question text remains below.

قرارات U المؤكدة وADR الهندسية في [SHARED_RULES](BUSINESS_RULES(1).md). الجدول لا يعيد فتحها. صاحب القرار التشغيلي: المستخدم بعد التحقق من المحل؛ صاحب التفصيل الهندسي: المطور ضمن المتطلبات. لا يلزم حسم أسئلة R2/R3 لبدء R1.

| ID | السؤال الحقيقي المتبقي | النوع/صاحب القرار | بوابة الحسم | العمل المستقل المسموح |
|---|---|---|---|---|
| O-01 | **RESOLVED R1:** billable time uses whole minutes (positive remainder rounds up), then the approved upward quarter-hour threshold. | تشغيل/المستخدم | مغلق للنطاق الحالي | BE-01 tested; no second-by-second billing |
| O-02 | **RESOLVED in part:** Pause is free and multiple intervals are summed once; exact Pause eligibility by SessionType remains open. | تشغيل/المستخدم | أهلية الأنواع قبل full Session | BE-02 timing mechanism only |
| O-03 | **RESOLVED in part:** no minimum charge; EGP fraction threshold and upward-only multiple-of-five rule approved. Test prices and storage precision remain separate decisions. | تشغيل/المستخدم | أسعار الإنتاج ودقة التخزين لاحقًا | BE-04 rounds only a supplied amount |
| O-04 | **RESOLVED R1:** one Match per Session, second Match needs new Session, fixed captured Match price, employee completion, no timer auto-completion. | تشغيل/المستخدم | مغلق للنطاق الحالي | BE-03 captures terms; full aggregate later |
| O-05 | **RESOLVED R1:** Fixed intended duration triggers an alert only, never automatic stop. Extension/early-exit workflow details remain later design. | تشغيل/المستخدم | تفاصيل UI لاحقًا | BE-03 captures planned duration |
| O-06 | [Historical] أنواع الخصومات وحدودها وترتيب الحساب والسبب المطلوب للإلغاء؟ | تشغيل/المستخدم | Superseded for R1: no discounts; Cancel reason optional | لا تنفيذ خصومات R1؛ أثر إلغاء فاتورة مدفوعة مفتوح منفصلًا |
| O-07 | **RESOLVED R1:** authorized Manager/Owner starts/ends selected employee shift; active assigned sessions need authorized handover before close, retaining original opener. | تشغيل/المستخدم | مغلق للنطاق الحالي | implementation later |
| O-08 | **RESOLVED R1:** branch-scoped internal quantities for Controllers/Accessories/Equipment/Assets; repair is Expense, not product inventory or per-unit damage lifecycle. | تشغيل/المستخدم | مغلق للنطاق الحالي | implementation later |
| O-09 | بداية يوم العمل وفترة التقارير وتعريف الإيراد المسدد/غير المسدد؟ | تشغيل/المستخدم | قبل تقارير M3 | بيانات اختبار للتجميع |
| O-10 | مدة السماح دون اتصال وصلاحيات المدير الحساسة والإبطال وتهيئة أول جهاز؟ | تشغيل وأمان/المستخدم والمطور | سياسة النواة قبل M1، استثناءات الإدارة قبل M3 | تصميم سياسة قابلة للاختبار دون افتراض سماح كامل |
| O-11 | حماية PIN ومفاتيح الجهاز، المحاولات والإقفال والاسترجاع؟ | أمان/المطور | قبل هوية M1 | مراجعة التصميم؛ لا نشر دخول غير محمي |
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
