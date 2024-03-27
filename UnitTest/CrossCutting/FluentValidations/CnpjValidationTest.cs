using CrossCutting.FluentValidations.Cnpj;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest.CrossCutting.FluentValidations
{
    public class CnpjValidationTest
    {
        [TestCase("68.159.892/0001-01")]
        public void Should_be_true(string cnpj)
        {
            CnpjValidation.IsValid(cnpj).Should().BeTrue();
        }
        
        [TestCase("")]
        [TestCase("a")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("68159892000101")]
        [TestCase("68.159.892/0001-0")]
        [TestCase("68.159.892/0001-0a")]
        [TestCase("68.159.892/0001-0a1")]
        [TestCase("68.159.892/0001-0a11")]
        public void Should_be_false(string cnpj)
        {
            CnpjValidation.IsValid(cnpj).Should().BeFalse();
        }
    }
}