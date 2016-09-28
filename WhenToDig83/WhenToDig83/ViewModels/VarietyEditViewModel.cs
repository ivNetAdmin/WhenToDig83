
using System;
using System.Windows.Input;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class VarietyEditViewModel : BaseModel
    {
        private INavigation _navigation;
        private PlantManager _plantManager;

        public VarietyEditViewModel()
        {
            _plantManager = new PlantManager();

            //MessagingCenter.Subscribe<PlantEditViewModel>(this, "PlantChanged", (message) => {
            //    GetPlants();
            //});
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;
                //GetPlants();
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }

        //protected override void CurrentPageOnDisappearing(object sender, EventArgs eventArgs)
        //{
        //    try
        //    {
        //        MessagingCenter.Unsubscribe<PlantEditViewModel>(this, "PlantChanged");
        //    }
        //    catch (Exception exception)
        //    {
        //        ResponseText = exception.ToString();
        //    }
        //}
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
                return new Command(async () =>
                {
                  //  _plantManager.AddVariety(Name, Notes, _selectedPlant == null ? 0 : _selectedPlant.ID);
                  //  MessagingCenter.Send(this, "VarietyChanged");
                    await _navigation.PopModalAsync();
                });
            }
        }
        #endregion
    }
}
