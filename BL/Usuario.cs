using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
	public class Usuario
	{
		public Usuario()
		{
		}
		public static ML.Result Add(ML.Usuario usuario)
		{
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    int query = 0;
                    if(usuario.ApellidoMaterno == null)
                    {
                         query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPatenro}', '{usuario.Email}'  ,@Password", new SqlParameter("@Password", usuario.Password));
                    }
                    else
                    {
                         query = context.Database.ExecuteSqlRaw($"AdminUsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPatenro}', '{usuario.Email}', '{usuario.ApellidoMaterno}', {usuario.Rol.IdRol}  ,@Password", new SqlParameter("@Password", usuario.Password));
                    }


                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPatenro}', '{usuario.Email}', '{usuario.ApellidoMaterno}'  ,@Password", new SqlParameter("@Password", usuario.Password));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static ML.Result GetByEmail(string email)
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var row = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{email}'").AsEnumerable().FirstOrDefault();

                    if (row != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = row.IdUsuario;
                        usuario.Password = row.Password;
                        usuario.Rol = new Rol();
                        usuario.Rol.Nombre = row.NombreRol;

                        result.Correct = true;
                        result.Object = usuario;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var row = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();

                    if (row != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var obj in row)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.ApellidoPatenro = obj.ApellidoPaterno;
                            usuario.Rol = new Rol();
                            usuario.Rol.IdRol = obj.Rol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.Status = obj.Status;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CambiarStatus(int idUsuario, bool status)
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus {idUsuario}, {status}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}

