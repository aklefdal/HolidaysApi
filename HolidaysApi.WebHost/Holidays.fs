module HolidaysApi.WebHost.Holidays

open System
open HolidaysApi.Core
open Oxpecker
open Renditions

let properHolidayResponse year country =
    let holidays =
        Holidays.ForYear country year
        |> Seq.map (fun holiday ->
            { Date = Dates.FormatDate holiday.Date
              Name = holiday.Name
              DateLink =
                { Rel = "http://aklefdal.com/date"
                  Href = Dates.FormatDateLinkWithCountry(CountryCode.CodeFromCountry country, holiday.Date) } })
        |> List.ofSeq
        |> List.sortBy _.Date

    { CountryCode = CountryCode.CodeFromCountry country
      Holidays = Seq.toArray holidays }

let findHolidayResponse countryCode year =
    countryCode |> Option.map (properHolidayResponse year)

let holidayHandlerForYearAndCountry country year : EndpointHandler =
    let countryInput = CountryCode.CountryFromCode country
    match findHolidayResponse countryInput year with
        | Some response -> response |> json
        | None -> setStatusCode 400

let holidayHandlerForYear year : EndpointHandler =
    properHolidayResponse year CountryCode.CurrentCountryOrDefault  |> json

let holidayHandler : EndpointHandler =    
        DateTime.Now.Year |> holidayHandlerForYear |> json
