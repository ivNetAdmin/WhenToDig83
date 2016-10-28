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

            MessagingCenter.Subscribe<PlantEditViewModel>(this, "PlantChanged", (message) =>
            {
                GetPlants();
            });

            MessagingCenter.Subscribe<PlantEditViewModel>(this, "PlantUnchanged", (message) =>
            {
                SelectedItem = null;
            });            
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

                    if (value == null) return;
                    _navigation.PushModalAsync(new PlantEditPage());
                    MessagingCenter.Send(this, "EditPlant", value);                    
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

        protected override void CurrentPageOnDisappearing(object sender, EventArgs eventArgs)
        {
            try
            {
                MessagingCenter.Unsubscribe<PlantEditViewModel>(this, "PlantChanged");
                MessagingCenter.Unsubscribe<PlantEditViewModel>(this, "PlantUnchanged");      
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
                    await _navigation.PushModalAsync(new PlantEditPage());
                });
            }
        }
        #endregion

        #region Navigation Events

        public ImageSource TaskIcon { get { return ImageSource.FromFile("ic_task_white_48dp.png"); } }
        public ImageSource PlantIcon { get { return ImageSource.FromFile("ic_plant_teal_48dp.png"); } }
        public ImageSource ReviewIcon { get { return ImageSource.FromFile("ic_review_white_48dp.png"); } }
        public ImageSource FrostIcon { get { return ImageSource.FromFile("ic_frost_white_48dp.png"); } }

        public ICommand ToolbarNavigationCommand
        {
            get
            {
                return new Command<string>(async (string paramter) =>
                {
                    switch (paramter)
                    {
                        case "task":
                            await _navigation.PushAsync(new WTDTaskPage());
                            break;
                        case "review":
                            await _navigation.PushAsync(new ReviewPage());
                            break;
                        case "frost":
                            await _navigation.PushAsync(new FrostPage());
                            break;
                    }

                });
            }
        }
        #endregion

        #region Private
        private async void GetPlants()
        {
            if (Plants != null) Plants.Clear();

            var plants = await _plantManager.GetPlants();
            Plants = new ObservableCollection<Plant>(plants);
        }
        #endregion
    }
}
