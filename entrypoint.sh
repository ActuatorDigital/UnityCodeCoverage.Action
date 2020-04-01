#!/bin/sh -l

echo "Starting coverage quality gate checks..."
dotnet script /entrypoint.csx $1 $2
echo "Code coverage quality gate complete..."