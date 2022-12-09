using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TooltipMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject go;
    public GameObject tooltipText;
    public string txt = "";

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        go.SetActive(true);
        tooltipText.GetComponent<UnityEngine.UI.Text>().text = txt;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        go.SetActive(false);
    }
}