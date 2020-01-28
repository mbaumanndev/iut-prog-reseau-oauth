using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IutAmiens.ProgReseau.Client
{
    public static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Premier appel, sans token");

            var v_FirstClient = new HttpClient();
            var v_FirstResponse = await  v_FirstClient.GetAsync("http://localhost:5001/identity");

            Console.WriteLine(v_FirstResponse.StatusCode);

            Console.WriteLine();
            Console.WriteLine("On appel le serveur d'identité");

            var v_DiscoveryClient = new HttpClient();
            var v_Discovery = await v_DiscoveryClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (v_Discovery.IsError)
            {
                Console.WriteLine(v_Discovery.Error);
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Récupération d'un token (scope non autorisé) auprès du serveur d'identité");

            var v_TokenResponseAutreScope = await v_DiscoveryClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = v_Discovery.TokenEndpoint,

                ClientId = "autreScope",
                ClientSecret = "secret",
                Scope = "autreScope"
            });

            if (v_TokenResponseAutreScope.IsError)
            {
                Console.WriteLine(v_TokenResponseAutreScope.Error);
                return;
            }

            Console.WriteLine(v_TokenResponseAutreScope.Json);

            Console.WriteLine();
            Console.WriteLine("Nouvel appel à l'API, avec token (scope non autorisé) récupéré auprès du serveur d'identité");

            var v_ClientAutreScope = new HttpClient();
            v_ClientAutreScope.SetBearerToken(v_TokenResponseAutreScope.AccessToken);

            var v_ResponseAutreScope = await v_ClientAutreScope.GetAsync("http://localhost:5001/identity");
            if (!v_ResponseAutreScope.IsSuccessStatusCode)
            {
                Console.WriteLine(v_ResponseAutreScope.StatusCode);
            }
            else
            {
                // On ne passera jamais ici
                var content = await v_ResponseAutreScope.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.WriteLine();
            Console.WriteLine("Récupération d'un token (scope autorisé) auprès du serveur d'identité");

            var v_TokenResponseBonScope = await v_DiscoveryClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = v_Discovery.TokenEndpoint,

                ClientId = "console",
                ClientSecret = "secret",
                Scope = "monApi"
            });

            if (v_TokenResponseBonScope.IsError)
            {
                Console.WriteLine(v_TokenResponseBonScope.Error);
                return;
            }

            Console.WriteLine(v_TokenResponseBonScope.Json);

            Console.WriteLine();
            Console.WriteLine("Nouvel appel à l'API, avec token (scope autorisé) récupéré auprès du serveur d'identité");

            var v_ClientBonScope = new HttpClient();
            v_ClientBonScope.SetBearerToken(v_TokenResponseBonScope.AccessToken);

            var v_ResponseBonScope = await v_ClientBonScope.GetAsync("http://localhost:5001/identity");
            if (!v_ResponseBonScope.IsSuccessStatusCode)
            {
                Console.WriteLine(v_ResponseBonScope.StatusCode);
            }
            else
            {
                var content = await v_ResponseBonScope.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadKey();
        }
    }
}