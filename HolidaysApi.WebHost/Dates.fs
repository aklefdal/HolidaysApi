module HolidaysApi.WebHost.Dates

open System
open Aklefdal.Holidays
open Aklefdal.Holidays.HttpApi
open Microsoft.AspNetCore.Http
open Oxpecker
open Renditions

let properDateResponse country date =
    let previousWorkday = Workdays.PreviousWorkday country date

    { CountryCode = CountryCode.CodeFromCountry country
      Date = Dates.FormatDate date
      IsSunday = Dates.IsSunday date
      IsSaturday = Dates.IsSaturday date
      IsHoliday = Holidays.IsHoliday country date
      IsWorkday = Workdays.IsWorkDay country date
      PreviousWorkday = Dates.FormatDate previousWorkday
      PreviousWorkdayLink =
        { Rel = "http://aklefdal.com/date"
          Href = Dates.FormatDateLinkWithCountry(CountryCode.CodeFromCountry country, previousWorkday) } }

let notImplemented (msg: string) : EndpointHandler =
    fun (ctx: HttpContext) ->
        ctx.SetStatusCode 501
        text msg ctx

let findDateResponse countryCode date =
    countryCode |> Option.map (fun c -> properDateResponse c date)

let dateHandlerForDateAndCountry (country: string) (year: int) (month: int) (day: int) : EndpointHandler =
    let countryInput = CountryCode.CountryFromCode country
    let date = DateTime(year, month, day)

    match findDateResponse countryInput date with
    | Some response -> response |> json
    | None -> "Not implemented for country: " + country |> notImplemented

let dateHandlerForDate (year: int) (month: int) (day: int) : EndpointHandler =
    let country = CountryCode.CurrentCountryCode
    dateHandlerForDateAndCountry country year month day
