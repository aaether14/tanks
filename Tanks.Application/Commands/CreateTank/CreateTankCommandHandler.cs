using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Commands.CreateTank;

public class CreateTankCommandHandler : IRequestHandler<CreateTankCommand, Guid>
{

    private readonly IRepository<Tank, Guid> _tankRepository;
    private readonly IMapper _mapper;

    public CreateTankCommandHandler(IRepository<Tank, Guid> tankRepository, IMapper mapper)
    {
        _tankRepository = tankRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateTankCommand request, CancellationToken cancellationToken)
    {
        // First, create the tank.
        Tank tank = _mapper.Map<Tank>(request);
        
        // Then, add to repository.
        await _tankRepository.AddAsync(tank);

        // Last, return the the new tank's id.
        return tank.Id;
    }
}
