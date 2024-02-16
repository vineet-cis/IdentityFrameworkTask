using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityFrameworkTask.DataModel;
using IdentityFrameworkTask.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace IdentityFrameworkTask
{


    public class appDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
    }
}
