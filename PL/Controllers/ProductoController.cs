using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();
            ML.Producto producto = new ML.Producto();
            producto.Productos = result.Objects;

            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int idProducto)
        {
            ML.Result resultDepartamentos = BL.Departamento.GetAll();
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();

            producto.Departamento.Departamentos = resultDepartamentos.Objects;

            if (idProducto == 0)
            {
                return View(producto);
            }
            else
            {
                ML.Result result = BL.Producto.GetByIdEF(idProducto);
                producto = (ML.Producto)result.Object;
                producto.Departamento.Departamentos = resultDepartamentos.Objects;
                return View(producto);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            if(producto.IdProducto == 0)
            {
                ML.Result result = BL.Producto.AddEF(producto);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha agregado el producto" + producto.Nombre + "a la base de datos";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + result.ErrorMessage;
                }

                return PartialView("Modal");
            }
            else
            {
                
                
                return PartialView("Modal");
            }
          
        }
        public ActionResult Delete(int idProducto)
        {
            //ML.Result result = BL.Producto.Delete(idProducto);
            ML.Result result = new ML.Result();
            result.Correct = true;
            if (result.Correct)
            {
                ViewBag.Mensaje = "El registro se ha eliminado";
            }   
            else
            {
                ViewBag.Mensaje = "Ocurrio un error" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}

//Javascript
//Jquery
//Selectores de CSS
//Eventos de HTML
//expresiones regulares
//Drop down list - con  Html helper