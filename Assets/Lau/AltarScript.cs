using UnityEngine;

public class AltarInteraction : MonoBehaviour
{
    [SerializeField] GameObject puzzleUI; 
    [SerializeField] GameObject interactMessage; 
    private bool playerInRange = false;
    private bool puzzleActive = false;


void Update()
{
    if (playerInRange && !puzzleActive)
    {
        interactMessage.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                puzzleUI.SetActive(true);
                interactMessage.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                puzzleActive = true;
        }
    }
    else
    {
        interactMessage.SetActive(false);
    }
}



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractRange"))
            playerInRange = true;
            Debug.Log("Jugador dentro del rango de interacción");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractRange"))
            playerInRange = false;
            Debug.Log("Jugador fuera del rango de interacción");
    }
}
