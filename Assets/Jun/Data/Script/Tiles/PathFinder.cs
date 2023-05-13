using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public List<TileStatus> FindPath(TileStatus start, TileStatus end)
    {
        List<TileStatus> openlist = new List<TileStatus>();
        List<TileStatus> closeList = new List<TileStatus>();
        openlist.Add(start);
        while(openlist.Count > 0)
        {
            TileStatus currentTile = openlist.OrderBy(x => x.H).First();

            openlist.Remove(currentTile);
            closeList.Add(currentTile);

            if(currentTile == end)
            {
                 //
            }
            var neigbourTile = GetNeighborTiles(currentTile);
        }
        return openlist;
    }
    private object GetNeighborTiles(TileStatus currentTile)
    {
        var map = MapManager.Inst.tiles;

        List<TileStatus> neighborTile = new List<TileStatus>();


        //left
        Vector2Int loctaion2Check = new Vector2Int(currentTile.pos.x - 1, currentTile.pos.y);

        if (map.ContainsKey(loctaion2Check))
        {
            neighborTile.Add(map[loctaion2Check]);
        }
        //right
        loctaion2Check = new Vector2Int(currentTile.pos.x + 1, currentTile.pos.y);

        if (map.ContainsKey(loctaion2Check))
        {
            neighborTile.Add(map[loctaion2Check]);
        }
        //foward
        loctaion2Check = new Vector2Int(currentTile.pos.x, currentTile.pos.y + 1);

        if (map.ContainsKey(loctaion2Check))
        {
            neighborTile.Add(map[loctaion2Check]);
        }
        //backward
        loctaion2Check = new Vector2Int(currentTile.pos.x, currentTile.pos.y - 1);

        if (map.ContainsKey(loctaion2Check))
        {
            neighborTile.Add(map[loctaion2Check]);
        }
        return neighborTile;


    }
}
