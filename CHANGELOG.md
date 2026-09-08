# Changelog

## [2026-09-08]

### Features

- Added management screens for application types and test types, including create, update, list, and validation workflows.
- Added user management, user details, password changes, and account settings.
- Added login validation, password protection, and a Remember Me option.
- Added people search, detailed person profiles, filtering, and improved people management screens.
- Added reusable person and user cards, updated application resources, and refreshed the desktop application's visual assets.

### Bug Fixes

- Corrected test type creation and update behavior so changes are saved and reflected consistently.
- Fixed application type form flows and validation issues in the existing screens.
- Corrected image handling for person records and improved person information reset and loading behavior.
- Fixed login, user-management, and legacy form-flow issues discovered during the recent feature work.

### Chores

- Refactored person, user, and application data-access and business-layer code for the updated forms.
- Improved shared UI controls, validation, resource registration, and form layouts.
- Added GitHub Actions support for the .NET desktop build and updated workflow environment paths.
- Renamed `Gendor` to `Gender` throughout the application.

### Breaking Changes

- None identified in the recent commits.
