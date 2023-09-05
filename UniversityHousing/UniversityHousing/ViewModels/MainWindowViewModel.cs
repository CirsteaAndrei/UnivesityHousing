using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversityHousing.Helpers;
using UniversityHousing.Views;

namespace UniversityHousing.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (openWindowCommand == null)
                {
                    openWindowCommand = new RelayCommand(OpenWindow);
                }
                return openWindowCommand;
            }
        }

        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    StudentView student= new StudentView();
                    student.ShowDialog();
                    break;
                case "2":
                    PaymentView payment = new PaymentView();
                    payment.ShowDialog();
                    break;
                case "3":
                    DormitoryView dormitory= new DormitoryView();
                    dormitory.ShowDialog();
                    break;
                case "4":
                    //AnotherPersonView anotherPers = new AnotherPersonView();
                    //anotherPers.ShowDialog();
                    break;
            }
        }
    }
}
