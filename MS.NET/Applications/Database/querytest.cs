using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

static class Program
{
	public static void Main(string[] args)
	{
		var settings = ConfigurationManager.ConnectionStrings[args[0]];
		var factory = DbProviderFactories.GetFactory(settings.ProviderName);
		var connection = factory.CreateConnection();

		connection.ConnectionString = settings.ConnectionString;
		connection.Open();

		var command = connection.CreateCommand();
		command.CommandText = "SELECT ProductNo, Price, Stock FROM Product";

		var reader = command.ExecuteReader();
		while(reader.Read())
			Console.WriteLine("{0, -6}{1, 10:0.00}{2, 8}", reader.GetInt32(0), reader.GetDecimal(1), reader["Stock"]);
		reader.Close();

		connection.Close();
		
	}
}

