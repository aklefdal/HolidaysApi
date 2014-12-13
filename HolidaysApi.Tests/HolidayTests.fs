module HolidayTests

open NUnit.Framework
open FsCheck
open System 
open Aklefdal.Holidays.HttpApi.Holidays

let ``Langfredag is fredag in Norway`` (date: DateTime) =
    let norwegianHolidays =  HolidaysNO date.Year 
    let (_, langfredag)= Seq.find (fun (name, holidayDate) -> name = "Langfredag") norwegianHolidays
    langfredag.DayOfWeek = DayOfWeek.Friday

[<Test>]
let ``Test that langfredag is fredag``() = 
    Check.QuickThrowOnFailure ``Langfredag is fredag in Norway``