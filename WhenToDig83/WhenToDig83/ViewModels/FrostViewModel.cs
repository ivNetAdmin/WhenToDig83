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

        private ObservableCollection<Frost> _lastFrostDates;
        public ObservableCollection<Frost> LastFrostDates
        {
            get { return _lastFrostDates; }
            set { _lastFrostDates = value; OnPropertyChanged(); }
        }

        private System.Collections.ObjectModel.ObservableCollection<Frost> _nextFrostDates;
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
                    _frostManager.Add();
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
            var dates = await _frostManager.GetDates();

            LastFrostDates = new ObservableCollection<Frost>(dates);

            
            NextFrostDates = new ObservableCollection<Frost>(dates);
        }
        #endregion
    }
}
