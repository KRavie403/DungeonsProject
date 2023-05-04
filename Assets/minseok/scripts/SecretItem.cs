using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretItem : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileState> tiles;
    Coroutine Find = null;
    public GameObject test = null;


    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = GameManager.Inst.GetTileIndex(other.gameObject);

            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
    void Start()
    {
        Setting();
        Find = StartCoroutine(FindPlayer());
        test = GameObject.Find("Canvas");
        this.transform.GetChild(1).gameObject.transform.parent = test.transform;


    }

    void Update()
    {

    }
    void Setting()
    {
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                tiles.Add(GameManager.Inst.tiles[pos.x + i, pos.y + j].GetComponent<TileState>());
            }
        }
    }
    IEnumerator FindPlayer()
    {
        while (true)
        {
            foreach (var tile in tiles)
            {
                if (tile.my_obj == OB_TYPES.PLAYER)
                {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    StopCoroutine(Find);
                }
            }
            yield return null;
        }
    }
}