using MediatR;

namespace VWFS.BuildingBlocks.Application;

public interface IQuery<out TResponse> : IRequest<TResponse> { }