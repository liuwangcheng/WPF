using WPF_CMS.Models;

namespace WPF_CMS.ViewModels
{
    public class CustomerViewModel
    {
        private Customer _customer;
        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }

        public int Id { get => _customer.Id; }

        public string Name
        {
            get => _customer.Name; set
            {
                if (value != _customer.Name)
                {
                    _customer.Name = value;
                }
            }
        }

        public string IdNumber
        {
            get => _customer.IdNumber; set
            {
                if (value != _customer.IdNumber)
                {
                    _customer.IdNumber = value;
                }
            }
        }

        public string Address
        {
            get => _customer.Address; set
            {
                if (value != _customer.Address)
                {
                    _customer.Address = value;
                }
            }
        }

    }
}