
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class WTDTaskViewModel : BaseModel
    {
        private INavigation _navigation;

        public WTDTaskViewModel()
        {
        }

        #region Properties
        private string _responseText;
        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<WTDTask> _wtdTasks;
        public ObservableCollection<WTDTask> WTDTasks
        {
            get { return _wtdTasks; }
            set { _wtdTasks = value; OnPropertyChanged(); }
        }
        #endregion

        #region Page Events
        protected override async void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;

                var wtdTaskManager = new WTDTaskManager();

                wtdTaskManager.AddTask("Hello mum", DateTime.Now, "Cultivate");

                var tasks = await wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);

                WTDTasks = new ObservableCollection<WTDTask>(tasks);
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
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

    }
}
