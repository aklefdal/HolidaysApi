module HolidaysApi.Tests

open FsCheck.Xunit
open System 
open Aklefdal.Holidays.HttpApi.Computus

[<Property>]
let ``Easter Day is Sunday`` (date: DateTime) =
    let easter = EasterDay2 date.Year
    easter.DayOfWeek = DayOfWeek.Sunday

[<Property>]
let ``Algorithms are equal`` (date: DateTime) =
    EasterDay date.Year = EasterDay2 date.Year

[<Property>]
let ``In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``(date: DateTime) =
    let easter = EasterDay date.Year
    let firstPossibleDay = DateTime(date.Year, 3, 22)
    let lastPossibleDay = DateTime(date.Year, 4, 25)
    easter >= firstPossibleDay && easter <= lastPossibleDay && easter.DayOfWeek = DayOfWeek.Sunday

[<Property>]
let ``Alg2: In Western Christianity, using the Gregorian calendar, Easter always falls on a Sunday between 22 March and 25 April inclusive``(date: DateTime) =
    let easter = EasterDay2 date.Year
    let firstPossibleDay = DateTime(date.Year, 3, 22)
    let lastPossibleDay = DateTime(date.Year, 4, 25)
    easter >= firstPossibleDay && easter <= lastPossibleDay 