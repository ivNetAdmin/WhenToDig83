using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class PlantEditPage : ContentPage
    {
        public PlantEditPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);
           
        }
    }
}
