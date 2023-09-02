using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DA.SmartMedCab.Poc.EFCore
{
	public class SmcContext:DbContext
	{
		public DbSet<LocationSettings> LocationSettings { get; set; }
		public DbSet<WeatherData> WeatherData { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connString = "server=localhost;database=DA_SmartMedCab;user=smc_user;password=smc;";
			optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<LocationSettings>(entity =>
			{
				entity.HasKey(e => e.LocationID);
				entity.Property(e => e.Name).IsRequired();
			});
			modelBuilder.Entity<WeatherData>(entity =>
			{
				//entity.HasKey(e => e.LocationSettings.LocationID);
				//entity.HasKey(e => e.CreationDate);
				entity.HasKey(e => e.WeatherDataID);
				entity.Property(e => e.DateTime).IsRequired();
				entity.Property(e =>e.WmoCode).IsRequired();
				entity.Property(e=>e.Temp_2m).IsRequired();
			});
		}
	}
}
