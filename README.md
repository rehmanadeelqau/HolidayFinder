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

## OTB.HolidaySearch.Domain
Domain layer is the core layer where business & domain related logic is implemented. Being the core layer of
the application, this layer does not depend on any other layer. 
This layer has core business logic implemented. It also defines interfaces for other layer to implement.

#### RecommendationsSearcher : IRecommendationSearcher
This is the main class which finds the matching flights, hotels based on the user input data. When comparing
the locations, this takes into account the airport code, city code & anywhere locations.
This class also matches the matching hotels with the user search criteria & creates a holiday recommendation.

## OTB.HolidaySearch.Gds
This is the libraby which provides the source data to the domain layer. It implements the interfaces defined
in the Domain layer & allows us to switch/change implementaion if we need to. If we want to switch from
json files to real GDS, we can easily achieve this by adding another implementation of the FlightsConverter, HotelsConverter etc.
This would avoid making any change in the domain layer.

This layer defins:
* Contracts
* Domain/Business implementation
* Interfaces (for GDS layer to implement)

##### public interface IFlightsConverter<out FlightsResponse> : IResponseConverter<FlightsResponse>
This is an interfaces which allows other interfaces to implement generics.
* public class FlightsConverter : IFlightsConverter<FlightsResponse>
* public class HotelsConverter : IHotelsConverter<HotelsResponse>
* public class LocationsConverter : ILocationsConverter<LocationsResponse>
##### Json Data
* flights.json
* hotels.json
* locations.json

##### locations.json
This file holds the referential data of different cities & airports in those cities. E.g.
`
[
...
	{
		"city_code": "MAN",
		"city_name": "Manchester",
		"airports": [
			{
				"airport_code": "MAN",
				"name": "Manchester International Airport",
				"country": "United Kingdom"
			}
		]
	},
	{
		"city_code": "LON",
		"city_name": "London",
		"airports": [
			{
				"airport_code": "LTN",
				"name": "London Luton Airport",
				"country": "United Kingdom"
			},
			{
				"airport_code": "LGW",
				"name": "London Gatwick Airport",
				"country": "United Kingdom"
			}
		]
	}
...
]
`
