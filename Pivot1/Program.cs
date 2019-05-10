using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pivot1
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Record> records = new List<Record>();
      records.Add(new Record { Month = "19-01", TallyName = "sub1", Account = "02", Amount = 13 });
      records.Add(new Record { Month = "19-01", TallyName = "sub3", Account = "05", Amount = 5 });
      records.Add(new Record { Month = "19-01", TallyName = "sub2", Account = "03", Amount = 11 });
      records.Add(new Record { Month = "19-01", TallyName = "sub3", Account = "04", Amount = 7 });
      records.Add(new Record { Month = "19-02", TallyName = "sub4", Account = "06", Amount = 19 });
      records.Add(new Record { Month = "19-02", TallyName = "sub3", Account = "02", Amount = 29 });
      records.Add(new Record { Month = "19-02", TallyName = "add1", Account = "01", Amount = 100 });
      records.Add(new Record { Month = "19-02", TallyName = "sub3", Account = "03", Amount = 23 });
      records.Add(new Record { Month = "19-01", TallyName = "add1", Account = "01", Amount = 200 });
      records.Add(new Record { Month = "19-02", TallyName = "sub4", Account = "07", Amount = 17 });

      var list2 = (from l in records
                   orderby l.Month, l.TallyName
                   group l by new { l.Month, l.TallyName}
                   into x
                     select new
                     {
                       x.Key.Month,
                       x.Key.TallyName,
                       //x.Key.Account,
                       s = x.Sum(y => y.Amount)
                     }
                   ).ToList();

      foreach (var rec in list2)
      {
        foreach (var item in rec.GetType().GetProperties())
        {
          Console.Write($"{item.GetValue(rec).ToString()}\t");
        }
        Console.WriteLine();
      }


      //var list1 = (from l in records
      //            select l
      //            ).ToList();

      //foreach (var rec in list1)
      //{
      //  foreach (var item in rec.GetType().GetProperties())
      //  {
      //    Console.Write($"{item.GetValue(rec).ToString()}\t");
      //  }
      //  Console.WriteLine();
      //}

      Console.Write("\nPress any key...");
      Console.ReadKey();
    }


  }

  public class Record
  {
    public string Month { get; set; }
    public string TallyName { get; set; }
    public string Account { get; set; }
    public int Amount { get; set; }
  }
}
