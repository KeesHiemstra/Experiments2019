using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Weather.Models;
using Newtonsoft.Json;

namespace Weather
{
	public class WeatherModelView
	{
		public async static Task<WeatherData> GetWeather(double Lat, double Lon)
		{
			//Get JSon string from the web
			var http = new HttpClient();
			var httpRespond = await http.GetAsync($"http://api.openweathermap.org/data/2.5/weather?lat={Lat}&lon={Lon}&appid=8b6fbc4fab942058a2e1967b913460a4&lang=nl");
			var httpResult = await httpRespond.Content.ReadAsStringAsync();

			//using (StreamWriter sw = File.AppendText(@"C:\Temp\Weather.log"))
			//{
				//await sw.WriteLineAsync(String.Format("{0}\t{1}",
					//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
					//httpResult));
			//}

      //Translate data
      WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(httpResult);
			//var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(httpResult));

			//var weatherData = (WeatherData)serializer.ReadObject(memoryStream);

			return weatherData;
		}

	}


	}
