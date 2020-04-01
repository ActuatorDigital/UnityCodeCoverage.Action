FROM andmos/dotnet-script:latest

COPY entrypoint.sh /entrypoint.sh
COPY entrypoint.csx /entrypoint.csx
RUN ["chmod", "+x", "/entrypoint.sh"]
ENTRYPOINT ["/entrypoint.sh"]