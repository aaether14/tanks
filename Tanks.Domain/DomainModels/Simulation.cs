using System;

namespace Tanks.Domain.DomainModels;

public class Simulation : IEntity<Guid>
{
     public Guid Id { get; set; }
     
}