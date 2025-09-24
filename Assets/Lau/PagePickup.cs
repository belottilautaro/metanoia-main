using UnityEngine;

public class PageInteract : MonoBehaviour
{
    public string pageID = "Page1";
    public GameObject floatingTextPrefab;
    public Transform textAnchor; // ← Asigná el Empty en el Inspector

    private GameObject spawnedText;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PuzzleInventory.Instance.CollectPage(pageID);

            if (spawnedText != null)
                Destroy(spawnedText);

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractRange") && spawnedText == null)
        {
            playerInRange = true;

            spawnedText = Instantiate(
                floatingTextPrefab,
                textAnchor.position,
                textAnchor.rotation
            );
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractRange"))
        {
            playerInRange = false;

            if (spawnedText != null)
                Destroy(spawnedText);
        }
    }
}

