namespace Patterns.Fabrics.UIElemetsCreator_fabric
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Запрос темы у пользователя
            Console.WriteLine("Выберите тему: light/dark.");
            string theme = Console.ReadLine()?.ToLower();

            // Выбор конкретной фабрики на основании введённой темы
            IUIFactory factory = theme switch
            {
                "light" => new LightUIFactory(),
                "dark" => new DarkUIFactory(),
                _ => null
            };

            if (factory == null)
            {
                Console.WriteLine("Неверная тема!");
                return;
            }

            // Создание компонентов через абстрактную фабрику
            IButton button = factory.CreateButton();
            ITextBlock textBlock = factory.CreateTextBlock();

            button.Render();
            textBlock.Render();
        }
    }

    // Интерфейс абстрактной фабрики
    interface IUIFactory
    {
        IButton CreateButton();
        ITextBlock CreateTextBlock();
    }

    // Фабрика для светлой темы
    class LightUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            // Тут может быть сложная реализация
            return new LightButton();
        }
        public ITextBlock CreateTextBlock()
        {
            // Тут может быть сложная реализация
            return new LightTextBlock();
        }
    }

    // Фабрика для тёмной темы
    class DarkUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            // Тут может быть сложная реализация
            return new DarkButton();
        }
        public ITextBlock CreateTextBlock()
        {
            // Тут может быть сложная реализация
            return new DarkTextBlock();
        }
    }

    // Интерфейсы для продуктов
    interface IButton
    {
        void Render();
    }

    interface ITextBlock
    {
        void Render();
    }

    // Конкретные реализации продуктов для светлой темы
    class LightButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Рендерится светлая кнопка.");
        }
    }

    class LightTextBlock : ITextBlock
    {
        public void Render()
        {
            Console.WriteLine("Рендерится светлый текстовый блок.");
        }
    }

    // Конкретные реализации продуктов для тёмной темы
    class DarkButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Рендерится тёмная кнопка.");
        }
    }

    class DarkTextBlock : ITextBlock
    {
        public void Render()
        {
            Console.WriteLine("Рендерится тёмный текстовый блок.");
        }
    }
}
