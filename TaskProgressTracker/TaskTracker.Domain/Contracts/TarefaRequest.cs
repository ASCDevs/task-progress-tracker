using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Contracts
{
    public class TarefaRequest
    {
        public string? IdTarefa { get; set; }
        public string? NomeTarefa { get; set; }
    }
}
