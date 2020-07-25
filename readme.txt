Instructions

A. To run API

1. CD to Assignment1 folder

2. RUN 

> docker-compose up


B. To run Production API

1. CD to Assignment1 folder

2. RUN 

> docker-compose -f docker-compose-prod.yml up


C. To run Tester

1. CD to AssignmentTester folder

2. RUN 

> docker ps

To list all services, find the container ID for image "bpbeernie/taskapi"

3. With container ID from 1., RUN

> docker inspect -f {{.NetworkSettings.Networks.assignment1_assignmentnetwork.IPAddress}} <<Container ID>>

To get the IP Address of the TaskAPI.

4. RUN 

> docker build --tag bpbeernie/tester .

To build tester.

5. With IP address from 2., RUN 

> docker run --rm  --env TESTURL=<<IP Address>> --network assignment1_assignmentnetwork -t bpbeernie/tester



