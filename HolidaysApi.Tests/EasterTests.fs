module HolidaysApi.Tests

open NUnit.Framework
open FsCheck
open System 
open Aklefdal.Holidays.HttpApi.Computus

//http://en.wikipedia.org/wiki/Easter
let ``Easter is in March and April`` (date: DateTime) =
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
let ``Test that easter is in march or april``() = 
    Check.QuickThrowOnFailure ``Easter is in March and April``