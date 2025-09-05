using MediatR;

namespace VWFS.BuildingBlocks.Application;

public interface ICommand<out TResponse> : IRequest<TResponse> { }
public interface ICommand : IRequest { }