# Test Cases — Ecommerce Automation Framework

These test cases cover the main flows on `https://automationexercise.com` across smoke, sanity, regression, and a few exploratory checks. They're written to align with what's actually automated in the framework, so each TC maps back to a real test file.

---

## Smoke Tests

Quick checks to confirm the basics are working before running anything deeper.

| TC ID | Title | Preconditions | Steps | Expected Result |
|-------|-------|---------------|-------|-----------------|
| SMK-001 | Home page loads | Site is up | Open the base URL | Home page renders without errors |
| SMK-002 | Login with valid credentials | User already exists | 1. Go to `/login` 2. Enter valid email + password 3. Click login | User is logged in — "Logged in as" text and logout option are visible |
| SMK-003 | Product search returns results | User is on the site | 1. Open Products page 2. Search for `Tops` | A list of matching results is displayed |
| SMK-004 | Add a product to cart | At least one product exists | 1. Open Products 2. Add the first product 3. Open cart | Cart shows at least one item |

---

## Sanity Tests

These hit the API directly to make sure the backend is responding correctly before running full flows.

| TC ID | Title | Preconditions | Steps | Expected Result |
|-------|-------|---------------|-------|-----------------|
| SAN-001 | Login API returns success | Valid user credentials available | Call `POST /api/verifyLogin` with valid credentials | HTTP 200 with a successful response body |
| SAN-002 | Get user details by email | User exists in the system | Call `GET /api/getUserDetailByEmail?email=...` | User object is returned with a matching email |
| SAN-003 | Products API is available | API is up | Call `GET /api/productsList` | A non-empty product list comes back |

---

## Regression Tests

These are the more end-to-end checks — the ones most likely to catch something if something breaks.

| TC ID | Title | Preconditions | Steps | Expected Result |
|-------|-------|---------------|-------|-----------------|
| REG-001 | Hybrid: UI login → API validation | User exists | 1. Login via the browser 2. Call verify and get-user APIs with the same email 3. Compare what the UI showed vs what the API returned | Data is consistent across both layers |
| REG-002 | Checkout flow continues after cart | Product is available | 1. Add a product to cart 2. Open cart 3. Click proceed to checkout | Either the checkout page loads or a login prompt appears — no dead ends |
| REG-003 | Retry logic and report generation | Framework is configured correctly | 1. Run the full test suite 2. Check for retry attempts on failures 3. Check the output folder for the report | Retries fire as expected, and an Extent HTML report is generated with results |

---

## Notes

All the scripted cases above are automated under `Tests/*Tests.cs`. This document exists as manual QA evidence and to give reviewers a clear picture of what's covered and why.
