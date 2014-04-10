namespace Aklefdal.Holidays.HttpApi

open System

module Holidays =
    // http://www.norges-bank.no/no/finansiell-stabilitet/norges-banks-oppgjorssystem/oppgjorsdager/
    let HolidaysNO year =
        let easterday = Computus.EasterDay year
        seq {
            yield ("1. nyttårsdag", new DateTime(year, 1, 1))
            yield ("Palmesøndag", easterday.AddDays(float -7)) 
            yield ("Skjærtorsdag", easterday.AddDays(float -3)) 
            yield ("Langfredag", easterday.AddDays(float -2)) 
            yield ("1. påskedag", easterday) 
            yield ("2. påskedag", easterday.AddDays(float 1)) 
            yield ("Kristi Himmelfartsdag", easterday.AddDays(float 39)) 
            yield ("1. pinsedag", easterday.AddDays(float 49)) 
            yield ("2. pinsedag", easterday.AddDays(float 50)) 
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
            yield ("Långfredagen", easterday.AddDays(float -2)) 
            yield ("Påskdagen", easterday) 
            yield ("Annandag påsk", easterday.AddDays(float 1)) 
            yield ("Kristi Himmelsfärdsdag", easterday.AddDays(float 39)) 
            yield ("Pingstdagen", easterday.AddDays(float 49)) 
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
    
    let ForYear(country:string, year:int) =
        let countryUpperCase = country.ToUpperInvariant()
        match countryUpperCase with
            | "NO" -> HolidaysNO year
            | "SE" -> HolidaysSE year
            | _ -> Seq.empty

    let DatesForYear country year =
        ForYear(country, year) |> Seq.map (fun (_, holiday) -> holiday)
    
    let IsHoliday(country, date:DateTime) =
        let holidays = DatesForYear country date.Year
        Seq.exists (fun elem -> elem = date.Date) holidays
