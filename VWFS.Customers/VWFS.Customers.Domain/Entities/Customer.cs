using System;
using VWFS.Customers.Domain.Validators;
using BuildingBlocksDomain = VWFS.BuildingBlocks.Domain;

namespace VWFS.Customers.Domain.Entities
{
    public enum CustomerType
    {
        PessoaFisica,
        PessoaJuridica
    }

    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty; // CPF ou CNPJ
        public BuildingBlocksDomain.Enum.CustomerType Type { get; set; }
        public DateTime BirthOrFoundationDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Customer(
                string name,
                string document,
                BuildingBlocksDomain.Enum.CustomerType type,
                DateTime birthOrFoundationDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome é obrigatório.", nameof(name));

            Name = name;
            Type = type;

            if (type == BuildingBlocksDomain.Enum.CustomerType.PessoaFisica)
            {
                if (!CpfValidator.IsValid(document))
                    throw new ArgumentException("CPF inválido.", nameof(document));
            }
            else
            {
                if (!CnpjValidator.IsValid(document))
                    throw new ArgumentException("CNPJ inválido.", nameof(document));
            }


            Document = document;
            BirthOrFoundationDate = birthOrFoundationDate;
            Id = Guid.NewGuid();
        }

        protected Customer() { }

        public bool IsLegalAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthOrFoundationDate.Year;

            // Ajusta caso ainda não tenha feito aniversário neste ano
            if (BirthOrFoundationDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }
    }
}
