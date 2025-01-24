/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using System.Data;
using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            // Simplify the data by only storing the player's total career points
            if (players.ContainsKey(playerId)) {
                players[playerId] += points;
            } else {
                players[playerId] = points;
            }
        }

        var topPlayers = new string[10];

        foreach (var player in players) {
            for (var i = 0; i < 10; i++) {
                if (topPlayers[i] == null || player.Value > players[topPlayers[i]]) {
                    for (var j = 9; j > i; j--) {
                        topPlayers[j] = topPlayers[j - 1];
                    }
                    topPlayers[i] = player.Key;
                    break;
                }
            }
        }

        Console.WriteLine("Top Players:");
        foreach (var player in topPlayers) {
            Console.WriteLine($"{player}: {players[player]}");
        }

    }
}