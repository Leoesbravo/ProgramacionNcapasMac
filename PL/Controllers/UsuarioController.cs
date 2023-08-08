﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: /<controller>/
        public ActionResult  Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(ML.Usuario usuario, string passwordIngresado)
        {
            var bcrypt = new Rfc2898DeriveBytes(passwordIngresado, new byte[0], 10000, HashAlgorithmName.SHA256);
            var passwordHash = bcrypt.GetBytes(20);
            usuario.Password = passwordHash;

            ML.Result result = BL.Usuario.Add(usuario);

            return RedirectToAction("Index", "Home");
        }
    }
}

