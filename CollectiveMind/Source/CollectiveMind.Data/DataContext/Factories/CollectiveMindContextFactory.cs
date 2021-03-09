﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CollectiveMind.Data.DataContext.Factories
{
	public class CollectiveMindContextFactory : IDesignTimeDbContextFactory<CollectiveMindContext>
	{
		public CollectiveMindContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CollectiveMindContext>();

			optionsBuilder.UseMySql(
				"Server=localhost;Port=1003;database=CollectiveMind;user=root;password=wachtwoord",
				new MySqlServerVersion(new Version(8,1,21)),
				mySqlOptions => mySqlOptions.EnableRetryOnFailure());
			
			return new CollectiveMindContext(optionsBuilder.Options);
		}
	}
}