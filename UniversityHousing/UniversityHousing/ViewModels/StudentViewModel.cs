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
using Microsoft.Extensions.DependencyInjection;

namespace UniversityHousing.ViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        private ObservableCollection<Student> studentsList;
        private readonly UnitOfWork unitOfWork;
        private readonly StudentsRepository _studentsRepository;

        public Array FeeStatuses
        {
            get { return Enum.GetValues(typeof(FeeStatus)); }
        }

        public StudentViewModel()
        {
            unitOfWork = ServiceLocator.ServiceProvider.GetService<UnitOfWork>();
            StudentsList = new ObservableCollection<Student> (unitOfWork.Students.GetAll());
            AddStudentCommand = new RelayCommand(AddStudent);
            DeleteStudentCommand = new RelayCommand(DeleteStudent);
            EditStudentCommand = new RelayCommand(UpdateStudent);
        }

        public ObservableCollection<Student> StudentsList
        {
            get => studentsList;
            set
            {
                studentsList = value;
                OnPropertyChanged(nameof(StudentsList));
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        public ICommand AddStudentCommand { get; set; }

        public void AddStudent(Object obj)
        {
            Student student = obj as Student;
            if (student != null)
            {
                if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName))
                {
                    ErrorMessage = "The name of the student must be given";
                }
                else if (string.IsNullOrEmpty(student.CNP) || string.IsNullOrEmpty(student.Faculty))
                {
                    ErrorMessage = "The CNP and faculty must be given";
                }
                else
                {
                    unitOfWork.Students.Insert(student);
                    unitOfWork.SaveChanges();
                    studentsList.Add(student);
                    ErrorMessage = "";
                }
            }
        }

        public ICommand EditStudentCommand { get; set; }
        public void UpdateStudent(Object obj)
        {
            Student student = obj as Student;
            if (student == null)
            {
                ErrorMessage = "You must select a user";
            }
            else if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName))
            {
                ErrorMessage = "The name of the employee must be given";
            }
            else if (string.IsNullOrEmpty(student.CNP) || string.IsNullOrEmpty(student.Faculty))
            {
                ErrorMessage = "The CNP and faculty must be given";
            }
            else
            {
                unitOfWork.Students.Update(student);
                unitOfWork.SaveChanges();
                ErrorMessage = "";
            }
        }

        public ICommand DeleteStudentCommand { get; set; }
        public void DeleteStudent(Object obj)
        {
            Student student = obj as Student;
            if (student == null)
            {
                ErrorMessage = "You must select a user";
            }
            else
            {
                Student u = unitOfWork.Students.GetById(student.Id);
                unitOfWork.Students.Remove(u);
                unitOfWork.SaveChanges();
                studentsList.Remove(student);
                ErrorMessage = "";
            }
        }
    }

}

