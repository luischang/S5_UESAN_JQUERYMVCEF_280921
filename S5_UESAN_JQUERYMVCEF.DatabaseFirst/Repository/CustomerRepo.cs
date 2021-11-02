using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S5_UESAN_JQUERYMVCEF.DatabaseFirst.Models;
using S5_UESAN_JQUERYMVCEF.DatabaseFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace S5_UESAN_JQUERYMVCEF.DatabaseFirst.Repository
{
    public class CustomerRepo
    {
        public static IEnumerable<Customer> GetCustomers()
        {
            using var data = new SalesDBContext();
            var customers = data.Customer.ToList();
            return customers;
        }

        public static IEnumerable<Customer> GetCustomersSP()
        {
            using var data = new SalesDBContext();
            var customers = data.Customer.FromSqlRaw("SP_GET_CUSTOMERS");

            //var customers2 = (from c in data.Customer
            //                  join o in data.Order on c.Id equals o.CustomerId
            //                  select new { c.FirstName, o.OrderNumber });

            return customers;
        }

        public static async Task<IEnumerable<CustomerViewModel>> GetCustomersAsync()
        {
            //using var data = new SalesDBContext();
            //var customers = await data.Customer.Include(x=>x.Order).ToListAsync();
            //return customers;

            using var httpClient = new HttpClient();
            using var response = await httpClient
                .GetAsync("http://localhost:1030/api/Customer/GetCustomers");
            string apiResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerViewModel>>(apiResponse);
            return customers;

        }

        public static async Task<CustomerViewModel> GetCustomer(int id)
        {
            //using var data = new SalesDBContext();
            //var customer = await data.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            //return customer;

            using var httpClient = new HttpClient();
            using var response = await httpClient
                .GetAsync("http://localhost:1030/api/Customer/GetCustomerById/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerViewModel>(apiResponse);
            return customer;

        }

        public static async Task<bool> Insert(Customer customer)
        {
            //bool exito = true;

            //try
            //{
            //    using var data = new SalesDBContext();
            //    await data.Customer.AddAsync(customer);
            //    await data.SaveChangesAsync();

            //}
            //catch (Exception)
            //{
            //    exito = false;
            //}
            //return exito;

            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            using var response = await httpClient
                .PostAsync("http://localhost:1030/api/Customer/PostCustomer", data);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonConvert.DeserializeObject<int>(apiResponse);
            return (customerResponse == 0 ? false : true);

        }


        public static async Task<bool> Update(Customer customer)
        {
            //bool exito = true;

            //try
            //{
            //    using var data = new SalesDBContext();
            //    var customerNow = await data.Customer.Where(x => x.Id == customer.Id).FirstOrDefaultAsync();

            //    customerNow.FirstName = customer.FirstName;
            //    customerNow.LastName = customer.LastName;
            //    customerNow.Country = customer.Country;
            //    customerNow.City = customer.City;
            //    customerNow.Phone = customer.Phone;

            //    await data.SaveChangesAsync();

            //}
            //catch (Exception)
            //{

            //    exito = false;
            //}

            //return exito;

            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            using var response = await httpClient
                .PutAsync("http://localhost:1030/api/Customer/PutCustomer", data);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonConvert.DeserializeObject<int>(apiResponse);
            return (customerResponse == 0 ? false : true);

        }

        public static async Task<bool> Delete(int id)
        {
            //bool exito = true;

            //try
            //{
            //    using var data = new SalesDBContext();
            //    var customerNow = await data.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();

            //    data.Customer.Remove(customerNow);
            //    await data.SaveChangesAsync();
            //}
            //catch (Exception)
            //{
            //    exito = false;
            //}

            //return exito;

            using var httpClient = new HttpClient();
            using var response = await httpClient
               .DeleteAsync("http://localhost:1030/api/Customer/DeleteCustomer?id=" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if ((int)response.StatusCode == 404)
                return false;

            return true;
        }



    }
}
