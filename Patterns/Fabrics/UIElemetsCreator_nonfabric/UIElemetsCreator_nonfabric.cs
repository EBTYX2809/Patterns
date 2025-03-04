namespace Patterns.Fabrics.UIElemetsCreator_nonfabric
{
    internal class UIElemetsCreator_nonfabric
    {
        static void Main(string[] args)
        {
            UIElemetsCreator elemetsCreator = new UIElemetsCreator();

            Button button = elemetsCreator.CreateButton();
            TextBlock textBlock = elemetsCreator.CreateTextBlock();

            button.Render();
            textBlock.Render();
        }
    }

    class UIElemetsCreator
    {
        string theme;

        public UIElemetsCreator()
        {
            AskTheme();
        }

        public Button CreateButton()
        {
            return new Button(theme);
        }

        public TextBlock CreateTextBlock()
        {
            return new TextBlock(theme);
        }

        private void AskTheme()
        {
            Console.WriteLine("Выберите тему: light/dark.");
            theme = Console.ReadLine()?.ToLower();
        }
    }

    abstract class UIElement
    {
        protected string theme;
        public UIElement(string _theme)
        {
            theme = _theme;
        }

        public abstract void Render();
    }

    class Button : UIElement
    {
        public Button(string _theme) : base(_theme) { }
        public override void Render()
        {
            Console.WriteLine($"Зарендерилась {theme} кнопка.");
        }
    }

    class TextBlock : UIElement
    {
        public TextBlock(string _theme) : base(_theme) { }
        public override void Render()
        {
            Console.WriteLine($"Зарендерился {theme} текстовый блок.");
        }
    }
}
