using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
   public GameObject creditsPanel;

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);  // Tampilkan Panel Credits
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false); // Sembunyikan Panel Credits
    }
}
