using Microsoft.EntityFrameworkCore;
using LinqEtSeedEF.Models;

namespace LinqEtSeedEF.Data
{
    public class LinqEtSeedEFContext : DbContext
    {
        public LinqEtSeedEFContext(DbContextOptions<LinqEtSeedEFContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>().HasData(
                new Client { Id = 1, Nom = "Marie-Josée Tremblay", Adresse = "123 rue Principale, Montréal", Telephone = "(514) 555-1234" },
                new Client { Id = 2, Nom = "Patrick Gagné", Adresse = "456 boulevard Saint-Laurent, Québec", Telephone = "(418) 555-5678" },
                new Client { Id = 3, Nom = "Sophie Dubois", Adresse = "789 avenue des Érables, Trois-Rivières", Telephone = "(819) 555-9012" },
                new Client { Id = 4, Nom = "Jean-François Lavoie", Adresse = "1010 rue Saint-Denis, Montréal", Telephone = "(514) 555-3456" },
                new Client { Id = 5, Nom = "Geneviève Parent", Adresse = "111 rue des Pionniers, Québec", Telephone = "(418) 555-7890" }
            );

            builder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1, Nom = "La graine du père George", Adresse = "456 rue Sainte-Catherine, Montréal", Telephone = "(514) 555-2345" },
                new Restaurant { Id = 2, Nom = "Le Bistro", Adresse = "789 rue Principale, Québec", Telephone = "(418) 555-6789" },
                new Restaurant { Id = 3, Nom = "La Belle Province", Adresse = "1010 boulevard René-Lévesque, Trois-Rivières", Telephone = "(819) 555-2345" },
                new Restaurant { Id = 4, Nom = "La Piazzetta", Adresse = "1111 rue de la Montagne, Montréal", Telephone = "(514) 555-6789" },
                new Restaurant { Id = 5, Nom = "Le Petit Coin", Adresse = "2222 rue Saint-Jean, Québec", Telephone = "(418) 555-2345" }
            );

            builder.Entity<Plat>().HasData(
                new Plat { Id = 1, Nom = "Sauté de légumes", Description = "Un mélange de légumes frais sautés à la poêle", Prix = 8.99m, RestaurantId = 1, Vegetarien = true },
                new Plat { Id = 2, Nom = "Riz aux carottes", Description = "Du riz blanc servi avec des carottes fraîches", Prix = 6.99m, RestaurantId = 1, Vegetarien = true },
                new Plat { Id = 3, Nom = "Poulet au gingembre", Description = "Un délicieux plat de poulet avec du gingembre frais", Prix = 12.99m, RestaurantId = 2, Vegetarien = false },
                new Plat { Id = 4, Nom = "Tofu au gingembre", Description = "Un délicieux plat de tofu avec du gingembre frais", Prix = 11.99m, RestaurantId = 2, Vegetarien = true },
                new Plat { Id = 5, Nom = "Poutine", Description = "Patate, sauce, fromage skwich skwich", Prix = 7.99m, RestaurantId = 3, Vegetarien = false },
                new Plat { Id = 6, Nom = "Pizza", Description = "Pepperonni fromage", Prix = 9.99m, RestaurantId = 4, Vegetarien = false }
            );

            builder.Entity<Commande>().HasData(
                new Commande { Id = 1, Date = new DateTime(2022, 05, 01), ClientId = 1, RestaurantId = 1 },
                new Commande { Id = 2, Date = new DateTime(2022, 05, 02), ClientId = 2, RestaurantId = 2 },
                new Commande { Id = 3, Date = new DateTime(2022, 05, 03), ClientId = 3, RestaurantId = 3 },
                new Commande { Id = 4, Date = new DateTime(2022, 05, 03), ClientId = 2, RestaurantId = 4 },

                // Nouvelle commande du 4 mai 2022 au Bistro
                new Commande { Id = 5, Date = new DateTime(2022, 05, 04), ClientId = 2, RestaurantId = 2 }
            );

            builder.Entity<CommandePlat>().HasData(
                new CommandePlat { CommandeId = 1, PlatId = 1, Quantite = 1 },
                new CommandePlat { CommandeId = 1, PlatId = 2, Quantite = 2 },

                new CommandePlat { CommandeId = 2, PlatId = 3, Quantite = 2 },
                new CommandePlat { CommandeId = 2, PlatId = 4, Quantite = 1 },

                new CommandePlat { CommandeId = 3, PlatId = 5, Quantite = 4 },

                new CommandePlat { CommandeId = 4, PlatId = 6, Quantite = 2 },

                // Plats de la nouvelle commande
                // 3 x Poulet au gingembre
                new CommandePlat { CommandeId = 5, PlatId = 3, Quantite = 3 },

                // 3 x Tofu au gingembre
                new CommandePlat { CommandeId = 5, PlatId = 4, Quantite = 3 }
            );

            builder.Entity<TestData>().HasData(
                new TestData { Id = 1, ValeurA = 1, ValeurB = 379 },
                new TestData { Id = 2, ValeurA = 85, ValeurB = 65000 }
            );

            // Lorsque l'on efface une entrée CommandePlate,
            // on ne veut pas effacer la commande
            builder.Entity<CommandePlat>()
                .HasOne(cp => cp.Commande)
                .WithMany(c => c.CommandesPlats)
                .OnDelete(DeleteBehavior.NoAction);

            // Lorsque l'on efface une entrée CommandePlate,
            // on ne veut pas effacer le plat
            builder.Entity<CommandePlat>()
                .HasOne(cp => cp.Plat)
                .WithMany(p => p.CommandesPlats)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Client> Client { get; set; } = default!;
        public DbSet<Commande> Commande { get; set; } = default!;
        public DbSet<CommandePlat> CommandePlat { get; set; } = default!;
        public DbSet<Plat> Plat { get; set; } = default!;
        public DbSet<Restaurant> Restaurant { get; set; } = default!;
        public DbSet<TestData> TestData { get; set; } = default!;
    }
}