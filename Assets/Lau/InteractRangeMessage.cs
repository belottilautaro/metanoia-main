using UnityEngine;

public class InteractRangeMessage : MonoBehaviour
{
    private bool playerInRange = false;
    [SerializeField] GameObject interactMessage;

    void Update()
    {
        interactMessage.SetActive(playerInRange);
    }
        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractRange"))
        {
            playerInRange = true;
            Debug.Log("Jugador dentro del rango de interacción");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractRange"))
        {
            playerInRange = false;
            Debug.Log("Jugador fuera del rango de interacción");
        }
    }         
}
