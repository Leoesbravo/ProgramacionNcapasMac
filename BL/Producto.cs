using System;
using System.Data;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using ML;

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

                    SQLiteCommand command = new SQLiteCommand("SELECT IdProducto, Nombre, Precio, IdDepartamento FROM Producto");
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

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = int.Parse(row[3].ToString());

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
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Producto (Nombre, Precio, IdDepartamento) VALUES(@Nombre, @Precio, @IdDepartamento)");

                    SQLiteParameter[] collection = new SQLiteParameter[3];

                    collection[0] = new SQLiteParameter("Nombre", DbType.String);
                    collection[0].Value = producto.Nombre;
                    collection[1] = new SQLiteParameter("Precio", DbType.Decimal);
                    collection[1].Value = producto.Precio;
                    collection[2] = new SQLiteParameter("IdDepartamento", DbType.Int16);
                    collection[2].Value = producto.Departamento.IdDepartamento;

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
        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(DL.Conexion.GetConnection()))
                {

                    SQLiteCommand command = new SQLiteCommand("SELECT IdProducto, Nombre, Precio, IdDepartamento FROM Producto WHERE IdProducto = @IdProducto");
                    command.Connection = context;

                    SQLiteParameter[] collection = new SQLiteParameter[1];

                    collection[0] = new SQLiteParameter("IdProducto", DbType.Int16);
                    collection[0].Value = idProducto;
                    command.Parameters.AddRange(collection);

                    DataTable tableProducto = new DataTable();

                    SQLiteDataAdapter adpter = new SQLiteDataAdapter(command);

                    adpter.Fill(tableProducto);
                    if (tableProducto.Rows.Count > 0)
                    {
                        DataRow row = tableProducto.Rows[0];

                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = int.Parse(row[0].ToString());
                        producto.Nombre = row[1].ToString();
                        producto.Precio = decimal.Parse(row[2].ToString());

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = int.Parse(row[3].ToString());

                        result.Object = producto; //boxing 
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
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(DL.Conexion.GetConnection()))
                {
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Producto SET Nombre = @Nombre,  Precio =@Precio, IdDepartamento =@IdDepartamento WHERE IdProducto = @IdProducto");

                    SQLiteParameter[] collection = new SQLiteParameter[4];

                    collection[0] = new SQLiteParameter("Nombre", DbType.String);
                    collection[0].Value = producto.Nombre;
                    collection[1] = new SQLiteParameter("Precio", DbType.Decimal);
                    collection[1].Value = producto.Precio;
                    collection[2] = new SQLiteParameter("IdDepartamento", DbType.Int16);
                    collection[2].Value = producto.Departamento.IdDepartamento;
                    collection[3] = new SQLiteParameter("IdProducto", DbType.Int16);
                    collection[3].Value = producto.IdProducto;

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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw("ProductoGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var row in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = row.IdProducto;
                            producto.Nombre = row.Nombre;
                            producto.Precio = row.Precio.Value;

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = row.IdDepartamento.Value;

                            result.Objects.Add(producto);
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
        public static ML.Result AddEF(ML.Producto producto)
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.Precio}, {producto.Departamento.IdDepartamento}");

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
        public static ML.Result GetByIdEF(int idProducto)
        {
            ML.Result result = new Result();
            try
            {
                using (DL.ProgramacionNcapasContext context = new DL.ProgramacionNcapasContext())
                {
                    var row = context.Productos.FromSqlRaw($"ProductoGetById {idProducto}").AsEnumerable().FirstOrDefault();

                    if (row != null)
                    {

                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = row.IdProducto;
                        producto.Nombre = row.Nombre;
                        producto.Precio = row.Precio.Value;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = row.IdDepartamento.Value;

                        result.Correct = true;
                        result.Object = producto;
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
