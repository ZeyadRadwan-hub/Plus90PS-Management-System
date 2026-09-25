# Plus90PS Management System

+90 PS is a staged PlayStation shop management project. This repository contains documentation **and working backend foundation/Domain code through BE-04**, not a complete Release 1 application.

Implemented: an ASP.NET Core Web API foundation with development Swagger and health endpoint, billable-time rules, session timing lifecycle, immutable session terms/pricing snapshot, money-rounding rules, and automated tests. The solution is at [Project.Code/Plus90PS.sln](Project.Code/Plus90PS.sln).

Not implemented: the full Session aggregate, hourly charge formula, pricing management, EF Core/database persistence (local SQLite or central SQL Server), authentication/authorization, synchronization, invoices/payments, or the WPF desktop frontend. Release 1 remains on a pre-code HOLD for all slices beyond those explicitly approved.

R1's intended architecture is WPF/MVVM → branch-local SQLite + durable outbox → background sync → authorized ASP.NET Core API → central SQL Server. See [current status](Project.Docs/Reviews/CURRENT_STATE.md), [gate](Project.Docs/Reviews/PRE_CODE_GATE_R1.md), and [business rules](Project.Docs/Requirements/BUSINESS_RULES_R1(1).md) before implementing another slice. The older [README(1).md](README(1).md) is a historical planning-package note, not this repository's current status.
