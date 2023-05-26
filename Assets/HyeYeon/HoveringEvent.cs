using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class HoveringEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IPointerMoveHandler
{

    public GameObject skillTextUI;      //스킬 설명 UI
    public CharcterSet CharSet;
    public int num = 0;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        skillTextUI.SetActive(true);
        skillTextUI.GetComponentInChildren<TMP_Text>().text = CharSet.chosenDB.Skill.List[num].myTipText;
        //skillTextUI.GetComponentInChildren<Image>().sprite = chosenDB.Skill.List[n].MySprite;
        // 스킬에 대한 설명 UI skillTextUI.gameObject.SetActive(true)를 해준다.

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 스킬에 대한 설명 UI skillTextUI.gameObject.SetActive(false)를 해준다.
        skillTextUI.SetActive(false);
    }

    /*public void OnPointerMove(PointerEventData eventData)
    {
        // 스킬에 대한 설명 UI skillTextUI가 마우스 위치로 이동
    }*/

}
