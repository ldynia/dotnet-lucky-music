#!/bin/sh

# Define vars
NC='\033[0m'
RED='\033[0;31m'
BLUE='\e[36m'
YELLOW='\033[0;33m'
EXIT_CODE=0

printf "${YELLOW}Testing Started${NC}\n"

dotnet test /src/tests/

printf "${YELLOW}Testing Ended${NC}\n"

exit $EXIT_CODE