using System.Collections.Generic;
using System.Linq;

namespace Gamba;

public class Player
{
    private static readonly List<Player> players = new();

    // Constructor
    public Player(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public int Bet { get; set; }
    public bool Blackjack { get; set; } = false;

    // Static method to add a player
    public static void AddPlayer(string name)
    {
        if (players.All(p => p.Name != name)) players.Add(new Player(name));
    }

    // Static method to remove a player
    public static void RemovePlayer(string name)
    {
        players.RemoveAll(p => p.Name == name);
    }

    public static void RemoveAllPlayers()
    {
        players.Clear();
    }

    // Static method to get a player by name
    public static Player? GetPlayer(string name)
    {
        return players.FirstOrDefault(p => p.Name == name);
    }

    public static int GetPlayerBet(string name)
    {
        var player = GetPlayer(name);
        if (player != null) return player.Bet;

        return 0;
    }

    // Static method to set a player's bet
    public static void PlayerBet(string name, int bet)
    {
        var player = GetPlayer(name);
        if (player != null) player.Bet = bet;
    }

    public static bool PlayerBlackjack(string name)
    {
        return GetPlayer(name)?.Blackjack ?? false;
    }

    // Static method to get all players
    public static List<Player> GetAllPlayers()
    {
        return players.ToList();
    }

    public static bool IsInGame(string name)
    {
        return players.Any(p => p.Name == name);
    }
}