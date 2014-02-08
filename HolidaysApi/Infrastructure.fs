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

//    config.Routes.MapHttpRoute(
//        "HolidaysMonth",
//        "holidays/{year}/{month}",
//        { Controller = "Holidays"; Id = RouteParameter.Optional }) |> ignore
//
//    config.Routes.MapHttpRoute(
//        "HolidaysDay",
//        "holidays/{year}/{month}/{day}",
//        { Controller = "Holidays"; Id = RouteParameter.Optional }) |> ignore

    config.Routes.MapHttpRoute(
        "DefaultAPI",
        "{controller}/{id}",
        { Controller = "Home"; Id = RouteParameter.Optional }) |> ignore

let Configure config =
    ConfigureRoutes config
