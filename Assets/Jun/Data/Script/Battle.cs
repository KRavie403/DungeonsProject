using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Battle : CharactorMovement
{
    protected IEnumerator Damaging(GameObject effect, float damage ,Vector2Int[] Indexs)
    {
        //effect Àç»ý
        Dictionary<GameObject, OB_TYPES> targets = new Dictionary<GameObject, OB_TYPES>();
        foreach(var i in Indexs)
        {
            GameObject target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
            Instantiate(effect, MapManager.Inst.tiles[i].transform);

            if (target != null && !targets.ContainsKey(target))
                targets.Add(target, target.GetComponent<CharactorProperty>().myType);
        }
        foreach(var target in targets)
        {
            if (target.Key != null)
            {
                if (target.Value == OB_TYPES.MONSTER && myType == OB_TYPES.PLAYER)
                {
                    target.Key.GetComponent<BossMonster>().TakeDamage(damage);

                }
                else if (target.Value == OB_TYPES.PLAYER && myType == OB_TYPES.MONSTER)
                {
                    target.Key.GetComponent<Player>().TakeDamage(damage);

                }
            }
        }

        yield return null;
    }
    protected IEnumerator StatModifiring(GameObject effect, StatModifire statm, Vector2Int[] Indexs) 
    {
       
        yield return null;
    }


    protected IEnumerator ActionPointDraining(GameObject effect, Vector2Int[] Indexs)
    {

        yield return null;

    }




}
