using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Projeto.Presentation.Api.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Projeto.Presentation.Api.Test
{
    public class ProdutosTest
    {
        private readonly HttpClient _httpClient;

        public ProdutosTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var server = new TestServer(new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>());

            _httpClient = server.CreateClient();
        }

        [Fact]
        public async Task Produtos_Post_ReturnsOk()
        {
            var model = new ProdutoCadastroModel
            {
                Nome = "Produto teste",
                Preco = 1000,
                Quantidade = 10
            };

            var request = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/produtos", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "Não implementado.")]
        public void Produtos_Put_ReturnsOk()
        {

        }

        [Fact(Skip = "Não implementado.")]
        public void Produtos_Delete_ReturnsOk()
        {

        }

        [Fact(Skip = "Não implementado.")]
        public void Produtos_GetAll_ReturnsOk()
        {

        }

        [Fact(Skip = "Não implementado.")]
        public void Produtos_GetById_ReturnsOk()
        {

        }
    }
}
