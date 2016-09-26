﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class WTDTaskEditViewModel : BaseModel
    {
        private INavigation _navigation;
        private WTDTaskManager _wtdTaskManager;
        private WTDTask _selcectedTask;


        public WTDTaskEditViewModel()
        {            
            Date = DateTime.Now;

            MessagingCenter.Subscribe<WTDTaskViewModel, WTDTask>(this, "EditTask", (message, args) => {

                _selcectedTask = args;
                Name = _selcectedTask.Name;
                Date = _selcectedTask.Date;
                Type = _selcectedTask.Type;
                Notes = _selcectedTask.Notes;
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
        
        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
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
        #endregion
        
        #region Page Events
         protected override void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            try
            {             
                _navigation = AppHelper.CurrentPage().Navigation;
                _wtdTaskManager = new WTDTaskManager();                
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
                    await _wtdTaskManager.AddTask(Name, Date, Type, Notes, _selcectedTask == null ? 0 : _selcectedTask.ID);
                    MessagingCenter.Send(this, "TasksChanged");
                    await _navigation.PopModalAsync();                    
                });
            }
        }
        #endregion
    }
}
