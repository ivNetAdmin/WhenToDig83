﻿
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class ReviewPage : ContentPage
    {
        public ReviewPage()
        {
            InitializeComponent();
            ((BaseModel)BindingContext).Initialise(this);      
        }
    }
}
