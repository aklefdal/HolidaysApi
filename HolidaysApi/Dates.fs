namespace Aklefdal.Holidays.HttpApi

open System

module Dates =
    type Span = Span of TimeSpan with
        static member (+) (d:DateTime, Span wrapper) = d + wrapper
        static member Zero = Span(new TimeSpan(0L))

    let IsSunday(date:DateTime) = 
        let day = date.DayOfWeek
        day = DayOfWeek.Sunday

    let IsSaturday(date:DateTime) = 
        let day = date.DayOfWeek
        day = DayOfWeek.Saturday

    let WeekdayAfterOrOn(weekday:DayOfWeek, firstDate:DateTime) =
        let ts = TimeSpan.FromDays(1.0)
        [ firstDate .. Span(ts) .. firstDate.AddDays(float 6) ]
        |> Seq.find (fun (date) -> date.DayOfWeek = weekday)

    let FormatDate(date:DateTime) =
        date.ToString("yyyy-MM-dd")

    let FormatDateLink (date:DateTime) = String.Format("date/{0}", date.ToString("yyyy\/MM\/dd"))