using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;

namespace Training.CircleSquareGame.Api;

public class Game

{
    public Dictionary<string, char?> Board = new ()
    {
        { "a1", null},
        { "a2", null},
        { "a3", null},
        { "b1", null},
        { "b2", null},
        { "b3", null},
        { "c1", null},
        { "c2", null},
        { "c3", null},
    };

    public char CurrentPlayer = ChooseFirstPlayer();
    public char? Winner = null;

    private static Random Random = new ();
    public static char ChooseFirstPlayer()
    {
        var players = new List<char> { 'x', 'o' };

        int index = Random.Next(players.Count);
        return players[index];
    }

    public void SetCurrentPlayer()
    {
         this.CurrentPlayer = this.CurrentPlayer.Equals('x') ? 'o' : 'x';
    }

    public void AddMarkToBoard(string field, char player)
    {
        if (Board.ContainsKey(field))
        {
            this.Board[field] = player;
        }
    }

    public bool CheckIfWin()
    {
        string[] rows = { "a", "b", "c" };
        string[] columns = { "1", "2", "3" };

        foreach (string row in rows)
        {
            if (CheckRow(row))
            {
                return true;
            };
        }

        foreach (string column in columns)
        {
            if (CheckColumn(column))
            {
                return true;
            };
        }

        if (CheckLeftDiagonal() || CheckRightDiagonal())
        {
            return true;
        }

        return false;
    }

    public bool CheckRow(string row)
    {
        return Board[$"{row}1"] != null &&
            Board[$"{row}1"].Equals(Board[$"{row}2"]) && 
            Board[$"{row}1"].Equals(Board[$"{row}3"]) ? true : false;
    }

    public bool CheckColumn(string column)
    {
        return Board[$"a{column}"] != null && 
            Board[$"a{column}"].Equals(Board[$"b{column}"]) && 
            Board[$"a{column}"].Equals(Board[$"c{column}"]) ? true : false;
    }

    public bool CheckLeftDiagonal()
    {
        return Board["a1"] != null && 
            Board["a1"].Equals(Board["b2"]) && 
            Board["a1"].Equals(Board["c3"]);
    }

    public bool CheckRightDiagonal()
    {
        return Board["a3"] != null && 
            Board["a3"].Equals(Board["b2"]) && 
            Board["a3"].Equals(Board["c1"]);
    }

    public bool CheckIfDraw()
    {
        var count = Board.Values.Count(x => x != null);
        return count == 9;
    }

    public void Reset()
    {
        Winner = null;
        CurrentPlayer = ChooseFirstPlayer();
        foreach (var field in Board)
        {
            Board[field.Key] = null;
        }
    }


}