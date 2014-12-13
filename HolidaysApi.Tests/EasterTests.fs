module HolidaysApi.Tests

open NUnit.Framework
open FsCheck
open System 
open Aklefdal.Holidays.HttpApi.Computus

//http://en.wikipedia.org/wiki/Easter
let ``Easter is in March and April using algorithm 1`` (date: DateTime) =
    let easter = EasterDay date.Year
    let easterMonth = easter.Month
    easterMonth = 3 || easterMonth = 4 

let ``Easter is in March and April using algorithm 2`` (date: DateTime) =
    let easter = EasterDay2 date.Year
    let easterMonth = easter.Month
    easterMonth = 3 || easterMonth = 4 

let ``Easter Day is Sunday`` (date: DateTime) =
    let easter = EasterDay2 date.Year
    easter.DayOfWeek = DayOfWeek.Sunday

[<Test>]
let ``Test that easterday is sunday``() = 
    Check.QuickThrowOnFailure ``Easter Day is Sunday``

[<Test>]
let ``Test that easter is in march or april using algorithim 1``() = 
    Check.QuickThrowOnFailure ``Easter is in March and April using algorithm 1``

[<Test>]
let ``Test that easter is in march or april using algorithm 2``() = 
    Check.QuickThrowOnFailure ``Easter is in March and April using algorithm 2``