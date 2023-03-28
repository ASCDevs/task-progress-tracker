using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    public class Tarefa
    {
        public string? IdTarefa { get; set; }
        public string? NomeTarefa { get; set; }
        public DateTime? PedidoTarefa { get; set; }
        public DateTime? InicioTarefa { get; set; }
        public DateTime? FimTarefa { get; set; }
        public string? Status { get; set; }

    }
}
