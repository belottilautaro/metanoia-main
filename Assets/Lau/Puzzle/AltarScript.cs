using UnityEngine;

public class AltarInteraction : MonoBehaviour
{
    [SerializeField] GameObject puzzleUI;
    [SerializeField] AudioClip puzzleStartSound;
    [SerializeField] AudioClip puzzleCorrectSound;
    [SerializeField] AudioClip puzzleFailSound;

    private bool playerInRange = false;
    public bool puzzleActive = false;
    private PuzzleCanvasManager canvasManager;

    void Update()
    {
        if (playerInRange && !puzzleActive && Input.GetKeyDown(KeyCode.E))
        {
            AudioSource.PlayClipAtPoint(puzzleStartSound, transform.position, 1f);
            puzzleUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            puzzleActive = true;

            if (canvasManager == null)
                canvasManager = puzzleUI.GetComponent<PuzzleCanvasManager>();

            canvasManager.RefreshPages();
            
        }
    }

    public void ClosePuzzle()
    {
        puzzleActive = false;
        puzzleUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void PlayPuzzleResult(bool success)
    {
        if (success && puzzleCorrectSound != null)
            AudioSource.PlayClipAtPoint(puzzleCorrectSound, transform.position, 1f);
        else if (!success && puzzleFailSound != null)
            AudioSource.PlayClipAtPoint(puzzleFailSound, transform.position, 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractRange"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractRange"))
        {
            playerInRange = false;
        }
    }
}
