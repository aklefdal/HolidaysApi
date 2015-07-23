HolidaysApi
===========

A web API for getting public holidays. Implemented for norwegian (no), swedish (se) and danish (dk) holidays.

`/easter/2015` - Gives you Easter Day for 2015

`/holidays/no/2015` - Gives you a list of norwegian holidays for 2015

`/date/no/2015/03/29` - Gives you a description of the date March 29, 2015 as related to norwegian holidays

 * Is it Sunday?
 * Is it Saturday?
 * Is it a holiday
 * Is it a workday (not Sunday, not Saturday, not Holiday)
 * What is the previous workday

## Project goals ##

 * Show that you can manage holidays without hardcoding everything.
 * As a tool for learning F#.
 * Show how to create a web site using F# only.

## Licensing ##

Apache License, see the file [LICENSE](./LICENSE)