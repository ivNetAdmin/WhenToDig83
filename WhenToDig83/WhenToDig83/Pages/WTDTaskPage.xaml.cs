
using System;
using System.Collections.Generic;
using WhenToDig83.ViewModels;
using Xamarin.Forms;

namespace WhenToDig83.Pages
{
    public partial class WTDTaskPage : ContentPage
    {

       // private string[] _weekDays = new[] { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
       // private int _rowCount;

        public WTDTaskPage()
        {         
            InitializeComponent();            
            ((BaseModel)BindingContext).Initialise(this);
        }


        protected override void OnAppearing()
        {
           // ShowCalendar();
            base.OnAppearing();
        }

        //private void ShowCalendar()
        //{
        //    var calendarGrid = this.FindByName<Grid>("CalendarGrid");
        //    calendarGrid.RowDefinitions.Clear();
        //    calendarGrid.Children.Clear();

        //    var grid = BuildCalendar();

        //    for (var r = 0; r < _rowCount; r++)
        //    {
        //        calendarGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //    }

        //    foreach (var child in grid.Children)
        //    {
        //        calendarGrid.Children.Add(child);
        //    }
        //}

        //private Grid BuildCalendar()
        //{
        //    var jobs = new List<WTDTask>(); // ((CalendarViewModel)BindingContext).Jobs;

        //    var grid = new Grid
        //    {
        //        VerticalOptions = LayoutOptions.Fill
        //    };

        //    var currentCallendarDate = DateTime.Now;  //((CalendarViewModel)BindingContext).CurrentDate;

        //    var month = currentCallendarDate.ToString("MMM yyyy");
        //    var fill = new int[] { 6, 0, 1, 2, 3, 4, 5 };

        //    var firstDayofMonth = new DateTime(currentCallendarDate.Year, currentCallendarDate.Month, 1).DayOfWeek;
        //    var calendarStartDate = new DateTime(currentCallendarDate.Year, currentCallendarDate.Month, 1)
        //        .AddDays(-1 * (fill[(int)firstDayofMonth]));

        //    var days = DateTime.DaysInMonth(currentCallendarDate.Year, currentCallendarDate.Month);

        //    _rowCount = days + fill[(int)firstDayofMonth] > 35 ? 6 : 5;

        //    var dates = new List<DateTime>();
        //    for (var d = 0; d < _rowCount * 7; d++)
        //    {
        //        dates.Add(calendarStartDate.AddDays(d));
        //    }

        //    for (var r = 0; r < _rowCount; r++)
        //    {
        //        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //        BuildCalendarCells(grid, _weekDays, dates, r, jobs);
        //    }

        //    return grid;
        //}

        //private void BuildCalendarCells(
        //    Grid grid, string[] weekDays,
        //    List<DateTime> dates, int r, IList<Job> jobs)
        //{

        //    bool lowlight = r == 0 ? true : false;
        //    for (var wd = 0; wd < weekDays.Length; wd++)
        //    {
        //        var dateIndex = (r) * weekDays.Length + wd;
        //        var dateStr = dateIndex < dates.Count ? Convert.ToString(dates[dateIndex].Day.ToString("D2")) : string.Empty;
        //        var today = ((DateTime)dates[dateIndex]).ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy");
        //        //var jobImage = GetJobImage(dates[dateIndex], jobs);

        //        if (dateStr == "01")
        //        {
        //            if (lowlight == true)
        //            {
        //                lowlight = false;
        //            }
        //            else
        //            {
        //                lowlight = true;
        //            }
        //        }

        //        var relativeLayout = new RelativeLayout
        //        {
        //            BackgroundColor = Color.Black
        //        };

        //        var backgroundImage = new Image()
        //        {
        //          //  Source = jobImage,
        //            IsOpaque = true,
        //            Opacity = 1.0,
        //        };

        //        var label = new Label
        //        {
        //            Text = dateStr,
        //            TextColor = lowlight == true ? Color.FromRgb(51, 51, 51) : today ? Color.Aqua : Color.Silver,
        //            BackgroundColor = Color.Black
        //        };

        //        relativeLayout.Children.Add(
        //            backgroundImage,
        //            Constraint.Constant(0),
        //            Constraint.Constant(0)
        //        );
        //        relativeLayout.Children.Add(

        //        label, Constraint.RelativeToParent((parent) => {
        //            return parent.Width / 3;
        //        }),
        //             Constraint.Constant(0));

        //        grid.Children.Add(relativeLayout, wd, r);

        //    }//
        //}

    }
}