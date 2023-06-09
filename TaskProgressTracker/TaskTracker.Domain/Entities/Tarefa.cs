﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    [Table("tarefas")]
    public class Tarefa
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("id_tarefa")]
        public string? IdTarefa { get; set; }
        [Column("nm_tarefa")]
        public string? NomeTarefa { get; set; }
        [Column("dt_pedido_tarefa")]
        public DateTime? PedidoTarefa { get; set; }
        [Column("dt_inicio_tarefa")]
        public DateTime? InicioTarefa { get; set; }
        [Column("dt_fim_tarefa")]
        public DateTime? FimTarefa { get; set; }
        [Column("status")]
        public string? Status { get; set; }

    }
}
