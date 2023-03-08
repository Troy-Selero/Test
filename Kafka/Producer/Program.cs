using Confluent.Kafka;
using Newtonsoft.Json;

ProducerConfig config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<Null, string>(config).Build();

try {
	string? animal;

	while ((animal = Console.ReadLine()) != null) {
		var response = await producer.ProduceAsync("AnimalTopic",
			new Message<Null, string>
			{
				Value = JsonConvert.SerializeObject(new Animal(animal))
			});

		Console.WriteLine(response.Value);
	}
}
catch (ProduceException<Null, string> ex) {
	Console.WriteLine(ex.Message);
}

public record Animal(string Name);