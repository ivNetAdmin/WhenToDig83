using System;
using System.Windows.Input;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;
using WhenToDig83.Core.Entities;
using System.Collections.ObjectModel;

namespace WhenToDig83.ViewModels
{
    internal class FrostViewModel : BaseModel
    {
        private INavigation _navigation;
        private FrostManager _frostManager;

        public FrostViewModel()
        {
            _frostManager = new FrostManager();
            Date = DateTime.Now;
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

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Frost> _lastFrostDates;
        public ObservableCollection<Frost> LastFrostDates
        {
            get { return _lastFrostDates; }
            set { _lastFrostDates = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Frost> _nextFrostDates;
        public ObservableCollection<Frost> NextFrostDates
        {
            get { return _nextFrostDates; }
            set { _nextFrostDates = value; OnPropertyChanged(); }
        }

        #endregion

        #region Events
        public ICommand New
        {
            get
            {
                return new Command(() =>
                {
                    _frostManager.Add(_date);
                    GetFrostDates();
                });
            }
        }
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;
                GetFrostDates();             
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }
        #endregion

        #region Navigation Events

        public ImageSource TaskIcon { get { return ImageSource.FromFile("ic_task_white_48dp.png"); } }
        public ImageSource PlantIcon { get { return ImageSource.FromFile("ic_plant_white_48dp.png"); } }
        public ImageSource ReviewIcon { get { return ImageSource.FromFile("ic_review_white_48dp.png"); } }
        public ImageSource FrostIcon { get { return ImageSource.FromFile("ic_frost_teal_48dp.png"); } }

        public ICommand ToolbarNavigationCommand
        {
            get
            {
                return new Command<string>(async (string paramter) =>
                {
                    switch(paramter)
                    {         
                     case "task":
                            await _navigation.PushAsync(new WTDTaskPage());
                            break;
                        case "plant":
                            await _navigation.PushAsync(new PlantPage());
                            break;
                        case "review":
                            await _navigation.PushAsync(new ReviewPage());
                            break;                       
                    }
                   
                });
            }
        }
        #endregion

    
        #region Private
        private async void GetFrostDates()
        {
            var allDates = await _frostManager.GetAllDates();
            var allDateCount = allDates.Count;
            if (allDateCount == 0) allDateCount = 1;

            var dates = await _frostManager.GetLastDates();
            SetChancePercent(dates, allDateCount);            
            LastFrostDates = new ObservableCollection<Frost>(dates);

            dates = await _frostManager.GetNextDates();
            SetChancePercent(dates, allDateCount);
            NextFrostDates = new ObservableCollection<Frost>(dates);
        }

        private void SetChancePercent(System.Collections.Generic.List<Frost> dates, int allDateCount)
        {
            foreach (var date in dates)
            {
                var count = ((Frost)date).Count;
                ((Frost)date).Count = (count / allDateCount) * 100;
            }
        }
        #endregion
    }
}
