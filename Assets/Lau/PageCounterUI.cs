using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PageCounterUI : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Si usás TextMeshPro, cambiá a TMP_Text
    public int totalPages = 4;

    void Update()
    {
        int collected = PuzzleInventory.Instance.GetAllPages().Count;
        if (collected > 0)
            counterText.gameObject.SetActive(true);
        counterText.text =  collected + " / " + totalPages;
    }
}

