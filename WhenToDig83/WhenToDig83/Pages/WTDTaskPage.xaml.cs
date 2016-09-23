
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
        }           
    }
}