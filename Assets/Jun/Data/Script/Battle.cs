using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Battle : CharactorMovement
{
    protected IEnumerator Damaging(SkillSet skill, float damage, Vector2Int[] Indexs)
    {
        Dictionary<GameObject, OB_TYPES> targets = new Dictionary<GameObject, OB_TYPES>();

        //effect Àç»ý
        switch (skill.myType)
        {
            case SkillSet.SkillType.Directing:
                Instantiate(skill.Effect, MapManager.Inst.tiles[Indexs[0]].transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
                foreach (var i in Indexs)
                {
                    GameObject target;
                    if (MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target != null)
                    {
                        target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
                        if (!targets.ContainsKey(target))
                            targets.Add(target, target.GetComponent<CharactorProperty>().myType);
                    }
                }
                foreach (var target in targets)
                {
                    if (target.Key != null)
                    {
                        if (target.Value == OB_TYPES.MONSTER && myType == OB_TYPES.PLAYER)
                        {
                            target.Key.GetComponent<BossMonster>().TakeDamage(AttackPower * damage);

                        }
                        else if (target.Value == OB_TYPES.PLAYER && myType == OB_TYPES.MONSTER)
                        {
                            target.Key.GetComponent<Player>().TakeDamage(AttackPower * damage);

                        }
                    }
                }
                break;
            case SkillSet.SkillType.Moveable:
                bool done = false;
                StartCoroutine(MovingToTile(Indexs[0], () => done = true));
                while (!done)
                {
                    yield return null;
                }
                Instantiate(skill.Effect, MapManager.Inst.tiles[Indexs[0]].transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
                foreach (var i in Indexs)
                {
                    GameObject target;
                    if (MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target != null)
                    {
                        target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
                        if (!targets.ContainsKey(target))
                            targets.Add(target, target.GetComponent<CharactorProperty>().myType);
                    }
                }
                foreach (var target in targets)
                {
                    if (target.Key != null)
                    {
                        if (target.Value == OB_TYPES.MONSTER && myType == OB_TYPES.PLAYER)
                        {
                            target.Key.GetComponent<BossMonster>().TakeDamage(AttackPower * damage);

                        }
                        else if (target.Value == OB_TYPES.PLAYER && myType == OB_TYPES.MONSTER)
                        {
                            target.Key.GetComponent<Player>().TakeDamage(AttackPower * damage);

                        }
                    }
                }
                break;
            case SkillSet.SkillType.Targeting:
                Instantiate(skill.Effect, MapManager.Inst.tiles[Indexs[0]].transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
                foreach (var i in Indexs)
                {
                    GameObject target;
                    if (MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target != null)
                    {
                        target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
                        if (!targets.ContainsKey(target))
                            targets.Add(target, target.GetComponent<CharactorProperty>().myType);
                    }
                }
                foreach (var target in targets)
                {
                    if (target.Key != null)
                    {
                        if (target.Value == OB_TYPES.MONSTER && myType == OB_TYPES.PLAYER)
                        {
                            target.Key.GetComponent<BossMonster>().TakeDamage(AttackPower * damage);

                        }
                        else if (target.Value == OB_TYPES.PLAYER && myType == OB_TYPES.MONSTER)
                        {
                            target.Key.GetComponent<Player>().TakeDamage(AttackPower * damage);

                        }
                    }
                }
                break;
        }

        yield return null;
    }
    protected IEnumerator StatModifiring(SkillSet skill, Vector2Int[] Indexs) 
    {
        StatModifire item = new StatModifire();

        item.lifeTime = skill._exStatus.lifeTime;
        item.Speed= skill._exStatus.Speed;
        item.AttackPower= skill._exStatus.AttackPower;
        item.DeffencePower= skill._exStatus.DeffencePower;
        item.Postive = skill._exStatus.Postive;
        item.Unbreakable = skill._exStatus.Unbreakable;
        Dictionary<GameObject, OB_TYPES> targets = new Dictionary<GameObject, OB_TYPES>();
        switch (skill.myType)
        {
            case SkillSet.SkillType.Directing:
                Instantiate(skill.Effect, MapManager.Inst.tiles[Indexs[0]].transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
                foreach (var i in Indexs)
                {
                    GameObject target;
                    if (MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target != null)
                    {
                        target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
                        if (!targets.ContainsKey(target))
                            targets.Add(target, target.GetComponent<CharactorProperty>().myType);
                    }
                }
                foreach (var target in targets)
                {
                    if (item.Postive && target.Value == myType)
                    {
                        target.Key.GetComponent<CharactorProperty>().AD_stat.Add(item);
                        target.Key.GetComponent<CharactorMovement>().RefreshStatus();
                    }

                    else if (!item.Postive && target.Value != myType)
                    {
                        target.Key.GetComponent<CharactorProperty>().AD_stat.Add(item);
                        target.Key.GetComponent<CharactorMovement>().RefreshStatus();
                    }

                }
                break;
            case SkillSet.SkillType.Targeting:
                Instantiate(skill.Effect, MapManager.Inst.tiles[Indexs[0]].transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
                foreach (var i in Indexs)
                {
                    GameObject target;
                    if (MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target != null)
                    {
                        target = MapManager.Inst.tiles[i].GetComponent<TileStatus>().my_target;
                        if (!targets.ContainsKey(target))
                            targets.Add(target, target.GetComponent<CharactorProperty>().myType);
                    }
                }
                foreach (var target in targets)
                {
                    if (item.Postive && target.Value == myType)
                    {
                        target.Key.GetComponent<CharactorProperty>().AD_stat.Add(item);
                        target.Key.GetComponent<CharactorMovement>().RefreshStatus();
                    }

                    else if (!item.Postive && target.Value != myType)
                    {
                        target.Key.GetComponent<CharactorProperty>().AD_stat.Add(item);
                        target.Key.GetComponent<CharactorMovement>().RefreshStatus();
                    }

                }
                break;
        }
        yield return null;
    }


    protected void SpecialEffect(SkillSet effect, Vector2Int[] Indexs)
    {


    }




}
