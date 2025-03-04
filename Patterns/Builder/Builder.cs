namespace Patterns.Builder
{
    using System;

    // === ОПИСАНИЕ ПРОДУКТА ===
    public class Mech
    {
        public string Frame { get; set; }
        public string WeaponSystem { get; set; }
        public string DefenseSystem { get; set; }
        public string AI { get; set; }
        public string PowerCore { get; set; }

        public void ShowSpecs()
        {
            Console.WriteLine($"Mech Specifications:\nFrame: {Frame}\nWeapon: {WeaponSystem}\nDefense: {DefenseSystem}\nAI: {AI}\nPowerCore: {PowerCore}");
        }
    }

    // === АБСТРАКТНЫЙ СТРОИТЕЛЬ ===
    public abstract class MechBuilder
    {
        protected Mech mech = new Mech();

        public abstract void BuildFrame();
        public abstract void InstallWeapons();
        public abstract void InstallDefense();
        public abstract void InstallAI();
        public abstract void InstallPowerCore();

        public Mech GetMech() => mech;
    }

    // === КОНКРЕТНЫЕ СТРОИТЕЛИ ===
    public class HeavyMechBuilder : MechBuilder
    {
        public override void BuildFrame()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Assembling reinforced titan-alloy frame...");
            mech.Frame = "Titanium Frame";
        }

        public override void InstallWeapons()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Mounting twin plasma cannons...");
            mech.WeaponSystem = "Twin Plasma Cannons";
        }

        public override void InstallDefense()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Activating energy shields...");
            mech.DefenseSystem = "Energy Shields";
        }

        public override void InstallAI()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Installing advanced battlefield AI...");
            mech.AI = "Advanced AI";
        }

        public override void InstallPowerCore()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Integrating nuclear fusion core...");
            mech.PowerCore = "Nuclear Fusion Core";
        }
    }

    public class ScoutMechBuilder : MechBuilder
    {
        // Тут в теории должна быть сложная логика
        public override void BuildFrame()
        {
            Console.WriteLine("Assembling lightweight carbon frame...");
            mech.Frame = "Carbon Fiber Frame";
        }

        public override void InstallWeapons()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Equipping rapid-fire laser guns...");
            mech.WeaponSystem = "Laser Guns";
        }

        public override void InstallDefense()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Installing stealth camouflage...");
            mech.DefenseSystem = "Adaptive Camouflage";
        }

        public override void InstallAI()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Installing high-speed tactical AI...");
            mech.AI = "Tactical AI";
        }

        public override void InstallPowerCore()
        {
            // Тут в теории должна быть сложная логика
            Console.WriteLine("Using high-efficiency battery pack...");
            mech.PowerCore = "Battery Pack";
        }
    }

    // === ДИРЕКТОР (УПРАВЛЯЕТ ПРОЦЕССОМ СБОРКИ) ===
    public class MechDirector
    {
        public Mech Construct(MechBuilder builder)
        {
            builder.BuildFrame();
            builder.InstallWeapons();
            builder.InstallDefense();
            builder.InstallAI();
            builder.InstallPowerCore();
            return builder.GetMech();
        }
    }

    // === ИСПОЛЬЗОВАНИЕ ===
    class Builder
    {
        static void Main()
        {
            MechDirector director = new MechDirector();

            Console.WriteLine("== Building Heavy Assault Mech ==");
            MechBuilder heavyBuilder = new HeavyMechBuilder();
            Mech heavyMech = director.Construct(heavyBuilder);
            heavyMech.ShowSpecs();

            Console.WriteLine("\n== Building Scout Mech ==");
            MechBuilder scoutBuilder = new ScoutMechBuilder();
            Mech scoutMech = director.Construct(scoutBuilder);
            scoutMech.ShowSpecs();
        }
    }

}
