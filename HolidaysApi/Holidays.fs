namespace Aklefdal.Holidays.HttpApi

open System

module Holidays =
    
    type Holiday = { 
        Name : string
        Date : DateTime }

    // http://www.norges-bank.no/no/finansiell-stabilitet/norges-banks-oppgjorssystem/oppgjorsdager/
    let HolidaysNO year =
        let easterday = Computus.EasterDay year
        seq {
            yield { Name =  "1. nyttårsdag"
                    Date = new DateTime(year, 1, 1) }
            yield { Name =   "Palmesøndag" 
                    Date = easterday.AddDays(-7.0) }
            yield { Name =   "Skjærtorsdag" 
                    Date = easterday.AddDays(-3.0) }
            yield { Name =   "Langfredag" 
                    Date = easterday.AddDays(-2.0) }
            yield { Name =  "1. påskedag" 
                    Date = easterday }
            yield { Name =  "2. påskedag" 
                    Date = easterday.AddDays(1.0) }
            yield { Name =  "Kristi Himmelfartsdag" 
                    Date = easterday.AddDays(39.0) }
            yield { Name =  "1. pinsedag" 
                    Date = easterday.AddDays(49.0) }
            yield { Name =  "2. pinsedag" 
                    Date = easterday.AddDays(50.0) }
            yield { Name =  "Offentlig høytidsdag" 
                    Date = new DateTime(year, 5, 1) }
            yield { Name =  "Grunnlovsdag" 
                    Date = new DateTime(year, 5, 17) }
            yield { Name =  "Julaften" 
                    Date = new DateTime(year, 12, 24) }
            yield { Name =  "1. juledag" 
                    Date = new DateTime(year, 12, 25) }
            yield { Name =  "2. juledag" 
                    Date = new DateTime(year, 12, 26) }
            }

    let HolidaysSE year =
        // http://www.riksbank.se/sv/Kalendarium/Bankhelgdagar/
        let easterday = Computus.EasterDay year
        seq {
            yield { Name =  "Nyårsdagen" 
                    Date = new DateTime(year, 1, 1) }
            yield { Name =  "Trettondedag jul" 
                    Date = new DateTime(year, 1, 6) }
            yield { Name =  "Långfredagen" 
                    Date = easterday.AddDays(-2.0) } 
            yield { Name =  "Påskdagen" 
                    Date = easterday } 
            yield { Name =  "Annandag påsk" 
                    Date = easterday.AddDays(1.0) } 
            yield { Name =  "Kristi Himmelsfärdsdag" 
                    Date = easterday.AddDays(39.0) } 
            yield { Name =  "Pingstdagen" 
                    Date = easterday.AddDays(49.0) } 
            yield { Name =  "Valborg, Första Maj" 
                    Date = new DateTime(year, 5, 1) } 
            yield { Name =  "Sveriges nationaldag" 
                    Date = new DateTime(year, 6, 6) } 
            yield { Name =  "Midsommarafton" 
                    Date =  Dates.WeekdayAfterOrOn(DayOfWeek.Friday, new DateTime(year, 6, 19)) }
            yield { Name =  "Midsommardagen" 
                    Date = Dates.WeekdayAfterOrOn(DayOfWeek.Saturday, new DateTime(year, 6, 20)) } 
            yield { Name =  "Alla Helgons dag" 
                    Date = Dates.WeekdayAfterOrOn(DayOfWeek.Saturday, new DateTime(year, 10, 31)) } 
            yield { Name =  "Julafton" 
                    Date = new DateTime(year, 12, 25) } 
            yield { Name =  "Juldagen" 
                    Date = new DateTime(year, 12, 25) } 
            yield { Name =  "Annandag Jul" 
                    Date = new DateTime(year, 12, 26) } 
            yield { Name =  "Nyårsafton" 
                    Date = new DateTime(year, 12, 31) } 
            }

    let HolidaysDK year =
        // http://www.finansraadet.dk/Bankkunde/Pages/bankhelligdage.aspx
        let easterday = Computus.EasterDay year
        seq {
            yield { Name = "Nytårsdag" 
                    Date = new DateTime(year, 1, 1) }
            yield { Name =  "Palmesøndag" 
                    Date = easterday.AddDays(-7.0) } 
            yield { Name =  "Skærtorsdag" 
                    Date = easterday.AddDays(-3.0) } 
            yield { Name =  "Langfredag" 
                    Date = easterday.AddDays(-2.0) } 
            yield { Name =  "1. påskedag" 
                    Date = easterday }
            yield { Name =  "2. påskedag" 
                    Date = easterday.AddDays(1.0) } 
            yield { Name =  "Store Bededag" 
                    Date = easterday.AddDays(26.0) } 
            yield { Name =  "Kristi Himmelfartsdag" 
                    Date = easterday.AddDays(39.0) } 
            yield { Name =  "Bankhelligdag" 
                    Date = easterday.AddDays(40.0) } 
            yield { Name =  "1. pinsedag" 
                    Date = easterday.AddDays(49.0) } 
            yield { Name =  "2. pinsedag" 
                    Date = easterday.AddDays(50.0) } 
            yield { Name =  "Grundlovsdag" 
                    Date = new DateTime(year, 6, 5) } 
            yield { Name =  "Juleaftensdag" 
                    Date = new DateTime(year, 12, 24) } 
            yield { Name =  "1. juledag" 
                    Date = new DateTime(year, 12, 25) } 
            yield { Name =  "2. juledag" 
                    Date = new DateTime(year, 12, 26) } 
            yield { Name =  "Nytårsaftensdag" 
                    Date = new DateTime(year, 12, 31) } 
            }
    
    let ForYear (country:CountryCode.Countries) (year:int) =
        match country with
            | CountryCode.Countries.Norway -> HolidaysNO year
            | CountryCode.Countries.Sweden -> HolidaysSE year
            | CountryCode.Countries.Denmark -> HolidaysDK year
            | CountryCode.Countries.GreatBrittain -> HolidaysDK year
            | CountryCode.Countries.USA -> HolidaysDK year

    let DatesForYear country year =
        ForYear country year |> Seq.map (fun holiday -> holiday.Date)
    
    let IsHoliday country  (date:DateTime) =
        let holidays = DatesForYear country date.Year
        Seq.exists (fun elem -> elem = date.Date) holidays
