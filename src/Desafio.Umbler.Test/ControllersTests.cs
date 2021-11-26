using Desafio.Umbler.Controllers;
using Desafio.Umbler.Dominio;
using Desafio.Umbler.Infra.Data.Contexto;
using Desafio.Umbler.Models;
using DnsClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Umbler.Service.Interfaces;
using Desafio.Umbler.Service;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Umbler.Repository.Interfaces;
using Desafio.Umbler.Repository.Repositories;
using Desafio.Umbler.Mapping;
using Desafio.Umbler.Dominio.Model;
using Whois.NET;

namespace Desafio.Umbler.Test
{
    [TestClass]
    public class ControllersTest
    {
        private readonly IDomainService _service;
        private readonly IMapper _mapper;

        public ControllersTest()
        {
            //arrange
            var services = new ServiceCollection();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Find_searches_url"));

            services.AddTransient<IDomainService, DomainService>();
            services.AddTransient<IWhoisClientService, WhoisClientService>();
            services.AddTransient<IDomainRepository, DomainRepository>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
            services.AddSingleton(_mapper);

            var serviceProvider = services.BuildServiceProvider();
            _service = serviceProvider.GetService<IDomainService>();
        }

        [TestMethod]
        public void Home_Index_returns_View()
        {
            //arrange 
            var controller = new HomeController();

            //act
            var response = controller.Index();
            var result = response as ViewResult;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Home_Error_returns_View_With_Model()
        {
            //arrange 
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var response = controller.Error();
            var result = response as ViewResult;
            var model = result.Model as ErrorViewModel;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Domain_In_Database()
        {
            //arrange 
            var controller = new DomainController(_service, _mapper);
            string registro = "test.com";
            //act
            var response = controller.Get(registro);
            var result = response.Result as OkObjectResult;
            var obj = result.Value as DomainModel;
            Assert.AreEqual(obj.Name, registro);
        }

        [TestMethod]
        public void Domain_In_Database_Inserir_Dominio()
        {
            //arrange 
            var domain = new Domain
            {
                Ip = "192.168.0.2",
                Name = "test.com",
                UpdatedAt = DateTime.Now,
                HostedAt = "teste",
                Ttl = 10,
                WhoIs = "Ns.umbler.com"
            };

            var controller = new DomainController(_service, _mapper);
            //act
            var response = controller.InserirDominio(domain);
            var result = response.Result as OkObjectResult;
            var obj = result.Value as DomainModel;
            Assert.AreEqual(obj.Name, domain.Name);
            Assert.AreEqual(obj.Ip, domain.Ip);
            Assert.AreEqual(obj.HostedAt, domain.HostedAt);
        }

        [TestMethod]
        public void Domain_Not_In_Database()
        {
            //arrange 
            var controller = new DomainController(_service, _mapper);

            //act
            var response = controller.Get("test.com");
            var result = response.Result as OkObjectResult;
            var obj = result.Value as DomainModel;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void Domain_Moking_LookupClient()
        {
            //arrange 
            var lookupClient = new Mock<ILookupClient>();
            string domainName = "globo.com";
            var dnsResponse = new Mock<IDnsQueryResponse>();

            lookupClient.Setup(l => l.Query(domainName, QueryType.ANY, QueryClass.IN)).Returns(dnsResponse.Object);

            //arrange 
            var controller = new DomainController(_service, _mapper);

            //act
            var response = controller.Get(domainName);
            var result = response.Result as OkObjectResult;
            var obj = result.Value as DomainModel;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void Domain_Moking_WhoisClient()
        {
            //arrange
            var whoisClient = new Mock<IWhoisClientService>();
            var domainName = "globo.com";
            var WhoisResponse = new WhoisResponse { OrganizationName = domainName };
            var resultado = whoisClient.Setup(x => x.ObterWhois(domainName)).Returns(Task.FromResult(
                WhoisResponse
                ));

            ////arrange 
            var controller = new TestController();

            //act
            var response = controller.Get(domainName, WhoisResponse);
            var result = response.Result as OkObjectResult;
            var obj = result.Value as Domain;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void Domain_GetErrorBadRequest()
        {
            string domain = string.Empty;
            ////arrange 
            var controller = new DomainController(_service, _mapper);

            //act
            var response = controller.GetErrorBadRequest(domain);
            var result = response.Result as BadRequestObjectResult;
            Assert.IsTrue(result.StatusCode == 400);
        }

        [TestMethod]
        public void GetErrorNotFound()
        {
            string domain = string.Empty;
            ////arrange 
            var controller = new DomainController(_service, _mapper);

            //act
            var response = controller.GetErrorNotFound(domain);
            var result = response.Result as NotFoundResult;
            Assert.IsTrue(result.StatusCode == 404);
        }
    }
}