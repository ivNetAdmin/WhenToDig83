
using System;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using Xamarin.Forms;

namespace WhenToDig83.ViewModels
{
    internal class PlantEditViewModel : BaseModel
    {
        private INavigation _navigation;
        private PlantManager _plantManager;
        private NoteManager _noteManager;
        private Plant _selectedPlant;

        public PlantEditViewModel()
        {
            _plantManager = new PlantManager();
            _noteManager = new NoteManager();

            MessagingCenter.Subscribe<PlantViewModel, Plant>(this, "EditPlant", (message, args) => {
                _selectedPlant = args;
                Name = _selectedPlant.Name;
              

                var notesResult = _noteManager.GetNote((int)NoteType.Plant, _selectedPlant.ID).Result;
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
                MessagingCenter.Unsubscribe<PlantViewModel, Plant>(this, "EditPlant");
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
                    _plantManager.AddPlant(Name);
                    MessagingCenter.Send(this, "PlantChanged");
                    await _navigation.PopModalAsync();
                });
            }
        }
        #endregion
    }
}
