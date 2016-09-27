
using System;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class VarietyViewModel : BaseModel
    {
        private INavigation _navigation;
        private PlantManager _plantManager;

        public VarietyViewModel()
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
                if (_responseText != value)
                {
                    _responseText = value;
                    OnPropertyChanged();
                }
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
    }
}
