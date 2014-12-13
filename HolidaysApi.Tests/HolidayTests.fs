module HolidayTests

open NUnit.Framework
open FsCheck
open System 
open Aklefdal.Holidays.HttpApi.Holidays

let ``Langfredag is fredag in Norway`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, langfredag)= Seq.find (fun (name, _) -> name = "Langfredag") norwegianHolidays
    langfredag.DayOfWeek = DayOfWeek.Friday

[<Test>]
let ``Test that langfredag is fredag``() = 
    Check.QuickThrowOnFailure ``Langfredag is fredag in Norway``

//http://no.wikipedia.org/wiki/Kristi_himmelfartsdag
//Kristi himmelfartsdag vil alltid falle på en torsdag 39 dager (5½ uke) etter 1. påskedag, dvs. i tidsrommet 30. april – 3. juni.

let ``Kristi himmelfartsdag is thursday`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, himmelfartsdag)= Seq.find (fun (name, _) -> name = "Kristi Himmelfartsdag") norwegianHolidays
    himmelfartsdag.DayOfWeek = DayOfWeek.Thursday

let ``Kristi himmelfartsdag is between 30. april – 3. juni`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, himmelfartsdag)= Seq.find (fun (name, _) -> name = "Kristi Himmelfartsdag") norwegianHolidays
    let year = date.Year
    let firstPossibleDay = new DateTime(year, 4, 30)
    let lastPossibleDay = new DateTime(year, 6, 3)
    himmelfartsdag >= firstPossibleDay && himmelfartsdag <= lastPossibleDay 

[<Test>]
let ``Test that Kristi himmelfartsdag is thursday``() = 
    Check.QuickThrowOnFailure ``Kristi himmelfartsdag is thursday``

[<Test>]
let ``Test that Kristi himmelfartsdag is in allowed range``() = 
    Check.QuickThrowOnFailure ``Kristi himmelfartsdag is between 30. april – 3. juni`` 