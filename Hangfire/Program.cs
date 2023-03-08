using Hangfire;
using Hangfire.PostgreSql;

GlobalConfiguration.Configuration
	.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
	.UseColouredConsoleLogProvider()
	.UseSimpleAssemblyNameTypeSerializer()
	.UseRecommendedSerializerSettings()
	.UsePostgreSqlStorage("Server=localhost;Port=5432;Database=HangfireTest;User Id=postgres;Password=selero02");

BackgroundJob.Enqueue(() => Console.WriteLine("Single Job"));
RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring Job"), Cron.Minutely);

using (var server = new BackgroundJobServer()) {
	Console.ReadLine();
}

