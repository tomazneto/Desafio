using Desafio.Umbler.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Desafio.Umbler.Test
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void IsActive()
        {
            //arrange 
            var domain = new Domain { Ttl = 60, UpdatedAt = DateTime.Now };

            //assert
            Assert.IsFalse(domain.IsActive);
        }
    }
}
