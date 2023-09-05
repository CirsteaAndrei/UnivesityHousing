using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversityHousing.Helpers;
using UniversityHousing.Models;
using UniversityHousing.Models.Entities;
using UniversityHousing.Models.Repositories;
using UniversityHousing.Views;

namespace UniversityHousing.ViewModels
{
    public class RoomViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _roomsList;
        private readonly UnitOfWork unitOfWork;
        private readonly RoomsRepository _roomsRepository;
        private readonly Dormitory currentDormitory;

        public RoomViewModel(Dormitory selectedDormitory)
        {
            unitOfWork = ServiceLocator.ServiceProvider.GetService<UnitOfWork>();
            currentDormitory = selectedDormitory;
            RoomsList = new ObservableCollection<Room>(unitOfWork.Rooms.GetRoomsByDormitoryId(selectedDormitory.Id));
            AddRoomCommand = new RelayCommand(AddRoom);
            DeleteRoomCommand = new RelayCommand(DeleteRoom);
            OpenAssignStudentViewCommand = new RelayCommand(OpenAssignStudentView);
            DeassignStudentCommand = new RelayCommand(DeassignStudent);
            ConfirmDeassignStudentCommand = new RelayCommand(DeassignSelectedStudent);
            RefreshRoomsCommand = new RelayCommand(RefreshRooms);
        }

        public ObservableCollection<Room> RoomsList
        {
            get => _roomsList;
            set
            {
                _roomsList = value;
                OnPropertyChanged(nameof(RoomsList));
            }
        }

        public ICommand AddRoomCommand { get; private set; }
        public ICommand DeleteRoomCommand { get; private set; }
        public ICommand OpenAssignStudentViewCommand { get; private set; }
        public ICommand DeassignStudentCommand { get; private set; }
        public ICommand RefreshRoomsCommand { get; private set; }
        public ICommand ConfirmDeassignStudentCommand { get; private set; }

        private void RefreshRooms(object obj)
        {
            RoomsList = new ObservableCollection<Room>(unitOfWork.Rooms.GetRoomsByDormitoryId(currentDormitory.Id));
        }

        private void AddRoom(object obj)
        {
            var room = new Room
            {
                DormitoryId = currentDormitory.Id
            };
            unitOfWork.Rooms.Insert(room);
            unitOfWork.SaveChanges();
            RoomsList.Add(room);
        }

        private void DeleteRoom(object obj)
        {
            Room room = obj as Room;
            if (room != null)
            {
                _roomsRepository.Remove(room);
                unitOfWork.SaveChanges();
                RoomsList.Remove(room);
            }
        }

        private void OpenAssignStudentView(object obj)
        {
            Room selectedRoom = obj as Room;
            if (selectedRoom != null)
            {
                var assignStudentViewModel = new AssignStudentViewModel(selectedRoom);
                var assignStudentView = new AssignStudentView { DataContext = assignStudentViewModel };
                assignStudentView.ShowDialog();
            }
        }

        private bool _isDeassignStudentPopupOpen;
        public bool IsDeassignStudentPopupOpen
        {
            get => _isDeassignStudentPopupOpen;
            set
            {
                _isDeassignStudentPopupOpen = value;
                OnPropertyChanged(nameof(IsDeassignStudentPopupOpen));
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        private void DeassignSelectedStudent(object obj)
        {
            if (SelectedStudent != null)
            {
                SelectedStudent.RoomId = null;
                unitOfWork.Students.Update(SelectedStudent);
                unitOfWork.SaveChanges();
                IsDeassignStudentPopupOpen = false;
            }

        }
        private void DeassignStudent(object obj)
        {
            Room room = obj as Room;
            if (room != null && room.Students.Any())
            {
                IsDeassignStudentPopupOpen = true;
            }
        }
    }
}
