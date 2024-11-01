module HolidaysApi.Core.Holidays

open System

type Holiday = { Name: string; Date: DateTime }

// http://www.norges-bank.no/no/finansiell-stabilitet/norges-banks-oppgjorssystem/oppgjorsdager/
let HolidaysNO year =
    let easterDay = Computus.EasterDay year

    seq {
        { Name = "1. nyttårsdag"
          Date = DateTime(year, 1, 1) }

        { Name = "Palmesøndag"
          Date = easterDay.AddDays(-7.0) }

        { Name = "Skjærtorsdag"
          Date = easterDay.AddDays(-3.0) }

        { Name = "Langfredag"
          Date = easterDay.AddDays(-2.0) }

        { Name = "1. påskedag"
          Date = easterDay }

        { Name = "2. påskedag"
          Date = easterDay.AddDays(1.0) }

        { Name = "Kristi Himmelfartsdag"
          Date = easterDay.AddDays(39.0) }

        { Name = "1. pinsedag"
          Date = easterDay.AddDays(49.0) }

        { Name = "2. pinsedag"
          Date = easterDay.AddDays(50.0) }

        { Name = "Offentlig høytidsdag"
          Date = DateTime(year, 5, 1) }

        { Name = "Grunnlovsdag"
          Date = DateTime(year, 5, 17) }

        { Name = "Julaften"
          Date = DateTime(year, 12, 24) }

        { Name = "1. juledag"
          Date = DateTime(year, 12, 25) }

        { Name = "2. juledag"
          Date = DateTime(year, 12, 26) }
    }

let HolidaysSE year =
    // http://www.riksbank.se/sv/Kalendarium/Bankhelgdagar/
    let easterDay = Computus.EasterDay year

    seq {
        { Name = "Nyårsdagen"
          Date = DateTime(year, 1, 1) }

        { Name = "Trettondedag jul"
          Date = DateTime(year, 1, 6) }

        { Name = "Långfredagen"
          Date = easterDay.AddDays(-2.0) }

        { Name = "Påskdagen"; Date = easterDay }

        { Name = "Annandag påsk"
          Date = easterDay.AddDays(1.0) }

        { Name = "Kristi Himmelsfärdsdag"
          Date = easterDay.AddDays(39.0) }

        { Name = "Pingstdagen"
          Date = easterDay.AddDays(49.0) }

        { Name = "Valborg, Första Maj"
          Date = DateTime(year, 5, 1) }

        { Name = "Sveriges nationaldag"
          Date = DateTime(year, 6, 6) }

        { Name = "Midsommarafton"
          Date = Dates.WeekdayAfterOrOn(DayOfWeek.Friday, DateTime(year, 6, 19)) }

        { Name = "Midsommardagen"
          Date = Dates.WeekdayAfterOrOn(DayOfWeek.Saturday, DateTime(year, 6, 20)) }

        { Name = "Alla Helgons dag"
          Date = Dates.WeekdayAfterOrOn(DayOfWeek.Saturday, DateTime(year, 10, 31)) }

        { Name = "Julafton"
          Date = DateTime(year, 12, 25) }

        { Name = "Juldagen"
          Date = DateTime(year, 12, 25) }

        { Name = "Annandag Jul"
          Date = DateTime(year, 12, 26) }

        { Name = "Nyårsafton"
          Date = DateTime(year, 12, 31) }
    }

let HolidaysDK year =
    // http://www.finansraadet.dk/Bankkunde/Pages/bankhelligdage.aspx
    let easterDay = Computus.EasterDay year

    seq {
        { Name = "Nytårsdag"
          Date = DateTime(year, 1, 1) }

        { Name = "Palmesøndag"
          Date = easterDay.AddDays(-7.0) }

        { Name = "Skærtorsdag"
          Date = easterDay.AddDays(-3.0) }

        { Name = "Langfredag"
          Date = easterDay.AddDays(-2.0) }

        { Name = "1. påskedag"
          Date = easterDay }

        { Name = "2. påskedag"
          Date = easterDay.AddDays(1.0) }

        { Name = "Store Bededag"
          Date = easterDay.AddDays(26.0) }

        { Name = "Kristi Himmelfartsdag"
          Date = easterDay.AddDays(39.0) }

        { Name = "Bankhelligdag"
          Date = easterDay.AddDays(40.0) }

        { Name = "1. pinsedag"
          Date = easterDay.AddDays(49.0) }

        { Name = "2. pinsedag"
          Date = easterDay.AddDays(50.0) }

        { Name = "Grundlovsdag"
          Date = DateTime(year, 6, 5) }

        { Name = "Juleaftensdag"
          Date = DateTime(year, 12, 24) }

        { Name = "1. juledag"
          Date = DateTime(year, 12, 25) }

        { Name = "2. juledag"
          Date = DateTime(year, 12, 26) }

        { Name = "Nytårsaftensdag"
          Date = DateTime(year, 12, 31) }
    }

let ForYear (country: CountryCode.Country) (year: int) =
    match country with
    | CountryCode.Country.Norway -> HolidaysNO year
    | CountryCode.Country.Sweden -> HolidaysSE year
    | CountryCode.Country.Denmark -> HolidaysDK year

let DatesForYear country year =
    ForYear country year |> Seq.map (fun holiday -> holiday.Date)

let IsHoliday country (date: DateTime) =
    let holidays = DatesForYear country date.Year
    Seq.exists (fun elem -> elem = date.Date) holidays
