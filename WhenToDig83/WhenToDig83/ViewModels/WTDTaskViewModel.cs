﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    public class WTDTaskViewModel : INotifyPropertyChanged, IPageLifeCycleEvents
    {
        private INavigation _navigation;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public WTDTaskViewModel(INavigation navigation)
        {
            _navigation = navigation;
            
            Task.Run(() => Init());
        }

        #region Properties
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
        #endregion

        #region Page Events
        public void OnAppearing()
        {

        }
        #endregion

        #region Events
        public ICommand New
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushModalAsync(new WTDTaskEditPage());
                });
            }
        }
        #endregion
        
        public async Task Init()
        {
            
            var wtdTaskManager = new WTDTaskManager();

            wtdTaskManager.AddTask("Hello mum", DateTime.Now, "Cultivate");

            var tasks = wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);

            WTDTasks = await new ObservableCollection<WTDTask>(tasks);
            
        }
    }
}
