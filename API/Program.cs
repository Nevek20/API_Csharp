using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    //Antes de criar a API, instalar no pacote NuGet: Newtonsoft.Json.
    //HttpClient: Usado para fazer requisições HTTP, como GET, POST, PUT, DELETE.
    //GetAsync: Método assíncrono usado para fazer requisição GET.
    //ReadAsStringAsync: Lê a resposta da API como uma string.
    //JsonConvert.DeserializeObject: Usado para converter o Json da resposta em um Objeto C#

    //Quando vc marca um método como async, o compilador permite o uso de await dentro dele, que é a palavra que indica onde o codigo deve esperar por uma operação assincrona.
    //Quando usa o VOID: ele não retorna nenhum valor, ele apenas executa a ação de imprimir os dados, sempre depende de algum recurso para exibir algo. EX: "Console.Write"
    internal class Program
    {
        static async Task Main(string[] args)
        {
        //Criação da instância do HTPPCLIENT
        HttpClient client = new HttpClient();

            //A URL da API
            string apiUrl = "https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL";

            try
            {
                //Envia uma requisição GET para a API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                //Verifica se a requisição foi um sucesso
                if (response.IsSuccessStatusCode)
                {
                    //lê o conteúdo da resposta como string
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    //Converte JSON para C#
                    var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                    Console.WriteLine("Resposta da API:");
                    
                    //Exibir o resultado
                    //Console.WriteLine(jsonResult);

                    foreach ( var produto in jsonObject )
                    {
                        var usdBrl = jsonObject["USDBRL"];
                        Console.WriteLine($"\n{usdBrl["name"]}");
                        Console.WriteLine($"Alta: {usdBrl["high"]}");
                        Console.WriteLine($"Baixa: {usdBrl["low"]}");
                        Console.WriteLine($"Última cotação: {usdBrl["bid"]}");
                    }
                }
                else
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                client.Dispose();
            }

        }
    }
}