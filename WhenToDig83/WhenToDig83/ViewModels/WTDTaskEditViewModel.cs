using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    public class WTDTaskEditViewModel : INotifyPropertyChanged, IPageLifeCycleEvents
    {
        private INavigation _navigation;
        private WTDTaskManager _wtdTaskManager;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public WTDTaskEditViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Date = DateTime.Now;

            _wtdTaskManager = new WTDTaskManager();

            //var tasks = wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);

            //WTDTasks = new ObservableCollection<WTDTask>(tasks);
        }
        
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
        
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged();
            }
        }
        
        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                RaisePropertyChanged();
            }
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
        public ICommand Cancel
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PopModalAsync();
                });
            }
        }
        
        public ICommand Save
        {
            get
            {
                // return new Command((nothing) =>
                //{
                //    _wtdTaskManager.AddTask(Name, Date, Notes);

                //    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - 1]);
                //});

                return new Command(async () =>
                {
                    
                    await _navigation.PopModalAsync();
                });
            }
        }
        #endregion
    }
}
