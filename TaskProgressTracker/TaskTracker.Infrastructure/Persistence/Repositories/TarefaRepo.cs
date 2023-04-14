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
        private readonly SQLServerContext _context;
        public TarefaRepo(SQLServerContext context)
        {
            _context = context;
        }
        public void Add(Tarefa tarefa)
        {
            try
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Tarefa tarefa)
        {
            try
            {
                _context.Tarefas.Update(tarefa);
                _context.SaveChanges();
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
    }
}
