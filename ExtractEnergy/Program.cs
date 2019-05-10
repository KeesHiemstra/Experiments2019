using System;
using System.Collections.Generic;
using System.IO;
using CHiDateTimeWeekNumber;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractEnergy
{
  class Program
  {
    public static List<JournalRecord> Records = new List<JournalRecord>();
    
    static void Main(string[] args)
    {
      ReadJournal();
      SplitLevels();
      CalculateLevels();
      WriteRecords();

      Console.Write("\nPress any key...");
      Console.ReadKey();
    }

    private static void SplitLevels()
    {
      string[] elements;
      string[] value;
      int level = 0;
      foreach (var record in Records)
      {
        elements = record.Levels.Split(',');

        value = elements[0].Split(' ');
        int.TryParse(value[1], out level);
        record.LevelGas = level;

        value = elements[1].Trim().Split(' ');
        int.TryParse(value[1], out level);
        record.LevelLow = level;

        value = elements[2].Trim().Split(' ');
        int.TryParse(value[1], out level);
        record.LevelHigh = level;
      }
    }

    private static void CalculateLevels()
    {
      DateTime lastDate = DateTime.Now;
      int lastGas = 0, lastLow = 0, lastHigh = 0;

      foreach (var record in Records)
      {
        DateTimeWeekNumber dwn = new DateTimeWeekNumber(record.Date);
        record.Week = dwn.ISOWeekNoExtended;

        if (lastGas != 0)
        {
          record.Time = record.Date - lastDate;
          record.Gas = record.LevelGas - lastGas;
          record.Low = record.LevelLow - lastLow;
          record.High = record.LevelHigh - lastHigh;
          if (record.Gas < 0)
          {
            record.Gas = record.LevelGas;
            record.Low = record.LevelLow;
            record.High = record.LevelHigh;
          }
        }
        record.Current = record.Low + record.High;
        lastDate = record.Date;
        lastGas = record.LevelGas;
        lastLow = record.LevelLow;
        lastHigh = record.LevelHigh;
      }
    }

    private static void WriteRecords()
    {
      using (StreamWriter stream = new StreamWriter(@"C:\Users\Kees\OneDrive\Data\Energie.csv"))
      {
        stream.WriteLine("Date,Week,Gas,Laag,Hoog,Gas,Stroom");
        foreach (var record in Records)
        {
          stream.WriteLine($"{record.Date},{record.Week}," +
            $"{record.LevelGas},{record.LevelLow},{record.LevelHigh}," +
            $"{record.Gas},{record.Current}");
        }
      }
    }

    private static void ReadJournal()
    {
      using (StreamReader stream = new StreamReader(@"C:\Users\Kees\OneDrive\Data\Energy.csv"))
      {
        int count = 0;
        while (!stream.EndOfStream)
        {
          count++;
          string Line = stream.ReadLine();
          ProcessLine(Line, count);
        }
      }
    }

    private static void ProcessLine(string line, int count)
    {
      string[] Elements = line.Replace("\"", "").Split(';');

      if (count > 1)
      {
        Records.Add(new JournalRecord
        {
          Date = DateTime.Parse(Elements[0]),
          Levels = Elements[1]
        });
      }
    }


  }

  public class JournalRecord
  {
    public DateTime Date { get; set; }
    public string Week { get; set; }
    public string Levels { get; set; }
    public int LevelGas { get; set; }
    public int LevelLow { get; set; }
    public int LevelHigh { get; set; }
    public TimeSpan Time { get; set; }
    public int Gas { get; set; }
    public int Low { get; set; }
    public int High { get; set; }
    public int Current { get; set; }
    public int WeekGas { get; set; }
    public int WeekLow { get; set; }
    public int WeekHigh { get; set; }
  }
}
