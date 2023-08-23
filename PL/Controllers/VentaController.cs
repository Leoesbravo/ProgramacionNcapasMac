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
            ML.Venta carrito = new ML.Venta();
            carrito.Carrito = new List<object>();
            ML.Result result = BL.Producto.GetByIdEF(idProducto);
            if (HttpContext.Session.GetString("Carrito") == null)
                {

                if (result.Correct)
                { 
                    ML.Producto producto = (ML.Producto)result.Object;
                    carrito.Carrito.Add(producto);
                    //serializar carrito
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.Carrito));
                }
            
            }
            else
            { 
            
                ML.Producto producto = (ML.Producto)result.Object;
                GetCarrito(carrito); //ya recupere el carrito
                carrito.Carrito.Add(producto);
                HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.Carrito));

            }

            return RedirectToAction("Catalogo");
        } 
        public ML.Venta GetCarrito(ML.Venta carrito)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito"));

            foreach (var obj in ventaSession)
            {
                ML.Producto objMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                carrito.Carrito.Add(objMateria);
            }
            return carrito;
        }
        public ActionResult Carrito()
        {
            ML.Venta carrito = new ML.Venta();
            carrito.Carrito = new List<object>();
            if(HttpContext.Session.GetString("Carrito") == null)
            {
                return View(carrito);
            }
            else
            {
                GetCarrito(carrito);
                return View(carrito);
            }
  
        }
    }
}

