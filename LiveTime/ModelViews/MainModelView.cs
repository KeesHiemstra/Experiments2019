using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LiveTime.ModelViews
{
  public class MainModelView : INotifyPropertyChanged
  {
    private string currentTime;

    public MainWindow View;

    public string CurrentTime
    {
      get => currentTime;
      set
      {
        if (value != currentTime)
        {
          currentTime = value;
          NotifyPropertyChanged();
        }
      }
    }

    // This take a lot resources
    public void UpdateTime()
    {
      Task.Run(() =>
       {
         CurrentTime = DateTime.Now.ToString("HH:mm:ss");
         Task.Delay(1000);
         UpdateTime();
       });
    }

    // This takes less resources
    private void timer_Tick(object sender, EventArgs e)
    {
      CurrentTime = DateTime.Now.ToString("HH:mm:ss");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainModelView(MainWindow mainWindow)
    {
      View = mainWindow;

      //UpdateTime();

      DispatcherTimer timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromMilliseconds(500);
      timer.Tick += new EventHandler(timer_Tick);
      timer.Start();

    }


  }
}
