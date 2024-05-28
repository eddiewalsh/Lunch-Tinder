using Lunch_Tinder.Models;
using Lunch_Tinder.Security;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Lunch_Tinder.Data
{
    public partial class LunchTinderContext : DbContext
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public LunchTinderContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<LunchGroup> LunchGroups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Invites> Invites { get; set; }
        public DbSet<InviteToLunchGroup> InvitesLG { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasData(
               new Restaurant()
               {
                   RestaurantID = 4,
                   RestaurantName = "McLaughlins Restaurant",
                   RestaurantDescription = "Hotel, Traditional Food"

               });

            modelBuilder.Entity<Restaurant>().HasData(
               new Restaurant()
               {
                   RestaurantID = 3,
                   RestaurantName = "Delish Cafe",
                   RestaurantDescription = "Sandwhiches, Salads,Breakfast Foods"

               });


            modelBuilder.Entity<Restaurant>().HasData(
               new Restaurant()
               {
                   RestaurantID = 2,
                   RestaurantName = "La Cucina Limerick",
                   RestaurantDescription = "Italian pizza and European dishes"

               });

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant()
                {
                    RestaurantID = 1,
                    RestaurantName = "Locke Burger Castletroy",
                    RestaurantDescription = "We don’t do burgers, we do Locke burgers."

                });


            modelBuilder.Entity<LunchGroup>().HasData(
            new LunchGroup()
            {
                GroupId = 1,
                GroupName = "Software Development",
                Description = "We code"

            });

            modelBuilder.Entity<User>().HasData(
            new User()
            {
                UserId = 1,
                EmailAddress = "useremail@gmail.com",
                UserName = "Test",
                Password = PasswordHelper.HashPassword("Password123!!!"),
                UserType = "USER"
            },
            new User()
            {
                UserId = 2,
                EmailAddress = "adminemail@gmail.com",
                UserName = "Test",
                Password = PasswordHelper.HashPassword("Password123!!!"),
                UserType = "ADMIN"
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
