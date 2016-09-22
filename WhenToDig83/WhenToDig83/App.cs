using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhenToDig83.Pages;
using Xamarin.Forms;

namespace WhenToDig83
{
    public class App : Application
    {
        public App()
        {

         //   var wtdTaskManager = new WTDTaskManager();

          //  wtdTaskManager.AddTask("Test", DateTime.Now, "Plant");

           // var list = wtdTaskManager.GetTasks();

           // var task = wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);

            // The root page of your application
            MainPage = new TaskPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
