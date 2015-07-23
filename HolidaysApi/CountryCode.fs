namespace Aklefdal.Holidays.HttpApi

open System

module CountryCode = 
    type Countries = Norway | Sweden | Denmark | GreatBrittain | USA

    let DefaultCountry = Norway

    let CountryFromCode (code:String) =
        match code.ToLower() with
            | "no" -> Some Norway
            | "se" -> Some Sweden
            | "dk" -> Some Denmark
            | "uk" -> Some GreatBrittain
            | "us" -> Some USA
            | _ -> None

    let CodeFromCountry country =
        match country with
            | Norway -> "no"
            | Denmark -> "dk"
            | Sweden -> "se"
            | GreatBrittain -> "uk"
            | USA -> "us"

    let CurrentCountryCode = 
        let currentCulture = System.Globalization.CultureInfo.CurrentCulture
        let countrychars = currentCulture.ToString().ToCharArray() |> Array.rev |> Seq.take 2 |> Seq.toArray |> Array.rev // TODO
        System.String.Concat(countrychars)

    let CurrentCountry = CountryFromCode CurrentCountryCode

    let CurrentCountryOrDefault =
        match CurrentCountry with
            | Some country -> country
            | None -> DefaultCountry
