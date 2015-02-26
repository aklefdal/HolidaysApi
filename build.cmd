@echo off
cls
if not exist "tools\FAKE\tools\Fake.exe" (
    ".nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion" "-Version 3.17.13"
)
"tools\FAKE\tools\Fake.exe" "build.fsx"
