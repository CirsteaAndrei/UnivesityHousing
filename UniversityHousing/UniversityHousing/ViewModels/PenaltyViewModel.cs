using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHousing.Models.Entities;
using UniversityHousing.Models.Repositories;
using UniversityHousing.Models;
using UniversityHousing.Helpers;

namespace UniversityHousing.ViewModels
{
    class PenaltyViewModel : ViewModelBase
    {
        private ObservableCollection<Penalty> _penalties;
        private Penalty _selectedPenalty;
        private readonly PenaltiesRepository _penaltiesRepository;

        public PenaltyViewModel()
        {
            _penaltiesRepository = new PenaltiesRepository(new AppDbContext());
            LoadPenalties();
        }

        public ObservableCollection<Penalty> Penalties
        {
            get => _penalties;
            set
            {
                _penalties = value;
                OnPropertyChanged(nameof(Penalties));
            }
        }

        public Penalty SelectedPenalty
        {
            get => _selectedPenalty;
            set
            {
                _selectedPenalty = value;
                OnPropertyChanged(nameof(SelectedPenalty));
            }
        }

        private void LoadPenalties()
        {
            Penalties = _penaltiesRepository.ListToObsCollection(_penaltiesRepository.GetAll());
        }
    }
}
