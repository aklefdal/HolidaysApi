module HolidaysApi.WebHost.Easter

open System
open Aklefdal.Holidays
open Aklefdal.Holidays.HttpApi
open Oxpecker
open Renditions

let getEasterForYear (year: int) =
    let easterDay = Computus.EasterDay year
    { EasterDay = Dates.FormatDate easterDay }

let easterHandler : EndpointHandler = DateTime.Now.Year |> getEasterForYear |> json

let easterHandlerForYear (year: int) : EndpointHandler =
    let response = year |> getEasterForYear
    json response

