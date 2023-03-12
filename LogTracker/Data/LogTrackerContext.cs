using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogTracker.Models;

namespace LogTracker.Data
{
    public class LogTrackerContext : DbContext
    {
        public LogTrackerContext (DbContextOptions<LogTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<LogTracker.Models.UserViewModel> UserViewModel { get; set; } = default!;
        public DbSet<LogTracker.Models.SalesViewModel> SalesViewModel { get; set; } = default!;
        public DbSet<LogTracker.Models.GameViewModel> GameViewModel { get; set; } = default!;
        public DbSet<LogTracker.Models.ProductViewModel> ProductViewModel { get; set; } = default!;
    }
}
