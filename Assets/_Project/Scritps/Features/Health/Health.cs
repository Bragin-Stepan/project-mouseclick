public class Health
    {
        private readonly float _maxHealth;
        private float _health;

        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            Initialize();
        }

        public bool IsDead { get; private set; }
        public bool IsDamaged { get; private set; }
        
        public float HealthPercent => _health / _maxHealth;
        public float CurrentHealth => _health;
        public bool IsFullHealth => _health >= _maxHealth;
        
        public void Increase(float value)
        {
            if (IsDead || value < 0)
                return;

            _health += value;

            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        public void Reduce(float value)
        {
            if (IsDead || value < 0)
                return;

            _health -= value;
            
            if (_health <= 0)
            {
                _health = 0;
                IsDead = true;
                return;
            }

            SetDamage(true);
        }

        public void Initialize()
        {
            _health = _maxHealth;
            IsDead = false;
        }

        public void SetDamage(bool value) => IsDamaged = value;
    }
