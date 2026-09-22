# About Zeyad — Context for Codex and AI collaborators

**Document purpose:** Help a new AI collaborator understand Zeyad as a learner, developer, and product owner, without needing access to his full chat history.  
**Last updated:** 2026-09-22  
**Scope:** User-shared background and observed collaboration preferences, not an exhaustive personal biography.  
**Privacy:** Deliberately excludes personal contact information, exact birth date, credentials, private identifiers, and unnecessary sensitive details.

> **How to use this file:** Read this for communication style and background. For implementation decisions, the latest *approved* project requirements, business rules, architecture, diagrams, and decision register take precedence. Never turn an unverified detail in this profile into a software requirement.

## 1. Who I am

- I am **Zeyad**. My first name is also written **Ziyad / Ziad** (زياد). Use "زياد" when speaking to me in Egyptian Arabic.
- I am a student at **El-Sewedy International School for Applied Technology and Software**, specializing in programming. I reported starting my third year in September 2026 and expecting graduation around April–May 2027.
- I am learning software engineering with a particular interest in **C# and .NET back-end development**.
- I have also practiced **karate** for many years. I value practice and long-term improvement.

## 2. What I am working toward

- Become a capable **Back-End / .NET developer** who can design, implement, test, secure, and deploy a real product—not just finish tutorials.
- Develop stronger skills in **system design, databases, APIs, software architecture, problem solving, testing, DevOps, and professional documentation**.
- Continue higher education in computing; I have explored studying abroad, undergraduate scholarships, and a longer-term possibility of graduate study in Germany. These are aspirations, **not confirmed admissions or qualifications**.
- Build **+90 PS** into a real PlayStation shop management product that can eventually support many branches (long-term ambition: 40+), using staged releases rather than putting every feature into the first release.
- Improve English for study and technical work, and continue learning German. My self-reported English level reached around **B1** in 2026.

## 3. Technologies I have studied or practiced

**Main direction:** C#, .NET, ASP.NET Core Web API, Entity Framework Core, SQL Server, and WPF.

**C# / .NET:** OOP; classes, inheritance, interfaces and abstract classes; collections and custom data structures; asynchronous programming concepts; desktop/WPF work; MVC and Web API; controllers and endpoints; DTOs; validation; AutoMapper/manual mapping; Dependency Injection; LINQ; EF Core entities, DbContext, relationships, Fluent API, migrations, and data access.

**C++ / problem solving:** OOP and STL collections such as `map`/`unordered_map`; arrays, strings, divisors, recursion/backtracking, two pointers, range techniques, elementary algorithms, and competitive-programming exercises. I have taken/practiced problem-solving material and want to improve systematically.

**Databases:** Basic SQL and database concepts; SQL Server, phpMyAdmin/MySQL practice, and EF Core. **Do not assume I remember database fundamentals**: teach PK/FK, constraints, types, indexes, transactions, normalization, and migrations from the beginning when relevant.

**Web / other:** HTML, CSS, JavaScript for front-end practice; introductory PHP/MySQL and Laravel setup; Arduino programming/electronics; basic UI/UX work in Figma.

**Tools used or studied:** Visual Studio Community, Visual Studio Code/CMD/Git/GitHub workflows, SQL Server tools, XAMPP, Arduino IDE, Tinkercad, and Figma. Familiarity does **not** mean mastery of every tool.

## 4. Projects and practical work I have discussed

These are projects or practice efforts I have **reported discussing, building, or designing**. Do not claim they are all completed, deployed, commercial, or production-ready.

### +90 PS — PlayStation shop management system — active, design/pre-code stage

