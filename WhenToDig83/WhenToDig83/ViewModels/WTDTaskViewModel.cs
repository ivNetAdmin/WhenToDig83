
using System;
using System.Collections.ObjectModel;
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
        private WTDTaskManager _wtdTaskManager;

        public WTDTaskViewModel()
        {
            _wtdTaskManager = new WTDTaskManager();
            MessagingCenter.Subscribe<WTDTaskEditViewModel>(this, "TasksChanged", (message) => {
                GetTasks();
            });
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
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;
                GetTasks();                
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

        #region Messages             

        #endregion

        private async void GetTasks()
        {
            // _wtdTaskManager.AddTask("zozo", DateTime.Now, "");

            var tasks = await _wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);
            WTDTasks = new ObservableCollection<WTDTask>(tasks);
        }

    }
}
