namespace Aklefdal.Holidays.HttpApi

open System
open System.Net
open System.Net.Http
open System.Web.Http

type HomeController() =
    inherit ApiController()

    member this.Get() =
        this.Request.CreateResponse(
            HttpStatusCode.OK,
            {
                Links =
                    [| {
                        Rel = "http://aklefdal.com/easter"
                        Href = "easter/2014" } |] })

type EasterController() =
    inherit ApiController()
    member this.Get(year:int) =
        let easterDay =
            Computus.EasterDay year

        this.Request.CreateResponse(
            HttpStatusCode.OK,
            { EasterDay = Dates.FormatDate easterDay })
    member this.Get() =
        let year =
            DateTime.Now.Year

        this.Get year

type HolidaysController() =
    inherit ApiController()

    let ProperHolidayResponse country year =
        let holidays =
            Holidays.ForYear country year
            |> Seq.map (fun (name, date) ->
                {
                    Date = Dates.FormatDate date
                    Name = name
                    DateLink = { Rel = "http://aklefdal.com/date"
                                 Href =  Dates.FormatDateLinkWithCountry(CountryCode.CodeFromCountry country, date)}})
            |> List.ofSeq
            |> List.sortBy (fun holiday -> holiday.Date)
        let response =
            {
                CountryCode = CountryCode.CodeFromCountry country
                Holidays = Seq.toArray holidays
            }
        response

    let FindHolidayResponse countryCode year =
        countryCode |> Option.map (fun c -> ProperHolidayResponse c year)

    member this.Get(country, year) =
        let countryInput = CountryCode.CountryFromCode country
        match FindHolidayResponse countryInput year with
            | Some response -> this.Request.CreateResponse(HttpStatusCode.OK, response)
            | None -> this.Request.CreateResponse(HttpStatusCode.NotImplemented)

    member this.Get(year:int) =
        let country = CountryCode.CurrentCountryOrDefault
        this.Get(CountryCode.CodeFromCountry country, year)

    member this.Get() =
        let year =
            DateTime.Now.Year

        this.Get year

type DateController() =
    inherit ApiController()

    let ProperDateResponse country date =
        let previousWorkday = Workdays.PreviousWorkday country date
        let response =
            {
                CountryCode = CountryCode.CodeFromCountry country
                Date = Dates.FormatDate date
                IsSunday = Dates.IsSunday date
                IsSaturday = Dates.IsSaturday date
                IsHoliday = Holidays.IsHoliday country date
                IsWorkday = Workdays.IsWorkDay country date
                PreviousWorkday = Dates.FormatDate previousWorkday
                PreviousWorkdayLink = { Rel = "http://aklefdal.com/date"
                                        Href = Dates.FormatDateLinkWithCountry(CountryCode.CodeFromCountry country, previousWorkday)}
            }
        response

    let FindDateResponse countryCode date =
        countryCode |> Option.map (fun c -> ProperDateResponse c date)

    member this.Get(country, year, month, day) =
        let countryInput = CountryCode.CountryFromCode country
        let date = new DateTime(year, month, day)
        match FindDateResponse countryInput date with
            | Some response -> this.Request.CreateResponse(HttpStatusCode.OK, response)
            | None -> this.Request.CreateResponse(HttpStatusCode.NotImplemented, "Not implemented for country: " + country)

    member this.Get(year, month, day) =
        let country = CountryCode.CurrentCountryCode
        this.Get(country, year, month, day)
