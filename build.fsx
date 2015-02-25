// include Fake libs
#r "tools/FAKE/tools/FakeLib.dll"

open Fake

RestorePackages()

open System.IO

// Directories
let buildDir  = "./build/"
let testDir   = "./test/"

// Filesets
let appReferences  = !! "HolidaysApi.WebHost\*.fsproj"

let testReferences = !! "HolidaysApi.Tests\*.fsproj"

// Targets
Target "Clean" (fun _ -> 
    CleanDirs [buildDir; testDir]
)

Target "BuildApp" (fun _ ->

    // compile all projects below src/app/
    MSBuildRelease buildDir "Build" appReferences
        |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
    MSBuildDebug testDir "Build" testReferences
        |> Log "TestBuild-Output: "
)

Target "NUnitTest" (fun _ ->  
    !! (testDir + "/HolidaysApi.Tests.dll")
        |> NUnit (fun p -> 
            {p with
                DisableShadowCopy = true; 
                OutputFile = testDir + "TestResults.xml"})
)

// Build order
"Clean"
  ==> "BuildApp"
  ==> "BuildTest"
  ==> "NUnitTest"

// start build
RunTargetOrDefault "NUnitTest"
