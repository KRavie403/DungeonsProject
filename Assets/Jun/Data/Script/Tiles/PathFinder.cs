using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : Singleton<PathFinder>
{
    public List<TileStatus> openlist = new List<TileStatus>();
    List<TileStatus> closeList = new List<TileStatus>();

    public List<TileStatus> FindPath(TileStatus start, TileStatus end)
    {
        openlist.Clear();
        closeList.Clear();

        openlist.Add(start);
        while (openlist.Count > 0)
        {
            TileStatus currentTile = openlist.OrderBy(x => x.H).First();

            openlist.Remove(currentTile);
            closeList.Add(currentTile);

            if (currentTile == end)
            {
                //finallize path
                return GetFinishedList(start, end);
            }
            var neighbourTiles = GetNeighborTiles(currentTile);
            foreach (TileStatus neighbourTile in neighbourTiles)
            {
                if (GameManager.Inst.characters[GameManager.Inst.curCharacter].CompareTag("Player"))
                {
                    if (neighbourTile.is_blocked || closeList.Contains(neighbourTile))
                        continue;
                    neighbourTile.G = GetManhattenDistance(start, neighbourTile);
                    neighbourTile.H = GetManhattenDistance(end, neighbourTile);

                    neighbourTile.prevTile = currentTile;

                    if (!openlist.Contains(neighbourTile))
                    {
                        openlist.Add(neighbourTile);
                    }
                }
                else 
                {
                    if ((neighbourTile.is_blocked && !neighbourTile.OnMyTarget().CompareTag("Monster") )|| closeList.Contains(neighbourTile))
                        continue;
                    neighbourTile.G = GetManhattenDistance(start, neighbourTile);
                    neighbourTile.H = GetManhattenDistance(end, neighbourTile);

                    neighbourTile.prevTile = currentTile;

                    if (!openlist.Contains(neighbourTile))
                    {
                        openlist.Add(neighbourTile);
                    }
                }
            }
        }
        return openlist;
    }

    private List<TileStatus> GetFinishedList(TileStatus start, TileStatus end)
    {
        List<TileStatus> finishedList = new List<TileStatus>();
        TileStatus currentTile = end;
        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.prevTile;
        }
        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattenDistance(TileStatus start, TileStatus neighbourTile)
    {
        return Mathf.Abs(start.gridPos.x - neighbourTile.gridPos.x) + Mathf.Abs(start.gridPos.y - neighbourTile.gridPos.y);
    }

    private List<TileStatus> GetNeighborTiles(TileStatus currentTile)
    {
        var map = MapManager.Inst.tiles;

        List<TileStatus> neighborTiles = new List<TileStatus>();

        // Left
        Vector2Int locationToCheck = new Vector2Int(currentTile.gridPos.x - 1, currentTile.gridPos.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Right
        locationToCheck = new Vector2Int(currentTile.gridPos.x + 1, currentTile.gridPos.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Forward
        locationToCheck = new Vector2Int(currentTile.gridPos.x, currentTile.gridPos.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Backward
        locationToCheck = new Vector2Int(currentTile.gridPos.x, currentTile.gridPos.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Additional tiles for 2x2 object
        // Top Left
        locationToCheck = new Vector2Int(currentTile.gridPos.x - 1, currentTile.gridPos.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Top Right
        locationToCheck = new Vector2Int(currentTile.gridPos.x + 1, currentTile.gridPos.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Bottom Left
        locationToCheck = new Vector2Int(currentTile.gridPos.x - 1, currentTile.gridPos.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        // Bottom Right
        locationToCheck = new Vector2Int(currentTile.gridPos.x + 1, currentTile.gridPos.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        return neighborTiles;
    }
    public List<TileStatus> GetPath()
    {
        return openlist;
    }


}
