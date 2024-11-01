module Aklefdal.Holidays.Renditions

[<CLIMutable>]
type EasterDefinition = {
    EasterDay : string }

[<CLIMutable>]
type AtomLinkRendition = {
    Rel : string
    Href : string }

[<CLIMutable>]
type LinkListRendition = {
    Links : AtomLinkRendition array }

[<CLIMutable>]
type HolidayRendition = {
    Date : string
    Name : string
    DateLink : AtomLinkRendition }

[<CLIMutable>]
type HolidaysRendition = {
    CountryCode: string
    Holidays : HolidayRendition array }

[<CLIMutable>]
type DateRendition = {
    CountryCode: string
    Date : string
    IsSunday: bool
    IsSaturday: bool
    IsHoliday: bool
    IsWorkday: bool
    PreviousWorkday: string
    PreviousWorkdayLink: AtomLinkRendition }
