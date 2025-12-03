#!/bin/bash

dotnet test backend/unit.tests/ --collect:"XPlat Code Coverage" --results-directory ./docs/coverage/unitTests

# Generate HTML report
reportgenerator -reports:./docs/coverage/unitTests/**/coverage.cobertura.xml -targetdir:./docs/coveragereport/html -reporttypes:Html

# Generate badge report for GitHub README
reportgenerator -reports:./docs/coverage/unitTests/**/coverage.cobertura.xml -targetdir:./docs/coveragereport/badge -reporttypes:MarkdownSummaryGithub


