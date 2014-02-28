namespace Aklefdal.Holidays.HttpApi

open System

module CountryCode = 
    let CurrentCountry = 
        let currentCulture = System.Globalization.CultureInfo.CurrentCulture.ToString()
        let countrychars = currentCulture.ToCharArray() |> Array.rev |> Seq.take 2 |> Seq.toArray |> Array.rev
        System.String.Concat(countrychars)
