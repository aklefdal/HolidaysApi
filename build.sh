#!/bin/bash
if test "$OS" = "Windows_NT"
then

  .nuget/nuget.exe restore
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
  .nuget/NuGet.exe install FAKE -OutputDirectory tools -ExcludeVersion -Prerelease
  mono tools/FAKE/tools/FAKE.exe build.fsx $@
else
  # use mono
  chmod +x .nuget/nuget.exe
  .nuget/nuget.exe restore
  .nuget/nuget.exe install FAKE -OutputDirectory tools -ExcludeVersion -Prerelease
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
  
  mono tools/FAKE/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
fi
