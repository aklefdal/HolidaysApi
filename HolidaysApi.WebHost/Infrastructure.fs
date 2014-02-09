namespace Aklefdal.Holidays.HttpApi.HttpHost

open System
open System.Web.Http
open System.Net.Http
open Aklefdal.Holidays.HttpApi.Infrastructure
open System.Threading;
open System.Threading.Tasks;

type HttpRouteDefaults = { Controller : string; Id : obj }

//type SetResponseCacheHeadersHandler() =
//    inherit DelegatingHandler()
//
//    let setHeader task:Task<HttpResponseMessage> = 
//        let mutable response = task.Result
//        response.Headers.CacheControl.Public <- true
//        response.Headers.CacheControl.MaxAge <- new Nullable<TimeSpan>(TimeSpan.FromDays(float 30))
//        response
//
//    override this.SendAsync(request:HttpRequestMessage, cancellationToken:CancellationToken) =
//        let response = base.SendAsync(request, cancellationToken).ContinueWith(fun task -> setHeader task)
//        response

type Global() =
    inherit System.Web.HttpApplication()
    member this.Application_Start (sender : obj) (e : EventArgs) =
        GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer <- true
        Configure GlobalConfiguration.Configuration
//        GlobalConfiguration.Configuration.MessageHandlers.Add(new SetResponseCacheHeadersHandler());
        