using BalanceApp.ModelViews;
using BalanceApp.Views;
using System;
using System.Collections.Generic;
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

namespace BalanceApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainModelView MainMV { get; set; }// = new MainModelView();

		public MainWindow()
		{
			InitializeComponent();

			try
			{
				MainMV = new MainModelView();
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//MainMV.SaveBalances();

		}

		private void BalancesButton_Click(object sender, RoutedEventArgs e)
		{
			BalanceWindow balanceWindow = new BalanceWindow(MainMV);
			balanceWindow.ShowDialog();
		}
	}
}
