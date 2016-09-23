
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class WTDTaskPage : ContentPage
    {
        public WTDTaskPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);
            //this.BindingContext = new WTDTaskViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {          
            base.OnAppearing();
        }       
    }
}