#!/bin/bash
if test "$OS" = "Windows_NT"
then

  .nuget/nuget.exe restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi

  packages/FAKE.3.17.9/tools/FAKE.exe build.fsx $@
else
  # use mono

  mono packages/FAKE.3.17.9/tools/ restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
  
  mono packages/FAKE.3.17.9/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
fi