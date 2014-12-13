module HolidaysApi.Tests

open NUnit.Framework
open FsCheck
open Aklefdal.Holidays.HttpApi.Computus

//http://en.wikipedia.org/wiki/Easter
let ``Easter is betweeen March and April`` year =
    let easter = EasterDay2 year 
    let easterMonth = easter.Month
    match easterMonth with
        | 3 -> true
        | 4 -> true
        | _ -> false

let ``Easter Day is Sunday`` year =
    let easter = EasterDay2 year 
    match easter.DayOfWeek with
        | System.DayOfWeek.Sunday -> true
        | _ -> false

let EasterIsInAllowedRange year= 
    true ==> (lazy (``Easter is betweeen March and April`` year))

let EasterDayIsSunday year= 
    true ==> (lazy (``Easter Day is Sunday`` year))

let smallYears = 
    FsCheck.Gen.choose(0,9999) |> Arb.fromGen

[<Test>]
let ``Test that easterday is sunday``() = 
    Check.QuickThrowOnFailure (Prop.forAll (smallYears) EasterDayIsSunday)

[<Test>]
let ``Test that easter is in march or april``() = 
    Check.QuickThrowOnFailure (Prop.forAll (smallYears) EasterIsInAllowedRange)