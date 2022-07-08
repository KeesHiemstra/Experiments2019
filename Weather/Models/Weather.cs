using System;
using System.Collections.Generic;

namespace Weather.Models
{
  #region WeatherData
  public class Coord
  {
    public double lon { get; set; }

    public double lat { get; set; }
  }


  public class WeatherI
  {
    public int id { get; set; }

    public string main { get; set; }

    public string description { get; set; }

    public string icon { get; set; }
  }

  public class Main
  {
    public double temp { get; set; }

    public int pressure { get; set; }

    public int humidity { get; set; }

    public double temp_min { get; set; }

    public double temp_max { get; set; }
  }


  public class Wind
  {
    public double speed { get; set; }

    public int deg { get; set; }
  }


  public class Clouds
  {
    public int all { get; set; }
  }


  public class Sys
  {
    public int type { get; set; }

    public int id { get; set; }

    public double message { get; set; }

    public string country { get; set; }

    public int sunrise { get; set; }

    public int sunset { get; set; }
  }


  public class WeatherData
  {
    public Coord coord { get; set; }

    public List<WeatherI> weather { get; set; }

    public string @base { get; set; }

    public Main main { get; set; }

    public int visibility { get; set; }

    public Wind wind { get; set; }

    public Clouds clouds { get; set; }

    public int dt { get; set; }

    public Sys sys { get; set; }

    public int id { get; set; }

    public string name { get; set; }

    public int cod { get; set; }
  }
  #endregion

  public static class Weather
  {
    #region Temperature
    /// <summary>
    /// Calculate temperature Kalvin to Celsius in 1 decimal.
    /// </summary>
    /// <param name="Kelvin"></param>
    /// <returns>Celsius</returns>
    public static double ToCelsius(this double Kelvin)
    {
      int Kelvin100 = (int)(Kelvin * 100) - 27315;
      return Math.Floor((((double)Kelvin100) / 10) + 0.5) / 10;
    }
    #endregion

    #region Wind
    /// <summary>
    /// Convert wind speed to scale of Beaufort.
    /// </summary>
    /// <param name="WindSpeed"></param>
    /// <returns></returns>
    public static int ConvertWindSpeedToBeaufort(double WindSpeed)
    {
      if (WindSpeed <= 0.2) { return 0; }
      if (WindSpeed <= 1.5) { return 1; }
      if (WindSpeed <= 3.3) { return 2; }
      if (WindSpeed <= 5.4) { return 3; }
      if (WindSpeed <= 7.9) { return 4; }
      if (WindSpeed <= 10.7) { return 5; }
      if (WindSpeed <= 13.8) { return 6; }
      if (WindSpeed <= 17.1) { return 7; }
      if (WindSpeed <= 20.7) { return 8; }
      if (WindSpeed <= 24.4) { return 9; }
      if (WindSpeed <= 28.4) { return 10; }
      if (WindSpeed <= 32.6) { return 11; }
      return 12;
    }

    /// <summary>
    /// Convert Beaufort to short description of the wind.
    /// </summary>
    /// <param name="WindBeaufort"></param>
    /// <returns></returns>
    public static string ConvertWindBeaufortToName(int WindBeaufort)
    {
      string[] WindName = new string[13] {
        "Wind stil",
        "Flauw en stil",
        "Zwakke wind",
        "Lichte koelte",
        "Matige koelte",
        "Vrij krachtige wind",
        "Krachtige wind",
        "Harde wind",
        "Stormachtige wind",
        "Storm",
        "Zware storm",
        "Zeer zware storm",
        "Orkaan"};
      return WindName[WindBeaufort];
    }

    /// <summary>
    /// Convert Beaufort to short description of the wind.
    /// </summary>
    /// <param name="WindBeaufort"></param>
    /// <returns></returns>
    public static string ConvertWindBeaufortToName(double WindSpeed)
    {
      return ConvertWindBeaufortToName(ConvertWindSpeedToBeaufort(WindSpeed));
    }

    /// <summary>
    /// Convert Beaufort to description of the wind.
    /// </summary>
    /// <param name="WindBeaufort"></param>
    /// <returns></returns>
    public static string ConvertWindBeaufortToDescription(int WindBeaufort)
    {
      string[] WindDescription = new string[13] {
        "Rook stijgt recht of bijna recht omhoog.",
        "Windrichting goed herkenbaar aan rookpluimen.",
        "Bladeren beginnen te ritselen en windvanen kunnen gaan bewegen. Wind begint merkbaar te worden in het gelaat.",
        "Bladeren en twijgen zijn voortdurend in beweging.",
        "Kleine takken beginnen te bewegen. Stof en papier beginnen van de grond op te dwarrelen.",
        "Kleine bebladerde takken maken zwaaiende bewegingen. Er vormen zich gekuifde golven op meren en kanalen.",
        "Grote takken bewegen. Paraplu’s kunnen slechts met moeite worden vastgehouden.",
        "Gehele bomen bewegen. De wind is hinderlijk wanneer men er tegen in loopt.",
        "Twijgen breken af. Fietsen en lopen wordt bemoeilijkt.",
        "Lichte schade aan gebouwen. Schoorsteenkappen en dakpannen worden afgerukt.",
        "Ontwortelde bomen. Aanzienlijke schade aan gebouwen enz. Komt boven land zelden voor.",
        "Uitgebreide schade.",
        "Komt boven land zeer zelden voor."};
      return WindDescription[WindBeaufort];
    }

    /// <summary>
    /// Convert Beaufort to description of the wind.
    /// </summary>
    /// <param name="WindBeaufort"></param>
    /// <returns></returns>
    public static string ConvertWindBeaufortToDescription(double WindSpeed)
    {
      return ConvertWindBeaufortToDescription(ConvertWindSpeedToBeaufort(WindSpeed));
    }

    /// <summary>
    /// Convert direction to direction name.
    /// </summary>
    /// <param name="Direction"></param>
    /// <returns></returns>
    public static string ConvertDirectionToName(int Direction)
    {
      string[] DirectionName = new string[16]
      {
        "N", "NNO", "NO", "ONO",
        "O", "OZO", "ZO", "ZZO",
        "Z", "ZZW", "ZW", "WZW",
        "W", "WNW", "NW", "NNW"
      };
      int direction = (int)Math.Round(((float)Direction / 22.5));
      if (direction == 16) { direction = 0; }
      return DirectionName[direction];
    }
    #endregion

    #region Time
    /// <summary>
    /// Convert Unix timestamp (number of seconds since epoch to date/time.
    /// </summary>
    /// <param name="unixTimeStamp"></param>
    /// <returns></returns>
    public static DateTime ConvertUnixTimeToDate(int unixTime)
    {
      // Unix time stamp is seconds past epoch
      System.DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
      return dateTime.AddSeconds(unixTime).ToLocalTime();
    }
    #endregion
  }//Weather
}
