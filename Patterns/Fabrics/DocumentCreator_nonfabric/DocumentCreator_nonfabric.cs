namespace Patterns.Fabrics.DocumentCreator_nonfabric
{
    internal class DocumentCreator_nonfabric
    {
        static void Main(string[] args)
        {
            DocumentCreator creator = new DocumentCreator();
            Document document;

            document = creator.CreateDocument();

            // Дальше любое взаимодействие с документом.
            document.Open();
            document.Save();
            document.Print();
        }
    }

    class DocumentCreator // Fabric
    {
        private string docType;
        private string title;

        private void PickTypeDocument()
        {
            Console.WriteLine("Выберите тип документа (text, spreadsheet, presentation):");
            docType = Console.ReadLine()?.ToLower();
        }

        private void NameDocument()
        {
            Console.Write("Введите заголовок документа: ");
            title = Console.ReadLine();
        }

        public Document CreateDocument()
        {
            PickTypeDocument();
            NameDocument();

            if (docType == "text")
            {
                return new TextDocument(title);
            }
            else if (docType == "spreadsheet")
            {
                return new SpreadsheetDocument(title);
            }
            else if (docType == "presentation")
            {
                return new PresentationDocument(title);
            }
            else
            {
                return null;
            }
        }
    }

    abstract class Document
    {
        public Document(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
        public virtual void Open() { }
        public virtual void Save() { }
        public virtual void Print() { }
    }

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
