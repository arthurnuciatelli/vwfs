using System;
using VWFS.Proposals.Application.DTOs;

namespace VWFS.Proposals.Application.Interfaces
{
    public interface IProposalAppService
    {
        Task<CreateProposalResponseDto> CreateProposalAsync(CreateProposalRequestDto dto);
    }
}
