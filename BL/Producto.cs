using System;
using System.Data;
using System.Data.SQLite;
namespace BL
{
    public class Producto
    {
        public Producto()
        {
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(DL.Conexion.GetConnection()))
                {

                    SQLiteCommand command = new SQLiteCommand("SELECT IdProducto, Nombre, Precio FROM Producto");
                    command.Connection = context;

                    DataTable tableProducto = new DataTable();

                    SQLiteDataAdapter adpter = new SQLiteDataAdapter(command);

                    adpter.Fill(tableProducto);
                    if (tableProducto.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tableProducto.Rows)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = int.Parse(row[0].ToString());
                            producto.Nombre = row[1].ToString();
                            producto.Precio = decimal.Parse(row[2].ToString());

                            result.Objects.Add(producto);
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
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(DL.Conexion.GetConnection()))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Producto (Nombre, Precio) VALUES(@Nombre, @Precio)");

                    SQLiteParameter[] collection = new SQLiteParameter[2];

                    collection[0] = new SQLiteParameter("Nombre", DbType.String);
                    collection[0].Value = producto.Nombre;
                    collection[1] = new SQLiteParameter("Precio", DbType.Decimal);
                    collection[1].Value = producto.Precio;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection = context;
                    cmd.Connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
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
