
using System;
using System.Collections.ObjectModel;
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
        private WTDTaskManager _wtdTaskManager;        

        public WTDTaskViewModel()
        {
            _wtdTaskManager = new WTDTaskManager();            
        }

        #region Properties
        private string _responseText;
        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                if (_responseText != value)
                {
                    _responseText = value;
                    OnPropertyChanged();
                }
            }
        }

        private WTDTask _selectedItem;
        public WTDTask SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();

                    //MessagingCenter.Send(this, "EditTask", value);
                    _navigation.PushModalAsync(new WTDTaskEditPage());
                }
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

                MessagingCenter.Subscribe<WTDTaskEditViewModel>(this, "TasksChanged", (message) => {
                    GetTasks();
                });

                GetTasks();                
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }

        protected override void CurrentPageOnDisappearing(object sender, EventArgs eventArgs)
        {
            try
            {
                MessagingCenter.Unsubscribe<WTDTaskEditViewModel>(this, "TasksChanged");
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

        #region Private

        private async void GetTasks()
        {
            // _wtdTaskManager.AddTask("zozo", DateTime.Now, "");

            var tasks = await _wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);
            WTDTasks = new ObservableCollection<WTDTask>(tasks);
        }
        #endregion

    }
}
