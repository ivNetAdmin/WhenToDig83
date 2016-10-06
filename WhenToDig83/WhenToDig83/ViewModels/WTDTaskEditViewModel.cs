using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class WTDTaskEditViewModel : BaseModel
    {
        private INavigation _navigation;
        private WTDTaskManager _wtdTaskManager;
        private NoteManager _noteManager;
        private WTDTask _selectedTask;

        public WTDTaskEditViewModel()
        {            
            Date = DateTime.Now;

            _wtdTaskManager = new WTDTaskManager();
            _noteManager = new NoteManager();
            TaskTypes = GetTaskTypes();

            MessagingCenter.Subscribe<WTDTaskViewModel, WTDTask>(this, "EditTask", (message, args) => {
                _selectedTask = args;
                Name = _selectedTask.Name;
                Date = _selectedTask.Date;
                Type = _selectedTask.Type;

                var notesResult = _noteManager.GetNote((int)NoteType.Task, _selectedTask.ID).Result;
                Notes = notesResult == null ? string.Empty : notesResult.Notes;
            });
        }
        
        #region Properties
        private string _responseText;
        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        
        private int _type;
        public int Type
        {
            get { return _type; }
            set
            {
                var cakes = value;
                _type = 1;
                OnPropertyChanged();
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _taskTypes;
        public ObservableCollection<string> TaskTypes
        {
            get { return _taskTypes; }
            set { _taskTypes = value; OnPropertyChanged(); }
        }
        #endregion

        #region Page Events
        protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {             
                _navigation = AppHelper.CurrentPage().Navigation;
               
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
                MessagingCenter.Unsubscribe<WTDTaskViewModel, WTDTask>(this, "EditTask");
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }
        #endregion

        #region Events
        public ICommand Cancel
        {
            get
            {
                return new Command(async () =>
                {
                    MessagingCenter.Send(this, "TaskUnchanged");
                    await _navigation.PopModalAsync();
                });
            }
        }
        
        public ICommand Save
        {
            get
            {
                return new Command(async () =>
                {
                    _wtdTaskManager.AddTask(Name, Date, (int)WTDTaskType.Cultivate, Notes, _selectedTask == null ? 0 : _selectedTask.ID);                   
                    MessagingCenter.Send(this, "TaskChanged");
                    await _navigation.PopModalAsync();                    
                });
            }
        }
        #endregion

        #region Private
        private ObservableCollection<string> GetTaskTypes()
        {
            var types = new List<string> { "Cultivate", "Plant","Other" };
            return new ObservableCollection<string>(types);
        }
        #endregion
    }
}
