using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public interface IBattle
{
    public IEnumerator Damaging(GameObject effect, float damage ,List<Vector2Int> Indexs)
    {
        List<GameObject> targets = new List<GameObject>();
        foreach(var i in Indexs)
        {
            GameObject target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
            if(!targets.Contains(target))
                targets.Add(target);
        }
        foreach(GameObject target in targets)
        {
            var p = target.GetComponent<Player>().IsUnityNull();
        }

        yield return null;
    }
    public IEnumerator StatModifiring(GameObject effect, StatModifire statm, List<Vector2Int> Indexs) 
    {
       
        yield return null;
    }


    public IEnumerator ActionPointDraining(GameObject effect, List<Vector2Int> Indexs)
    {

        yield return null;

    }




}
