module HolidayTests

open NUnit.Framework
open FsCheck
open System 
open FsCheck.NUnit
open Aklefdal.Holidays.HttpApi.Holidays

[<Property>]
let ``Langfredag is fredag in Norway`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, langfredag)= Seq.find (fun (name, _) -> name = "Langfredag") norwegianHolidays
    langfredag.DayOfWeek = DayOfWeek.Friday

//http://no.wikipedia.org/wiki/Kristi_himmelfartsdag
//Kristi himmelfartsdag vil alltid falle på en torsdag 39 dager (5½ uke) etter 1. påskedag, dvs. i tidsrommet 30. april – 3. juni.

[<Property>]
let ``Kristi himmelfartsdag is thursday`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, himmelfartsdag)= Seq.find (fun (name, _) -> name = "Kristi Himmelfartsdag") norwegianHolidays
    himmelfartsdag.DayOfWeek = DayOfWeek.Thursday

[<Property>]
let ``Kristi himmelfartsdag is between 30. april – 3. juni`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, himmelfartsdag)= Seq.find (fun (name, _) -> name = "Kristi Himmelfartsdag") norwegianHolidays
    let year = date.Year
    let firstPossibleDay = new DateTime(year, 4, 30)
    let lastPossibleDay = new DateTime(year, 6, 3)
    himmelfartsdag >= firstPossibleDay && himmelfartsdag <= lastPossibleDay 