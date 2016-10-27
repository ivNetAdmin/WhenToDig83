
using System;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Core.Helpers;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class VarietyEditViewModel : BaseModel
    {
        private INavigation _navigation;
        private PlantManager _plantManager;
        private NoteManager _noteManager;
        private Plant _selectedPlant;
        private Variety _selectedVariety;
        
        public VarietyEditViewModel()
        {
            _plantManager = new PlantManager();
            _noteManager =  new NoteManager();
 
            MessagingCenter.Subscribe<PlantEditViewModel, Plant>(this, "Plant", (message, args) => {
                _selectedPlant = args;
                PlantName = _selectedPlant.Name;          
            });
            
             MessagingCenter.Subscribe<PlantEditViewModel, Variety>(this, "EditVariety", (message, args) => {
                _selectedVariety = args;
                Name = _selectedVariety.Name;
              
               _selectedPlant=_plantManager.GetPlant(_selectedVariety.PlantId).Result;
                 PlantName = _selectedPlant.Name;

                 var notesResult = _noteManager.GetNote((int)NoteType.Variety, _selectedVariety.ID).Result;
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

        private string _plantName;
        public string PlantName
        {
            get { return _plantName; }
            set
            {
                _plantName = value;
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

        public string VarietyImage { get { return ImageHelper.Variety(); } }

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
                MessagingCenter.Unsubscribe<PlantEditViewModel>(this, "Plant");
                MessagingCenter.Unsubscribe<PlantEditViewModel>(this, "Variety");
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
                    MessagingCenter.Send(this, "VarietyUnchanged");
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
                    _plantManager.AddVariety(Name, Notes, _selectedPlant.ID, _selectedPlant.Name, _selectedVariety == null ? 0 : _selectedVariety.ID);
                    MessagingCenter.Send(this, "VarietyChanged");
                    await _navigation.PopModalAsync();
                });
            }
        }
        
        public ICommand Delete
        {
            get
            {
                return new Command(async () =>
                {
                    _plantManager.DeleteVariety(_selectedVariety.ID);
                    MessagingCenter.Send(this, "VarietyChanged");
                    await _navigation.PopModalAsync();
                });
            }
        }
        #endregion
        
        public ImageSource DeleteIcon { get { return ImageSource.FromFile("delete.png"); } }
    }
}
