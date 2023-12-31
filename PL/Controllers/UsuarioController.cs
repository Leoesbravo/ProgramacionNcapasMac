﻿using System;
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
                    HttpContext.Session.SetString("Rol", usuario.Rol.Nombre);
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
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.Email == null)
            {
                usuario.Operacion = "Registrar";
                return View(usuario);
            }
            else
            {
                usuario.Operacion = "Completar";
                return View(usuario);
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, string password)
        {
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            var passwordHash = bcrypt.GetBytes(20);
            usuario.Password = passwordHash;
            if(usuario.Operacion == "Completar")
            {

                ML.Result result1 = BL.Usuario.GetByEmail(usuario.Email);
                ML.Usuario usuario1 = (ML.Usuario)result1.Object;
                usuario.IdUsuario = usuario1.IdUsuario;
                usuario.Password = usuario1.Password;
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
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario.Usuarios = result.Objects;

                return View(usuario);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string email)
        {
            ML.Result result = BL.Usuario.GetByEmail(email);
            if (result.Correct)
            {
                //proceso para enviar email lescogido@digis01.com
                ViewBag.Accion = "Login";
                ViewBag.Mensaje = "Se ha enviado un correo con instrucciones";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Accion = "Email";
                ViewBag.Mensaje = "No existe el correo ingresado";
                return PartialView("Modal");
            }
        }
        [HttpPost]
        public JsonResult CambiarStatus(int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.CambiarStatus(idUsuario, status); 
            return Json(result);
        }
    }
}