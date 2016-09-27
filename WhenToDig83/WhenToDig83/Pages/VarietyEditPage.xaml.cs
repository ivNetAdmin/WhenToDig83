
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class VarietyEditPage : ContentPage
    {
        public VarietyEditPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);
        }
    }
}
