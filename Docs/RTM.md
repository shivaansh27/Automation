# Requirement Traceability Matrix (RTM)

**Project:** Hybrid QA Automation Framework for E-commerce
**Stack:** Selenium + RestSharp + NUnit + ExtentReports

---

This RTM maps each requirement to its corresponding test cases and automation files.

| Req ID | Requirement | Type | Test Case ID(s) | Automation File(s) | Status |
|--------|-------------|------|-----------------|-------------------|--------|
| R-001 | User can login with valid credentials | Functional / UI | SMK-002, REG-001 | `Tests/LoginTests.cs` → `User_Login_Should_Succeed`, `Login_UI_Then_Validate_User_Via_API` | ✅ Covered |
| R-002 | User can search for products | Functional / UI | SMK-003 | `Tests/ProductTests.cs` → `Product_Search_Should_Show_Results` | ✅ Covered |
| R-003 | User can add a product to cart | Functional / UI | SMK-004, REG-002 | `Tests/CheckoutTests.cs` | ✅ Covered |
| R-004 | User can continue through the checkout flow | Functional / UI | REG-002 | `Tests/CheckoutTests.cs` | ✅ Covered |
| R-005 | API can verify a user login | Functional / API | SAN-001, REG-001 | `API/UserApi.cs`, `Tests/LoginTests.cs` | ✅ Covered |
| R-006 | API can fetch user details by email | Functional / API | SAN-002, REG-001 | `API/UserApi.cs`, `Tests/LoginTests.cs` | ✅ Covered |
| R-007 | API can return the product list | Functional / API | SAN-003 | `API/ProductApi.cs`, `Tests/ProductTests.cs` | ✅ Covered |
| R-008 | Reports capture pass/fail results and screenshots | Non-functional | REG-003 | `Reports/ExtentReportManager.cs`, `Tests/BaseTest.cs` | ✅ Covered |
| R-009 | Framework supports parallel execution and retry | Non-functional | REG-003 | `Properties/AssemblyInfo.cs`, `Tests/*` | ✅ Covered |
| R-010 | Framework supports cross-browser configuration | Non-functional | Exploratory | `Utilities/DriverFactory.cs`, `.github/workflows/test.yml` | ✅ Covered |
