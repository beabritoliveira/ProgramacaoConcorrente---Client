using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using CriptoClientConsoleApp; // O namespace configurado no .proto

class Program
{
    static async Task Main(string[] args)
    {
        // Cria o canal gRPC apontando para o servidor
        using var channel = GrpcChannel.ForAddress("https://localhost:7276"); 

        // Cria o cliente para o serviço Greeter
        var client = new Greeter.GreeterClient(channel);

        // Prepara a requisição para o método PostMoedas
        var moeda1 = new PostMoedaRequest
         {
             Id = 1,
             Name = "BitCoin",
             Valor = "100"
         };
         var moeda2 = new PostMoedaRequest
         {
             Id = 2,
             Name = "CeciCon",
             Valor = "10"
         };

         var response1 = await client.PostMoedasAsync(moeda1);
         var response2 = await client.PostMoedasAsync(moeda2);

         var carteira1 = new PostCarteiraRequest
         {
             NumeroConta = "001",
             IdMoeda = 2,
             CodResponsavel = "1234",
             NomeResponsavel = "Bia",
             QtdMoedas = "10",
             StatusConta = "Positivo",
         };

         var carteira2 = new PostCarteiraRequest
         {
             NumeroConta = "002",
             IdMoeda = 1,
             CodResponsavel = "1234",
             NomeResponsavel = "Bia",
             QtdMoedas = "5",
             StatusConta = "Positivo",
         };

         var carteira3 = new PostCarteiraRequest
         {
             NumeroConta = "004",
             IdMoeda = 1,
             CodResponsavel = "1234",
             NomeResponsavel = "Bia",
             QtdMoedas = "0",
             StatusConta = "Neutro",
         };

         var inseriCart = await client.PostCarteiraAsync(carteira1);
        var inseriCart2 = await client.PostCarteiraAsync(carteira2);
         var inseriCart3 = await client.PostCarteiraAsync(carteira3);
         Console.WriteLine(inseriCart3);   // Não retorna mensagem no controller

        var id_Delete = new DeleteCarteiraRequest {IdCarteira = "003"};
        var deletCart = await client.DeleteCarteiraAsync(id_Delete); // Não retorna nada no controller


       var atual = new ComprarRequest { IdConta = "001", Valor = "50" };
       var comprarMoeda = await client.ComprarMoedaAsync(atual);
       Console.WriteLine(comprarMoeda.ToString());


       var nov_trans = new TransfRequest { IdOrigem = "002", IdDestino = "003", Valor = "50" };
       var transf1 = await client.TransferirMoedaAsync(nov_trans); // Não retorna nada no controller

       var nov_val = new ValMoedaRequest { IdMoeda = 2, Valor = "8" };
       var atualizaMoeda = await client.AtualizarValMoedaAsync(nov_val); // Não retorna nada no controller

       var t = new Empty();
       var teste = await client.GetMoedasAsync(t);
       Console.WriteLine(teste.Moedas);

       var acharSaldo = new VisuSaldoRequest
       {
           Id = "001"
       };
       var encontrar = await client.SaldoCarteiraAsync(acharSaldo);
       Console.WriteLine(encontrar);

        var simCompra = new RequestSimulacao
        {
            IdOrigem = "001",
            IdDestino = "",
            Valor = "100"
        };
        var simCompra2 = new RequestSimulacao
        {
            IdOrigem = "001",
            IdDestino = "002",
            Valor = "50"
        };
        var sim1 = await client.SimularCompraTransferenciaAsync(simCompra);
        var sim2 = await client.SimularCompraTransferenciaAsync(simCompra2);
        Console.WriteLine(sim1);
        Console.WriteLine(sim2);
    }
}
