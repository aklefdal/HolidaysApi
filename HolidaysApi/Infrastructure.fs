module Aklefdal.Holidays.HttpApi.Infrastructure

open System
open System.Net.Http
open System.Web.Http

type HttpRouteDefaults = { Controller : string; Id : obj }

let ConfigureRoutes (config : HttpConfiguration) =
    config.Routes.MapHttpRoute(
        "Easter",
        "easter/{year}",
        { Controller = "Easter"; Id = System.DateTime.Now.Year }) |> ignore

    config.Routes.MapHttpRoute(
        "HolidaysYear",
        "holidays/{year}",
        { Controller = "Holidays"; Id = RouteParameter.Optional }) |> ignore

    config.Routes.MapHttpRoute(
        "HolidaysCountryYear",
        "holidays/{country}/{year}",
        { Controller = "Holidays"; Id = RouteParameter.Optional }) |> ignore

    config.Routes.MapHttpRoute(
        "Dates",
        "date/{year}/{month}/{day}",
        { Controller = "Date"; Id = RouteParameter.Optional }) |> ignore

    config.Routes.MapHttpRoute(
        "DateForCountry",
        "date/{country}/{year}/{month}/{day}",
        { Controller = "Date"; Id = RouteParameter.Optional }) |> ignore

    config.Routes.MapHttpRoute(
        "DefaultAPI2",
        "{controller}/{id}",
        { Controller = "Home"; Id = RouteParameter.Optional }) |> ignore

let Configure config =
    ConfigureRoutes config
