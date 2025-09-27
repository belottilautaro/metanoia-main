using System.Collections.Generic;
using UnityEngine;

public class PuzzleInventory : MonoBehaviour
{
    public static PuzzleInventory Instance;

    private Dictionary<string, GameObject> collectedPagePrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        // Asegura que solo haya una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }


    public void CollectPage(string pageID, GameObject uiPrefab)
    {
        if (string.IsNullOrEmpty(pageID))
        {
            Debug.LogWarning("Intento de recoger página con ID vacío.");
            return;
        }

        if (uiPrefab == null)
        {
            Debug.LogWarning($"El prefab de UI para la página '{pageID}' es nulo.");
            return;
        }

        if (!collectedPagePrefabs.ContainsKey(pageID))
        {
            collectedPagePrefabs.Add(pageID, uiPrefab);
            Debug.Log($"Página recogida: {pageID} | Prefab: {uiPrefab.name}");
        }
        else
        {
            Debug.Log($"La página '{pageID}' ya fue recogida.");
        }
    }


    public bool HasPage(string pageID)
    {
        return collectedPagePrefabs.ContainsKey(pageID);
    }


    public List<GameObject> GetAllPagePrefabs()
    {
        return new List<GameObject>(collectedPagePrefabs.Values);
    }


    public List<string> GetAllPageIDs()
    {
        return new List<string>(collectedPagePrefabs.Keys);
    }
}
