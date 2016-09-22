
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WhenToDig83.Core.Entities;
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

            WTDTasks = new ObservableCollection<WTDTask>(tasks);
        }

        private ObservableCollection<WTDTask> _wtdTasks;
        public ObservableCollection<WTDTask> WTDTasks
        {
            get { return _wtdTasks; }
            set { _wtdTasks = value; RaisePropertyChanged(); }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
