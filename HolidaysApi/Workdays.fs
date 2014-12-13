namespace Aklefdal.Holidays.HttpApi

open System

module Workdays =
    let IsWorkDay(country:string, date:DateTime) =
        if Dates.IsSunday date then false
        elif Dates.IsSaturday date then false
        elif Holidays.IsHoliday(country, date) then false
        else true

    let rec PreviousWorkday(country:string, date:DateTime) =
        let previousDay = date.AddDays(-1.0)
        if IsWorkDay(country, previousDay) then previousDay
        else PreviousWorkday(country, previousDay)
