#!/bin/bash

dotnet pack -o out -p:PackageVersion=$VERSION

if test -n "$(find ./out -name '*.nupkg' -print -quit)"
then
    dotnet nuget push './out/*.nupkg' --skip-duplicate --no-symbols --source $NUGET_SOURCE --api-key $NUGET_KEY
fi