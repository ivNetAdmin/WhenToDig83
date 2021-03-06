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
        private List<DateTime> _dates;
        private int _rowCount;
        private Dictionary<DateTime, List<int>> _taskDates;

        public WTDTaskViewModel()
        {
            _wtdTaskManager = new WTDTaskManager();
            _currentCalendarDate = DateTime.Now;
            _dates = new List<DateTime>();
            _taskDates = new Dictionary<DateTime, List<int>>();

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

        private bool _isAddMonthTasksButtonVisible;
        public bool IsAddMonthTasksButtonVisible
        {
            get { return _isAddMonthTasksButtonVisible; }
            set
            {
                _isAddMonthTasksButtonVisible = value;
                OnPropertyChanged();
            }
        }
       
        public string AddMonthTaskText
        {
            get { return string.Format("Add to {0}", DateTime.Now.Year); }            
        }
        
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {
                _navigation = AppHelper.CurrentPage().Navigation;
                IsAddMonthTasksButtonVisible = false;
                GetTasks();
                
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

        public ICommand AddMonthTasks
        {
            get
            {
                return new Command(async () =>
                {                
                    GetDates();
                    var tasks = await _wtdTaskManager.GetTasksByDateRange(_dates[0], _dates[_dates.Count - 1]);
                    foreach(var task in tasks)
                    {
                        _wtdTaskManager.AddTask(task.Name, GetNewDate(task.Date), task.TypeId, string.Empty, 0);
                    }

                    _currentCalendarDate = _currentCalendarDate.AddYears(1);
                    GetTasks();
                });
            }
        }
      
        #endregion

        #region Navigation Events

        public ImageSource TaskIcon { get { return ImageSource.FromFile("ic_task_teal_48dp.png"); } }
        public ImageSource PlantIcon { get { return ImageSource.FromFile("ic_plant_white_48dp.png"); } }
        public ImageSource ReviewIcon { get { return ImageSource.FromFile("ic_review_white_48dp.png"); } }
        public ImageSource FrostIcon { get { return ImageSource.FromFile("ic_frost_white_48dp.png"); } }

        public ICommand ToolbarNavigationCommand
        {
            get
            {
                return new Command<string>(async (string parameter) =>
                {
                    switch(parameter)
                    {                        
                        case "plant":
                            await _navigation.PushAsync(new PlantPage());
                            break;
                        case "review":
                            await _navigation.PushAsync(new ReviewPage());
                            break;
                        case "frost":
                            await _navigation.PushAsync(new FrostPage());
                            break;
                    }
                   
                });
            }
        }
        #endregion

        #region  Calendar Events
        public ICommand CalendarChange
        {
            get
            {
                return new Command<string>((string parameter) =>
                {
                    IsAddMonthTasksButtonVisible = false;
                    switch (parameter)
                    {
                        case "NextMonth":
                            _currentCalendarDate = _currentCalendarDate.AddMonths(1);
                            break;
                        case "NextYear":
                            _currentCalendarDate = _currentCalendarDate.AddYears(1);
                            break;
                        case "LastMonth":
                            _currentCalendarDate = _currentCalendarDate.AddMonths(-1);
                            break;
                        case "LastYear":
                            _currentCalendarDate = _currentCalendarDate.AddYears(-1);
                            break;
                    }

                    if (_currentCalendarDate.Year < DateTime.Now.Year)
                    {
                        IsAddMonthTasksButtonVisible = true;
                    }

                    GetTasks();
                    
                });              
            }
        }
        #endregion

        #region Private
        private void GetDates()
        {
            _dates.Clear();

            DisplayCalendarDate = _currentCalendarDate.ToString("MMM yyyy");

            var month = _currentCalendarDate.ToString("MMM yyyy");
            var fill = new int[] { 6, 0, 1, 2, 3, 4, 5 };

            var firstDayofMonth = new DateTime(_currentCalendarDate.Year, _currentCalendarDate.Month, 1).DayOfWeek;
            var calendarStartDate = new DateTime(_currentCalendarDate.Year, _currentCalendarDate.Month, 1)
                .AddDays(-1 * (fill[(int)firstDayofMonth]));

            var days = DateTime.DaysInMonth(_currentCalendarDate.Year, _currentCalendarDate.Month);

            _rowCount = days + fill[(int)firstDayofMonth] > 35 ? 6 : 5;
           
            for (var d = 0; d < _rowCount * 7; d++)
            {
                _dates.Add(calendarStartDate.AddDays(d));
            }
        }

        private async void GetTasks()
        {
            if (WTDTasks != null) WTDTasks.Clear();

            GetDates();
            var tasks = await _wtdTaskManager.GetTasksByDateRange(_dates[0], _dates[_dates.Count - 1]);

            foreach(WTDTask task in tasks)
            {
                if (!_taskDates.ContainsKey(task.Date))
                {
                    _taskDates.Add(task.Date, new List<int> { task.TypeId });
                }
                else
                {
                    var taskTypes = _taskDates[task.Date];
                    if(!taskTypes.Contains(task.TypeId))
                    {
                        taskTypes.Add(task.TypeId);
                    }
                }
            }

            WTDTasks = new ObservableCollection<WTDTask>(tasks);
            ShowCalendar();
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

            for (var r = 0; r < _rowCount; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = 24 });
                BuildCalendarCells(grid, _dates, r);
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
                var jobImage = GetJobImage((DateTime)dates[dateIndex]);

                lowlight = SetLowLisght(lowlight, dateStr);

                var relativeLayout = new RelativeLayout
                {
                    BackgroundColor = Color.Black
                };

                var backgroundImage = new Image()
                {
                    Source = jobImage,
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

                label.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OnLabelClicked(dates[dateIndex])),
                });


                grid.Children.Add(relativeLayout, wd, r);
            }
        }

        private async void OnLabelClicked(DateTime date)
        {
            await _navigation.PushModalAsync(new WTDTaskEditPage());
            MessagingCenter.Send(this, "EditTask", new WTDTask { Date = date });
        }

        private FileImageSource GetJobImage(DateTime date)
        {
            var image = "tasktype.png";

            if (_taskDates.ContainsKey(date))
            {
                _taskDates[date].Sort();
                image = string.Empty;

                foreach (int taskType in _taskDates[date])
                {
                    image = string.Format("{0}{1}", image, taskType);
                }

                image = string.Format("tt{0}.png", image);
            }

            return new FileImageSource() { File = image };
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

        private DateTime GetNewDate(DateTime date)
        {
            var dow = date.DayOfWeek;
            var newDate = new DateTime(DateTime.Now.Year, date.Month, date.Day);
            return AdjustDate(newDate, dow);
        }

        private DateTime AdjustDate(DateTime newDate, DayOfWeek dow)
        {
            // find closest date to day of week
            var countForward = 0;
            var countBackward = 0;
            var adjustDate = newDate;

            for(countForward = 0; countForward<8; countForward++)
            {
                if (newDate.DayOfWeek == dow) break;
                newDate = newDate.AddDays(1);
            }

            newDate = adjustDate;

            for (countBackward = 0; countBackward < 8; countBackward++)
            {
                if (newDate.DayOfWeek == dow) break;
                newDate = newDate.AddDays(-1);
            }

            if(countForward<= countBackward)
            {
                return adjustDate.AddDays(countForward);
            }else
            {
                return adjustDate.AddDays(countBackward * -1);
            }            
        }

        #endregion
    }
}
