using System;
using System.Collections.Generic;
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
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;              
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
    }
}
