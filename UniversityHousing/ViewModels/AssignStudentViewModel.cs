using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversityHousing.Helpers;
using UniversityHousing.Models.Entities;
using UniversityHousing.Models;
using Microsoft.Extensions.DependencyInjection;

namespace UniversityHousing.ViewModels
{
    public class AssignStudentViewModel : ViewModelBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly Room currentRoom;

        public AssignStudentViewModel(Room selectedRoom)
        {
            unitOfWork = ServiceLocator.ServiceProvider.GetService<UnitOfWork>();
            currentRoom = selectedRoom;
            UnassignedStudents = new ObservableCollection<Student>(unitOfWork.Students.GetUnassignedStudents());
            AssignStudentCommand = new RelayCommand(AssignStudent);
        }

        public ObservableCollection<Student> UnassignedStudents { get; set; }

        public ICommand AssignStudentCommand { get; private set; }

        private void AssignStudent(object obj)
        {
            Student student = obj as Student;
            if (student != null)
            {
                student.RoomId = currentRoom.Id;
                unitOfWork.Students.Update(student);
                unitOfWork.SaveChanges();
            }
        }
    }
}
