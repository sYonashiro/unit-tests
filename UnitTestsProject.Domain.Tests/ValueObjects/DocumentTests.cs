using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestsProject.Domain.Enums;
using UnitTestsProject.Domain.ValueObjects;

namespace UnitTestsProject.Domain.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        private const string VALID_CNPJ = "08.453.530/0001-05";
        private const string INVALID_CNPJ = "8120381";
        private const string VALID_CPF = "057.411.200-67";
        private const string INVALID_CPF = "6454";

        [TestMethod]
        [DataTestMethod]
        [DataRow(VALID_CNPJ)]
        [DataRow("08453530000105")]
        [DataRow("78.536.709/0001-01")]
        public void ShouldBeValidWhenCnpjIsValid(string cnpj)
        {
            var document = new Document(cnpj, EDocType.CNPJ);
            Assert.IsTrue(document.Valid);
        }

        [TestMethod]
        public void ShouldBeInvalidWhenCnpjIsInvalid()
        {
            var document = new Document(INVALID_CNPJ, EDocType.CNPJ);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(VALID_CPF)]
        [DataRow("301.003.480-68")]
        [DataRow("30100348068")]
        public void ShouldBeValidWhenCpfIsValid(string cpf)
        {
            var document = new Document(cpf, EDocType.CPF);
            Assert.IsTrue(document.Valid);
        }

        [TestMethod]
        public void ShouldBeInvalidWhenCpfIsInvalid()
        {
            var document = new Document(INVALID_CPF, EDocType.CPF);
            Assert.IsTrue(document.Invalid);
        }
    }
}
