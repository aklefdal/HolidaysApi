#!/bin/bash
if test "$OS" = "Windows_NT"
then

  .nuget/nugete.exe restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi

  packages/FAKE/tools/FAKE.exe build.fsx $@
else
  # use mono

  mono .fake/fake.exe restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
  
  mono packages/FAKE/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
fi