using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Done.Models;

namespace To_Done.ModelViews
{
	public class MainModelView
	{
		public MainWindow MainV;

		public ObservableCollection<ToDone> ToDones = new ObservableCollection<ToDone>();

		public string Path { get; set; } = Options.ToDoFileName;

		public MainModelView()
		{
			MainV = new MainWindow(this);

			MainV.Show();

		}

		private void SaveToDone()
		{
		}
	}


}
