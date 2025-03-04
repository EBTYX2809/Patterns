namespace Patterns.Prototype
{
    public abstract class Object
    {
        public abstract Object Clone();
    }

    public class ConcreteObject : Object
    {
        public int Value { get; set; }

        public ConcreteObject(int value)
        {
            Value = value;
        }

        public override Object Clone()
        {
            return new ConcreteObject(this.Value);
        }
    }

    class Prototype
    {
        static void Main()
        {
            ConcreteObject prototype1 = new ConcreteObject(42);
            ConcreteObject clone = (ConcreteObject)prototype1.Clone();

            Console.WriteLine(prototype1.Value); // 42
            Console.WriteLine(clone.Value); // 42

            clone.Value = 100;
            Console.WriteLine(prototype1.Value); // 42
            Console.WriteLine(clone.Value); // 100
        }
    }

}
