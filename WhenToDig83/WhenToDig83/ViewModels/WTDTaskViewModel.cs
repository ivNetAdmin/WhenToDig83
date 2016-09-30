﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;
using static Xamarin.Forms.Grid;

namespace WhenToDig83.ViewModels
{
    internal class WTDTaskViewModel : BaseModel
    {
        private INavigation _navigation;
        private WTDTaskManager _wtdTaskManager;
        private DateTime _currentCalendarDate;

        public WTDTaskViewModel()
        {
            _wtdTaskManager = new WTDTaskManager();
            _currentCalendarDate = DateTime.Now;
           DisplayCalendarDate = _currentCalendarDate.ToString("MMM yyyy");

            MessagingCenter.Subscribe<WTDTaskEditViewModel>(this, "TaskChanged", (message) => {
                GetTasks();
            });
            MessagingCenter.Subscribe<WTDTaskEditViewModel>(this, "TaskUnchanged", (message) => {
                SelectedItem=null;
            });
        }

        #region Properties
        private string _responseText;
        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                if (_responseText != value)
                {
                    _responseText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _displayCalendarDate;
        public string DisplayCalendarDate
        {
            get { return _displayCalendarDate; }
            set
            {
                if (_displayCalendarDate != value)
                {
                    _displayCalendarDate = value;
                    OnPropertyChanged();
                }
            }
        }
         

        private WTDTask _selectedItem;
        public WTDTask SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();

                    if (value == null) return;
                    _navigation.PushModalAsync(new WTDTaskEditPage());
                     MessagingCenter.Send(this, "EditTask", value);                   
                }
            }
        }

        private ObservableCollection<WTDTask> _wtdTasks;
        public ObservableCollection<WTDTask> WTDTasks
        {
            get { return _wtdTasks; }
            set { _wtdTasks = value; OnPropertyChanged(); }
        }

        private IGridList<View> _calendarGridChildren;
        public IGridList<View> CalendarGridChildren
        {
            get { return _calendarGridChildren; }
            set { _calendarGridChildren = value; OnPropertyChanged(); }
        }
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;
                GetTasks();
                ShowCalendar();   
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }       

        protected override void CurrentPageOnDisappearing(object sender, EventArgs eventArgs)
        {
            try
            {
                MessagingCenter.Unsubscribe<WTDTaskEditViewModel>(this, "TaskChanged");
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }
        #endregion

        #region Events
        public ICommand New
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushModalAsync(new WTDTaskEditPage());
                });
            }
        }
        #endregion

        #region Navigation Events
        public ICommand ToolbarNavigationCommand
        {
            get
            {
                return new Command<string>(async (string paramter) =>
                {
                    switch(paramter)
                    {                        
                        case "plant":
                            await _navigation.PushAsync(new PlantPage());
                            break;
                    }
                   
                });
            }
        }
        #endregion

        #region Private
        private async void GetTasks()
        {
            // _wtdTaskManager.AddTask("zozo", DateTime.Now, "");

            var tasks = await _wtdTaskManager.GetTasksByMonth(DateTime.Now.Month, DateTime.Now.Year);
            WTDTasks = new ObservableCollection<WTDTask>(tasks);
        }

        private void ShowCalendar()
        {
            var cp = (ContentPage)AppHelper.CurrentPage();
            var calendarGridHolder = cp.FindByName<StackLayout>("CalendarGridHolder");

            var calendarGrid = new Grid();

            calendarGrid = BuildCalendar();

            calendarGridHolder.Children.Clear();
            calendarGridHolder.Children.Add(calendarGrid);

        }

        private Grid BuildCalendar()
        {

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };
                        
            var month = _currentCalendarDate.ToString("MMM yyyy");
            var fill = new int[] { 6, 0, 1, 2, 3, 4, 5 };

            var firstDayofMonth = new DateTime(_currentCalendarDate.Year, _currentCalendarDate.Month, 1).DayOfWeek;
            var calendarStartDate = new DateTime(_currentCalendarDate.Year, _currentCalendarDate.Month, 1)
                .AddDays(-1 * (fill[(int)firstDayofMonth]));

            var days = DateTime.DaysInMonth(_currentCalendarDate.Year, _currentCalendarDate.Month);

            var rowCount = days + fill[(int)firstDayofMonth] > 35 ? 6 : 5;

            var dates = new List<DateTime>();
            for (var d = 0; d < rowCount * 7; d++)
            {
                dates.Add(calendarStartDate.AddDays(d));
            }

            for (var r = 0; r < rowCount; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                BuildCalendarCells(grid, dates, r);
            }

            return grid;
        }

        private void BuildCalendarCells(Grid grid, List<DateTime> dates, int r)
        {
            var weekDays = new[] { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };

            bool lowlight = r == 0 ? true : false;

            for (var wd = 0; wd < weekDays.Length; wd++)
            {
                var dateIndex = (r) * weekDays.Length + wd;
                var dateStr = dateIndex < dates.Count ? Convert.ToString(dates[dateIndex].Day.ToString("D2")) : string.Empty;
                var today = ((DateTime)dates[dateIndex]).ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy");

                lowlight = SetLowLisght(lowlight, dateStr);

                var relativeLayout = new RelativeLayout
                {
                    BackgroundColor = Color.Black
                };

                var backgroundImage = new Image()
                {
                    //  Source = jobImage,
                    IsOpaque = true,
                    Opacity = 1.0,
                };

                var label = new Label
                {
                    Text = dateStr,
                    TextColor = lowlight == true ? Color.FromRgb(51, 51, 51) : today ? Color.Aqua : Color.Silver,
                    BackgroundColor = Color.Black
                };

                relativeLayout.Children.Add(
                    backgroundImage,
                    Constraint.Constant(0),
                    Constraint.Constant(0)
                );
                relativeLayout.Children.Add(

                label, Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width / 3;
                }),
                     Constraint.Constant(0));

                grid.Children.Add(relativeLayout, wd, r);
            }
        }

        private bool SetLowLisght(bool lowlight, string dateStr)
        {
            if (dateStr == "01")
            {
                if (lowlight == true)
                {
                     return false;
                }
                else
                {
                    return true;
                }
            }

            return lowlight;
        }
        #endregion
    }
}
