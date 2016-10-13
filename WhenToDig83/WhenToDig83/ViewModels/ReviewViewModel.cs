
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
using Xamarin.Forms;
using static Xamarin.Forms.Grid;

namespace WhenToDig83.ViewModels
{
    internal class ReviewViewModel : BaseModel
    {
        private INavigation _navigation;
        private ReviewManager _reviewManager;      
        
        public ReviewViewModel()
        {
            _reviewManager = new ReviewManager();
            SearchTerm = string.Empty;
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
        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(); }
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
        #endregion

        #region Events
        public ICommand TaskSearch
        {
            get
            {
                return new Command(async () =>
                {                    
                    var notes = await _reviewManager.Search(_searchTerm, (int)NoteType.Task);
                    Notes = notes == null ? new ObservableCollection<Note>() : new ObservableCollection<Note>(notes);
                });
            }
        }
        public ICommand PlantSearch
        {
            get
            {
                return new Command(async () =>
                {                  
                    var notes = await _reviewManager.Search(_searchTerm, (int)NoteType.Plant);
                    Notes = notes == null ? new ObservableCollection<Note>() : new ObservableCollection<Note>(notes);
                });
            }
        }
        public ICommand VarietySearch
        {
            get
            {
                return new Command(async () =>
                {                    
                    var notes = await _reviewManager.Search(_searchTerm, (int)NoteType.Variety);
                    Notes = notes == null ? new ObservableCollection<Note>() : new ObservableCollection<Note>(notes);
                });
            }
        }
        public ICommand AllSearch
        {
            get
            {
                return new Command(async () =>
                {                   
                    var notes = await _reviewManager.Search(_searchTerm);
                    Notes = notes == null ? new ObservableCollection<Note>() : new ObservableCollection<Note>(notes);
                });
            }
        }
        #endregion
        #region Navigation Events

        public ImageSource TaskIcon { get { return ImageSource.FromFile("ic_task_white_48dp.png"); } }
        public ImageSource PlantIcon { get { return ImageSource.FromFile("ic_plant_white_48dp.png"); } }
        public ImageSource ReviewIcon { get { return ImageSource.FromFile("ic_review_teal_48dp.png"); } }
        public ImageSource FrostIcon { get { return ImageSource.FromFile("ic_frost_white_48dp.png"); } }

        public ICommand ToolbarNavigationCommand
        {
            get
            {
                return new Command<string>(async (string paramter) =>
                {
                    switch (paramter)
                    {
                        case "task":
                            await _navigation.PushAsync(new WTDTaskPage());
                            break;
                        case "plant":
                            await _navigation.PushAsync(new PlantPage());
                            break;    
                        case "frost":
                            await _navigation.PushAsync(new FrostPage());
                            break;
                    }

                });
            }
        }
        #endregion
    }
}
