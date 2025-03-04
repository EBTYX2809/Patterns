namespace Patterns.Decorators.AttackUnit_withDecorator
{
    public class AttackUnit_withDecorator
    {
        static void Main()
        {
            Unit unit = new Unit(new BaseAttack());

            Console.WriteLine("=== Обычная атака ===");
            unit.Attack();
            Console.WriteLine();

            Console.WriteLine("=== Огонь + Яд ===");
            unit.SetAttack(new FireAttack(new PoisonAttack(new BaseAttack())));
            unit.Attack();
            Console.WriteLine();

            Console.WriteLine("=== Огонь + Крит ===");
            unit.SetAttack(new FireAttack(new CriticalAttack(new BaseAttack())));
            unit.Attack();
            Console.WriteLine();

            Console.WriteLine("=== Огонь + Яд + Крит ===");
            unit.SetAttack(new FireAttack(new PoisonAttack(new CriticalAttack(new BaseAttack()))));
            unit.Attack();
            Console.WriteLine();

            Console.WriteLine("=== Обычная атака ===");
            //unit.SetAttack(new BaseAttack());
            //unit.SetAttack(new FireAttack(null));
            unit.Attack();
        }
    }

    public interface IAttack
    {
        void Attack();
    }

    public class BaseAttack : IAttack
    {
        public void Attack()
        {
            Console.Write("Base attack.");
        }
    }

    public abstract class AttackDecorator : IAttack
    {
        protected IAttack wrappedAttack;
        public AttackDecorator(IAttack attack)
        {
            wrappedAttack = attack;
        }

        public virtual void Attack()
        {
            wrappedAttack?.Attack();
        }
    }

    public class FireAttack : AttackDecorator
    {
        public FireAttack(IAttack attack) : base(attack) { }
        public override void Attack()
        {
            wrappedAttack?.Attack();
            Console.Write(" Fire attack.");
        }
    }

    public class PoisonAttack : AttackDecorator
    {
        public PoisonAttack(IAttack attack) : base(attack) { }
        public override void Attack()
        {
            wrappedAttack?.Attack();
            Console.Write(" Poison attack.");
        }
    }

    public class CriticalAttack : AttackDecorator
    {
        public CriticalAttack(IAttack attack) : base(attack) { }
        public override void Attack()
        {
            wrappedAttack?.Attack();
            Console.Write(" Critical attack!");
        }
    }

    public class Unit
    {
        private IAttack attack;
        public Unit(IAttack _attack)
        {
            attack = _attack;
        }

        public void SetAttack(IAttack newAttack)
        {
            attack = newAttack;
        }

        public void Attack()
        {
            attack?.Attack();
            Console.WriteLine();
            attack = null;
        }
    }
}
