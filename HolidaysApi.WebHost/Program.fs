module HolidaysApi.WebHost.Program

open Aklefdal.Holidays
open Aklefdal.Holidays.HttpApi
open HolidaysApi.WebHost
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Oxpecker
open Renditions
open Easter
open Holidays
open Dates

let indexHandler: EndpointHandler =
    { Links =
        [| { Rel = "https://aklefdal.com/easter"
             Href = "easter/2014" } |] }
    |> json

let endpoints =
    [ route "/" indexHandler
      routef "/easter/{%i}" easterHandlerForYear
      route "/easter" easterHandler
      routef "/holidays/{%s}/{%i}" holidayHandlerForYearAndCountry
      routef "/holidays/{%i}" holidayHandlerForYear
      route "/holidays" holidayHandler
      routef "/date/{%i}/{%i}/{%i}" dateHandlerForDate
      routef "/date/{%s}/{%i}/{%i}/{%i}" dateHandlerForDateAndCountry ]

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddRouting().AddOxpecker() |> ignore
    let app = builder.Build()
    app.UseRouting().UseOxpecker(endpoints) |> ignore

    app.Run()
    0
