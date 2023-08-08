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
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPatenro}', '{usuario.Email}'  ,@Password", new SqlParameter("@Password", usuario.Password));

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

