# Ecommerce Hybrid Automation Framework (C# / .NET 8)

Production-style QA Automation Framework for an e-commerce application using **Selenium + RestSharp + NUnit + Extent Reports** with a **hybrid UI + API** testing approach.

> Built to demonstrate SDET/QA Engineer capabilities expected in modern product teams and campus hiring.

---

## Recruiter Snapshot

- ? End-to-end framework in **C# (.NET 8)**
- ? **UI Automation** with Selenium and Page Object Model (POM)
- ? **API Automation** with RestSharp
- ? **Hybrid Test** (UI login + API user validation)
- ? **Parallel execution** + retry handling
- ? **Extent Reports**, screenshot capture, and logging
- ? **Environment-based configuration**
- ? **Cross-browser support** (`chrome`, `edge`)
- ? **GitHub Actions CI** with artifact publishing
- ? Added QA artifacts: **Test Cases**, **RTM**, **Postman collection**

---

## Tech Stack

- **Language:** C# 12
- **Target Framework:** .NET 8
- **Test Framework:** NUnit
- **UI:** Selenium WebDriver
- **API:** RestSharp
- **Reporting:** Extent Reports (Spark)
- **Config:** `appsettings.json` + `testdata.json`
- **CI/CD:** GitHub Actions

---

## Framework Architecture

- Page Object Model (POM) for maintainability
- Layered separation:
  - `Pages` for UI actions
  - `API` for endpoint clients
  - `Models` for typed data
  - `Utilities` for reusable framework services
  - `Tests` for scenarios
- Thread-safe driver/report handling for parallel runs

---

## Project Structure

```text
EcommerceAutomationFramework/
??? API/
?   ??? BaseApiClient.cs
?   ??? ProductApi.cs
?   ??? UserApi.cs
??? Config/
?   ??? appsettings.json
?   ??? testdata.json
??? Models/
?   ??? ConfigModels.cs
?   ??? ProductModel.cs
?   ??? UserModel.cs
??? Pages/
?   ??? BasePage.cs
?   ??? CartPage.cs
?   ??? HomePage.cs
?   ??? LoginPage.cs
?   ??? ProductPage.cs
??? Properties/
?   ??? AssemblyInfo.cs
??? Reports/
?   ??? ExtentReportManager.cs
??? Tests/
?   ??? BaseTest.cs
?   ??? CheckoutTests.cs
?   ??? LoginTests.cs
?   ??? ProductTests.cs
??? Utilities/
?   ??? ConfigReader.cs
?   ??? DriverFactory.cs
?   ??? Logger.cs
?   ??? ScreenshotHelper.cs
?   ??? TestDataHelper.cs
?   ??? WaitHelper.cs
??? Docs/
?   ??? RTM.md
?   ??? TestCases.md
??? Postman/
?   ??? AutomationExercise.postman_collection.json
??? README.md
```

---

## Test Coverage

### UI Tests
- User Login
- Product Search
- Add to Cart
- Checkout Flow continuation

### API Tests
- Create User (`POST /createAccount`)
- Verify Login (`POST /verifyLogin`)
- Get User Detail (`GET /getUserDetailByEmail`)
- Get Product List (`GET /productsList`)

### Hybrid Test
- `Login_UI_Then_Validate_User_Via_API`
  1. Login via UI
  2. Capture/confirm user context
  3. Validate user via API
  4. Assert response status and data consistency

---

## Advanced Framework Features

- Parallel test execution
- Retry for flaky tests
- Extent report generation
- Screenshot on failure
- File logging (`Logs/`)
- Environment-based configuration (`dev/staging`)
- Cross-browser execution through config/env (`chrome`, `edge`)
- Runtime-unique email generation for create-user flows to avoid duplicate account failures

---

## Configuration

### `Config/appsettings.json`
- `Environment`
- `TimeoutSeconds`
- `Browser`
- `Api.BaseUrl`
- Environment URLs (`dev`, `staging`)

### `Config/testdata.json`
- Login credentials
- Search keyword
- User payload for API create-user

> Security note: do not commit real credentials in public repositories. Use environment secrets for CI.

---

## How to Run

### Prerequisites
- .NET 8 SDK
- Chrome and/or Edge installed

### Local

```bash
dotnet restore
dotnet test
```

### Run with environment secrets (recommended)

PowerShell:

```powershell
$env:AUT_EMAIL='your_registered_email@example.com'
$env:AUT_PASSWORD='your_password'
dotnet test
```

### Run by browser

PowerShell:

```powershell
$env:BROWSER='chrome'; dotnet test
$env:BROWSER='edge'; dotnet test
```

### Run specific suite

```bash
dotnet test --filter "FullyQualifiedName~LoginTests"
dotnet test --filter "FullyQualifiedName~ProductTests"
dotnet test --filter "FullyQualifiedName~CheckoutTests"
```

---

## Reports & Artifacts

After execution:
- `Reports/` ? Extent HTML report
- `Screenshots/` ? failure screenshots
- `Logs/` ? execution logs

---

## CI/CD

GitHub Actions workflow: `.github/workflows/test.yml`
- CI run on `chrome` (stable hosted-runner path)
- Executes `dotnet test`
- Uploads:
  - TRX test results
  - Reports, Screenshots, Logs

---

## QA Documentation Added

- Manual test design: `Docs/TestCases.md`
- Requirement traceability: `Docs/RTM.md`
- Postman API evidence: `Postman/AutomationExercise.postman_collection.json`

---

## Why this project is recruiter-relevant

This repository demonstrates practical skills expected for QA/SDET roles:
- Test framework design and scalability
- Automation coding standards (POM, reusable utilities)
- Hybrid UI/API validation approach
- Failure analysis and reporting
- CI readiness and team collaboration artifacts (RTM/manual test docs)

---

## Public Test Site

- Web: `https://automationexercise.com`
- API: `https://automationexercise.com/api`

---

## Author

If you are evaluating this project for QA/SDET hiring, feel free to review the framework structure, test strategy, and CI artifacts for production-readiness indicators.
