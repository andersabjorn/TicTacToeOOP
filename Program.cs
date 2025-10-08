using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace TicTacToeOOP
{
    public interface IPlayer

    //interface = kontakt som säger vilka medlemmar en spelare MÅSTE ha
    {
        // Property (egenskaper) med get-accessotr
        string Name { get; }

        // Property (egenskap)
        char Mark { get; }

        //Metod (måste implemeteras i klassen

        int GetMove(Board board);
    }

    //Klass: HumanPlayer = en mänsklig spelare som implementerar IPlayer
    public class HumanPlayer : IPlayer
    {
        //Properties (publika egenskaper med bara get

        public string Name { get; }
        public char Mark { get; }

        //Konstuktor . körs när man skriver "new Human Player(...)"
        //Sätter värden på Name och Mark

        public HumanPlayer(string name, char mark)
        {

            Name = name;
            Mark = mark;
        }


        //METOD - Hämtar ett drag (Position 1-9) från användaren
        public int GetMove(Board board)
        {
            while (true)
            {
                Console.WriteLine($"{Name} ({Mark}) välj position 1-9: ");
                var input = Console.ReadLine();

                // TryParse = försöker konvertera text till int
                if (int.TryParse(input, out int pos))

                {
                    pos -= 1; // omvandla 1-9 till index 0-8
                    if (board.IsMoveValid(pos))
                        return pos; // returnerar positionen om giltig

                }

                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }
    }

    // Klass: Board = själva spelbrädet, ansvarar för Logok och rendering

    public class Board
    {
        private readonly char[] _cells = new char[9]; // nio rutor

        //Konstruktor - initierar brädet till tomma rutor
        public Board()
        {
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = ' ';
        }

        // Metod - kollar om ett drag är giltigt
        public bool IsMoveValid(int index) =>
            index >= 0 && index < 9 && _cells[index] == ' ';

        //METOD placerar ur x eller 0 på brädet
        public void PlaceMark(int index, char mark)
        {
            if (!IsMoveValid(index))
                throw new InvalidOperationException("Ogiltigt drag.");
            _cells[index] = mark;
        }

        // Metod - kollar om brädet är fullt
        public bool IsFull()
        {
            foreach (var c in _cells)
                if (c == ' ')
                    return false;
            return true;
        }
        // MEtod -Kontrollerar om någon har vunnit
        public bool HasWinner(out char winner)
        {
            //Möjliga vinstlinjer (rader, kolumner, diagonaler)
            int[][] lines = new[]
            {
                new[]{0,1,2}, new[]{3,4,5}, new []{6,7,8}, // rader
                new[]{0,3,6}, new[]{1,4,7}, new []{2,5,8}, // rader
                new[]{0,4,8}, new[]{2,4,6},  // diagonaler
            };

            foreach (var line in lines)
            {

                char a = _cells[line[0]], b = _cells[line[1]], c = _cells[line[2]];
                if (a != ' ' && a == b && b == c)
                {
                    winner = a;
                    return true;

                }
            }

            winner = ' ';
            return false;
        }

        //METOD - skriver ut brädet i konsolen
        public void Render()
        {
            Console.Clear();
            Console.WriteLine();
            for (int r = 0; r < 3; r++)
            {
                int i = r * 3;
                Console.WriteLine($" {_cells[i]} | {_cells[i + 1]} | {_cells[i + 2]} ");
                if (r < 2) Console.WriteLine("---+---+---");
            }
            Console.WriteLine();
            Console.WriteLine("Positionskarta: 1 2 3 / 4 5 6 / 7 8 9");
            Console.WriteLine();
        }
    }

    //Klass Game = hanterar hela spelets gång

    public class Game
    {
        //FÄLT - privata variabler
        private readonly Board _board = new Board();
        private readonly IPlayer _p1;
        private readonly IPlayer _p2;

        // Konstruktor - tar in två spelare och sparar dem

        public Game(IPlayer p1, IPlayer p2)
        {
            _p1 = p1;
            _p2 = p2;
        }

        //METOD - kör hela spel-loopsen
        public void Run()
        {
            IPlayer current = _p1; // börjar med spelare 1

            while (true)
            {
                _board.Render(); //rita brädet

                int move = current.GetMove(_board); // hämta drag
                _board.PlaceMark(move, current.Mark); // placera markering

                if (_board.HasWinner(out char w))
                {

                    _board.Render();
                    Console.WriteLine($"🎉 {current.Name} vinner med {w}!");
                    return; //Avsluta spel
                }

                if (_board.IsFull())
                {
                    _board.Render();
                    Console.WriteLine("🤝 Oavgjort!");
                    return;
                }

                //Växla spelare

                current = current == _p1 ? _p2 : _p1;

            }
        }


    }

    //Klass: Program = startpunkten för hela applikationen

    class Program
    {
        // MAIN-MEtoden = programmet startar här

        static void Main()
        {
            // Tilåter emojis och specialtecken
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Skapar objekt (instanser) av HumanPlayer med "new
            var p1 = new HumanPlayer("Spelare 1", 'X');
            var p2 = new HumanPlayer("Spelare 2", 'O');

            //Skapar Geme-Objekt och skickar in spelarna
            var game = new Game(p1, p2);

            // Kör spelet (antopar metoden Run)
            game.Run();

            //Vänta på tangenttryckning innan programmet stängs
            Console.WriteLine("\nTryck valfri tangent för att avsluta...");
            Console.ReadKey(true);


        }

    }
}
