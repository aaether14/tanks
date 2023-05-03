namespace Tanks.Application.Commands.CreateRandomMap;

public class CreateRandomMapConfig
{
    public const string SectionName = "CreateRandomMapConfig";

    public int MinWidth { get; init; }
    public int MinHeight { get; init; }

}