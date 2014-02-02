namespace Aklefdal.Holidays.HttpApi

open System
open System.Net
open System.Net.Http
open System.Web.Http

type HomeController() =
    inherit ApiController()
    member this.Get() = new HttpResponseMessage()

type EasterController() =
    inherit ApiController()
    member this.Get() = new HttpResponseMessage()

