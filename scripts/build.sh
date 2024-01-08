#!/bin/bash

dotnet clean $SOLUTIONFILE

find ./ -type d -name 'bin' -exec rm -r {} +
find ./ -type d -name 'obj' -exec rm -r {} +

dotnet restore $SOLUTIONFILE

dotnet build $SOLUTIONFILE /p:Version=$VERSION