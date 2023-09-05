using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversityHousing.Helpers;
using UniversityHousing.Models.Entities;
using UniversityHousing.Models.Repositories;
using UniversityHousing.Models;
using UniversityHousing.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Windows.Media.Animation;

namespace UniversityHousing.ViewModels
{
    class DormitoryViewModel : ViewModelBase
    {
        private ObservableCollection<Dormitory> dormsList;
        private readonly UnitOfWork unitOfWork;
        private readonly DormitoriesRepository _dormitoriesRepository;

        public DormitoryViewModel()
        {
            unitOfWork = ServiceLocator.ServiceProvider.GetService<UnitOfWork>();
            DormsList = new ObservableCollection<Dormitory>(unitOfWork.Dormitories.GetAll());
            AddDormitoryCommand = new RelayCommand(AddDormitory);
            EditDormitoryCommand = new RelayCommand(EditDormitory);
            DeleteDormitoryCommand = new RelayCommand(DeleteDormitory);
            RoomsCommand = new RelayCommand(OpenRoomsView);
        }

        public ObservableCollection<Dormitory> DormsList
        {
            get => dormsList;
            set
            {
                dormsList = value;
                OnPropertyChanged(nameof(DormsList));
            }
        }

        private string _feeAmountText;
        public string FeeAmountText
        {
            get => _feeAmountText;
            set
            {
                _feeAmountText = value;
                OnPropertyChanged(nameof(FeeAmountText));
            }
        }

        public ICommand AddDormitoryCommand { get; private set; }
        public ICommand EditDormitoryCommand { get; private set; }
        public ICommand DeleteDormitoryCommand { get; private set; }
        public ICommand RoomsCommand { get; private set; }

        private void AddDormitory(object obj)
        {
            decimal feeAmount = (decimal)obj;
            var dormitory = new Dormitory
            {
                FeeAmount = feeAmount
            };

            unitOfWork.Dormitories.Insert(dormitory);
            unitOfWork.SaveChanges();
            DormsList.Add(dormitory);
        }

        private void EditDormitory(object obj)
        {
            Dormitory dormitory = obj as Dormitory;
            if (dormitory != null)
            {
                unitOfWork.Dormitories.Update(dormitory);
                unitOfWork.SaveChanges();
            }
        }

        private void DeleteDormitory(object obj)
        {
            Dormitory dormitory = obj as Dormitory;
            if (dormitory != null)
            {
                unitOfWork.Dormitories.Remove(dormitory);
                unitOfWork.SaveChanges();
                DormsList.Remove(dormitory);
            }
        }
        private void OpenRoomsView(object obj)
        {
            Dormitory selectedDormitory = obj as Dormitory;
            if (selectedDormitory != null)
            {
                var roomsView = new RoomView();
                var roomsViewModel = new RoomViewModel(selectedDormitory);
                roomsView.DataContext = roomsViewModel;
                roomsView.Show();
            }
        }
    }
}
