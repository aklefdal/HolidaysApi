﻿module HolidaysApi.Core.CountryCode

open System

type Country = Norway | Sweden | Denmark

let DefaultCountry = Norway

let CountryFromCode (code:String) =
    match code.ToLower() with
        | "no" -> Some Norway
        | "se" -> Some Sweden
        | "dk" -> Some Denmark
        | _ -> None

let CodeFromCountry country =
    match country with
        | Norway -> "no"
        | Denmark -> "dk"
        | Sweden -> "se"

let CurrentCountryCode = 
    let currentCulture = System.Globalization.CultureInfo.CurrentCulture
    let countrychars = currentCulture.ToString().ToCharArray() |> Array.rev |> Seq.take 2 |> Seq.toArray |> Array.rev // TODO
    String.Concat(countrychars)

let CurrentCountry = CountryFromCode CurrentCountryCode

let CurrentCountryOrDefault =
    match CurrentCountry with
        | Some country -> country
        | None -> DefaultCountry
