using VWFS.Proposals.Application.DTOs;
using VWFS.Proposals.Application.Interfaces;
using VWFS.Proposals.Domain;
using VWFS.Proposals.Infrastructure.Persistence;

namespace VWFS.Proposals.Application.Services;

public class ProposalAppService : IProposalAppService
{
    private readonly IProposalRepository _repository;

    public ProposalAppService(IProposalRepository repository) 
    {
        _repository = repository;
    }

    public async Task<CreateProposalResponseDto> CreateProposalAsync(CreateProposalRequestDto dto)
    {
        if (dto.Installments <= 0 || dto.Price <= 0)
            return new CreateProposalResponseDto
            {
                ProposalId = Guid.Empty,
                Status = "Error",
                Message = "Parâmetros inválidos"
            };

        var proposal = new Proposal(dto.CustomerId, dto.Vehicle, dto.Year,
                                    dto.Price, dto.DownPayment, dto.Installments,
                                    dto.MonthlyInterest);

        await _repository.AddAsync(proposal);

        return new CreateProposalResponseDto
        {
            ProposalId = proposal.Id,
            Status = "Success",
            Message = "Proposta criada com sucesso"
        };
    }
}
