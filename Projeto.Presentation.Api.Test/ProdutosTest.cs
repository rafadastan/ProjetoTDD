using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Projeto.Infra.Data.Entities;
using Projeto.Presentation.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Projeto.Presentation.Api.Test
{
    /// <summary>
    /// Classe de teste para o ENDPOINT 'api/produtos'
    /// </summary>
    public class ProdutosTest
    {
        //atributo
        private readonly HttpClient _httpclient;

        //construtor -> ctor + 2x[tab]
        public ProdutosTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>());

            _httpclient = server.CreateClient();
        }

        [Fact]
        public async Task Produtos_Post_ReturnsOk()
        {
            await CriarProduto();
        }

        [Fact]
        public async Task Produtos_Put_ReturnsOk()
        {
            var produto = await CriarProduto();

            var model = new ProdutoEdicaoModel
            {
                Id = produto.Id,
                Nome = "Produto teste atualizado",
                Preco = 2000,
                Quantidade = 20
            };

            var request = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            var respose = await _httpclient.PutAsync("api/produtos", request);
            respose.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = string.Empty;
            using (var content = respose.Content) { responseContent += content.ReadAsStringAsync().Result; }

            var result = JsonConvert.DeserializeObject<ProdutoReturnsOk>(responseContent);
            result.message.Should().Be("Produto atualizado com sucesso");
        }

        [Fact]
        public async Task Produtos_Delete_ReturnsOk()
        {
            var produto = await CriarProduto();

            var respose = await _httpclient.DeleteAsync("api/produtos/" + produto.Id);
            respose.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = string.Empty;
            using (var content = respose.Content) { responseContent += content.ReadAsStringAsync().Result; }

            var result = JsonConvert.DeserializeObject<ProdutoReturnsOk>(responseContent);
            result.message.Should().Be("Produto excluido com sucesso");
        }

        [Fact]
        public async Task Produtos_GetAll_ReturnsOk()
        {
            var produto = await CriarProduto();

            var respose = await _httpclient.GetAsync("api/produtos/");
            respose.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = string.Empty;
            using (var content = respose.Content) { responseContent += content.ReadAsStringAsync().Result; }

            var result = JsonConvert.DeserializeObject<List<ProdutoConsultaModel>>(responseContent);

            //critério do teste -> verificar se a lista contem o produto cadastrado..
            result.Should().Contain(p => p.Id.Equals(produto.Id));
        }

        [Fact]
        public async Task Produtos_GetById_ReturnsOk()
        {
            var produto = await CriarProduto();

            var respose = await _httpclient.GetAsync("api/produtos/" + produto.Id);
            respose.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = string.Empty;
            using (var content = respose.Content) { responseContent += content.ReadAsStringAsync().Result; }

            var result = JsonConvert.DeserializeObject<ProdutoConsultaModel>(responseContent);

            //critério do teste -> verificar se a lista contem o produto cadastrado..
            result.Id.Should().Be(produto.Id);
        }

        /// <summary>
        /// Método para cadastrar um produto na API
        /// </summary>
        /// <returns>Dados obtidos da API após a realização do cadastro</returns>
        private async Task<Produto> CriarProduto()
        {
            var model = new ProdutoCadastroModel
            {
                Nome = "Produto teste",
                Preco = 1000,
                Quantidade = 10
            };

            var request = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            var respose = await _httpclient.PostAsync("api/produtos", request);

            //utilizando a biblioteca do FluentAssertions para verificar o retorno da API..
            respose.StatusCode.Should().Be(HttpStatusCode.OK);

            //obtendo o resultado retornado pela API..
            var responseContent = string.Empty;
            using (var content = respose.Content) { responseContent += content.ReadAsStringAsync().Result; }

            var result = JsonConvert.DeserializeObject<ProdutoReturnsOk>(responseContent);

            //comparando a mensagem obtida após a realização do cadastro
            result.message.Should().Be("Produto cadastrado com sucesso");

            //retorno..
            return result.produto;
        }
    }

    /// <summary>
    /// Modelo de dados para o retorno de sucesso da API
    /// em métodos do tipo POST, PUT ou DELETE
    /// </summary>
    public class ProdutoReturnsOk
    {
        public string message { get; set; }
        public Produto produto { get; set; }
    }
}
