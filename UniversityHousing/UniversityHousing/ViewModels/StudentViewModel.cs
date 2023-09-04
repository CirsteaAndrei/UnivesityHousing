using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversityHousing.Models.Entities;
using UniversityHousing.Models.Repositories;
using UniversityHousing.Models;
using UniversityHousing.Helpers;
using System.ComponentModel;

namespace UniversityHousing.ViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        private ObservableCollection<Student> _students;
        private Student _selectedStudent;
        private readonly StudentsRepository _studentsRepository;

        public StudentViewModel()
        {
            _studentsRepository = new StudentsRepository(new AppDbContext());
            LoadStudents();
        }

        public ObservableCollection<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }

        public ICommand AddStudentCommand => new RelayCommand(AddStudent);
        public ICommand UpdateStudentCommand => new RelayCommand(UpdateStudent);
        public ICommand DeleteStudentCommand => new RelayCommand(DeleteStudent);

        private void LoadStudents()
        {
            Students = _studentsRepository.ListToObsCollection(_studentsRepository.GetAll());
        }

        private void AddStudent(object obj)
        {
            // Logic to add a student
            // You might want to get data from text boxes or other input fields
            var student = new Student { /* set properties here */ };
            _studentsRepository.Insert(student);
            LoadStudents();
        }

        private void UpdateStudent(object obj)
        {
            if (SelectedStudent == null) return;
            _studentsRepository.Update(SelectedStudent);
            LoadStudents();
        }

        private void DeleteStudent(object obj)
        {
            if (SelectedStudent == null) return;
            _studentsRepository.Remove(SelectedStudent);
            LoadStudents();
        }

        public Room SelectedRoom { get; set; }
        public ICommand AssociateRoomCommand => new RelayCommand(AssociateRoom);
        public ICommand DisassociateRoomCommand => new RelayCommand(DisassociateRoom);

        private void AssociateRoom(object obj)
        {
            if (SelectedStudent == null || SelectedRoom == null) return;
            SelectedStudent.Room = SelectedRoom;
            _studentsRepository.Update(SelectedStudent);
            LoadStudents();
        }

        private void DisassociateRoom(object obj)
        {
            if (SelectedStudent == null) return;
            SelectedStudent.Room = null;
            _studentsRepository.Update(SelectedStudent);
            LoadStudents();
        }

        public ICommand ExpelStudentCommand => new RelayCommand(ExpelStudent);

        private void ExpelStudent(object obj)
        {
            if (SelectedStudent == null) return;
            SelectedStudent.Room = null; // Remove association with room
                                         // You might also want to set other properties to indicate the student is expelled
            _studentsRepository.Update(SelectedStudent);
            LoadStudents();
        }

    }
}
