﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using To_Done.ModelViews;

namespace To_Done
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainModelView MainVM;

		public MainWindow(MainModelView mainVM)
		{
			MainVM = mainVM;

			InitializeComponent();
			Title = $"To-Done ({Assembly.GetExecutingAssembly().GetName().Version})";

			DataContext = MainVM;
		}
	}
}
