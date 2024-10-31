using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CMS.Models;
using WPF_CMS.ViewModels;

namespace WPF_CMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            _viewModel.LoadCustomers();
            DataContext = _viewModel;
            //ShowCustomers();
        }

        private void ClearSelectedCustomer_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearSelectedCustomer();
        }

        private void SavaCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = nameTextbox.Text.Trim();
                string idNumber = idNumberTextbox.Text.Trim();
                string address = addressTextbox.Text.Trim();
                _viewModel.SaveCustomer(name, idNumber, address);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.AddAppointment();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        //private void ShowCustomers()
        //{
        //    try
        //    {
        //        using (var db = new AppDbContext())
        //        {
        //            var customer = db.Customers.ToList();
        //            customerList.DisplayMemberPath = "Name";
        //            customerList.SelectedValuePath = "Id";
        //            customerList.ItemsSource = customer;
        //        }
        //        //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Customers", _sqlConnection);
        //        //using (sqlDataAdapter)
        //        //{
        //        //    DataTable dt = new DataTable();
        //        //    sqlDataAdapter.Fill(dt);
        //        //    customerList.DisplayMemberPath = "Name";
        //        //    customerList.SelectedValuePath = "Id";
        //        //    customerList.ItemsSource = dt.DefaultView;

        //        //}
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //    }

        //}

        //private void CustomerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (customerList.SelectedItem is Customer selectItem)
        //        {
        //            if (selectItem == null)
        //            {
        //                appointmentList.ItemsSource = null;
        //                NameTextbox.Text = null;
        //                IdTextbox.Text = null;
        //                AddressTextbox.Text = null;
        //                return;
        //            }
        //            NameTextbox.Text = selectItem.Name;
        //            IdTextbox.Text = selectItem.IdNumber;
        //            AddressTextbox.Text = selectItem.Address;
        //        }
        //        using (var db = new AppDbContext())
        //        {
        //            var customerId = customerList.SelectedValue;
        //            if (customerId == null)
        //            {
        //                appointmentList.ItemsSource = null;
        //                NameTextbox.Text = null;
        //                IdTextbox.Text = null;
        //                AddressTextbox.Text = null;
        //                return;
        //            }
        //            var appointment = db.Appointments.Where(a => a.CustomerId == (int)customerId).ToList();
        //            appointmentList.DisplayMemberPath = "Time";
        //            appointmentList.SelectedValuePath = "Id";
        //            appointmentList.ItemsSource = appointment;
        //        }
        //        //string sql = "select A.* from Appointments A,Customers B where A.CustomerId = B.Id and B.Id = @CustomerId";
        //        //var customerId = customerList.SelectedValue;
        //        //if (customerId == null)
        //        //{
        //        //    appointmentList.ItemsSource = null;
        //        //    NameTextbox.Text = null;
        //        //    IdTextbox.Text = null;
        //        //    AddressTextbox.Text = null;
        //        //    return;
        //        //}
        //        //if (customerList.SelectedItem is DataRowView Item)
        //        //{
        //        //    NameTextbox.Text = Item["Name"] as string;
        //        //    IdTextbox.Text = Item["IdNumber"] as string;
        //        //    AddressTextbox.Text = Item["Address"] as string;
        //        //}
        //        //SqlCommand sqlCommand = new SqlCommand(sql,_sqlConnection);
        //        //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        //        //sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);

        //        //using (sqlDataAdapter)
        //        //{
        //        //    DataTable appointTable = new DataTable();
        //        //    sqlDataAdapter.Fill(appointTable);
        //        //    appointmentList.DisplayMemberPath = "Time";
        //        //    appointmentList.SelectedValuePath = "Id";
        //        //    appointmentList.ItemsSource = appointTable.DefaultView;

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        var customerId = customerList.SelectedValue;
        //        if (customerId == null)
        //        {
        //            MessageBox.Show("请选择需要删除的客户");
        //        }
        //        else
        //        {
        //            using var db = new AppDbContext();
        //            var appointmentToRemove = db.Appointments.Where(a => a.CustomerId == (int)customerId);
        //            if (appointmentToRemove != null)
        //            {
        //                db.Appointments.RemoveRange(appointmentToRemove);
        //            }
        //            var customerToRemove = db.Customers.Where(a => a.Id == (int)customerId).FirstOrDefault();
        //            if (customerToRemove != null)
        //            {
        //                db.Customers.Remove(customerToRemove);
        //                db.SaveChanges();
        //            }
        //            //string sql1 = "delete from Appointments where CustomerId = @Id";
        //            //string sql2 = "delete from Customers where Id = @Id";
        //            //SqlCommand sqlCommand1 = new SqlCommand(sql1, _sqlConnection);
        //            //SqlCommand sqlCommand2 = new SqlCommand(sql2, _sqlConnection);

        //            //sqlCommand1.Parameters.AddWithValue("@Id", customerId);
        //            //sqlCommand2.Parameters.AddWithValue("@Id", customerId);
        //            //_sqlConnection.Open();
        //            //sqlCommand1.ExecuteNonQuery();
        //            //sqlCommand2.ExecuteNonQuery();
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        ShowCustomers();
        //        CustomerList_SelectionChanged(null, null);
        //    }
        //}

        //private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        var Id = appointmentList.SelectedValue;
        //        if (Id == null)
        //        {
        //            MessageBox.Show("请选择需要取消的预约记录");
        //        }
        //        else
        //        {
        //            using var db = new AppDbContext();
        //            var appointmentToRemove = db.Appointments.Where(a =>a.Id == (int)Id).FirstOrDefault();
        //            if (appointmentToRemove != null)
        //            {
        //                db.Appointments.Remove(appointmentToRemove);
        //            }                   
        //            db.SaveChanges();
        //            //string sql = "delete from Appointments where Id = @Id";
        //            //SqlCommand sqlCommand = new SqlCommand(sql, _sqlConnection);
        //            //sqlCommand.Parameters.AddWithValue("@Id", Id);

        //            //_sqlConnection.Open();
        //            //sqlCommand.ExecuteNonQuery();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        CustomerList_SelectionChanged(null,null);
        //    }
        //}

        //private void AddCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(NameTextbox.Text) 
        //         || string.IsNullOrWhiteSpace(IdTextbox.Text)
        //         || string.IsNullOrWhiteSpace(AddressTextbox.Text))
        //        {

        //        }
        //        else
        //        {
        //            using (var db = new AppDbContext())
        //            {
        //                var customer = new Customer()
        //                {
        //                    Name = NameTextbox.Text,
        //                    IdNumber = IdTextbox.Text,
        //                    Address = AddressTextbox.Text
        //                };
        //                db.Customers.Add(customer);
        //                db.SaveChanges();
        //            }

        //            //string sql = "insert into Customers values(@name,@Id,@Address)";
        //            //SqlCommand sqlCommand = new SqlCommand(sql, _sqlConnection);
        //            //sqlCommand.Parameters.AddWithValue("@name", NameTextbox.Text);
        //            //sqlCommand.Parameters.AddWithValue("@Id", IdTextbox.Text);
        //            //sqlCommand.Parameters.AddWithValue("@Address", AddressTextbox.Text);

        //            //_sqlConnection.Open();
        //            //sqlCommand.ExecuteNonQuery();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        ShowCustomers();
        //    }
        //}

        //private void AddAppointment_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (customerList.SelectedValue == null)
        //        {
        //            MessageBox.Show("请选择客户");
        //        }
        //        else if (string.IsNullOrWhiteSpace(DateText.Text))
        //        {
        //            MessageBox.Show("请选择预约时间");
        //        }
        //        else
        //        {
        //            using (var db = new AppDbContext())
        //            {
        //                var appointment = new Appointment()
        //                {
        //                    Time = DateTime.Parse(DateText.Text),
        //                    CustomerId = (int)customerList.SelectedValue
        //                };
        //                db.Appointments.Add(appointment);
        //                db.SaveChanges();
        //            }
        //            //string sql = "insert into Appointments values(@Time,@CustomerId)";
        //            //SqlCommand sqlCommand = new SqlCommand(sql, _sqlConnection);
        //            //sqlCommand.Parameters.AddWithValue("@Time", DateText.Text);
        //            //sqlCommand.Parameters.AddWithValue("@CustomerId", customerList.SelectedValue);

        //            //_sqlConnection.Open();
        //            //sqlCommand.ExecuteNonQuery();
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        CustomerList_SelectionChanged(null, null);
        //    }
        //}

        //private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        var Id = customerList.SelectedValue;
        //        if (Id == null)
        //        {
        //            MessageBox.Show("请选择需要修改的客户");
        //        }
        //        else 
        //        {
        //            using (var db = new AppDbContext())
        //            {
        //                var customer = db.Customers.Where(c => c.Id == (int)Id).FirstOrDefault();
        //                if (customer != null)
        //                {
        //                    customer.Name = NameTextbox.Text.Trim();
        //                    customer.IdNumber = IdTextbox.Text.Trim();
        //                    customer.Address = AddressTextbox.Text.Trim();
        //                    db.SaveChanges();
        //                }                        
        //            }
        //            //string sql = "update Customers set name = @Name,IdNumber = @IdNumber,Address = @Address where Id = @Id";
        //            //SqlCommand sqlCommand = new SqlCommand(sql, _sqlConnection);
        //            //sqlCommand.Parameters.AddWithValue("@Name", NameTextbox.Text);
        //            //sqlCommand.Parameters.AddWithValue("@IdNumber", IdTextbox.Text);
        //            //sqlCommand.Parameters.AddWithValue("@Address", AddressTextbox.Text);
        //            //sqlCommand.Parameters.AddWithValue("@Id", Id);

        //            //_sqlConnection.Open();
        //            //sqlCommand.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        ShowCustomers();
        //    }
        //}
    }
}