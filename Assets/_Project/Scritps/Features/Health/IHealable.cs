
public interface IHealable
{
    bool CanHeal { get; }
    void Heal(float value);

    bool IsDead { get; }
    bool IsDamaged { get; }
    float HealthPercent { get; }
    void SetDamage(bool value);
}

