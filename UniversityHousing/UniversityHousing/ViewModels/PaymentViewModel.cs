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
using System.IO;
using System.Reflection.Metadata;
using System.Windows.Documents;
using System.Windows;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;

namespace UniversityHousing.ViewModels
{
    class PaymentViewModel : ViewModelBase
    {
        private ObservableCollection<Payment> _payments;
        private Payment _selectedPayment;
        private readonly PaymentsRepository _paymentsRepository;

        public PaymentViewModel()
        {
            _paymentsRepository = new PaymentsRepository(new AppDbContext());
            LoadPayments();
        }

        public ObservableCollection<Payment> Payments
        {
            get => _payments;
            set
            {
                _payments = value;
                OnPropertyChanged(nameof(Payments));
            }
        }

        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }

        public ICommand MakePaymentCommand => new RelayCommand(MakePayment);
        public ICommand GenerateReceiptCommand => new RelayCommand(GenerateReceipt);

        private void LoadPayments()
        {
            Payments = _paymentsRepository.ListToObsCollection(_paymentsRepository.GetAll());
        }

        private void MakePayment(object obj)
        {
            if (SelectedPayment == null) return;
            HandlePayment(SelectedPayment.Student);
            DeregisterStudent(SelectedPayment.Student);
        }

        private void GenerateReceipt(object obj)
        {
            if (SelectedPayment == null) return;

            string fileName = $"Receipt_{SelectedPayment.Student.CNP}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            PdfWriter writer = new PdfWriter(filePath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            document.Add(new Paragraph($"Receipt for {SelectedPayment.Student.FirstName} {SelectedPayment.Student.LastName}"));
            document.Add(new Paragraph($"CNP: {SelectedPayment.Student.CNP}"));
            document.Add(new Paragraph($"Faculty: {SelectedPayment.Student.Faculty}"));
            // ... add other details ...

            document.Close();

            MessageBox.Show($"Receipt generated at {filePath}");
        }

        private decimal CalculatePaymentAmount(Student student)
        {
            decimal baseAmount = student.Room.Dormitory.FeeAmount; // Assuming the fee is associated with the dormitory
            decimal amountDue = 0;

            switch (student.FeeStatus)
            {
                case FeeStatus.Budgeted:
                    amountDue = baseAmount;
                    break;
                case FeeStatus.FeePaying:
                    amountDue = 2 * baseAmount;
                    break;
                case FeeStatus.PartiallyExempted:
                    amountDue = 0.5m * baseAmount;
                    break;
            }

            return amountDue;
        }
        private void HandlePayment(Student student)
        {
            decimal amountDue = CalculatePaymentAmount(student);
            //decimal penalty = CalculatePenalty(student); // This method will calculate any penalties based on overdue payments

            SelectedPayment.AmountPaid = amountDue; // Assuming AmountPaid is the amount the student has paid
            SelectedPayment.PaymentDate = DateTime.Now;

            _paymentsRepository.Update(SelectedPayment);
            LoadPayments();
        }

        private void DeregisterStudent(Student student)
        {
            // Logic to check if the student hasn't paid for 3 consecutive months
            var unpaidPayments = _paymentsRepository.GetUnpaidPayments(student.Id); // This method should be implemented in the repository

            if (unpaidPayments.Count() >= 3)
            {
                // Deregister the student
                student.Room = null; // Assuming the student's association with a room is through a Room property
                                     // Update the student in the database
            }
        }

    }
}
