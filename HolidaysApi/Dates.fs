namespace Aklefdal.Holidays.HttpApi

open System

module Dates =
    let IsSunday(date:DateTime) = 
        let day = date.DayOfWeek
        day = DayOfWeek.Sunday

    let IsSaturday(date:DateTime) = 
        let day = date.DayOfWeek
        day = DayOfWeek.Saturday

    let IsWorkDay(country:string, date:DateTime) =
        if IsSunday date then false
        elif IsSaturday date then false
        elif Holidays.IsHoliday(country, date) then false
        else true

    let rec PreviousWorkday(country:string, date:DateTime) =
        let previousDay = date.AddDays(float -1)
        if IsWorkDay(country, previousDay) then previousDay
        else PreviousWorkday(country, previousDay)

    let FormatDate(date:DateTime) =
        date.ToString("yyyy-MM-dd")

    let FormatDateLink (date:DateTime) = String.Format("date/{0}", date.ToString("yyyy\/MM\/dd"))