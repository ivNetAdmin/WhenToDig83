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
    internal class PlantViewModel : BaseModel
    {
        private INavigation _navigation;
        private PlantManager _plantManager;
    
        public PlantViewModel()
        {
            _plantManager = new PlantManager();
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
        
        private Plant _selectedItem;
        public Plant SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    
                    //_navigation.PushModalAsync(new WTDTaskEditPage());
                    // MessagingCenter.Send(this, "EditTask", value);
                }
            }
        }

        private ObservableCollection<Plant> _plants;
        public ObservableCollection<Plant> Plants
        {
            get { return _plants; }
            set { _plants = value; OnPropertyChanged(); }
        }
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation; 
                GetPlants();  
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
                    }
                   
                });
            }
        }
        #endregion
        
        #region Private
        private async void GetPlants()
        {
            _plantManager.AddPlant("Carrots");
            var plants = await _plantManager.GetPlants();
            Plants = new ObservableCollection<Plant>(plants);
        }
        #endregion
    }
}
