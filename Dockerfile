#FROM gableroux/unity3d:2019.3.4f1
FROM alpine:3.10

COPY entrypoint.sh /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]