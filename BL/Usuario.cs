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
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate '{usuario.Nombre}', '{usuario.ApellidoPatenro}', '{usuario.Email}', '{usuario.ApellidoMaterno}'  ,@Password", new SqlParameter("@Password", usuario.Password));

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
    }
}

