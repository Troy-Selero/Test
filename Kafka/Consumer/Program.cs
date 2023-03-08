using Confluent.Kafka;
using Newtonsoft.Json;

ConsumerConfig config = new ConsumerConfig
{
	GroupId = "animal-consumer-group",
	BootstrapServers = "localhost:9092",
	AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();

consumer.Subscribe("AnimalTopic");

CancellationTokenSource token = new CancellationTokenSource();

try {
	while (true) {
		var response = consumer.Consume(token.Token);

		if (response.Message != null) {
			var animal = JsonConvert.DeserializeObject<Animal>(response.Message.Value);

			Console.WriteLine($"Name: {animal.Name}");
		}
	}
}
catch (Exception ex) {
	Console.WriteLine(ex.Message);
}

public record Animal(string Name);