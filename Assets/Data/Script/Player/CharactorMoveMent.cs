using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMoveMent : CharactorProperty
{
    protected void MoveToTile(Transform target)
    {
        StartCoroutine(MovingToTile(target));
    }

    void GettingItem()
    {

    }

    void Attack()
    {

    }

    void Guard()
    {

    }
    IEnumerator MovingToTile(Transform target)
    {
        yield return null;
    }

}
