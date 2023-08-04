using System;
using System.Collections.Generic;

namespace DL;

public partial class Area
{
    public int IdArea { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
}
