using Tanks.Domain.DomainModels;

namespace Tanks.Domain.Factories;

public interface IMapFactory
{
    Map Create(int width, int height);
}