module HolidaysApi.Core.Dates

open System

type Span = Span of TimeSpan with
    static member (+) (d:DateTime, Span wrapper) = d + wrapper
    static member Zero = Span(TimeSpan(0L))

let IsSunday(date:DateTime) = 
    let day = date.DayOfWeek
    day = DayOfWeek.Sunday

let IsSaturday(date:DateTime) = 
    let day = date.DayOfWeek
    day = DayOfWeek.Saturday

let WeekdayAfterOrOn(weekday:DayOfWeek, firstDate:DateTime) = //TODO
    let ts = TimeSpan.FromDays(1.0)
    [ firstDate .. Span(ts) .. firstDate.AddDays(6.0) ]
    |> Seq.find (fun date -> date.DayOfWeek = weekday)

let FormatDate(date:DateTime) =
    date.ToString("yyyy-MM-dd")

let FormatDateLink (date:DateTime) = String.Format("date/{0}", date.ToString("yyyy\/MM\/dd"))

let FormatDateLinkWithCountry (country:String, date:DateTime) = String.Format("date/{0}/{1}", country, date.ToString("yyyy\/MM\/dd"))
