using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain
{
    public class TaskInfoView
    {
        public string IdTask { get; set; }
        public string TaskName { get; set; }
        public string DtSolicitacao { get; set; }
        public string DtFinalizacao { get; set; }
        public string Status { get; set; }
    }
}
