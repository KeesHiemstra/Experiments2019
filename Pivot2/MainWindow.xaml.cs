using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pivot2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<Bank> Accounts { get; set; } = new List<Bank>();
		public List<string> Tallies { get; set; } = new List<string>();
		public DataSet Data { get; set; } = new DataSet("Data");
		DataView PivotView { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			LoadAccounts();
			CollectTallies();
			CreatePivotTable();
			FillDataGrid();

			PivotDataGrid.ItemsSource = null;
			PivotView = new DataView(Data.Tables["Pivot"]);
			PivotDataGrid.ItemsSource = PivotView;
		}

		private void LoadAccounts()
		{
			using (StreamReader stream = File.OpenText(@"C:\Tmp\Account.json"))
			{
				string json = stream.ReadToEnd();
				Accounts = JsonConvert.DeserializeObject<List<Bank>>(json);
			}
		}

		private void CollectTallies()
		{
			bool isPositiveAmounts = false;
			string originName = "Alles";
			Tallies = Accounts
				.Where(x => originName == "Alles" ? (x.Amount != 0) :
					(isPositiveAmounts ? (x.Amount > 0) :
						(x.Amount < 0 && (originName == "Onverwacht" ? x.Origin is null : x.Origin == originName))))
				.Select(x => x.TallyName)
				.OrderBy(x => x)
				.Distinct()
				.ToList();
		}

		private List<(string Month, string Tally, decimal SumAmount)> GetPivot(bool isPositiveAmounts, string originName)
		{

			List<(string Month, string Tally, decimal SumAmount)> result = Accounts
				.Where(x => originName == "Alles" ? (x.Amount != 0) :
					(isPositiveAmounts ? (x.Amount > 0) :
						(x.Amount < 0 && (originName == "Onverwacht" ? x.Origin is null : x.Origin == originName))))
				.OrderByDescending(x => (x.Date, x.TallyName))
				.GroupBy(x => (x.Month, x.TallyName),
				(key, pivot) =>
				{
					return (Month: key.Month,
					TallyName: key.TallyName,
					SumAmount: pivot.Sum(x => x.Amount)
					);
				}).ToList();

			return result;

		}


		/// <summary>
		/// Create a pivot table from scratch.
		/// </summary>
		private void CreatePivotTable()
		{

			if (Data.Tables.Count > 0) { Data.Tables.Clear(); };

			DataTable pivot = new DataTable("Pivot");
			DataColumn column = pivot.Columns.Add("Month", typeof(string));
			pivot.Columns[0].AllowDBNull = false;

			int count = 0;
			foreach (string col in Tallies)
			{
				pivot.Columns.Add(col, typeof(decimal));
				count++;
				pivot.Columns[count].AllowDBNull = true;
			};

			Data.Tables.Add(pivot);

		}

		private void FillDataGrid()
		{

			string originName = "Alles";
			List<(string Month, string Tally, decimal SumAmount)> Pivot = new List<(string Month, string Tally, decimal SumAmount)>();
			Pivot = GetPivot(false, originName);

			DataRow row = null;
			string month = string.Empty;
			for (int i = 0; i < Pivot.Count - 1; i++)
			{
				if (month != Pivot[i].Month)
				{
					if (!string.IsNullOrEmpty(month) || row != null)
					{
						Data.Tables[0].Rows.Add(row);
					}
					row = Data.Tables[0].NewRow();
					month = Pivot[i].Month;
					row["Month"] = month;
				}

				row[Pivot[i].Tally] = Pivot[i].SumAmount;
			}

			Data.Tables[0].Rows.Add(row);

		}

		private void PivotDataGrid_Loaded(object sender, RoutedEventArgs e)
		{
			for (int i = 1; i < PivotDataGrid.Columns.Count; i++)
			{
//				PivotDataGrid.Columns[i].MinWidth = 70;
				//                PivotDataGrid.Columns[i].Width = 10;
				//              PivotDataGrid.Columns[i].Header = 70;
			}

		}
	}
}
