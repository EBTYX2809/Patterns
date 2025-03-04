namespace Patterns.Singleton
{
    public class Singleton
    {
        static void Main(string[] args)
        {
            SingletonExample.Instance.Property = 10;

            SingletonExample.Instance.ExampleMethod();
        }
    }

    public interface IExample
    {
        void ExampleMethod();
    }

    public class SingletonExample : IExample
    {
        private static SingletonExample instance = new SingletonExample();
        public static SingletonExample Instance => instance;
        public int Property { get; set; }

        public void ExampleMethod()
        {
            Console.WriteLine(Property);
        }

        private SingletonExample() { }
    }
}
