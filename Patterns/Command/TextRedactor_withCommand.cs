using System.Threading.Channels;

namespace Patterns.Command
{
    // Интерфейс команды
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Команда добавления текста
    public class AddTextCommand : ICommand
    {
        private TextEditor editor;
        private char character;

        public AddTextCommand(TextEditor editor, char character)
        {
            this.editor = editor;
            this.character = character;
        }

        public void Execute()
        {
            editor.AddCharacter(character);
        }

        public void Undo()
        {
            editor.RemoveLastCharacter();
        }
    }

    // Команда удаления текста
    public class RemoveTextCommand : ICommand
    {
        private TextEditor editor;
        private char removedChar;

        public RemoveTextCommand(TextEditor editor)
        {
            this.editor = editor;
        }

        public void Execute()
        {
            removedChar = editor.RemoveLastCharacter();
        }

        public void Undo()
        {
            if (removedChar != '\0')
                editor.AddCharacter(removedChar);
        }
    }

    public class EnterCommand : ICommand
    {
        private TextEditor editor;
        public EnterCommand(TextEditor editor)
        {
            this.editor = editor;
        }

        public void Execute()
        {
            editor.AddCharacter('\n');
        }

        public void Undo()
        {
            editor.RemoveLastCharacter();
        }
    }

    // Класс редактора
    public class TextEditor
    {
        private List<char> text = new List<char>();

        public void AddCharacter(char character)
        {
            text.Add(character);
            DisplayText();
        }

        public char RemoveLastCharacter()
        {
            if (text.Count > 0)
            {
                char removed = text[^1];
                text.RemoveAt(text.Count - 1);
                DisplayText();
                return removed;
            }
            return '\0';
        }

        public void DisplayText()
        {
            Console.Clear();
            Console.WriteLine("Текущий текст: " + new string(text.ToArray()));
        }
    }

    // Менеджер команд
    public class CommandManager
    {
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear(); // Очистка стека "повторить" при новом вводе
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                ICommand command = undoStack.Pop();
                command.Undo();
                redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                ICommand command = redoStack.Pop();
                command.Execute();
                undoStack.Push(command);
            }
        }
    }

    // Основная программа
    public class TextRedactor_withCommand
    {
        public static void Main()
        {
            TextEditor editor = new TextEditor();
            CommandManager commandManager = new CommandManager();

            Console.WriteLine("Введите текст. ESC - выйти, Backspace - удалить символ, Ctrl+Z - отмена, Ctrl+Y - повтор");

            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape) break;
                else if (key.Key == ConsoleKey.Enter)
                {
                    commandManager.ExecuteCommand(new EnterCommand(editor));
                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    commandManager.ExecuteCommand(new RemoveTextCommand(editor));
                }
                else if (key.Modifiers.HasFlag(ConsoleModifiers.Control) && key.Key == ConsoleKey.Z)
                {
                    commandManager.Undo();
                }
                else if (key.Modifiers.HasFlag(ConsoleModifiers.Control) && key.Key == ConsoleKey.Y)
                {
                    commandManager.Redo();
                }
                else if (!char.IsControl(key.KeyChar)) // Только если это текстовый символ
                {
                    commandManager.ExecuteCommand(new AddTextCommand(editor, key.KeyChar));
                }
            }
        }
    }
}
