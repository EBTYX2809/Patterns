namespace Patterns.Observers.Game_withObserver2
{
    internal class Game_withObserver2
    {
        static void Main()
        {
            GameEventNotifier notifier = new GameEventNotifier();

            // Создаем подписчиков
            UI ui = new UI();
            Logger logger = new Logger();
            NetworkManager networkManager = new NetworkManager();
            AIPlayer ai = new AIPlayer();

            // Подписываем их с разными приоритетами
            notifier.Subscribe(ui, priority: 1);
            notifier.Subscribe(logger, priority: 3);
            notifier.Subscribe(networkManager, priority: 2);
            notifier.Subscribe(ai, priority: 1);

            // Игровой процесс
            notifier.TriggerEvent("Player1 played a card");
            notifier.TriggerEvent("Player2 played a spell");
            notifier.TriggerEvent("Player1 won the round");
        }
    }

    // 🔴 Интерфейс Наблюдателя
    interface IObserver
    {
        void OnEventReceived(string message);
    }

    // 🟢 Интерфейс Нотификатора
    interface ISubject
    {
        void Subscribe(IObserver observer, int priority);
        void Unsubscribe(IObserver observer);
        void NotifySubscribers(string message);
    }

    // 🔥 Реализация сложного Наблюдателя с приоритетами
    class GameEventNotifier : ISubject
    {
        private SortedDictionary<int, List<IObserver>> observers = new();

        public void Subscribe(IObserver observer, int priority)
        {
            if (!observers.ContainsKey(priority))
                observers[priority] = new List<IObserver>();

            observers[priority].Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            foreach (var group in observers.Values)
                group.Remove(observer);
        }

        public void NotifySubscribers(string message)
        {
            foreach (var group in observers.OrderBy(p => p.Key)) // Выполняем по приоритету
            {
                foreach (var observer in group.Value)
                {
                    observer.OnEventReceived(message);
                    if (message.Contains("won")) // Например, если кто-то выиграл, отключаем AI
                        Unsubscribe(observer);
                }
            }
        }

        public void TriggerEvent(string message)
        {
            Console.WriteLine($"\n🔔 Game Event: {message}");
            NotifySubscribers(message);
        }
    }

    // 🖥️ UI (приоритет 1)
    class UI : IObserver
    {
        public void OnEventReceived(string message)
        {
            Console.WriteLine($"[UI] Обновление интерфейса: {message}");
            Thread.Sleep(100); // Симуляция задержки
        }
    }

    // 📜 Логгер (приоритет 3)
    class Logger : IObserver
    {
        public void OnEventReceived(string message)
        {
            Console.WriteLine($"[Logger] Запись в лог: {message}");
        }
    }

    // 🌍 Сетевой менеджер (приоритет 2)
    class NetworkManager : IObserver
    {
        public void OnEventReceived(string message)
        {
            Console.WriteLine($"[Network] Отправка события на сервер: {message}");
        }
    }

    // 🤖 AI-бот (приоритет 1, но отключается при победе)
    class AIPlayer : IObserver
    {
        public void OnEventReceived(string message)
        {
            Console.WriteLine($"[AI] Анализ хода: {message}");
        }
    }
}
