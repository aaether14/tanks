using System;

namespace Tanks.Domain.DomainModels;

public class Tank : IEntity<Guid>
{
    public Guid Id { get; set; }
    public int Health { get; set; }
    public int AttackMin { get; set; }
    public int AttackMax { get; set; }
    public int DefenseMin { get; set; }
    public int DefenseMax { get; set; }
    public int Range { get; set; }

    public bool Alive => Health > 0;

    public Tank(int health, int attackMin, int attackMax, int defenseMin, int defenseMax, int range)
    {
        if (health <= 0)
        {
            throw new ArgumentException("Cannot create a dead tank.");
        }
        if (attackMin < 0 || attackMax < 0 || attackMin > attackMax)
        {
            throw new ArgumentException("AttackMin has to be <= AttackMax. Both need to be >= 0.");
        }
        if (defenseMin < 0 || defenseMax < 0 || defenseMin > defenseMax)
        {
            throw new ArgumentException("DefenseMin has to be <= DefenseMax. Both need tp be >= 0.");
        }

        Id = Guid.NewGuid();
        Health = health;
        AttackMin = attackMin;
        AttackMax = attackMax;
        DefenseMin = defenseMin;
        DefenseMax = defenseMax;
    }

    public int RollDamage(Random random)
    {
        return random.Next(AttackMin, AttackMax + 1);
    }

    public void TakeDamage(int damage, Random random)
    {
        if (damage < 0)
        {
            throw new ArgumentException("Cannot take damage < 0.");
        }

        int rolledDefense = random.Next(DefenseMin, DefenseMax + 1);
        damage = Math.Max(0, damage - rolledDefense);
        Health = Math.Max(0, Health - damage);
    }

}