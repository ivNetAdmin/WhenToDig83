using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class WTDTaskEditPage : ContentPage
    {

        public WTDTaskEditPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);
        }
    }
}
