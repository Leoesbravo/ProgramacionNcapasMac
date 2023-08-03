using System;
using System.Collections.Generic;

namespace DL;

public partial class Tipo
{
    public int IdTipo { get; set; }

    public string? Nombre { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
