using Microsoft.AspNetCore.Mvc;
using S5_UESAN_JQUERYMVCEF.DatabaseFirst.Models;
using S5_UESAN_JQUERYMVCEF.DatabaseFirst.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S5_UESAN_JQUERYMVCEF.DatabaseFirst.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(int idCliente)
        {
            bool exito = await CustomerRepo.Delete(idCliente);
            return Json(exito);
        }


        public async Task<IActionResult> Obtener(int idCliente)
        {
            var customer = await CustomerRepo.GetCustomer(idCliente);
            return Json(customer);
        }

        public async Task<IActionResult> Listado() 
        {
            var customers = await CustomerRepo.GetCustomersAsync();
            return PartialView(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Grabar(int idCliente, string nombre,
            string apellido, string ciudad, string telefono, string pais)
        {

            var customer = new Customer()
            {
                FirstName = nombre,
                LastName = apellido,
                City = ciudad,
                Country = pais,
                Phone = telefono
            };
            bool exito = true;
            if (idCliente == -1)
                exito = await CustomerRepo.Insert(customer);
            else {
                customer.Id = idCliente;
                exito = await CustomerRepo.Update(customer);
            }
            return Json(exito);
        }


    }
}
