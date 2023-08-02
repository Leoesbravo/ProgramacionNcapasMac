using System;
using System.Data;
using System.Data.SQLite;

namespace BL
{
	public class Departamento
	{
		public Departamento()
		{
		}
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(DL.Conexion.GetConnection()))
                {

                    SQLiteCommand command = new SQLiteCommand("SELECT IdDepartamento, Nombre FROM Departamento");
                    command.Connection = context;

                    DataTable tableProducto = new DataTable();

                    SQLiteDataAdapter adpter = new SQLiteDataAdapter(command);

                    adpter.Fill(tableProducto);
                    if (tableProducto.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tableProducto.Rows)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = int.Parse(row[0].ToString());
                            departamento.Nombre = row[1].ToString();

                            result.Objects.Add(departamento);
                        }
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
                result.Ex = ex;
            }
            return result;
        }
    }
}

