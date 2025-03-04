namespace Patterns.Decorators.AttackUnit_withoutDecorator2
{
    public class AttackUnit_withoutDecorator2
    {
        static void Main()
        {
            Unit unit = new Unit();

            unit.Attack();
            Console.WriteLine();

            unit.AddAttack(new FireAttack());
            unit.Attack();

            unit.AddAttack(new PoisonAttack());
            unit.Attack();

            unit.AddAttack(new CriticalAttack());
            unit.Attack();

            unit.AddAttack(new List<IAttack>() { new FireAttack(), new PoisonAttack() });
            unit.Attack();

            unit.AddAttack(new List<IAttack>() { new CriticalAttack(), new PoisonAttack(), new FireAttack() });
            unit.Attack();
        }
    }

    public interface IAttack
    {
        public void Attack();
    }

    public class Unit
    {
        private List<IAttack> attacks = new List<IAttack>();
        public Unit() { }

        public void AddAttack(IAttack attackType)
        {
            attacks.Add(attackType);
        }

        public void AddAttack(List<IAttack> attackType)
        {
            attacks.AddRange(attackType);
        }

        public void Attack()
        {
            Console.Write("Base attack.");

            foreach (var attack in attacks)
            {
                attack.Attack();
            }

            Console.WriteLine();

            // Сделал для удобства очистку атак сразу
            attacks.Clear();
        }
    }

    public class FireAttack : IAttack
    {
        public void Attack()
        {
            Console.Write(" Fire attack.");
        }
    }

    public class PoisonAttack : IAttack
    {
        public void Attack()
        {
            Console.Write(" Poison attack.");
        }
    }

    public class CriticalAttack : IAttack
    {
        public void Attack()
        {
            Console.Write(" Critical attack.");
        }
    }
}
