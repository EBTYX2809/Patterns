namespace Patterns.Decorators.ArmorModifications_withoutDecorator
{
    public class ArmorModifications_withoutDecorator
    {
        static void Main()
        {
            Unit unit = new Unit();

            Console.WriteLine("=== Удар принимает обычная броня ===");
            unit.AddArmor(new BaseArmor());
            unit.TakeHit(100);

            Console.WriteLine("=== Удар принимает зачарованая броня(без магическое слоя) ===");
            unit.AddArmor(new EnchantedArmor());
            unit.TakeHit(100);

            Console.WriteLine("=== Удар принимает зачарованая броня(с магическим слоем) ===");
            unit.AddArmor(new EnchantedArmor(_hasMagicLayer: true));
            unit.TakeHit(100);

            Console.WriteLine("=== Удар принимает энергетический щит ===");
            unit.AddArmor(new EnergyShield());
            unit.TakeHit(100);

            Console.WriteLine("=== Удар принимает зачарования броня с шипами ===");
            unit.AddArmor(new List<IArmor>() { new EnchantedArmor(), new ThornArmor() });
            unit.TakeHit(100);

            Console.WriteLine("=== Удар принимает энергетический щит с шипами ===");
            unit.AddArmor(new List<IArmor>() { new ThornArmor(), new EnergyShield() });
            unit.TakeHit(100);

            Console.WriteLine("=== Удар принимает зачарованая броня с энергетическим щитом и шипами ===");
            unit.AddArmor(new List<IArmor>() { new EnergyShield(), new ThornArmor(), new EnchantedArmor() });
            unit.TakeHit(100);
        }
    }

    public abstract class IArmor
    {
        protected static List<IArmor> armorModifications = new List<IArmor>();
        public virtual void TakeHit(ref int damage) { }

        public void ClearModifications()
        {
            armorModifications.Clear();
        }
    }

    public class Unit
    {
        private List<IArmor> armors = new List<IArmor>();
        public Unit() { }

        public void AddArmor(IArmor armorType)
        {
            armors.Add(armorType);
        }

        public void AddArmor(List<IArmor> armorTypes)
        {
            armors.AddRange(armorTypes);
        }

        public void TakeHit(int damage)
        {
            // Тут я так и не разобрался как не вызывать метод базовой защиты,
            // если есть другие типы брони которые вмете должны принять урон
            if (!armors.Any(armor => armor is BaseArmor))
            {
                armors.Add(new BaseArmor());
            }

            foreach (var armor in armors)
            {
                if (armor is BaseArmor && armors.Count > 1)
                {
                    break;
                }
                armor.TakeHit(ref damage);
            }

            Console.WriteLine();

            armors.First().ClearModifications();
            // Сделал для удобства очистку атак сразу
            armors.Clear();
        }
    }

    public class BaseArmor : IArmor
    {
        // Ну вообще рил должно быть protected
        public int defense = 10;
        public BaseArmor()
        {
            armorModifications.Add(this);
        }

        public override void TakeHit(ref int damage)
        {
            damage -= defense;
            Console.WriteLine($"Base armor deflected {defense} damage. Unit got {damage} damage.");
        }
    }

    public class EnchantedArmor : IArmor
    {
        private bool hasMagicLayer;

        public EnchantedArmor(bool _hasMagicLayer = false)
        {
            armorModifications.Add(this);
            hasMagicLayer = _hasMagicLayer;
        }
        public override void TakeHit(ref int damage)
        {
            if (armorModifications.First() is BaseArmor baseArmor) // Первый элемент всегда BaseArmor
            {
                if (!hasMagicLayer)
                {
                    damage -= baseArmor.defense * 2;
                    Console.WriteLine($" Enchanted armor deflected {baseArmor.defense * 2} damage. Unit got {damage} damage.");
                }
                else baseArmor.TakeHit(ref damage);
            }
        }
    }

    public class ThornArmor : IArmor
    {
        public ThornArmor()
        {
            armorModifications.Add(this);
        }
        public override void TakeHit(ref int damage)
        {
            if (armorModifications.Any(armor => armor is EnchantedArmor))
            {
                Console.WriteLine(" Thorn damaged attacker.");
            }
        }
    }

    public class EnergyShield : IArmor
    {
        public EnergyShield()
        {
            armorModifications.Add(this);
        }
        public override void TakeHit(ref int damage)
        {
            if (armorModifications.Any(armor => armor is ThornArmor))
            {
                Console.WriteLine(" Energy explosion by thorns.");
            }

            damage /= 2;
            Console.WriteLine($" Energy armor deflected {damage} damage. Unit got {damage} damage.");
        }
    }
}
