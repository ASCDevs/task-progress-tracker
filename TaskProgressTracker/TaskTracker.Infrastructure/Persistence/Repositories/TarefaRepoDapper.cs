using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Repositories;
using TaskTracker.Infrastructure.Persistence.Context;

namespace TaskTracker.Infrastructure.Persistence.Repositories
{
    public class TarefaRepoDapper : ITarefaRepo
    {
        private const int nrTasksPerCall = 3;
        private readonly DapperContext _context;
        public TarefaRepoDapper(DapperContext context)
        {
            _context = context;
        }

        public Tarefa Add(Tarefa tarefa)
        {
            tarefa.IdTarefa = Guid.NewGuid().ToString();
            tarefa.PedidoTarefa = DateTime.Now;
            tarefa.Status = "Solicitado";

            using(var connection = _context.CreateConnection())
            {
                var sql = "INSERT INTO tarefas (id_tarefa,nm_tarefa,dt_pedido_tarefa,status) VALUES(@id_tarefa,@nm_tarefa,@dt_pedido_tarefa,@status)";
                int rowsAffected = connection.Execute(sql,tarefa);
                if (rowsAffected > 0) { return tarefa; }
                return null;
            }
        }

        public List<Tarefa> GetAll()
        {
            string query = "SELECT * FROM tarefas";

            using(var connection = _context.CreateConnection()) 
            { 
                return connection.Query<Tarefa>(query).ToList();
            }
        }

        public List<Tarefa> GetNotExecuted()
        {
            string query = $"SELECT TOP({nrTasksPerCall}) * FROM tarefas WHERE dt_inicio_tarefa is null AND dt_fim_tarefa is null";

            using(var connection = _context.CreateConnection())
            {
                return connection.Query<Tarefa>(query).ToList();
            }
        }

        public Tarefa Update(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }
    }
}
