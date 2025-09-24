using System.Collections.Generic;
using UnityEngine;

public class PuzzleInventory : MonoBehaviour
{
    public static PuzzleInventory Instance;
    private HashSet<string> collectedPages = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    public void CollectPage(string pageID)
    {
        if (!collectedPages.Contains(pageID))
        {
            collectedPages.Add(pageID);
            Debug.Log("Página recogida: " + pageID);
        }
    }

    public bool HasPage(string pageID)
    {
        return collectedPages.Contains(pageID);
    }

    public List<string> GetAllPages()
    {
        return new List<string>(collectedPages);
    }
}
