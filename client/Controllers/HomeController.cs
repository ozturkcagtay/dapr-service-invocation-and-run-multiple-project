using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;
using Dapr.Client;
using Dapr.Client.Http;

namespace client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DaprClient _daprClient;

        public HomeController(ILogger<HomeController> logger,
        DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<IActionResult> Index()
        {
            var httpExtension = new HTTPExtension
            {
                Verb = HTTPVerb.Get
            };

            var weatherForecastResult = await _daprClient.InvokeMethodAsync<object>("server-app","WeatherForecast",httpExtension);

            ViewBag.WeatherForecast = weatherForecastResult;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
