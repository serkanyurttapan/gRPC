using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using TestGrpc;
using static TestGrpc.Greeter;

namespace GrpcTestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var input = Console.ReadLine();

            while(input.ToLower() != "q")
            {
                switch(input)
                {
                    case "1":
                        Console.WriteLine(await GetHello(client));
                        break;
                    case "2":
                        Console.WriteLine(await GetGoodbye(client));
                        break;
                    default:
                        Console.WriteLine("Valid inputs are '1' and '2', 'q' to quit.");
                        break;
                }

                input = Console.ReadLine();
            }
        }

        private static async Task<string> GetHello(GreeterClient client)
        {
            var response =  await client.SayHelloAsync(new CommonRequest { Name = "GreeterClient" });
            return response.Message;
        }

        private static async Task<string> GetGoodbye(GreeterClient client)
        {
            var response = await client.SayByeAsync(new CommonRequest { Name = "GreeterClient" });
            return response.Message;
        }
    }
}
