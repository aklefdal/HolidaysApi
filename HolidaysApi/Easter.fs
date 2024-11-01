namespace Aklefdal.Holidays.HttpApi

open System

module Computus =
    // Based upon the Python example in https://en.wikipedia.org/wiki/Computus
    let EasterDay year = 
        let a = year % 19
        let b = year >>> 2
        let c = b / 25 + 1
        let d = (c * 3) >>> 2
        let e = ((a * 19) - ((c * 8 + 5) / 25) + d + 15) % 30
        let e2 = e + ((29578 - a - e * 32) >>> 10)
        let e3 = e2 - ((year % 7) + b - d + e2 + 2) % 7
        let d2 = e3 >>> 5
        let day = e3 - d2 * 31
        let month = d2 + 3
        DateTime(year, month, day)

    // Based upon the VB example in https://en.wikipedia.org/wiki/Computus
    let EasterDay2 (year:int) = 
        let secularNumber = year / 100
        let secularMoonShift = 15 + ((3 * secularNumber) + 3) / 4 - ((8 * secularNumber) + 13) / 25
        let secularSunShift = 2 - ((3 * secularNumber) + 3) / 4
        let moonParameter = year % 19
        let fullMoonSeed = ((19 * moonParameter) + secularMoonShift) % 30
        let calendarianCorrection = fullMoonSeed / 29 + (fullMoonSeed / 28 - fullMoonSeed / 29) * (moonParameter / 11)
        let easterLimit = 21 + fullMoonSeed - calendarianCorrection
        let firstSundayInMarch = 7 - ((year + year / 4 + secularSunShift) % 7)
        let easterSundayDistance = 7 - ((easterLimit - firstSundayInMarch) % 7)
        let firstOfMarch = DateTime(year, 3, 1)
        firstOfMarch.AddDays(float easterLimit).AddDays(float easterSundayDistance).AddDays(-1.0)
