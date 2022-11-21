using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class AchivementsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData e)
    {
        Debug.Log(text);
        TooltipMenu.text = text;
        TooltipMenu.isUI = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData e)
    {
        TooltipMenu.isUI = false;
    }
}
