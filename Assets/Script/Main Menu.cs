using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    // Method untuk tombol Play
    public void PlayGame()
    {
        // Ganti "GameScene" dengan nama scene game utama kamu
        SceneManager.LoadScene("Level_1");
    }

    // Method untuk tombol Options
    public void OpenOptions()
    {
        Debug.Log("Options Opened"); // Ganti ini dengan logika pengaturan jika diperlukan
    }

    // Method untuk tombol Exit
    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit(); // Ini hanya berfungsi setelah game dibangun, bukan di editor
    }
    public void ShowCredits()
    {
        creditsPanel.SetActive(true);  // Tampilkan Panel Credits
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false); // Sembunyikan Panel Credits
    }
}
