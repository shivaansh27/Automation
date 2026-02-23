# Requirement Traceability Matrix (RTM)

## Project
Hybrid QA Automation Framework for E-commerce (`Selenium + RestSharp + NUnit + ExtentReports`)

| Req ID | Requirement | Type | Test Case ID(s) | Automation File(s) | Status |
|---|---|---|---|---|---|
| R-001 | User can login with valid credentials | Functional/UI | SMK-002, REG-001 | `Tests/LoginTests.cs` (`User_Login_Should_Succeed`, `Login_UI_Then_Validate_User_Via_API`) | Covered |
| R-002 | User can search products | Functional/UI | SMK-003 | `Tests/ProductTests.cs` (`Product_Search_Should_Show_Results`) | Covered |
| R-003 | User can add product to cart | Functional/UI | SMK-004, REG-002 | `Tests/CheckoutTests.cs` | Covered |
| R-004 | User can continue to checkout flow | Functional/UI | REG-002 | `Tests/CheckoutTests.cs` | Covered |
| R-005 | API can verify login | Functional/API | SAN-001, REG-001 | `API/UserApi.cs`, `Tests/LoginTests.cs` | Covered |
| R-006 | API can fetch user by email | Functional/API | SAN-002, REG-001 | `API/UserApi.cs`, `Tests/LoginTests.cs` | Covered |
| R-007 | API can fetch products | Functional/API | SAN-003 | `API/ProductApi.cs`, `Tests/ProductTests.cs` | Covered |
| R-008 | Reporting captures pass/fail and evidence | Non-functional | REG-003 | `Reports/ExtentReportManager.cs`, `Tests/BaseTest.cs` | Covered |
| R-009 | Framework supports parallel/retry execution | Non-functional | REG-003 | `Properties/AssemblyInfo.cs`, `Tests/*` | Covered |
| R-010 | Framework supports cross-browser run config | Non-functional | Exploratory | `Utilities/DriverFactory.cs`, `.github/workflows/test.yml` | Covered |

## Remarks

- Manual and automation evidence are intentionally combined for campus/recruiter review.
- Mapping can be extended when new requirements/stories are added.
