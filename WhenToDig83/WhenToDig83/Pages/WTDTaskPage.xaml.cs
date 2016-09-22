
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
            this.BindingContext = new WTDTaskViewModel();
        }
    }
}
