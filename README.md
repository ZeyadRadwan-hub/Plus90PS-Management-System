# Plus90PS Management System

+90 PS is a staged PlayStation shop management project. This repository contains documentation **and working backend foundation/Domain code through BE-08**, not a complete Release 1 application.

Implemented: an ASP.NET Core Web API foundation with development Swagger and health endpoint, billable-time and money-rounding rules, session timing/terms/pricing snapshots, pricing catalog, Branch/GameConsole foundation, Session aggregate core, Business/Employee/Role business scope, and automated tests. The solution is at [Project.Code/Plus90PS.sln](Project.Code/Plus90PS.sln).

Not implemented: durable Session workflows, the full hourly charge formula, application-level pricing management, EF Core/database persistence (local SQLite or central SQL Server), authentication/authorization, synchronization, invoices/payments, or the WPF desktop frontend. Release 1 remains on a pre-code HOLD for all slices beyond those explicitly approved.

R1's intended architecture is WPF/MVVM → branch-local SQLite + durable outbox → background sync → authorized ASP.NET Core API → central SQL Server. See [current status](Project.Docs/Reviews/CURRENT_STATE.md), [gate](Project.Docs/Reviews/PRE_CODE_GATE_R1.md), and [business rules](Project.Docs/Requirements/BUSINESS_RULES_R1(1).md) before implementing another slice. The older [README(1).md](README(1).md) is a historical planning-package note, not this repository's current status.

DB-01 / DB-01.1 are documentation-only database designs, completed / ready for review; no database or migration is implemented. BE-09 is the next planned backend slice and is not authorized. Latest reported tests: 206 passed, 0 failed; this ZIP inspection independently counts 206 static xUnit cases but does not execute them. Current BusinessDay reporting uses the Branch-local start date/month/year; Manager Start/End and unfinished-Session closing guards are requirement decisions, not running features. See the linked current state and decision requests.
