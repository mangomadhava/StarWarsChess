public class Monster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Range { get; set; }
    public int Movement { get; set; }
    public int OwnerId { get; set; }

    public Monster(Monster monster, int OwnerId)
    {
        Name = monster.Name;
        Health = monster.Health;
        Attack = monster.Attack;
        Range = monster.Range;
        Movement = monster.Movement;
        this.OwnerId = OwnerId;
    }

    // Returns true if inflicting damage kills the monster, false otherwise.
    public bool InflictDamage(int damage)
    {
        Health -= damage;
        return Health <= 0;
    }
}