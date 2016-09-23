
using Xamarin.Forms;

namespace WhenToDig83.Helpers
{
    public static class AppHelper
    {
        public static Page CurrentPage()
        {
            var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;

            return navigationStack.Count == 0
                ? Application.Current.MainPage
                : navigationStack[
                    navigationStack.Count - 1];
        }
    }
}
