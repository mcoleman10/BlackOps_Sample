using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlattSampleApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpClient GetClient(string url)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        //public async Task<T> GetAsync<T>(string url)
        //{

        //    T result = default(T);

        //    using (HttpClient client = GetClient("https://swapi.co/api/"))
        //    {
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        //throw if error
        //        response.EnsureSuccessStatusCode();
        //        result = await response.Content.ReadAsAsync<T>();
        //    }

        //    return result;
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllPlanets()
        {
            var model = new AllPlanetsViewModel();

            model = GetPlanetList();

            int count = SortPlanetsByDiameter(ref model);
            
            model.AverageDiameter = model.AverageDiameter / count;

            model.Planets.Sort();

            return View(model);
        }

        public ActionResult GetPlanetTwentyTwo(int planetid)
        {
            var model = new SinglePlanetViewModel();
            
            using (HttpClient client = GetClient("https://swapi.co/api/"))
            {
                HttpResponseMessage response = client.GetAsync("planets/" + planetid).Result;
                response.EnsureSuccessStatusCode();
                model = response.Content.ReadAsAsync<SinglePlanetViewModel>().Result;
            }

            return View(model);
        }

        public ActionResult GetResidentsOfPlanetNaboo(string planetname)
        {
            var model = new PlanetResidentsViewModel();
            
            // Get all planets
            AllPlanetsViewModel planetsList = new AllPlanetsViewModel();
            planetsList = GetPlanetList();

            // Find Naboo ID from all planets 
            PlanetDetailsViewModel planet = planetsList.Planets.Find(x => x.Name.ToUpper() == planetname.ToUpper());

            // Use residents property to make another API call
            model = GetResidentsByPlanet(ref planet);

            // Sort residents by name
            model.Residents.Sort((x, y) => x.Name.CompareTo(y.Name));

            return View(model);
        }

        public ActionResult VehicleSummary()
        {
            var model = new VehicleSummaryViewModel();
            
            // Get a list of all vehicles
            List<Vehicle> allVehicles = GetAllVehicles();

            // Get total number of unique manufacturers
            model.ManufacturerCount = (from x in allVehicles select x.manufacturer).Distinct().Count();

            // Remove vehicles that have an unknown cost
            foreach (var vehicle in allVehicles.Where(x => x.cost_in_credits.ToLower() == "unknown").ToArray())
                allVehicles.Remove(vehicle);

            // Get number of vehicles with a known cost
            model.VehicleCount = allVehicles.Count();
            
            // Get number of vehicles and average cost for each manufacturer
            var groupCountManufacturer = allVehicles.GroupBy(x => x.manufacturer).Select(y => new { manufacturer = y.Key, count = y.Count(), totalCost = y.Sum(s => double.Parse(s.cost_in_credits)) });
            foreach (var vehicle in groupCountManufacturer)
            {
                VehicleStatsViewModel vehicleStats = new VehicleStatsViewModel();
                vehicleStats.ManufacturerName = vehicle.manufacturer;
                vehicleStats.VehicleCount = vehicle.count;
                vehicleStats.AverageCost = vehicle.totalCost / vehicle.count;
                model.Details.Add(vehicleStats);
            }

            // Sort by count and cost
            model.Details.Sort((x, y) =>
            {
                int result = y.VehicleCount.CompareTo(x.VehicleCount);
                if (result == 0)
                    result = y.AverageCost.CompareTo(x.AverageCost);
                return result;
            });

            return View(model);
        }

        public AllPlanetsViewModel GetPlanetList()
        {
            AllPlanetsViewModel completeList = new AllPlanetsViewModel();
            Planets listOfPlanets = new Planets();
            //List<Planets> items = new List<Planets>();
            for (int i = 1; i < 8; i++)
            {
                using (HttpClient client = GetClient("https://swapi.co/api/"))
                {
                    HttpResponseMessage response = client.GetAsync("planets/?page=" + i).Result;
                    response.EnsureSuccessStatusCode();
                    listOfPlanets = response.Content.ReadAsAsync<Planets>().Result;
                    completeList.Planets.AddRange(listOfPlanets.results);
                }
            }
            return completeList;
        }

        public int SortPlanetsByDiameter(ref AllPlanetsViewModel listOfPlanets)
        {
            int count = 0;
            foreach (PlanetDetailsViewModel planet in listOfPlanets.Planets)
            {
                double diameter = 0.0;
                if (double.TryParse(planet.Diameter, out diameter))
                {
                    listOfPlanets.AverageDiameter += diameter;
                }
                count++;
            }
            return count;
        }

        public PlanetResidentsViewModel GetResidentsByPlanet(ref PlanetDetailsViewModel planet)
        {
            PlanetResidentsViewModel allResidents = new PlanetResidentsViewModel();
            ResidentSummary resident = new ResidentSummary();
            foreach (var res in planet.residents)
            {
                using (HttpClient client = GetClient(res))
                {
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    resident = response.Content.ReadAsAsync<ResidentSummary>().Result;
                    allResidents.Residents.Add(resident);
                }
            }
            return allResidents;
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> completeList = new List<Vehicle>();
            AllVehicles listOfVehicles = new AllVehicles();

            using (HttpClient client = GetClient("https://swapi.co/api/"))
            {
                do
                {
                    string i = "1";
                    if (listOfVehicles.isNext)
                    {
                        i = listOfVehicles.next.Substring(listOfVehicles.next.Length - 1, 1);
                    }
                    HttpResponseMessage response = client.GetAsync("vehicles/?page=" + i).Result;
                    response.EnsureSuccessStatusCode();
                    listOfVehicles = response.Content.ReadAsAsync<AllVehicles>().Result;
                    completeList.AddRange(listOfVehicles.results);
                }
                while (listOfVehicles.isNext);
            }
            return completeList;
        }
    }
}