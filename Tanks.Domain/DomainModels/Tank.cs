using System;

namespace Tanks.Domain.DomainModels;

public class Tank
{
    public Guid Id { get; set; }
    public uint Health { get; set; }
    public uint AttackMin { get; set; }
    public uint AttackMax { get; set; }
    public uint DefenseMin { get; set; }
    public uint DefenseMax { get; set; }

    public Tank(uint health, uint attackMin, uint attackMax, uint defenseMin, uint defenseMax)
    {
        if (health <= 0)
        {
            throw new ArgumentException("Cannot create a dead tank.");
        }
        if (attackMin > attackMax)
        {
            throw new ArgumentException("AttackMin has to be <= AttackMax.");
        }
        if (defenseMin > defenseMax)
        {
            throw new ArgumentException("DefenseMin has to be <= DefenseMax.");
        }

        Id = Guid.NewGuid();
        Health = health;
        AttackMin = attackMin;
        AttackMax = attackMax;
        DefenseMin = defenseMin;
        DefenseMax = defenseMax;
    }

    public void TakeDamage(uint damage)
    {
        Health -= Math.Min(Health, damage);
    }
}