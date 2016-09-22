using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    public class WTDTaskEditViewModel : INotifyPropertyChanged, IPageLifeCycleEvents
    {
        private INavigation _navigation;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public WTDTaskEditViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Date = DateTime.Now;
            
            //var wtdTaskManager = new WTDTaskManager();

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
    }
}