- My major long-term software product. Planned R1 architecture: **WPF Desktop per branch + local SQLite + background synchronization + ASP.NET Core Web API + central SQL Server**. R1 is **desktop-first and offline-first**, with a shared branch PC and distinct employee PIN identities.
- Planned three releases: **R1 operational shop core**; **R2 customer accounts/history/bookings**; **R3 expanded/full product**. Later website/mobile clients may use the same central API; they are not normal R1 clients.
- R1 roles are **Owner, Manager, Cashier**. Cashier should not automatically receive administrative permissions. Core work includes console availability, Open/Fixed/Match gaming sessions, branch-specific pricing, invoices/cash, shifts, basic internal asset quantities, expenses, reporting, audit, and security/offline behaviors.
- Current working approach: **Docs first → diagrams/technical design → explicit pre-code approval → implementation**. I have said the original code/database were reset and R1 implementation was at zero at the time of planning. **Never assume a feature is implemented just because it is described or pictured.**
- I made/reviewed R1 diagrams in Draw.io from PlantUML prompts. The logical ERD is a *draft* pending database-design reconciliation, not an approved physical schema.
- The UI screenshots are a **visual reference**, but some older raster screens do not actually reflect the latest approved changes. Treat the approved `UI_REVIEW_R1.md`, `DESIGN_SYSTEM_R1.md`, `UX_FLOWS_R1.md`, business rules, and decisions as the intended functional baseline—not every pixel or stale button in an old ZIP.
- Latest DB implementation preference: **EF Core Code First**, while still completing database design **before** models, DbContexts, migrations, or other application code.

### Eyes of the Steps — assistive wearable — student/team electronics project

- A wearable concept for visually impaired users that detects nearby obstacles/stairs and alerts through vibration and buzzer feedback.
- I worked on Arduino code; discussed ultrasonic/IR sensors, buzzer, vibration motor, signal behavior, troubleshooting, and future upgrade ideas.
- This was a **team project** with presentation/design/documentation contributions; do not describe it as a solo commercial product or assume proposed future features were built.

### SmartEvent / Event Management API — programming/assignment practice

- ASP.NET Core Web API + EF Core/SQL Server practice with Event, Organizer, Venue, Attendee and Registration-style entities.
- Topics worked on include CRUD endpoints, validation, DTOs, relationship configuration, querying, filters/sorting, soft delete/restore, uniqueness, and GitHub workflow.
- This is a **learning/assignment project**, not a confirmed production service.

### School Project / School System API — programming practice

- Practiced teacher/student/department/enrollment-style models, endpoints, DTO mapping, combining/splitting names, EF Core relationships, and validation.
- I sometimes ask to use **School System** examples for EF Core explanations instead of +90 PS. Follow my current example preference.

### Fashion Store — front-end practice

- Practiced a storefront layout and pages such as home, product details, login, and cart using HTML/CSS/JavaScript.
- Discussed responsive design, dark mode persistence, localStorage-based cart behavior, and product interactions. Do not assume a deployed commercial store.

### C# / OOP practice projects

- Built or practiced a custom generic `MyList<T>`, indexing/collection operations, custom queue/circular queue/stack structures, and sorting exercises.
- Worked on or discussed Library System, Football Club System, and Ecommerce Management System models/case studies.
- Treat these as **coding exercises, prototypes, or case-study work** unless a repository or working build proves a completed application.

### C++ practice exercises

- Smart Number Analyzer, a menu calculator, text/array exercises, frequency counting, divisor calculations, and other problem-solving tasks.

### Axova — luxury-watch brand concept

- Explored a men's watch brand/business idea, branding, costs, pricing, advertising, and store options. This is a **business concept/plan**; do not infer an established operating company.

## 5. How to communicate with me

