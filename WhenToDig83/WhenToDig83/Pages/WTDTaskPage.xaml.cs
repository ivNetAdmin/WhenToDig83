
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class WTDTaskPage : ContentPage
    {
        public WTDTaskPage()
        {
            InitializeComponent();
            this.BindingContext = new WTDTaskViewModel();
        }
    }
}
