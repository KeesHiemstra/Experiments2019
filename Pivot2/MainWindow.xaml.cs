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

			//DataContext = this;
			//PivotDataGrid.DataContext = Table;

			LoadAccounts();
			BuildTable();
			FillDataGrid();

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

		private void BuildTable()
		{
			Tallies = Accounts
				.Where(x => (x.Amount < 0 && x.Origin == "Gezamenlijk"))
				.Select(x => x.TallyName)
				.OrderBy(x => x)
				.Distinct()
				.ToList();

			DataTable pivot = new DataTable("Pivot");
			DataColumn column = pivot.Columns.Add("Month", typeof(string));
			column.AllowDBNull = false;

			int count = 0;
			foreach (string col in Tallies)
			{
				pivot.Columns.Add(col, typeof(decimal));
				count++;
				pivot.Columns[count].AllowDBNull = true;
			};

			foreach (var month in Accounts
				.Where(x => (x.Amount < 0 && x.Origin == "Gezamenlijk"))
				.OrderByDescending(x => x.Month)
				.Select(x => x.Month)
				.Distinct()
				.ToList()
				)
			{
				DataRow row;
				row = pivot.NewRow();
				row["Month"] = month;

				count = 0;
				foreach (string col in Tallies)
				{
					row[col] = count;
					count++;
				}
				pivot.Rows.Add(row);
			}

			Data.Tables.Add(pivot);

		}

		private void FillDataGrid()
		{
		}
	}
}
