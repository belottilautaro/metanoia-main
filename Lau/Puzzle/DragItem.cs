using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string pageID; // ID de la página
    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform); // Lo llevamos al frente
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
    canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
    GameObject dropped = eventData.pointerDrag;

    if (dropped != null)
    {
        dropped.transform.SetParent(transform);
        dropped.transform.localPosition = Vector3.zero;

        DragItem dragItem = dropped.GetComponent<DragItem>();
        if (dragItem != null)
        {
            dragItem.SetNewParent(transform); // Actualiza el nuevo padre
        }
    }
}

    public void SetNewParent(Transform newParent)
    {
        originalParent = newParent;
    }

}
