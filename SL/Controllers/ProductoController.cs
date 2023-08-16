using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            ML.Result result = BL.Producto.GetAllEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }

        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Producto producto)
        {
            //session
            //como consumir un servicio web desde mvc .net 6 

            ML.Result result = BL.Producto.AddEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }

        }
    }
}

