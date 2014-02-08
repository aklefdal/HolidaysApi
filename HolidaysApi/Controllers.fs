namespace Aklefdal.Holidays.HttpApi

open System
open System.Net
open System.Net.Http
open System.Web.Http

type HomeController() =
    inherit ApiController()

    member this.Get() =
        this.Request.CreateResponse(
            HttpStatusCode.OK,
            {
                Links =
                    [| {
                        Rel = "http://aklefdal.com/easter"
                        Href = "easter/2014" } |] })

type EasterController() =
    inherit ApiController()
    member this.Get(year:int) = 
        let easterDay =
            Computus.EasterDay year

        this.Request.CreateResponse(
            HttpStatusCode.OK,
            { EasterDay = easterDay.ToString("yyyy-MM-dd") })
    member this.Get() = 
        let year =
            DateTime.Now.Year

        this.Get year

type HolidaysController() =
    inherit ApiController()
    member this.Get(year:int) = 
        let holidays =
            Holidays.ForYear year
            |> Seq.map (fun (name, date) -> 
                { 
                    Date = date.ToString("yyyy-MM-dd")
                    Name = name
                    DateLink = { Rel = "http://aklefdal.com/date"
                                 Href =  String.Format("date/{0:yyyy-MM-dd}", date)}})
            |> List.ofSeq
            |> List.sortBy (fun holiday -> holiday.Date)
        this.Request.CreateResponse(
            HttpStatusCode.OK,
            { Holidays = Seq.toArray holidays })
    member this.Get() = 
        let year =
            DateTime.Now.Year

        this.Get year
