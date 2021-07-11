# SearchEngineBackend

Please start the application and make sure it is available at this ssl port 44380

The application is build using three-tier architecture.
The db layer contains the information about search engines.
More search engines can be added to the application by following the SearchEngine class from the EntityModels folder.
The db layer is only used by the business layer not the controller.

The business layer contains this main pipeline function SearchEngineUrlRankService.FindUrlRankFromSearchEngine
It is served to direct to flow of the logic. The logic is broken down to smaller functions across difference services that implement interfaces.
The business layer is only used by controllers. It can only return Dtos instead of entity models.

The controller only serves to receive requests, call business layer and return responses.

Application is built with loose coupling in mind. Components can be easily swapped out and tested easily

The application is caching the link results instead of the entire page to reduce memory footprint. 

Unit test project added using Xunit and NSubstitute.

If more time is given can implement these additional feature
Bing can only return around 50 results in one go even though I specified the count parameter as 100.
We can add send multiple requests with different paging info to make sure we get all 100 results

Some search engines do not return the result in html straight away. They call their api service onPageLoad using ajax.
The currently web client is not able to read those result. We can add a variation to call those search engine api endpoints.

Increase unit test coverage
