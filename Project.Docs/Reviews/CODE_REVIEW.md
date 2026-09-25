# مراجعة المصدر الموجود — قراءة فقط

> **HISTORICAL REVIEW / SUPERSEDED.** This review describes an older `net8.0` API/codebase in a different project snapshot. It does **not** describe the current BE-00 through BE-04 .NET 10 solution or prove that its old findings apply here. Retain the findings below as provenance only; inspect [CURRENT_STATE.md](CURRENT_STATE.md) and the actual `Project.Code/` tree for present implementation. No current build/test claim is made by this historical review.

المصدر: Project Code/Web/+90_Web/+90_Web داخل مجلد المشروع الأصلي. تمت قراءة مشروع ASP.NET Core وProgram وDbContext وModels وControllers. لم نشغّل API أو نتصل بقاعدة البيانات أو نطبّق migrations، ولم نراجع بيانات التشغيل. لا نتائج build أو اختبار تطبيق مدّعاة.

| الموجود فعليًا | التقييم | التصرف في خطة التنفيذ |
|---|---|---|
| net8.0، EF Core وSQL Server، Swagger | أساس API موجود | إعادة استخدام التنظيم بعد فحص نسخة تشغيل مدعومة؛ لا إعادة كتابة تلقائية |
| Branch: Id/Name/IsActive | بداية نموذج فرع | إضافة علاقة الملكية ونطاق الهوية قبل التشغيل متعدد المستخدمين |
| Device: Name/Type/BranchId | بداية إدارة أجهزة | إعادة استخدام الفكرة؛ إضافة تقييد الحالة وربط الجلسات والأسعار |
| Controllers تقرأ كل الفروع/الأجهزة وتقبل entities مباشرة | CRUD تجريبي | لا تعتبر BranchId authorization؛ أضف هوية وصلاحيات وعقود إدخال مقيدة |
| UseAuthorization دون إعداد مصادقة أو [Authorize] في Controllers المقروءة | لا دليل على حماية endpoints الحالية | استكمال سياسة الهوية والفرع قبل نشرها؛ وجود middleware وحده لا يكفي |
| Customer: Phone/Name/Points/TotalHours | سياق عملاء تجريبي | الاحتفاظ كمرجع R2؛ Points لا تضيف loyalty أو membership تلقائيًا |
| Product: BuyPrice/SellPrice/Stock | نموذج مبيعات منتجات | خارج نطاق R1؛ لا يطلق ضمنه ولا يحول تلقائيًا لأصول دون تصميم؛ لا نحذف بيانات |
| Subscription: صلاحية فرع مدفوعة وانتهاء | تجربة اشتراك منصة، ليست بالضرورة عضوية لاعب | مرجع لباقات R3؛ لا يجعل انتهاء الاشتراك شرطًا للأوفلاين قبل تصميم السياسة |
| WeatherForecast | مثال القالب | ليس جزءًا من نطاق المنتج؛ يمكن استبعاده عند تنفيذ تنظيف موثق |
| Session/Invoice/Payment/Pricing/WPF/SQLite/Sync غير موجودة في ملفات المصدر المفحوصة | فجوة التنفيذ الأساسية | مهام FIRST_SLICE وM2؛ لا يُبنى تقدير التقدم على كثرة وثائق المشروع |

## قرار إعادة الاستخدام

نستفيد من أساس API وEF وفكرة Branch/Device، مع مراجعة مخطط البيانات قبل التعديل. لا نحذف migrations أو قواعد بيانات ولا نرحّل بيانات حقيقية دون حسم O-16. لا كود منتج تغيّر في هذه المهمة.

## فجوات يجب إقفالها أثناء التنفيذ

هوية محلية ومركزية، عزل الملكية/الفرع، قواعد حساب مشتركة، لقطات أسعار، معاملات مالية، تخزين محلي، معرفات مزامنة ثابتة، إعادة محاولات وإقرارات، واجهة WPF، استعادة واختبارات. الفحص هنا يحدد اتجاه إعادة الاستخدام؛ ليس تدقيق أمان كاملًا ولا إثباتًا أن المشروع يبني بنجاح.
