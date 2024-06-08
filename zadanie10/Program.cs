using System;
using System.Collections.Generic;

// Observer interface
public interface IObserver
{
    void Update(int score);
}

// Game class (Subject)
public class Game
{
    private List<IObserver> observers;
    private Dictionary<Player, int> scores;

    public Game()
    {
        observers = new List<IObserver>();
        scores = new Dictionary<Player, int>();
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            if (observer is Player player)
            {
                observer.Update(scores[player]);
            }
        }
    }

    public void SetScore(Player player, int score)
    {
        scores[player] = score;
        Notify();
    }
}

// Player class (Observer)
public class Player : IObserver
{
    private int score;
    public string Name { get; }

    public Player(string name)
    {
        Name = name;
    }

    public void Update(int score)
    {
        this.score = score;
        Display();
    }

    public void Display()
    {
        Console.WriteLine($"{Name} score: {score}");
    }
}

// Client code to test the implementation
public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();

        Player player1 = new Player("Player 1");
        Player player2 = new Player("Player 2");
        Player player3 = new Player("Player 3");

        game.Attach(player1);
        game.Attach(player2);
        game.Attach(player3);

        game.SetScore(player1, 10);
        game.SetScore(player2, 20);
        game.SetScore(player3, 30);
    }
}
