using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Radar.Models;

namespace Radar.Controllers {
  public class RadarController: Controller {
    private readonly HttpClient _httpClient;
    private
    const string ApiKey = "3c933a6b-9178-4305-829e-3b64c868d5fa";
    private
    const string ApiUrl = "https://airlabs.co/api/v9/flights";

    public RadarController() {
      _httpClient = new HttpClient();
    }

    public async Task < ActionResult > Index() {
      var apiUrl = "https://airlabs.co/api/v9/flights?_view=array&_fields=lat,lng,dir,alt,dep_iata,arr_iata&api_key=3c933a6b-9178-4305-829e-3b64c868d5fa";

      var response = await _httpClient.GetAsync(apiUrl);

      var content = await response.Content.ReadAsStringAsync();

      var flights = JsonConvert.DeserializeObject < List < List < object >>> (content);

      if (flights == null) {
        throw new Exception("Failed to create flight data.");
      }

      var flightData = flights.Select(flight => new Flight {
        Lat = Convert.ToDouble(flight[0]),
          Lng = Convert.ToDouble(flight[1]),
          Dir = Convert.ToDouble(flight[2]),
          Alt = Convert.ToDouble(flight[3]),
          Dep = flight[4] != null ? Convert.ToString(flight[4]) : "N/A",
          Arr = flight[5] != null ? Convert.ToString(flight[5]) : "N/A"
      }).ToList();

      ViewData["BingMapsApiKey"] = "AkdRKULTar3VtV9muldEh5v-CXQuvfP0VZtIr4_LDUABcuPWq-9jP86ZAMtUx-yR";
      ViewData["FlightData"] = JsonConvert.SerializeObject(flightData);

      return View();
    }
  }
}