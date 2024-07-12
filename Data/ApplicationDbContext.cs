using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GopherToolboxRefresh.Models;
using System;

namespace GopherToolboxRefresh.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Quest> Quests { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<CancelRequest> CancelRequests { get; set; }




		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Quest>().HasData(
				new Quest { Id = 1, Name = "Pyrkon - Bejbiczki", City = "Poznań", Address = "Międzynarodowe Targi Poznańskie, Głogowska 4, Poznań", ImageUrl = "/images/1.jpg", QuestDateStart = new DateTime (2025, 06 , 13), QuestDateEnd = new DateTime(2025, 06, 15), 
				Description = "Quest polega na zajmowaniu się dziecmi w wieku poniżej 13 roku życia. Jako główne cechy oczekujemy, rzyczliwości i umiejętności opeki nad najmłodzymi uczestnikami konwentu :)", SlotLimit = 20 },
				new Quest { Id = 2, Name = "Pyrkon - RPG", City = "Poznań", Address = "Międzynarodowe Targi Poznańskie, Głogowska 4, Poznań", ImageUrl = "/images/1.jpg", QuestDateStart = new DateTime(2025, 06, 13), QuestDateEnd = new DateTime(2025, 06, 15),
				Description = "Quest polega na udzielaniu pomocy gościom oraz prowadzącym sesje RPG", SlotLimit = 40},
				new Quest { Id = 3, Name = "Pyrkon - Wypożyczalnia", City = "Poznań", Address = "Międzynarodowe Targi Poznańskie, Głogowska 4", ImageUrl = "/images/1.jpg", QuestDateStart = new DateTime(2025, 06, 13), QuestDateEnd = new DateTime(2025, 06, 15),
				Description = "Quest polega na wypożyczaniu oraz odbieraniu gier w wypożyczalni. Wymagamy podstawowej wiedzy z zakresu gier planszowych oraz chęci do nauki nowych gier.", SlotLimit = 30},
				new Quest { Id = 4, Name = "AnimeCon - Opieka Sceny", City = "Poznań", Address = "Liceum Ogólnokształące Mistrzostwa Sporotowego im. Poznańskich Olimpijczyków, Osiedle Tysiąclecia 43", ImageUrl = "/images/2.jpg", QuestDateStart = new DateTime(2024, 10, 31), QuestDateEnd = new DateTime(2024, 10, 31),
				Description = "Opieka nad sceną, czyli pomoc występującym, szybkie sprzątanie pomiędzy występami", SlotLimit = 10},
				new Quest { Id = 5, Name = "AnimeCon - Akra", City = "Poznań", Address = "Liceum Ogólnokształące Mistrzostwa Sporotowego im. Poznańskich Olimpijczyków, Osiedle Tysiąclecia 43", ImageUrl = "/images/2.jpg", QuestDateStart = new DateTime(2024, 10, 31), QuestDateEnd = new DateTime(2024, 10, 31),
				Description = "Prowadzenie akry", SlotLimit = 5},
				new Quest { Id = 6, Name = "Hikari - Plener", City = "Poznań", Address = "Collegium Da Vinci, Kutrzeby 10, Poznań", ImageUrl = "/images/3.png", QuestDateStart = new DateTime(2024, 08, 16), QuestDateEnd = new DateTime(2024, 08, 18),
				Description = "Zajmowanie się terenem plenerowym aby były na bierząco dostarczane przedmioty do prowadzących", SlotLimit = 15},
				new Quest { Id = 7, Name = "Hikari - Scena", City = "Poznań", Address = "Collegium Da Vinci, Kutrzeby 10, Poznań", ImageUrl = "/images/3.png", QuestDateStart = new DateTime(2024, 08, 16), QuestDateEnd = new DateTime(2024, 08, 18),
				Description = "Administracja sceną, wpuszczanie oraz informowanie o zejscu ze sceny", SlotLimit = 10},
				new Quest { Id = 8, Name = "Gdakon - Wsparcie ZTM Hotel", City = "Gdańsk", Address = "Hotel Novotel Marina, Jelitkowska 20, 80-342 Gdańsk", ImageUrl = "/images/4.png", QuestDateStart = new DateTime(2025, 02, 23), QuestDateEnd = new DateTime(2025, 03, 01),
				Description = "Pomoc pracownikom ZTM Gdańsk przy ładowaniu oraz rozładowaniu pasażerów, podróżujących pomiędzy Hotelem a eventem w Plenerze", SlotLimit = 5},
				new Quest { Id = 9, Name = "Gdakon - Wsparcie ZTM Plener", City = "Gdańsk", Address = "Hotel Novotel Marina, Jelitkowska 20, 80-342 Gdańsk", ImageUrl = "/images/4.png", QuestDateStart = new DateTime(2025, 02, 23), QuestDateEnd = new DateTime(2025, 03, 01),
				Description = "Pomoc pracownikom ZTM Gdańsk przy ładowaniu oraz rozładowaniu pasażerów, podróżujących pomiędzy Hotelem a eventem w Plenerze",SlotLimit = 5},
				new Quest { Id = 10, Name = "Gdakon - Akredytacja", City = "Gdańsk", Address = "Hotel Novotel Marina, Jelitkowska 20, 80-342 Gdańsk", ImageUrl = "/images/4.png", QuestDateStart = new DateTime(2025, 02, 23), QuestDateEnd = new DateTime(2025, 03, 01),
				Description = "Prowadzenie akredytacji oraz pomoc przy odbiorach identyfikatorów", SlotLimit = 10}
			);
		}
	}
}
