using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.ModelViews
{
  public class MainModelView
  {
    List<LocationWeather> Weathers = new List<LocationWeather>();

    public MainModelView()
    {
      Weathers.Add(new LocationWeather { Name = "Meerkerk", Lat = 51.913580, Lon = 4.999771 });

      foreach (var weather in Weathers)
      {
        GetWeather(weather);
      }
    }

    public async void GetWeather(LocationWeather location)
    {

      WeatherData weather = await WeatherModelView.GetWeather(location.Lat, location.Lon);

      location.Temperature = weather.main.temp.ToCelsius();
    }
  }
}
