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
            { EasterDay = Dates.FormatDate easterDay })
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
                    Date = Dates.FormatDate date
                    Name = name
                    DateLink = { Rel = "http://aklefdal.com/date"
                                 Href =  Dates.FormatDateLink date}})
            |> List.ofSeq
            |> List.sortBy (fun holiday -> holiday.Date)
        this.Request.CreateResponse(
            HttpStatusCode.OK,
            { Holidays = Seq.toArray holidays })
    member this.Get() = 
        let year =
            DateTime.Now.Year

        this.Get year

type DateController() =
    inherit ApiController()

    member this.Get year month day =
        let date = new DateTime(year, month, day) 
        let previousWorkday = Dates.PreviousWorkday date
        this.Request.CreateResponse(
            HttpStatusCode.OK,
            {
                Date = Dates.FormatDate date
                IsSunday = Dates.IsSunday date
                IsSaturday = Dates.IsSaturday date
                IsHoliday = Holidays.IsHoliday date
                IsWorkday = Dates.IsWorkDay date
                PreviousWorkday = Dates.FormatDate previousWorkday
                PreviousWorkdayLink = { Rel = "http://aklefdal.com/date"
                                        Href = Dates.FormatDateLink previousWorkday}})

//    
//    member this.Get() = 
//        let today = DateTime.Today
//        this.Request.CreateResponse(
//            HttpStatusCode.OK,
//            makeRendition today)
