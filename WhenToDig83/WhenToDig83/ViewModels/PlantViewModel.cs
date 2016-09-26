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
                            await _navigation.PushModalAsync(new WTDTaskPage());
                            break;                        
                    }
                   
                });
            }
        }
        #endregion
    }
}
