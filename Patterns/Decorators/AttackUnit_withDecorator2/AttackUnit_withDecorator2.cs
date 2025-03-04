namespace Patterns.Decorators.AttackUnit_withDecorator2
{
    // 9. Симуляция боя
    class AttackUnit_withDecorator2
    {
        static void Main()
        {
            Player player = new Player();

            Console.WriteLine("=== Обычный враг ===");
            IEnemyAI enemy = new BasicEnemyAI();
            enemy.TakeAction(player);
            Console.WriteLine();

            Console.WriteLine("=== Агрессивный враг ===");
            IEnemyAI aggressiveEnemy = new AggressiveAI(new BasicEnemyAI());
            aggressiveEnemy.TakeAction(player);
            Console.WriteLine();

            Console.WriteLine("=== Ядовитый враг ===");
            IEnemyAI poisonousEnemy = new PoisonousAI(new BasicEnemyAI());
            poisonousEnemy.TakeAction(player);
            Console.WriteLine();

            Console.WriteLine("=== Разумный враг ===");
            IEnemyAI smartEnemy = new SmartAI(new BasicEnemyAI());
            smartEnemy.TakeAction(player);
            Console.WriteLine();

            Console.WriteLine("=== Комбинированный враг (Агрессивный + Ядовитый) ===");
            IEnemyAI ultimateEnemy = new PoisonousAI(new AggressiveAI(new BasicEnemyAI()));
            ultimateEnemy.TakeAction(player);
            Console.WriteLine();

            Console.WriteLine("=== Обороняющийся игрок ===");
            IEnemyAI defensiveEnemy = new DefensiveAI(new BasicEnemyAI());
            defensiveEnemy.TakeAction(player);
            Console.WriteLine();
        }
    }

    // 1. Интерфейс поведения ИИ
    public interface IEnemyAI
    {
        void TakeAction(Player player);
    }

    // 2. Базовое поведение: простой враг, который просто атакует
    public class BasicEnemyAI : IEnemyAI
    {
        public void TakeAction(Player player)
        {
            Console.WriteLine("Враг наносит обычную атаку.");
            player.TakeDamage(10);
        }
    }

    // 3. Абстрактный класс-декоратор для изменения поведения
    public abstract class EnemyAIDecorator : IEnemyAI
    {
        protected IEnemyAI _baseAI;

        public EnemyAIDecorator(IEnemyAI baseAI)
        {
            _baseAI = baseAI;
        }

        public virtual void TakeAction(Player player)
        {
            _baseAI.TakeAction(player);
        }
    }

    // 4. Агрессивный ИИ: атакует дважды
    public class AggressiveAI : EnemyAIDecorator
    {
        public AggressiveAI(IEnemyAI baseAI) : base(baseAI) { }

        public override void TakeAction(Player player)
        {
            base.TakeAction(player);
            Console.WriteLine("Враг становится агрессивным и атакует еще раз!");
            player.TakeDamage(10);
        }
    }

    // 5. Защитный ИИ: блокирует часть урона, если здоровье ниже 30%
    public class DefensiveAI : EnemyAIDecorator
    {
        public DefensiveAI(IEnemyAI baseAI) : base(baseAI) { }

        public override void TakeAction(Player player)
        {
            if (player.Health < 30)
            {
                Console.WriteLine("Враг замечает опасность и использует блок!");
                player.BlockNextAttack();
            }
            else
            {
                base.TakeAction(player);
            }
        }
    }

    // 6. Ядовитый ИИ: накладывает яд
    public class PoisonousAI : EnemyAIDecorator
    {
        public PoisonousAI(IEnemyAI baseAI) : base(baseAI) { }

        public override void TakeAction(Player player)
        {
            base.TakeAction(player);
            Console.WriteLine("Враг отравляет игрока! ☠️");
            player.ApplyPoison();
        }
    }

    // 7. Разумный ИИ: выбирает атаку или защиту в зависимости от здоровья игрока
    public class SmartAI : EnemyAIDecorator
    {
        public SmartAI(IEnemyAI baseAI) : base(baseAI) { }

        public override void TakeAction(Player player)
        {
            if (player.Health > 50)
            {
                Console.WriteLine("Враг решает атаковать игрока, пока он силен!");
                base.TakeAction(player);
            }
            else
            {
                Console.WriteLine("Враг замечает, что у игрока мало здоровья, и атакует сильнее!");
                player.TakeDamage(15);
            }
        }
    }

    // 8. Игрок (для тестирования)
    public class Player
    {
        public int Health { get; private set; } = 100;
        private bool _nextAttackBlocked = false;

        public void TakeDamage(int damage)
        {
            if (_nextAttackBlocked)
            {
                Console.WriteLine("Игрок блокирует атаку! Урон не нанесен.");
                _nextAttackBlocked = false;
            }
            else
            {
                Health -= damage;
                Console.WriteLine($"Игрок получает {damage} урона. Осталось {Health} HP.");
            }
        }

        public void BlockNextAttack()
        {
            _nextAttackBlocked = true;
            Console.WriteLine("Игрок готовится блокировать следующую атаку!");
        }

        public void ApplyPoison()
        {
            Console.WriteLine("Игрок отравлен и теряет 5 HP каждый ход.");
            Health -= 5;
        }
    }
}
