FROM python:3.6-alpine

COPY entrypoint.sh .

RUN apk --no-cache add curl
RUN apk add --update bash


RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh