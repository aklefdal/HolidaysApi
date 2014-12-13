namespace Aklefdal.Holidays.HttpApi

open System

module Holidays =
    // http://www.norges-bank.no/no/finansiell-stabilitet/norges-banks-oppgjorssystem/oppgjorsdager/
    let HolidaysNO year =
        let easterday = Computus.EasterDay year
        seq {
            yield ("1. nyttårsdag", new DateTime(year, 1, 1))
            yield ("Palmesøndag", easterday.AddDays(-7.0)) 
            yield ("Skjærtorsdag", easterday.AddDays(-3.0)) 
            yield ("Langfredag", easterday.AddDays(-2.0)) 
            yield ("1. påskedag", easterday) 
            yield ("2. påskedag", easterday.AddDays(1.0)) 
            yield ("Kristi Himmelfartsdag", easterday.AddDays(39.0)) 
            yield ("1. pinsedag", easterday.AddDays(49.0)) 
            yield ("2. pinsedag", easterday.AddDays(50.0)) 
            yield ("Offentlig høytidsdag", new DateTime(year, 5, 1)) 
            yield ("Grunnlovsdag", new DateTime(year, 5, 17)) 
            yield ("Julaften", new DateTime(year, 12, 24)) 
            yield ("1. juledag", new DateTime(year, 12, 25)) 
            yield ("2. juledag", new DateTime(year, 12, 26)) 
            }

    let HolidaysSE year =
        // http://www.riksbank.se/sv/Kalendarium/Bankhelgdagar/
        let easterday = Computus.EasterDay year
        seq {
            yield ("Nyårsdagen", new DateTime(year, 1, 1))
            yield ("Trettondedag jul", new DateTime(year, 1, 6))
            yield ("Långfredagen", easterday.AddDays(-2.0)) 
            yield ("Påskdagen", easterday) 
            yield ("Annandag påsk", easterday.AddDays(1.0)) 
            yield ("Kristi Himmelsfärdsdag", easterday.AddDays(39.0)) 
            yield ("Pingstdagen", easterday.AddDays(49.0)) 
            yield ("Valborg, Första Maj", new DateTime(year, 5, 1)) 
            yield ("Sveriges nationaldag", new DateTime(year, 6, 6)) 
            yield ("Midsommarafton", Dates.WeekdayAfterOrOn(DayOfWeek.Friday, new DateTime(year, 6, 19))) 
            yield ("Midsommardagen", Dates.WeekdayAfterOrOn(DayOfWeek.Saturday, new DateTime(year, 6, 20))) 
            yield ("Alla Helgons dag", Dates.WeekdayAfterOrOn(DayOfWeek.Saturday, new DateTime(year, 10, 31))) 
            yield ("Julafton", new DateTime(year, 12, 25)) 
            yield ("Juldagen", new DateTime(year, 12, 25)) 
            yield ("Annandag Jul", new DateTime(year, 12, 26)) 
            yield ("Nyårsafton", new DateTime(year, 12, 31)) 
            }

    let HolidaysDK year =
        // http://www.finansraadet.dk/Bankkunde/Pages/bankhelligdage.aspx
        let easterday = Computus.EasterDay year
        seq {
            yield ("Nytårsdag", new DateTime(year, 1, 1))
            yield ("Palmesøndag", easterday.AddDays(-7.0)) 
            yield ("Skærtorsdag", easterday.AddDays(-3.0)) 
            yield ("Langfredag", easterday.AddDays(-2.0)) 
            yield ("1. påskedag", easterday) 
            yield ("2. påskedag", easterday.AddDays(1.0)) 
            yield ("Store Bededag", easterday.AddDays(26.0)) 
            yield ("Kristi Himmelfartsdag", easterday.AddDays(39.0)) 
            yield ("Bankhelligdag", easterday.AddDays(40.0)) 
            yield ("1. pinsedag", easterday.AddDays(49.0)) 
            yield ("2. pinsedag", easterday.AddDays(50.0)) 
            yield ("Grundlovsdag", new DateTime(year, 6, 5)) 
            yield ("Juleaftensdag", new DateTime(year, 12, 24)) 
            yield ("1. juledag", new DateTime(year, 12, 25)) 
            yield ("2. juledag", new DateTime(year, 12, 26)) 
            yield ("Nytårsaftensdag ", new DateTime(year, 12, 31)) 
            }
    
    let ForYear(country:string, year:int) =
        let countryUpperCase = country.ToUpperInvariant()
        match countryUpperCase with
            | "NO" -> HolidaysNO year
            | "SE" -> HolidaysSE year
            | "DK" -> HolidaysDK year
            | _ -> Seq.empty

    let DatesForYear country year =
        ForYear(country, year) |> Seq.map (fun (_, holiday) -> holiday)
    
    let IsHoliday(country, date:DateTime) =
        let holidays = DatesForYear country date.Year
        Seq.exists (fun elem -> elem = date.Date) holidays
