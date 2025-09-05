using System;

namespace VWFS.Proposals.Application.DTOs;

public class CreateProposalRequestDto
{
    public Guid CustomerId { get; set; }
    public string Vehicle { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public decimal DownPayment { get; set; }
    public int Installments { get; set; }
    public decimal MonthlyInterest { get; set; }
}
