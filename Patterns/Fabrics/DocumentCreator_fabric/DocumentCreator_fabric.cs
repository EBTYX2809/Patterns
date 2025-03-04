namespace Patterns.Fabrics.DocumentCreator_fabric
{
    internal class DocumentCreator_fabric
    {
        static void Main(string[] args)
        {
            // Клиентский код запрашивает тип документа и заголовок
            Console.WriteLine("Выберите тип документа (text, spreadsheet, presentation):");
            string docType = Console.ReadLine()?.ToLower();

            Console.Write("Введите заголовок документа: ");
            string title = Console.ReadLine();

            // Выбираем конкретную фабрику (создатель) на основании типа
            DocumentCreator creator = docType switch
            {
                "text" => new TextDocumentCreator(),
                "spreadsheet" => new SpreadsheetDocumentCreator(),
                "presentation" => new PresentationDocumentCreator(),
                _ => null
            };

            if (creator == null)
            {
                Console.WriteLine("Неизвестный тип документа.");
                return;
            }

            // Создаём документ через фабричный метод
            Document document = creator.CreateDocument(title);

            // Дальше любое взаимодействие с документом.
            document.Open();
            document.Save();
            document.Print();
        }
    }

    // Абстрактный создатель (фабрика)
    abstract class DocumentCreator
    {
        public abstract Document CreateDocument(string title);
    }

    // Конкретные создатели для каждого типа документа
    class TextDocumentCreator : DocumentCreator
    {
        public override Document CreateDocument(string title)
        {
            // Тут может быть какая-то более сложная реализация
            return new TextDocument(title);
        }
    }

    class SpreadsheetDocumentCreator : DocumentCreator
    {
        public override Document CreateDocument(string title)
        {
            // Тут может быть какая-то более сложная реализация
            return new SpreadsheetDocument(title);
        }
    }

    class PresentationDocumentCreator : DocumentCreator
    {
        public override Document CreateDocument(string title)
        {
            // Тут может быть какая-то более сложная реализация
            return new PresentationDocument(title);
        }
    }

    // Абстрактный класс документа
    abstract class Document
    {
        public Document(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
        public abstract void Open();
        public abstract void Save();
        public abstract void Print();
    }

    // Конкретные классы документов
    class TextDocument : Document
    {
        public string Content { get; set; }
        public TextDocument(string title) : base(title) { }
        public override void Open()
        {
            Console.WriteLine($"Открыт текстовый документ: {Title}");
        }

        public override void Save()
        {
            Console.WriteLine($"Сохранён текстовый документ: {Title}");
        }

        public override void Print()
        {
            Console.WriteLine($"Печатается текстовый документ: {Title}");
        }
    }

    class SpreadsheetDocument : Document
    {
        public int[,] Cells { get; set; }
        public SpreadsheetDocument(string title) : base(title) { }
        public override void Open()
        {
            Console.WriteLine($"Открыта таблица: {Title}");
        }

        public override void Save()
        {
            Console.WriteLine($"Сохранена таблица: {Title}");
        }

        public override void Print()
        {
            Console.WriteLine($"Печатается таблица: {Title}");
        }
    }

    class PresentationDocument : Document
    {
        public List<string> Slides { get; set; }
        public PresentationDocument(string title) : base(title) { }
        public override void Open()
        {
            Console.WriteLine($"Открыта презентация: {Title}");
        }

        public override void Save()
        {
            Console.WriteLine($"Сохранена презентация: {Title}");
        }

        public override void Print()
        {
            Console.WriteLine($"Печатается презентация: {Title}");
        }
    }
}
