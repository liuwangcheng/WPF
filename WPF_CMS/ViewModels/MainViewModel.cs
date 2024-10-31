using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_CMS.Models;

namespace WPF_CMS.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //public List<Customer> Customers { get; set; } = new();
        public ObservableCollection<CustomerViewModel> Customers { get; set; } = new();
        public ObservableCollection<DateTime> Appointments { get; set; } = new();

        private DateTime? _selectedDatetime;
        public DateTime? SelectedDatetime
        {
            get => _selectedDatetime;
            set
            {
                if (value != _selectedDatetime)
                {
                    _selectedDatetime = value;
                    RaisePropertyChanged(nameof(SelectedDatetime));
                }
            }
        }
        public void LoadCustomers()
        {
            Customers.Clear();
            using (var db = new AppDbContext())
            {
                var customer = db.Customers.ToList();
                foreach (var c in customer)
                {
                    Customers.Add(new CustomerViewModel(c));
                }
            }
        }
        private CustomerViewModel _selectedCustomer;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (value != SelectedCustomer)
                {
                    _selectedCustomer = value; 
                    RaisePropertyChanged(nameof(SelectedCustomer)); 
                    LoadAppointments(SelectedCustomer.Id);
                }
            }
        }
        public void ClearSelectedCustomer()
        {
            _selectedCustomer = null;
            RaisePropertyChanged(nameof(SelectedCustomer));
        }
        public void SaveCustomer(string name,string idNumber,string address)
        {
            if (SelectedCustomer != null)
            {
                using (var db = new AppDbContext())
                {
                    var customer = db.Customers.Where(c => c.Id == SelectedCustomer.Id).FirstOrDefault();
                    if (customer != null)
                    {
                        customer.Name = name;
                        customer.Address = address;
                        customer.IdNumber = idNumber;
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new AppDbContext())
                {
                    var newCustomer = new Customer()
                    {
                        Name = name,
                        IdNumber = idNumber,
                        Address = address
                    };
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                }
                LoadCustomers();

            }
        }

        public void LoadAppointments(int customerId)
        {
            Appointments.Clear();
            using (var db = new AppDbContext())
            {
                var appointment = db.Appointments.Where(c => c.CustomerId == (int)customerId).ToList();
                foreach (var c in appointment)
                {
                    Appointments.Add(c.Time);
                }
            }
        }

        public void AddAppointment()
        {
            if (SelectedCustomer == null)
            {
                return;
            }
            using (var db = new AppDbContext())
            {
                var newAppointment = new Appointment()
                {
                    Time = SelectedDatetime.Value,
                    CustomerId = SelectedCustomer.Id
                };
                db.Appointments.Add(newAppointment);
                db.SaveChanges();
            }
            SelectedDatetime = null;
            LoadAppointments(SelectedCustomer.Id);
        }
    }
}
