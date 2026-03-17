# Ecommerce Hybrid Automation Framework (C# / .NET 8)

A QA automation framework I built.

---

## What it covers

The framework handles UI automation via Selenium, API testing with RestSharp, and a hybrid approach where both are used together in the same test flow. Tests run on Chrome and Edge, support parallel execution, and feed into Extent HTML reports with screenshots on failure.

Built with C# (.NET 8), NUnit, and wired up to GitHub Actions for CI.

---

## How the code is organized

I went with a layered structure to keep things clean and avoid test code turning into a mess:

- `Pages` — UI interactions following the Page Object Model
- `API` — API clients and endpoint definitions
- `Models` — request/response data shapes
- `Utilities` — shared helpers for driver setup, waits, config, and logging
- `Tests` — the actual test scenarios
- `Docs / Postman` — QA artifacts like test cases, RTM, and a Postman collection

```
EcommerceAutomationFramework/
├── API/
├── Config/
├── Models/
├── Pages/
├── Reports/
├── Tests/
├── Utilities/
├── Docs/
├── Postman/
└── README.md
```

---

## Test coverage

**UI** — login, product search, add to cart, basic checkout flow

**API** — create user, verify login, get user details, get product list

**Hybrid** — `Login_UI_Then_Validate_User_Via_API`

This one was the most interesting to build. The flow logs in through the browser, captures the user's email, hits the API to fetch that user's details, then cross-validates the response against what the UI showed. The idea was to demonstrate how UI and API can work together for richer validation rather than testing them in isolation.

---

## Other things I added

- Retry logic for flaky tests
- Screenshot capture on failure
- Config-based environment switching (`dev` / `staging`)
- Dynamic email generation to prevent duplicate user conflicts
- Basic logging under `Logs/`

---

## Configuration

`appsettings.json` controls environment, browser, base URL, and timeouts. `testdata.json` holds login credentials, search keywords, and the API user payload. Real credentials aren't committed — the config supports environment variables for that.

---

## Running it locally

You'll need .NET 8 SDK and Chrome or Edge installed, then just run the NUnit tests through your preferred runner or via the CLI.

---

## Test site

[automationexercise.com](https://automationexercise.com) + its [API](https://automationexercise.com/api)
