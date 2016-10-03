
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
using static Xamarin.Forms.Grid;

namespace WhenToDig83.ViewModels
{
    internal class ReviewViewModel : BaseModel
    {
        private INavigation _navigation;
        private ReviewManager _reviewManager;      
        
        public ReviewViewModel()
        {
            _reviewManager = new ReviewManager();          
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
    }
}
