namespace Aklefdal.Holidays.HttpApi.HttpHost

open System
open System.Web.Http
open Aklefdal.Holidays.HttpApi.Infrastructure

type HttpRouteDefaults = { Controller : string; Id : obj }

type Global() =
    inherit System.Web.HttpApplication()
    member this.Application_Start (sender : obj) (e : EventArgs) =
        GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer <- true
        Configure GlobalConfiguration.Configuration
        