
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class PlantPage : ContentPage
    {
        public PlantPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);

        }
    }
}
