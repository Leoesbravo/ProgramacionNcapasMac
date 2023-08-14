using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        // GET: /<controller>/
        public ActionResult Catalogo()
        {
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();
            ML.Result result = BL.Producto.GetAllEF(producto);
            producto.Productos = result.Objects;
            return View(producto);
        }
    }
}

