using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMoveMent : CharactorProperty
{
    protected void MoveToTile(Vector3 target)
    {
        transform.Translate(target, Space.World);

        // StartCoroutine(MovingToTile(target));
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
    IEnumerator MovingToTile(Vector3 target)
    {
        transform.Translate(target, Space.World);
        yield return null;
    }

}
