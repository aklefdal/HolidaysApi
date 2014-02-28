namespace Aklefdal.Holidays.HttpApi

open System

module Holidays =
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
            yield ("1. juledag", new DateTime(year, 12, 25)) 
            yield ("2. juledag", new DateTime(year, 12, 26)) 
            }
    
    let ForYear(country:string, year:int) =
        let countryUpperCase = country.ToUpperInvariant()
        match countryUpperCase with
            | "NO" -> HolidaysNO year
            | _ -> Seq.empty

    let DatesForYear country year =
        ForYear(country, year) |> Seq.map (fun (_, holiday) -> holiday)
    
    let IsHoliday(country, date:DateTime) =
        let holidays = DatesForYear country date.Year
        Seq.exists (fun elem -> elem = date.Date) holidays
