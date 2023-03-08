using System;
using System.Data;
using Npgsql;

namespace PostgreSQL
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try {
				string connectionString = "Server=localhost;Port=5432;Database=Test;User Id=postgres;Password=selero02";

				NpgsqlConnection connection = new NpgsqlConnection(connectionString);
				connection.Open();

				if (connection.State == ConnectionState.Open) {
					NpgsqlCommand command = new NpgsqlCommand
					{
						Connection = connection,
						CommandType = CommandType.Text,
						CommandText = "select * from \"Entity\""
					};

					NpgsqlDataReader reader = command.ExecuteReader();
					if (reader.HasRows) {
						while (reader.Read()) {
							long entityUID = (long)reader["EntityUID"];
							string name = reader["Name"].ToString();
						}
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
				Console.Read();
			}
		}
	}
}
