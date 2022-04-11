# Holiday Search Tool

This is a holiday search API which lets users search by departure & arrival locations &
finds the best holiday recommendations. The user also specifies the length of
holiday & this API finds the best hotels deals as well.


# Technical Details

The solution is built on .Net 6.0 (LTS) using C# language. 

## OTB.HolidaySearch.Api
Api layer is the entry point of this holiday search tool. User specifies
the departure, arrival locations & other required data.

### FluentValidation
A set of validation rules have been added to filter out any bad data or invalid requests.
There is an important validation rule added to check the search locations must be valid locations based
on locations file.

Validator checks the minimum length of depature/arrival airports as well as tries to match the input
location with airport code, city code or anywhere.

### Autofac IoC container
Autofac DI container is used to resolve & inject dependencies.

### Api layer references: OTB.HolidaySearch.Domain & OTB.HolidaySearch.Gds layers