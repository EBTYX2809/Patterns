namespace Patterns.Decorators.ArmorModifications_withDecorator
{
    class ArmorModifications_withDecorator
    {
        static void Main()
        {
            Console.WriteLine("=== Обычная броня ===");
            IArmor armor = new BaseArmor();
            armor.TakeHit(30);

            armor = null;
            Console.WriteLine("=== Обычная броня c энергетическим щитом, шипами и зачарованием ===");
            armor = new EnchantedArmor(new ThornArmor(new EnergyShield(new BaseArmor())));
            armor.TakeHit(30);

            /*Console.WriteLine("=== Обычная броня ===");
            IArmor armor = new BaseArmor();
            armor.TakeHit(30);

            Console.WriteLine("\n=== Зачарованная броня ===");
            armor = new EnchantedArmor(armor);
            armor.TakeHit(30);

            Console.WriteLine("\n=== Шипастая зачарованная броня ===");
            armor = new ThornArmor(armor);
            armor.TakeHit(30);

            Console.WriteLine("\n=== Энергетический щит поверх шипов ===");
            armor = new EnergyShield(armor);
            armor.TakeHit(30);*/
        }
    }

    // Базовый интерфейс брони
    public interface IArmor
    {
        int GetDefense(); // Получить защиту
        void TakeHit(int damage); // Получить урон
    }

    // Базовый класс брони
    public class BaseArmor : IArmor
    {
        protected int Defense = 10;

        public virtual int GetDefense() => Defense;

        public virtual void TakeHit(int damage)
        {
            Console.WriteLine($"Броня поглотила {GetDefense()} урона. Осталось: {Math.Max(0, damage - GetDefense())}");
        }
    }

    // Базовый класс декоратора
    public abstract class ArmorDecorator : IArmor
    {
        protected IArmor wrappedArmor;

        public ArmorDecorator(IArmor armor)
        {
            wrappedArmor = armor;
        }

        public virtual int GetDefense() => wrappedArmor.GetDefense();

        public virtual void TakeHit(int damage) => wrappedArmor.TakeHit(damage);
    }

    // Декоратор "Зачарованная броня" — удваивает защиту, но только если поверх нет магии
    public class EnchantedArmor : ArmorDecorator
    {
        private bool hasMagicLayer;

        public EnchantedArmor(IArmor armor, bool hasMagicLayer = false) : base(armor)
        {
            this.hasMagicLayer = hasMagicLayer;
        }

        public override int GetDefense()
        {
            // Если поверх уже есть другой магический эффект — не удваиваем
            return hasMagicLayer ? base.GetDefense() : base.GetDefense() * 2;
        }

        public override void TakeHit(int damage)
        {
            Console.WriteLine("✨ Магическая броня активирована!");
            base.TakeHit(damage);
        }
    }

    // Декоратор "Шипастая броня" — наносит урон атакующему, если есть зачарование
    public class ThornArmor : ArmorDecorator
    {
        public ThornArmor(IArmor armor) : base(armor) { }

        public override void TakeHit(int damage)
        {
            base.TakeHit(damage);

            if (wrappedArmor is EnchantedArmor)
            {
                Console.WriteLine("🦔 Шипы нанесли урон атакующему!");
            }
        }
    }

    // Декоратор "Энергетический щит" — поглощает урон, но если есть шипы — взрыв!
    public class EnergyShield : ArmorDecorator
    {
        public EnergyShield(IArmor armor) : base(armor) { }

        public override void TakeHit(int damage)
        {
            Console.WriteLine("⚡ Энергетический щит активирован!");

            if (wrappedArmor is ThornArmor)
            {
                Console.WriteLine("💥 Взрыв энергии из-за шипов!");
            }

            base.TakeHit((int)(damage * 0.5)); // Щит снижает урон вдвое
        }
    }
}
