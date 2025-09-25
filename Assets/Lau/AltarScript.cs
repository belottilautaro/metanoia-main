using UnityEngine;

public class AltarInteraction : MonoBehaviour
{
    [SerializeField] GameObject puzzleUI;

    private bool playerInRange = false;
    private bool puzzleActive = false;
    private PuzzleCanvasManager canvasManager;

    void Update()
    {

        if (playerInRange && !puzzleActive && Input.GetKeyDown(KeyCode.E))
        {
            puzzleUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            puzzleActive = true;

            if (canvasManager == null)
                canvasManager = puzzleUI.GetComponent<PuzzleCanvasManager>();

            canvasManager.RefreshPages(); // Asegura que las páginas se actualicen
        }
    }

    public void ClosePuzzle()
    {
        puzzleUI.SetActive(false);
        puzzleActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
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
