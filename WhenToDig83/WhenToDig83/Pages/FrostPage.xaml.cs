using WhenToDig83.ViewModels;
using Xamarin.Forms;

using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class FrostPage : ContentPage
    {
        public FrostPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);    
        }
    }
}
