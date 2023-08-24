using System;
namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public List<object> Productos { get; set; }
        public ML.Departamento Departamento { get; set; }
    }
}

