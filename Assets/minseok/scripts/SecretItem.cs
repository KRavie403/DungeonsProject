using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretItem : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileStatus> tiles;
    Coroutine Find = null;
    public GameObject test = null;


    private void OnTriggerEnter(Collider other) //자기타일 인식시키기
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = MapManager.Inst.GetTileIndex(other.gameObject);

            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
    void Start()
    {
        Setting();
        Find = StartCoroutine(FindPlayer());
        //test = GameObject.Find("Canvas");
        //this.transform.GetChild(1).gameObject.transform.parent = test.transform;

    }

    void Update()
    {

    }
    void Setting() //자기주변 기준으로 5x5인식
    {
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                tiles.Add(MapManager.Inst.tiles[pos.x + i, pos.y + j].GetComponent<TileStatus>());
            }
        }
    }
    IEnumerator FindPlayer() //인식한범위안에 플레이어가 들어오면 실행
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