using System;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
	public class Area
	{
		public Area()
		{
		}
        public static ML.Result GetAllEF()
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var query = context.Areas.FromSqlRaw("AreaGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var row in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = row.IdArea;
                            area.Nombre = row.Nombre;


                            result.Objects.Add(area);
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
    }
}

