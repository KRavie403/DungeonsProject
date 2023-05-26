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
        //skillTextUI.GetComponentInChildren<TMP_Text>().text = CharSet.chosenDB.Skill.List[num].myTipText;
        var texts = skillTextUI.GetComponentsInChildren<TMP_Text>();
        texts[0].text = CharSet.chosenDB.Skill.List[num].myTipText;
        texts[1].text = CharSet.chosenDB.Skill.List[num].myTipText2;

        var sprites = skillTextUI.GetComponentsInChildren<Image>();

        sprites[0].sprite = CharSet.chosenDB.Skill.List[num].MySprite;
        sprites[2].sprite = CharSet.chosenDB.Skill.List[num].myTipSprite;
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
