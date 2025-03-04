namespace Patterns.Observers.CardGame_withoutObserver
{
    internal class CardGame_withoutObserver
    {
        static void Main()
        {
            Player player = new Player();

            Log log = new Log(player);
            UI ui = new UI(player);

            player.cardMoved += ui.GameStateChanged;

            // Initializing deck cards
            player.deck.cards.AddRange(new List<string>() { "card1", "card2", "card3" });

            player.TakeCard(); // Will notify every subscribers
        }
    }

    class Player
    {
        public Hand hand;
        public Deck deck;
        public event Action<string> cardMoved;
        public Player()
        {
            hand = new Hand(this);
            cardMoved += hand.GameStateChanged;
            deck = new Deck(this);
            cardMoved += deck.GameStateChanged;
        }

        public void TakeCard()
        {
            var card = deck.cards.First();
            hand.cards.Add(card);
            deck.cards.Remove(card);

            cardMoved?.Invoke(card);
        }
    }

    class Hand
    {
        public Hand(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public int cardsCount;
        public List<string> cards = new List<string>();

        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in hand changed by {card}.");
            cardsCount++;
        }
    }

    class Deck
    {
        public Deck(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public int cardsCount;
        public List<string> cards = new List<string>();
        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in deck changed by {card}.");
            cardsCount++;
        }
    }

    class Log
    {
        public Log(Player player)
        {
            player.cardMoved += GameStateChanged;
        }

        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in log changed by {card}.");
        }
    }

    class UI
    {
        public UI(Player player)
        {
            player.cardMoved += GameStateChanged;
        }
        public void GameStateChanged(string card)
        {
            Console.WriteLine($"Game state in UI changed by {card}.");
        }
    }
}
