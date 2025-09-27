using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            Debug.Log("🚫 Este slot ya tiene una página.");
            return; // No acepta más de una
        }

        GameObject dropped = eventData.pointerDrag;

        if (dropped != null)
        {
            dropped.transform.SetParent(transform);
            dropped.transform.localPosition = Vector3.zero;
        }
    }  
 
}

