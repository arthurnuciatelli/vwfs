using System;

namespace VWFS.Proposals.Application.DTOs;

public class CreateProposalResponseDto
{
    public Guid ProposalId { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}
