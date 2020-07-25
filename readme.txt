==========   Assumptions   ==========

- You are running this project in a linux box with enough resources allocated to it to compile and run docker images.

- You have everything setup before hand to run docker and docker-compose

- Your ports 5003 and 5005 are free.

- You're not going to try to run the developer and production versions side by side. In fact, because I'm paranoid, I run the following commands before running any commands...
> sudo snap remove docker
> sudo snap install docker

- bpbeernie/tester is in my repository so to run the tester, you can just do step 5 if you already know the IP Address of the taskAPI service.

- If you want to call the endpoints via a web browser or Postman, simply enter the following urls depending on whether you're running the development or production versions of the taskAPI service.
> localhost:5003/api/tasks
> localhost:5005/api/tasks 

==========   Instructions   ==========

A. To run API

1. CD to Assignment1 folder

2. RUN 

> docker-compose up


B. To run Production API

1. CD to Assignment1 folder

2. RUN 

> docker-compose -f docker-compose-prod.yml up


C. To run Tester

1. In a new terminal, CD to AssignmentTester folder

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

==========   Cleanup   ==========

> docker-compose down

==========   Challenges   ==========

- I had huge issues when trying to connect my TaskAPI to the sql database. Usually, I'd get some vague error about login failed or handshake failed that could only inconsistenly reproduce and I didn't understand it. Eventually, I realized that the database needed time to startup and be ready to accept connections and the existing mechanisms (ie., dependson in docker-compose.yml) simply didn't allow for that reliably. I ended up adding an additional check on the .NET core side to double check the database is up before trying to establish a connection.
