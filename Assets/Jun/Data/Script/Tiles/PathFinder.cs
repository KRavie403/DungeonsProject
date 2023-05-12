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
        }
        return openlist;
    }
}
