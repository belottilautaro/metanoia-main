using UnityEngine;

public class PageInteract : MonoBehaviour
{
    public string pageID;
    public GameObject uiPrefab;
    public GameObject floatingTextPrefab;
    public Transform textAnchor;
    public AudioClip pickupSound;

    private GameObject spawnedText;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PuzzleInventory.Instance.CollectPage(pageID, uiPrefab);

            if (spawnedText != null)
                Destroy(spawnedText);

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

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