- Prefer **Egyptian Arabic** for direct explanations. Keep important technical identifiers, keywords, C# types, and framework names **in English**.
- When useful for study, I have asked for roughly **70% simple B1-level English / 30% Arabic**, with translations for unfamiliar technical terms. But **follow my most recent instruction for the current task** rather than forcing this ratio into every response.
- Keep responses **roughly 25% shorter** by removing repetition and low-value commentary, **not** by omitting important reasoning or steps.
- Address me naturally as **زياد**; don't use stiff honorifics such as «حضرتك».
- Explain *why* a design/code choice exists and *what changes if it is removed*. Give small relevant examples, then apply them to my project.
- When I say I don't remember a subject (especially Database Design), start with fundamentals and build up gradually. Never confuse “I have studied it before” with “I currently know it well.”
- When I ask for an exact change, **do only the requested change**. Avoid inventing additional features, renaming files, changing visual identity, altering business rules, or starting extra documents.
- If I ask for a hint or debugging guidance rather than a full solution, respect that. If I request a complete implementation, provide it and explain the important parts.
- In code reviews, prioritize **correctness, maintainability, security, testability, and appropriate performance**, not merely shorter code.
- For design/UI tasks: preserve the specified style, colors, fonts, components, Arabic/English parity, and scope. Do not quietly substitute a different UI.
- Never say something is completed, tested, reviewed, or updated unless it was actually done and verified. Clearly distinguish **planned**, **documented**, **implemented**, and **tested**.

## 6. How I approach decisions and project work

These are observed working preferences, **not claims about my personality or psychological traits**:

- I like an explicit sequence of steps, clear progress tracking, and a clear separation between **R1, R2, and R3**.
- I care about understanding systems deeply instead of copying working code without comprehension.
- I prefer the assistant to challenge ambiguities and explain trade-offs, but **not to silently decide business policy for me**.
- I notice differences between what I asked for and what was actually delivered. Accurate status reports and transparent corrections matter.
- I want changes traceable in the appropriate documentation and diagrams so the implementation stays consistent.
- My long-term product ambition is large, but I prefer shipping in **incremental, operational releases**, rather than mixing future-scope features into R1.
- I work with a combination of self-study, practical projects, visual diagrams, and detailed technical questions.

## 7. Instructions specifically for Codex on +90 PS

1. **Read the current approved project Docs and diagrams first.** This profile is about the human collaborator; it is not a substitute for technical requirements.
2. **Do not start coding merely because you have access to the repository.** Wait for my explicit code-start approval and the project pre-code gate.
3. When coding begins, I want Codex to handle the back-end implementation in agreed, reviewable slices. State what you will modify, implement the approved slice, run relevant tests, and report what passed/failed.
4. Use **C# / ASP.NET Core / EF Core Code First** as agreed. The database design is still required; Code First is *not* “make up tables as you go.”
5. Treat R1 as **offline-first**: branch operations save locally first; synchronize through the API; protect against duplicate operations; keep role/branch boundaries and business records intact. Refer to current approved Docs for exact policy.
6. Respect **Owner / Manager / Cashier** boundaries. Never give Cashier Manager/Owner actions by default.
7. Do not bring **Customer Accounts, Bookings, product/food orders, or discounts** into R1 just because an old file, screenshot, or example mentions them.
8. Treat stale screenshots, old markdown copies, and previously suggested diagrams/schema fields as **candidates**, not automatically final. Raise conflicts against the approved source of truth; ask rather than guessing.
9. For any domain rule, technical decision, or scope ambiguity, present the specific question and possible implications. **I make the final business choice.**
10. Before saying work is finished, provide actual changed-file names, test/build outcomes, migration impact, and any remaining issues.

## 8. Things a collaborator must not assume

- Do not assume this document contains literally everything about me or every project I have ever made; it contains only user-shared/recoverable details.
- Do not assume all listed projects are complete or professionally deployed.
- Do not assume a planned R1 feature already exists as code/database.
- Do not invent academic awards, credentials, employment, scholarships, completed releases, or professional certifications.
- Do not infer private characteristics or personal details from software preferences.
- Do not invent my preferences when I haven't stated them; ask when a decision is material.
- My newest explicit instruction in the current task supersedes an older preference in this document.

**Maintenance rule:** Update `AboutZeyad.md` only when I explicitly correct or add a personal detail, experience, project status, or collaboration preference. Update technical business decisions in the appropriate project Doc, not here.
