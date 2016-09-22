
using System;
using System.ComponentModel;
using WhenToDig83.Managers;

namespace WhenToDig83.ViewModels
{
    public class WTDTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WTDTaskViewModel()
        {
            var wtdTaskManager = new WTDTaskManager();

            var tasks = wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);
        }
    }
}
