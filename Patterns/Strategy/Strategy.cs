using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Strategy
{
    public class Strategy // RoundWinManager
    {
        static void Main()
        {
            RoundWinner roundWinner = new RoundWinner(new Player1WinRound());

            roundWinner.WinRound(); // Player1WinRound

            roundWinner.ChangeWinner(new DrawInRound());

            roundWinner.WinRound(); // DrawInRound
        }
    }

    public interface IWinRound
    {
        public void WinRound();
    }

    public class RoundWinner
    {
        private IWinRound roundWinner;
        public RoundWinner(IWinRound _roundWinner) 
        {
            roundWinner = _roundWinner;
        }

        // Метод для изменения стратегии победителя раунда
        public void ChangeWinner(IWinRound _roundWinner)
        {
            roundWinner = _roundWinner;
        }

        public void WinRound()
        {
            roundWinner.WinRound();
        }
    }

    public class Player1WinRound : IWinRound 
    {
        public void WinRound() 
        {
            // Обработка для Player1
        }
    }

    public class Player2WinRound : IWinRound
    {
        public void WinRound()
        {
            // Обработка для Player2
        }
    }

    public class DrawInRound : IWinRound
    {
        public void WinRound()
        {
            // Обработка для ничьи
        }
    }
}
