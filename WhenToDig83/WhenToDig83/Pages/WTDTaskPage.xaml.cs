
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class WTDTaskPage : ContentPage
    {
        public WTDTaskPage()
        {
            InitializeComponent();
            this.BindingContext = new WTDTaskViewModel(this.Navigation);
        }
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
