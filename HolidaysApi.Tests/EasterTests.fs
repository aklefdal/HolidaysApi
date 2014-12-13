module HolidaysApi.Tests

open NUnit.Framework
open FsCheck
open Aklefdal.Holidays.HttpApi.Computus

//http://en.wikipedia.org/wiki/Easter
//let ``Easter is betweeen 4th April and 8th of May`` year? =
let ``Easter is betweeen March and May`` year =
    let easter = EasterDay2 year 
    let easterMonth = easter.Month
    match easterMonth with
        | 3 -> true
        | 4 -> true
        | 5 -> true
        | _ -> false

let EasterIsInAllowedRange year= 
    true ==> (lazy ( ``Easter is betweeen March and May`` year))

let smallYears = 
    FsCheck.Gen.choose(2000,2500) |> Arb.fromGen

[<Test>]
let ``Test that easter is in april or may``() = 
    Check.QuickThrowOnFailure (Prop.forAll (smallYears) EasterIsInAllowedRange)