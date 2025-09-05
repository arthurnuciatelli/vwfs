using System;
using VWFS.Customers.Domain.Entities;
using BuildingBlocksDomain = VWFS.BuildingBlocks.Domain;
using Xunit;

namespace VWFS.Customers.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void CriarPessoaFisica_ComCpfValido_DeveCriar()
        {
            // Arrange
            string nome = "João Silva";
            string cpfValido = "12345678909"; // substitua por CPF válido para testes
            var tipo = BuildingBlocksDomain.Enum.CustomerType.PessoaFisica;

            // Act
            var customer = new Customer(nome, cpfValido, tipo);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(nome, customer.Name);
            Assert.Equal(cpfValido, customer.Document);
            Assert.Equal(tipo, customer.Type);
        }

        [Fact]
        public void CriarPessoaFisica_ComCpfInvalido_DeveLancarExcecao()
        {
            // Arrange
            string nome = "João Silva";
            string cpfInvalido = "11111111111";
            var tipo = BuildingBlocksDomain.Enum.CustomerType.PessoaFisica;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Customer(nome, cpfInvalido, tipo));
            Assert.Contains("CPF inválido", ex.Message);
        }

        [Fact]
        public void CriarPessoaJuridica_ComCnpjValido_DeveCriar()
        {
            // Arrange
            string nome = "Empresa Ltda";
            string cnpjValido = "29615716000185"; // substitua por CNPJ válido
            var tipo = BuildingBlocksDomain.Enum.CustomerType.PessoaJuridica;

            // Act
            var customer = new Customer(nome, cnpjValido, tipo);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(nome, customer.Name);
            Assert.Equal(cnpjValido, customer.Document);
            Assert.Equal(tipo, customer.Type);
        }

        [Fact]
        public void CriarPessoaJuridica_ComCnpjInvalido_DeveLancarExcecao()
        {
            // Arrange
            string nome = "Empresa Ltda";
            string cnpjInvalido = "11111111111111";
            var tipo = BuildingBlocksDomain.Enum.CustomerType.PessoaJuridica;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Customer(nome, cnpjInvalido, tipo));
            Assert.Contains("CNPJ inválido", ex.Message);
        }

        [Fact]
        public void CriarCustomer_SemNome_DeveLancarExcecao()
        {
            // Arrange
            string nomeVazio = "";
            string cpfValido = "12345678909";
            var tipo = BuildingBlocksDomain.Enum.CustomerType.PessoaFisica;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Customer(nomeVazio, cpfValido, tipo));
            Assert.Contains("O nome é obrigatório", ex.Message);
        }
    }
}
