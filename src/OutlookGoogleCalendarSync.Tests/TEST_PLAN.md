# Test Infrastructure Plan

## Overview
This document outlines the test infrastructure being created to support the .NET 8 migration. Tests will be created in xUnit for .NET 8, ensuring that functionality is preserved during and after migration.

## Test Project Structure
```
src/
├── OutlookGoogleCalendarSync/           (Main application - .NET Framework 4.6.2)
├── OutlookGoogleCalendarSync.Tests/     (Test project - .NET 8)
    ├── Extensions/
    │   ├── StringExtensionsTests.cs     ✅ Created
    │   ├── DateTimeExtensionsTests.cs   ✅ Created
    │   └── ...
    ├── Core/
    │   ├── XMLManagerTests.cs           📝 Planned
    │   └── ...
    └── OutlookGoogleCalendarSync.Tests.csproj
```

## Testing Strategy

### Phase 2A: Create Test Infrastructure (Current)
1. ✅ Create test project with xUnit
2. ✅ Create tests for utility classes
3. 📝 Add project reference after main project conversion
4. 📝 Run tests to establish baseline

### Phase 2B: Core Logic Tests
Test non-UI, non-COM components:
- XMLManager (serialization/deserialization)
- String extensions
- DateTime extensions
- Settings management (basic)
- Helper utilities

### Phase 2C: Integration Tests (After Migration)
- Google Calendar API integration
- Settings persistence
- Sync logic (mocked Outlook)

## Tests Created

### Extensions/StringExtensionsTests.cs
Tests for `OutlookGoogleCalendarSync.Extensions.OgcsString`:
- ✅ Append() - null, empty, and value cases
- ✅ Prepend() - null, empty, and value cases
- ✅ RemoveLineBreaks() - handles \r, \n, \r\n
- ✅ RemoveNBSP() - replaces non-breaking spaces
- ✅ ToBase64String() - encodes to Base64

**Coverage:** 100% of public methods

### Extensions/DateTimeExtensionsTests.cs
Tests for `OutlookGoogleCalendarSync.Extensions.DateTime` and `OgcsDateTime`:
- ✅ GetPreciseDate() - parsing and validation
- ✅ ToPreciseString() - formatting and UTC conversion
- ✅ Round-trip conversion
- ✅ OgcsDateTime class - ToString(), Equals(), GetHashCode()

**Coverage:** ~85% of public methods (AllDayEvent() requires Outlook/Google types)

## Running Tests

### Before Migration (Baseline)
```bash
# Cannot run yet - needs project reference which requires SDK-style csproj
# Will run after Phase 3 conversion
```

### After Migration
```bash
cd src/OutlookGoogleCalendarSync.Tests
dotnet test
```

## Test Coverage Goals

### Minimum (Must Have)
- [x] String utilities: 100%
- [x] DateTime utilities: 85%
- [ ] XMLManager: 80%
- [ ] Helper class (non-Registry): 60%
- [ ] Settings (basic load/save): 60%

**Overall Target:** 60% coverage of testable code

### Excluded from Testing (Initial Phase)
- Windows Forms UI (MainForm, etc.)
- COM Interop (Outlook integration)
- Google Calendar API calls (integration tests only)
- Registry access (platform-specific)
- System event handlers

## Next Steps

1. **Wait for Phase 3** - Convert main project to SDK-style
2. **Add Project Reference** - Reference main project from test project
3. **Run Initial Tests** - Establish baseline on .NET Framework 4.6.2
4. **Create Additional Tests** - XMLManager, Helper, Settings
5. **Verify Coverage** - Use coverlet to measure coverage
6. **Migrate and Re-test** - Run same tests on .NET 8

## Notes

- Tests are written for .NET 8 but will test .NET Framework 4.6.2 code initially
- This verifies compatibility and establishes a baseline
- Same tests will run against .NET 8 version after migration
- Any test failures after migration indicate breaking changes
- Focus is on business logic, not UI or external dependencies

## Test Execution Timeline

| Phase | Framework | Status |
|-------|-----------|--------|
| Phase 2 | Tests created but can't run | ✅ Current |
| Phase 3 | Convert project, add reference | 📝 Planned |
| Phase 3 | Run on .NET Framework 4.6.2 | 📝 Planned |
| Phase 4 | Migrate to .NET 8 | 📝 Planned |
| Phase 6 | Run on .NET 8, compare results | 📝 Planned |
