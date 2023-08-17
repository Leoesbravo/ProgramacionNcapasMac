using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public ActionResult AddCarrito(int idProducto)
        {
            if(HttpContext.Session.GetString("Carrito") == null)
            {
                ML.Venta carrito = new ML.Venta();
                carrito.Carrito = new List<object>();
                ML.Result result = BL.Producto.GetByIdEF(idProducto);
                if (result.Correct)
                { 
                    ML.Producto producto = (ML.Producto)result.Object;
                    carrito.Carrito.Add(producto);
                    //serializar carrito
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.ToString()));
                }
            
            }
            else
            {
                //deserialiar la sesion
               ML.Venta venta = HttpContext.Session.GetString("Carrito");
                //agregar el nuevo producto, tienen modificar el total
            }

            return RedirectToAction("Catalogo");
        }
        public ActionResult GetCarrito()
        {

        }
    }
}

