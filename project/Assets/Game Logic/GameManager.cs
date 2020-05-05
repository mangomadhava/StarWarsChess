using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

// Rules: https://starwars.fandom.com/wiki/Dejarik
class GameManager : MonoBehaviour
{
    // The board is made up of three concentric rings.
    // The outer and middle rings have 12 spaces, and the middle ring has one space.
    public Monster[] Board { get; }

    public GameManager()
    {
        Board = new Monster[25];
    }

    public void Start()
    {
        SetupMonsters();
    }

    public void AttackMonster(int src, int dest)
    {
        if (Board[src] == null || Board[dest] == null)
        {
            throw new MissingMonsterException();
        }

        var didKill = Board[dest].InflictDamage(Board[src].Attack);
        if (didKill)
        {
            KillMonster(src);
        }
    }

    public void MoveMonster(int src, int dest)
    {
        if (!GetMovableSpaces(src).Contains(dest))
        {
            throw new SpaceNotWithinRangeException();
        }

        Board[dest] = Board[src];
        Board[src] = null;
    }

    public List<int> GetMovableSpaces(int spaceIndex)
    {
        if (Board[spaceIndex] == null)
        {
            throw new MissingMonsterException();
        }
        
        return GetEmptySpacesWithinRange(spaceIndex, Board[spaceIndex].Movement);
    }

    public List<int> GetAttackableSpaces(int spaceIndex)
    {
        if (Board[spaceIndex] == null)
        {
            throw new MissingMonsterException();
        }
        
        return GetEmptySpacesWithinRange(spaceIndex, Board[spaceIndex].Range);
    }

    private void SetupMonsters()
    {
        var players = new List<int> {0, 1};
        var monsters = GetMonstersFromJson();
        var positions = new List<List<int>>() 
        {
            new List<int> { 1, 2, 3, 4 },
            new List<int>  { 7, 8, 9, 10 }
        };

        foreach (var player in players)
        {
            var available = monsters;
            foreach (var position in positions[player])
            {
                var index = (int) Random.Range(0, available.Count);
                var monster = available[index];
                available.RemoveAt(index);
                Board[position] = new Monster(monster, player);
            }
        }
    }

    private List<Monster> GetMonstersFromJson()
    {
        // TODO: Make sure this works
        StreamReader r = new StreamReader("Data/MonsterData.json");
        string json = r.ReadToEnd();
        List<Monster> monsters = JsonUtility.FromJson<List<Monster>>(json);

        return monsters;
    }

    private void PlaceMonster(int spaceIndex, Monster monster)
    {
        if (Board[spaceIndex] != null)
        {
            throw new MonsterOnSpaceException();
        }

        Board[spaceIndex] = monster;
    }

    private void KillMonster(int spaceIndex)
    {
        if (Board[spaceIndex] == null)
        {
            throw new MissingMonsterException();
        }
        
        Board[spaceIndex] = null;
    }

    private List<int> GetAdjacentSpaces(int spaceIndex)
    {
        if (spaceIndex == 24)
        {
            return new List<int>(new int[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 });
        }

        var result = new List<int>();
        result.Add(SpaceAdd(spaceIndex, 1));            // Right
        result.Add(SpaceSubtract(spaceIndex, 1));       // Left
        result.Add(SpaceAdd(spaceIndex, 12));           // Up

        if (spaceIndex > 11)
        {
            result.Add(SpaceSubtract(spaceIndex, 12));  // Down
        }

        return result;
    }

    private int SpaceAdd(int spaceIndex, int num)
    {
        var result = spaceIndex + num;
        return (result > 24) ? 24 : result;
    }

    private int SpaceSubtract(int spaceIndex, int num)
    {
        var result = spaceIndex - num;
        return (result < 0) ? result + 12 : result;
    }

    private List<int> GetSpacesWithinRange(int spaceIndex, int range)
    {
        var adjacent = GetAdjacentSpaces(spaceIndex).ToList();

        if (range > 1)
        {     
            var ret = new List<int>();
            foreach (var adj in GetAdjacentSpaces(spaceIndex))
            {
                ret.AddRange(GetSpacesWithinRange(spaceIndex, range - 1));
            }

            return ret.Distinct().ToList();
        }

        return adjacent;
    }

    private List<int> GetEmptySpacesWithinRange(int spaceIndex, int range)
    {
        return GetSpacesWithinRange(spaceIndex, range)
            .Where(x => Board[x] == null).ToList<int>();
    }
}