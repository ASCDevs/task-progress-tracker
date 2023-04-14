using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Infrastructure.Persistence.Context
{
    public class SQLServerContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public SQLServerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("SQLServerConn"));
        }
        public DbSet<Tarefa> Tarefas { get; set; }

    }
}
