#!/bin/bash

printf "\n======================================================\n"
printf "Testing First GET, Will contain initial seeded\n"

curl -s -X GET \
  "$TESTURL/api/tasks" \
  -H 'cache-control: no-cache' \
  -H 'content-type: application/json' \
  | python -m json.tool

printf "\n======================================================\n"
printf "Testing POST\n"

curl -s -X POST \
  "$TESTURL/api/tasks" \
  -H 'cache-control: no-cache' \
  -H 'content-type: application/json' \
  -d '    {
        "title": "Commission Summary Report",
        "description": "Calculate commissions over the past month",
        "startDate": "2020-03-01T00:00:00",
        "endDate": "2020-04-01T00:00:00",
        "priority": 1,
        "category": "top",
        "status": 200
    }' \
    | python -m json.tool

printf "\n======================================================\n"
printf "Testing Second GET, Should Include Recent POST\n"

curl -s -X GET \
  "$TESTURL/api/tasks" \
  -H 'cache-control: no-cache' \
  -H 'content-type: application/json' \
    | python -m json.tool

printf "\n======================================================\n"
