namespace Tanks.Domain.DomainModels;

public interface IEntity<TId>
{

    TId Id { get; }

}