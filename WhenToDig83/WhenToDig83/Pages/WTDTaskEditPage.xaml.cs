using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class WTDTaskEditPage : ContentPage
    {
        public WTDTaskEditPage()
        {
            InitializeComponent();
            this.BindingContext = new WTDTaskEditViewModel(this.Navigation);
        }
        
        protected override void OnAppearing()
        {
            Context.OnAppearing();
            base.OnAppearing();
        }

        private IPageLifeCycleEvents Context
        {
            get { return (IPageLifeCycleEvents)BindingContext; }
        }
    }
}
