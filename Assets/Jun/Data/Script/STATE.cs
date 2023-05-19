using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum STATE
{
    CREATE, ACTION, INTERACT, MOVE, ATTACK, SKILL_CAST, GUARD_UP, IDLE, SEARCH, WANDER
}

public abstract class BattleState
{
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator CastingSkill()
    {
        yield break;
    }

    public virtual IEnumerator Wander() { 
        yield break; 
    }

}
