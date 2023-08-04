using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

//agregadas por mi

    public int Area { get; set; }
}
