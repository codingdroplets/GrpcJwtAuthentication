using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5137");

            try
            {
                var authenticationClient = new Authentication.AuthenticationClient(channel);
                var authenticationResponse = authenticationClient.Authenticate(new AuthenticationRequest
                {
                    UserName = "admin",
                    Password = "admin"
                });

                Console.WriteLine($"Received Auth Response | Token: {authenticationResponse.AccessToken} | Expires In: {authenticationResponse.ExpiresIn}");


                var calculationClient = new Calculation.CalculationClient(channel);
                var headers = new Metadata();
                headers.Add("Authorization", $"Bearer {authenticationResponse.AccessToken}");
                //var sumResult = calculationClient.Add(new InputNumbers { Number1 = 5, Number2 = 10 }, headers);
                //Console.WriteLine($"Sum Result: 5+10={sumResult.Result}");

                //var subtractResult = calculationClient.Subtract(new InputNumbers { Number1 = 20, Number2 = 5 }, headers);
                //Console.WriteLine($"Subtract result: 20-5={subtractResult.Result}");


                var multiplyResult = calculationClient.Multiply(new InputNumbers { Number1 = 5, Number2 = 6 });
                Console.WriteLine($"Multiply Result: 5*6={multiplyResult.Result}");

            }
            catch(RpcException ex)
            {
                Console.WriteLine($"Status Code: {ex.StatusCode} | Error: {ex.Message}");
                return;
            }

            await channel.ShutdownAsync();
        }
    }
}
