namespace Patterns.Observers.CardGame_withoutObserver2
{
    internal class CardGame_withoutObserver2
    {
        static void Main()
        {
            Player player = new Player();

            Log log = new Log();
            log.Subscribe(player);

            UI ui = new UI();
            log.Subscribe(player);

            // Initializing deck cards
            player.deck.cards.AddRange(new List<string>() { "card1", "card2", "card3" });

            player.TakeCard(); // Will notify every subscribers
        }
    }

    interface Obserever
    {
        void Subscribe(Player player);
    }

    class Player
    {
        public Hand hand;
        public Deck deck;
        public event Action<string> cardMoved;
        public Player()
        {
            hand = new Hand();
            hand.Subscribe(this);
            deck = new Deck();
            deck.Subscribe(this);
        }

        public void TakeCard()
        {
            var card = deck.cards.First();
            hand.cards.Add(card);
            deck.cards.Remove(card);

            cardMoved?.Invoke(card);
        }
    }

    class Hand : Obserever
    {
        public Hand()
        { }

        public int cardsCount;
        public List<string> cards = new List<string>();

        public void Subscribe(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in hand changed by {card}.");
            cardsCount++;
        }
    }

    class Deck : Obserever
    {
        public Deck()
        { }

        public int cardsCount;
        public List<string> cards = new List<string>();

        public void Subscribe(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in deck changed by {card}.");
            cardsCount++;
        }
    }

    class Log : Obserever
    {
        public Log()
        { }

        public void Subscribe(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in log changed by {card}.");
        }
    }

    class UI : Obserever
    {
        public UI()
        { }

        public void Subscribe(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in UI changed by {card}.");
        }
    }
}
