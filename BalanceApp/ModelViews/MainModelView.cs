using BalanceApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BalanceApp.ModelViews
{
	public class MainModelView : INotifyPropertyChanged
	{
		public bool ToSaveBalances { get; set; } = false;

		public ObservableCollection<Balance> Balances { get; set; } =
			new ObservableCollection<Balance>();

		public ObservableCollection<CurrentBalance> CurrentBalances { get; set; } =
			new ObservableCollection<CurrentBalance>();

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

		public MainModelView()
		{

			//Balances.Add(new Balance
			//{
			//	Name = "NL87INGB 000 3992526"
			//});
			//Balances[0].Amounts.Add(new BalanceAmount { Date = new DateTime(2019, 03, 01), Amount = 2382.96m });
			//Balances[0].Amounts.Add(new BalanceAmount { Date = new DateTime(2019, 05, 01), Amount = 2291.00m });
			//Balances[0].Amounts.Add(new BalanceAmount { Date = new DateTime(2019, 06, 01), Amount = 4223.30m });
			//
			//Balances.Add(new Balance
			//{
			//	Name = "NC 78 INGB 0005 0565 84"
			//});
			//Balances[1].Amounts.Add(new BalanceAmount { Date = new DateTime(2019, 03, 01), Amount = 112.78m});

			//SaveBalances();

			OpenBalances();
			GetCurrentBalances();

		}

		public void GetCurrentBalances()
		{

			CurrentBalances.Clear();

			foreach (Balance balance in Balances)
			{
				List<BalanceAmount> current = balance.Amounts
					.OrderByDescending(x => x.Date)
					.ToList();

				decimal lastAmount = current[0].Amount;
				for (int i = 1; i < current.Count; i++)
				{
					current[i - 1].Difference = lastAmount - current[i].Amount;
					lastAmount = current[i].Amount;
				}

				CurrentBalances.Add(new CurrentBalance
				{
					Name = balance.Name,
					Date = current[0].Date,
					Amount = current[0].Amount,
					Diffence = current[0].Difference
				});
			}

		}

		private void OpenBalances()
		{

			using (StreamReader stream = File.OpenText(".\\Balance.json"))
			{
				string json = stream.ReadToEnd();
				Balances = JsonConvert.DeserializeObject< ObservableCollection<Balance>>(json);
			}

		}

		public void SaveBalances()
		{

			string json = JsonConvert.SerializeObject(Balances, Formatting.Indented);
			using (StreamWriter stream = new StreamWriter(".\\Balance.json"))
			{
				stream.Write(json);
			}

		}

	}
}
