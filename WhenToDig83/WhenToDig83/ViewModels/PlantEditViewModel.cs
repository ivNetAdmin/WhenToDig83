﻿
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Helpers;
using WhenToDig83.Managers;
using WhenToDig83.Pages;
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

            MessagingCenter.Subscribe<PlantViewModel, Plant>(this, "EditPlant", async (message, args) =>
            {
                _selectedPlant = args;
                Name = _selectedPlant.Name;

                var notesResult = await _noteManager.GetNote((int)NoteType.Plant, _selectedPlant.ID);
                Notes = notesResult == null ? string.Empty : notesResult.Notes;

                GetVarieties();

                IsNewVarietyButtonVisible = true;
            });
            
            MessagingCenter.Subscribe<VarietyEditViewModel>(this, "VarietyChanged", (message) => {
                GetVarieties();
            });

            MessagingCenter.Subscribe<VarietyEditViewModel>(this, "VarietyUnchanged", (message) => {
                SelectedItem = null;
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

        private Variety _selectedItem;
        public Variety SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    if (value == null) return;

                    _navigation.PushModalAsync(new VarietyEditPage());
                    MessagingCenter.Send(this, "EditVariety", value);
                   
                }
            }
        }

        private ObservableCollection<Variety> _varieties;
        public ObservableCollection<Variety> Varieties
        {
            get { return _varieties; }
            set {
                _varieties = value;               
                OnPropertyChanged();
            }
        }

        private bool _isNewVarietyButtonVisible;
        public bool IsNewVarietyButtonVisible
        {
            get { return _isNewVarietyButtonVisible; }
            set
            {                
                _isNewVarietyButtonVisible = value;
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
                IsNewVarietyButtonVisible = false;
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
                MessagingCenter.Unsubscribe<VarietyEditViewModel>(this, "VarietyChanged");
                MessagingCenter.Unsubscribe<VarietyEditViewModel>(this, "VarietyUnchanged");
            }
            catch (Exception exception)
            {
                ResponseText = exception.ToString();
            }
        }
        #endregion

        #region Events
        public ICommand NewVariety
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushModalAsync(new VarietyEditPage());
                    MessagingCenter.Send(this, "Plant", _selectedPlant);
                });
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new Command(async () =>
                {
                    MessagingCenter.Send(this, "PlantUnchanged");
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
                    _plantManager.AddPlant(Name,  Notes, _selectedPlant == null ? 0 : _selectedPlant.ID);
                    MessagingCenter.Send(this, "PlantChanged");
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
                    _plantManager.DeletePlant(_selectedPlant.ID);
                    MessagingCenter.Send(this, "PlantChanged");
                    await _navigation.PopModalAsync();
                });
            }
        }
        
        #endregion

        public ImageSource DeleteIcon { get { return ImageSource.FromFile("delete.png"); } }

        #region Private
        private async void GetVarieties()
        {
            if (Varieties != null) Varieties.Clear();
            var varieties = await _plantManager.GetVarieties(_selectedPlant.ID);
            Varieties = new ObservableCollection<Variety>(varieties);
        }
        #endregion
    }
}
