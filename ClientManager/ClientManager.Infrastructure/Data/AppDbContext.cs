using ClientManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
