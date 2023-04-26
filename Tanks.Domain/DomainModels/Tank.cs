using System;

namespace Tanks.Domain.DomainModels;

public class Tank
{
    public Guid Id { get; private init; }
    public uint Health { get; private set; }
    public uint AttackMin { get; private init; }
    public uint AttackMax { get; private init; }
    public uint DefenseMin { get; private init; }
    public uint DefenseMax { get; private init; }

    private Tank() {}

    public static Tank Create(uint health, uint attackMin, uint attackMax, uint defenseMin, uint defenseMax)
    {
        if (health <= 0)
        {
            throw new ArgumentException("Cannot create dead tank.");
        }
        if (attackMin > attackMax)
        {
            throw new ArgumentException("AttackMin has to be <= AttackMax.");
        }
        if (defenseMin > defenseMax)
        {
            throw new ArgumentException("DefenseMin has to be <= DefenseMax.");
        }

        return new Tank()
        {
            Id = Guid.NewGuid(),
            Health = health, 
            AttackMin = attackMin,
            AttackMax = attackMax, 
            DefenseMin = defenseMin,
            DefenseMax = defenseMax
        };
    }

    public void TakeDamage(uint damage)
    {
        Health -= Math.Min(Health, damage);
    }
}