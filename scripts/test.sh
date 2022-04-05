#!/bin/sh

# Define vars
NC='\033[0m'
RED='\033[0;31m'
BLUE='\e[36m'
YELLOW='\033[0;33m'
EXIT_CODE=0

printf "${YELLOW}Testing Started${NC}\n"

# Set up file system
mkdir -p /tmp/coverage
rm -rf /tmp/coverage/* \
    /src/app/obj/ \
    /src/app/out/ \
    /src/app/bin/ \
    /src/tests/obj/ \
    /src/tests/out/ \
    /src/tests/bin/

# Unit Tests Project
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura  /p:CoverletOutput=/tmp/coverage/ /src/tests > /tmp/coverage/test.log 2>&1

# Generate Report
reportgenerator -reports:"/tmp/coverage/coverage.cobertura.xml" -targetdir:"/tmp/coverage/htmlcov" -reporttypes:Html > /tmp/coverage/report.log 2>&1

TEST_SUCCEED=$(cat /tmp/coverage/test.log | grep -E 'Failed:\s+0')
if [ "$TEST_SUCCEED" ]; then
    printf "${RED}ERROR:${YELLOW} Unit test has failed! Please investigate below log${NC}\n"
    cat /tmp/coverage/test.log
    EXIT_CODE=1
fi

printf "${YELLOW}Testing Ended${NC}\n"

exit $EXIT_CODE
