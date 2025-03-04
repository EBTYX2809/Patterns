namespace Patterns.Decorators.AttackUnit_withoutDecorator
{
    public class AttackUnit_withoutDecorator
    {
        static void Main()
        {
            Unit unit = new Unit();

            unit.Attack();
            Console.WriteLine();

            unit.SetAttack(new FireAttack());
            unit.Attack();

            unit.SetAttack(new PoisonAttack());
            unit.Attack();

            unit.SetAttack(new CriticalAttack());
            unit.Attack();
        }
    }

    public interface IAttack
    {
        public void Attack();
    }

    public class Unit
    {
        private IAttack attackType;
        public Unit() { }

        public void SetAttack(IAttack _attackType)
        {
            attackType = _attackType;
        }

        public void Attack()
        {
            Console.Write("Base attack. ");
            attackType?.Attack();
        }
    }

    public class FireAttack : IAttack
    {
        public void Attack()
        {
            Console.WriteLine("Fire attack.");
        }
    }

    public class PoisonAttack : IAttack
    {
        public void Attack()
        {
            Console.WriteLine("Poison attack.");
        }
    }

    public class CriticalAttack : IAttack
    {
        public void Attack()
        {
            Console.WriteLine("Critical attack.");
        }
    }
}
