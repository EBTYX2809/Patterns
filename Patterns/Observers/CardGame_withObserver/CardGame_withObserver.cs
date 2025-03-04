namespace Patterns.Observers.CardGame_withObserver
{
    internal class CardGame_withObserver
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            // Initializing deck cards
            player.deck.cards.AddRange(new List<string>() { "card1", "card2", "card3" });

            player.TakeCard(); // Will notify every subscribers
        }
    }

    interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void NotifySubsribers();
    }

    interface IObserver
    {
        void UpdateState(string card);
    }

    class GameInfoNotificator : ISubject
    {
        private List<IObserver> observers = new List<IObserver>(); // Список подписчиков
        private string lastCard;

        public void Subscribe(IObserver observer) => observers.Add(observer);
        public void Unsubscribe(IObserver observer) => observers.Remove(observer);

        public void NotifySubsribers()
        {
            foreach (var observer in observers)
            {
                observer.UpdateState(lastCard);
            }
        }

        public void GameStateChanged(string card) // Invoke
        {
            lastCard = card;
            Console.WriteLine($"Game state changed by {card}.");
            NotifySubsribers();
        }
    }

    class Player
    {
        private GameInfoNotificator gameInfoNotificator;
        public Hand hand;
        public Deck deck;
        public Player()
        {
            gameInfoNotificator = new GameInfoNotificator(); // Уведомитель

            hand = new Hand();
            deck = new Deck();

            gameInfoNotificator.Subscribe(hand); // +=
            gameInfoNotificator.Subscribe(deck); // +=            
        }

        public void TakeCard()
        {
            var card = deck.cards.First();
            hand.cards.Add(card);
            deck.cards.Remove(card);

            gameInfoNotificator.GameStateChanged(card); // Invoke(card);
        }
    }

    class Hand : IObserver
    {
        public int cardsCount;
        public List<string> cards = new List<string>();
        public void UpdateState(string card)
        {
            // Тут можеть быть сложная логика обработки
            cardsCount++;
            Console.WriteLine($"Hand changed by {card}.");
        }
    }

    class Deck : IObserver
    {
        public int cardsCount;
        public List<string> cards = new List<string>();
        public void UpdateState(string card)
        {
            // Тут можеть быть сложная логика обработки
            cardsCount++;
            Console.WriteLine($"Deck changed by {card}.");
        }
    }
}
