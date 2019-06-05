# Instructions
You will submit a small project .NET MVC project. While the project domain is entirely fictional, and the tasks you complete are rather contrived, the code you will need to write to complete the tasks has many parallels to code written in real-world business situations. Write and structure your code with the same care you would apply in a real-world business application.

A project "skeleton" has been provided containing a single controller with an Index action and view, as well as four unimplemented actions. The Index view is a simple list of buttons corresponding to those unimplemented actions.

A set of simple views and their associated view models has been provided. Your focus should be on the back-end code. You will be working with a public RESTful API that returns results JSON-formatted data sets based on the 7 Star Wars films. The documentation for querying the API can be found at https://swapi.co/documentation

Your task is to write the backend code needed to build out the provided View Models for each of the views. You may query the Star Wars API using any built-in .NET classes you would like for making web requests. Do not use RestSharp or any other 3rd-party library/packages to query the API.

General Guidelines and Tips
---------------------------
* The project skeleton was built using Visual Studio 2015 and .NET 4.6. However, you may elect to implement your project using Visual Studio 2017 and .NET Core (1.1 or 2.0) if you prefer. (Optional)

* Keep your controller actions small. Delegate your data access code and manipulation of result sets to supporting classes as appropriate. Create as many additional classes as you need. You may rewrite any existing code as needed -- the purpose of the existing code in the controller actions is just to show you the type of View Model you need to build out in each action in order for the view to render properly.

* You will need to create the base classes corresponding to the result sets returned by the Star Wars API. In these base classes, it is advisable to implement all properties as type `string` or `List<string>`, since the API can return "unknown" for any property. Conform to generally-accepted conventions in .NET regarding property naming, capitalization, etc. Your property names may not match those in the JSON returned by the API. Demonstrate how to map the JSON property names to the property names in your base classes so that you can easily deserialize the response data from the API using the Newtonsoft.Json deserializer. You will not need to use the "created" or "edited" properties. Feel free to omit those from your models.

* Even though this task is purely fictional in nature, be mindful of the organization of files within your project. Assume that this is just the starting point for a much larger project. Create any needed folders to group your classes just as you would in a real-world project. Do not, however, create additional projects within the solution. 

* Create and use Interfaces where appropriate. If you wish to utilize dependency injection, you may use the DI framework of your choice. (This is optional, but encouraged!)

* Do not write any Javascript. Perform all API queries using C#.

* When you are finished, we recommend uploading the completed project to a personal Github repository and providing us with the link to the repository when ready for us to view your code.

Controller Actions to Implement:
--------------------------------
1) GetAllPlanets()
   -- For this action, retrieve a list of all planets, and order them by their Diameter, from largest to smallest. The number of planets displayed by the view should be 61. Do not introduce paging into the view. Display the full list on a single page. Some of the planets have a diameter of "unknown". These should be at the end of the list displayed by the view. For each planet, we want to see its name, diameter, how long its year (orbital period) is, its population, and its terrain. In addition, you will determine the average diameter of those planets whose diameter is known. (Do not include the planets with an unknown diameter when computing the average.)
   
2) GetPlanetTwentyTwo()
   -- Although we only want information about planet id 22, the controller action should be written to get information about any planet id. (The button linking to this action from the Index view will pass a planetid of 22 into the controller action.) Use the appropriate Star Wars API endpoint to retrieve information for a single planet only. You will use the provided View Model to pass the planet's name, population, day length (rotation period), year length (orbital period), diameter, climate, surface water percentage, and gravity information into the provided View.
   
3) GetResidentsOfPlanetNaboo()
   -- Although we only want information about planet Naboo, the controller action should be written to get information about any planet name passed into it. (The button linking to this action from the provided Index view will pass Naboo as the planetname parameter into the controller action) You will use the Star Wars API endpoints as needed to obtain a list of residents for the planet. For each resident, we want their name, eye color, hair color, skin color, gender, height, and weight. Sort the results alphabetically by name prior to passing them to the view.
   
4) VehicleSummary()
   -- Get a list of all vehicles from the Star Wars API and present some summary information. The top portion of the view will display the total number of vehicles where the cost is not "unknown", as well as the total number of unique manufacturers. For the table, take the vehicles where the cost is not "unknown", and group them by manufacturer. For each group (table row), get the manufacturer name, the number of vehicles by that manufacturer, and their average cost. Sort them by vehicle count (highest to lowest), then by average cost (highest to lowest). 

5) (Optional, but encouraged!)
   -- Implement an additional controller action of your choosing. Use one or more API endpoints that weren't used previously to retrieve data sets from the Star Wars API, and do something interesting with that data! The choice is up to you, but this is your opportunity to show us what else you can do with C#. 