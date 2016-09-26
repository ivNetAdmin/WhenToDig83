using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
