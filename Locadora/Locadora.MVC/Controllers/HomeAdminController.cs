using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locadora.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.MVC.Controllers
{
    public class HomeAdminController: Controller
    {
        public IActionResult index() {

            return View();
        }

        public IActionResult cadastroCliente() {
            return View();
        }

        public IActionResult cadastroItens() {
            return View();
        }
    }
}
