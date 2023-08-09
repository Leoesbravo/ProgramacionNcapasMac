using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);

            var passwordHash = bcrypt.GetBytes(20);
            ML.Result result = BL.Usuario.GetByEmail(email);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if(usuario.Password.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("Index", "Home");
                }       
                else
                {
                    ViewBag.Mensaje = "La contraseña es incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Mensaje = "No existe el usuario ingresado";
                return PartialView("Modal");
            }
        }
        [HttpPost]
        public ActionResult Register(ML.Usuario usuario, string passwordIngresado)
        {
            var bcrypt = new Rfc2898DeriveBytes(passwordIngresado, new byte[0], 10000, HashAlgorithmName.SHA256);
            var passwordHash = bcrypt.GetBytes(20);
            usuario.Password = passwordHash;

            ML.Result result = BL.Usuario.Add(usuario);

            return RedirectToAction("Form", "Usuario", usuario);
        }
        [HttpGet]
        public ActionResult Form(ML.Usuario usuario, int sobrecarga)
        {
            if (usuario.Email == null)
            {
                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result1 = BL.Usuario.GetByEmail(usuario.Email);
            ML.Usuario usuario1 = (ML.Usuario)result1.Object;
            usuario.IdUsuario = usuario1.IdUsuario;
            usuario.Password = usuario1.Password;

            if(usuario.IdUsuario != 0)
            {
                ML.Result result = BL.Usuario.Update(usuario);
                ViewBag.Mensaje = "Se ha completado tu informacion";
            }
            else
            {
                ML.Result result = BL.Usuario.Add(usuario);
                ViewBag.Mensaje = "Se ha dado de alta al nuevo usuario";
            }
            return PartialView("Modal");
            
        }
    }
}