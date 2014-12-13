module HolidaysApi.Tests

open NUnit.Framework
open FsCheck
open System 
open Aklefdal.Holidays.HttpApi.Computus

let ``Easter Day is Sunday`` (date: DateTime) =
    let easter = EasterDay2 date.Year
    easter.DayOfWeek = DayOfWeek.Sunday

let ``Algorithms are equal`` (date: DateTime) =
    EasterDay date.Year = EasterDay2 date.Year

[<Test>]
let ``Test Algorithms are equal`` () = 
    Check.QuickThrowOnFailure ``Algorithms are equal``

[<Test>]
let ``Test that easterday is sunday``() = 
    Check.QuickThrowOnFailure ``Easter Day is Sunday``

let ``In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``(date: DateTime) =
    let easter = EasterDay date.Year
    let firstPossibleDay = new DateTime(date.Year, 3, 22)
    let lastPossibleDay = new DateTime(date.Year, 4, 25)
    easter >= firstPossibleDay && easter <= lastPossibleDay && easter.DayOfWeek = DayOfWeek.Sunday

let ``Alg2: In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``(date: DateTime) =
    let easter = EasterDay2 date.Year
    let firstPossibleDay = new DateTime(date.Year, 3, 22)
    let lastPossibleDay = new DateTime(date.Year, 4, 25)
    easter >= firstPossibleDay && easter <= lastPossibleDay 


[<Test>]
let ``Test that In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``() =
    Check.QuickThrowOnFailure ``In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``

[<Test>]
let ``Alg2: Test that In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``() =
    Check.QuickThrowOnFailure ``Alg2: In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``