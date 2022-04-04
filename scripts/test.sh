#!/bin/sh

# Define vars
NC='\033[0m'
RED='\033[0;31m'
BLUE='\e[36m'
YELLOW='\033[0;33m'
EXIT_CODE=0

printf "${YELLOW}Testing Started${NC}\n"

mkdir -p /tmp/coverage
rm -rf /tmp/coverage/*

# Unit Tests Project
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura  /p:CoverletOutput=/tmp/coverage/ /src/tests > /tmp/coverage/test.log 2>&1

# Generate Report
reportgenerator -reports:"/tmp/coverage/coverage.cobertura.xml" -targetdir:"/tmp/coverage/htmlcov" -reporttypes:Html

printf "${YELLOW}Testing Ended${NC}\n"

exit $EXIT_CODE