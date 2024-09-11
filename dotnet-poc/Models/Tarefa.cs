using System;
using System.Collections.Generic;

namespace dotnet_poc.Models;

public partial class Tarefa
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public bool? Feita { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
