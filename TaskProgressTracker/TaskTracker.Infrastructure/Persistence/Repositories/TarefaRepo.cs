using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Repositories;
using TaskTracker.Infrastructure.Persistence.Context;

namespace TaskTracker.Infrastructure.Persistence.Repositories
{
    public class TarefaRepo : ITarefaRepo
    {
        private const int nrTasksPerCall = 3;
        private readonly SQLServerContext _context;
        public TarefaRepo(SQLServerContext context)
        {
            _context = context;
        }
        public Tarefa Add(Tarefa tarefa)
        {
            try
            {
                //Tranferir para classe de tratamento de lógica de negócio
                tarefa.IdTarefa = Guid.NewGuid().ToString();
                tarefa.PedidoTarefa = DateTime.Now;
                tarefa.Status = "Solicitado";
                tarefa = _context.Tarefas.Add(tarefa).Entity;
                if(_context.SaveChanges() > 0) return tarefa;
                return null;
            }catch (Exception ex)
            {
                throw ex;
                
            }
        }

        public Tarefa Update(Tarefa tarefa)
        {
            try
            {
                tarefa = _context.Tarefas.Update(tarefa).Entity;
                if(_context.SaveChanges() > 0) return tarefa;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tarefa> GetAll()
        {
            try
            {
                return _context.Tarefas.ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Tarefa> GetNotExecuted()
        {
            try
            {
                return _context.Tarefas.Where(x => x.InicioTarefa == null && x.FimTarefa == null).Take(nrTasksPerCall).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
