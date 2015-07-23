module HolidayTests

open NUnit.Framework
open FsCheck
open System 
open FsCheck.NUnit
open Aklefdal.Holidays.HttpApi.Holidays

[<Property>]
let ``Langfredag is fredag in Norway`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let langfredag = Seq.find (fun holiday -> holiday.Name = "Langfredag") norwegianHolidays
    langfredag.Date.DayOfWeek = DayOfWeek.Friday

//http://no.wikipedia.org/wiki/Kristi_himmelfartsdag
//Kristi himmelfartsdag vil alltid falle på en torsdag 39 dager (5½ uke) etter 1. påskedag, dvs. i tidsrommet 30. april – 3. juni.

[<Property>]
let ``Kristi himmelfartsdag is thursday`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let himmelfartsdag = Seq.find (fun holiday -> holiday.Name = "Kristi Himmelfartsdag") norwegianHolidays
    himmelfartsdag.Date.DayOfWeek = DayOfWeek.Thursday

[<Property>]
let ``Kristi himmelfartsdag is between 30. april – 3. juni`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let himmelfartsdag = Seq.find (fun holiday -> holiday.Name = "Kristi Himmelfartsdag") norwegianHolidays
    let year = date.Year
    let firstPossibleDay = new DateTime(year, 4, 30)
    let lastPossibleDay = new DateTime(year, 6, 3)
    himmelfartsdag.Date >= firstPossibleDay && himmelfartsdag.Date <= lastPossibleDay 