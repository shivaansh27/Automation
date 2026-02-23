# Test Cases - Ecommerce Automation Framework

## Scope
Covers smoke, sanity, regression, and exploratory checks for `https://automationexercise.com`.

## Smoke Test Cases

| TC ID | Title | Preconditions | Steps | Expected Result |
|---|---|---|---|---|
| SMK-001 | Launch home page | Site available | 1. Open base URL | Home page loads successfully |
| SMK-002 | Login with valid credentials | Existing user | 1. Go to `/login` 2. Enter valid email/password 3. Click login | User is logged in (`Logged in as`/logout visible) |
| SMK-003 | Product search | User on site | 1. Open Products 2. Search `Tops` | Search results list is displayed |
| SMK-004 | Add product to cart | Product exists | 1. Open Products 2. Add first product 3. Open cart | Cart contains at least one item |

## Sanity Test Cases

| TC ID | Title | Preconditions | Steps | Expected Result |
|---|---|---|---|---|
| SAN-001 | Verify login API for valid user | Valid user credentials | 1. Call `POST /api/verifyLogin` | HTTP success with valid response code |
| SAN-002 | Get user details by email | Existing user email | 1. Call `GET /api/getUserDetailByEmail?email=...` | User object returned with matching email |
| SAN-003 | Products API availability | API available | 1. Call `GET /api/productsList` | Product list returned, non-empty |

## Regression Test Cases

| TC ID | Title | Preconditions | Steps | Expected Result |
|---|---|---|---|---|
| REG-001 | Hybrid: UI login then API validation | Existing user | 1. Login via UI 2. Call verify/get-user APIs 3. Compare email/data | UI and API data stay consistent |
| REG-002 | Checkout flow continuation | Product available | 1. Add product 2. Open cart 3. Proceed checkout | Checkout page or login prompt appears |
| REG-003 | Retry and reporting behavior | Test framework configured | 1. Run suite 2. Observe retries/reports | Retries applied, Extent report generated |

## Exploratory Checklist

- Validate behavior with slow network / delayed page rendering.
- Validate cart behavior when modal is closed quickly.
- Validate login behavior with repeated attempts and stale sessions.
- Validate cross-browser behavior (`chrome`, `edge`).
- Validate API and UI consistency after repeated runs.

## Notes

- Automated coverage is implemented in `Tests/*Tests.cs`.
- This document is for manual QA evidence aligned with SDLC/STLC and recruiter review.
