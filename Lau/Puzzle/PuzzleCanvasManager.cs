using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public class PageEntry
{
    public string pageID;
    public GameObject pageObject;

}

public class PuzzleCanvasManager : MonoBehaviour
{
    [Header("Lista de páginas posibles (ya en el Canvas)")]
    public List<PageEntry> allPages;
    [SerializeField] GameObject LockedDoor;
    void Start()
    {
        ActivateCollectedPages();
    }

    void ActivateCollectedPages()
    {
        if (PuzzleInventory.Instance == null)
        {
            Debug.LogError("❌ PuzzleInventory.Instance es null. ¿Está el objeto en la escena?");
            return;
        }

        List<string> collectedIDs = PuzzleInventory.Instance.GetAllPageIDs();
        Debug.Log("📦 Páginas recogidas: " + string.Join(", ", collectedIDs));

        foreach (PageEntry entry in allPages)
        {
            if (string.IsNullOrEmpty(entry.pageID) || entry.pageObject == null)
            {
                Debug.LogWarning("⚠️ Entrada inválida en allPages: ID vacío o objeto nulo.");
                continue;
            }

            bool shouldShow = collectedIDs.Contains(entry.pageID);
            entry.pageObject.SetActive(shouldShow);

            Debug.Log($"{(shouldShow ? "✅ Activada" : "🚫 Oculta")}: {entry.pageID}");
        }
    }

    [Header("Slots para ordenar las páginas")]
    public List<Transform> dropSlots;

    [Header("Orden correcto de páginas (por GameObject)")]
    public List<GameObject> correctPageObjects;


    public void CheckPuzzleOrder()
    {
        bool isCorrect = true;

        for (int i = 0; i < dropSlots.Count; i++)
        {
            Transform slot = dropSlots[i];

            if (slot.childCount == 0)
            {
                isCorrect = false;
                Debug.Log($"❌ Slot {i + 1} está vacío.");
                break;
            }

            GameObject placedPage = slot.GetChild(0).gameObject;

            if (placedPage != correctPageObjects[i])
            {
                isCorrect = false;
                Debug.Log($"❌ Página incorrecta en slot {i + 1}: {placedPage.name}");
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("✅ ¡Puzzle resuelto correctamente!");
            Object.FindAnyObjectByType<AltarInteraction>().PlayPuzzleResult(true);
            Destroy(LockedDoor);
        }
        else
        {
            Debug.Log("🚫 El orden es incorrecto.");
            Object.FindAnyObjectByType<AltarInteraction>().PlayPuzzleResult(false);
        }
    }

    public void RefreshPages()
    {
        ActivateCollectedPages();
    }


}
