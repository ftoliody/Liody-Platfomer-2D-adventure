using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenuUI;  // UI menu pause
    public Button pauseButton;      // Tombol untuk ikon pause
    private bool isPaused = false;  // Menyimpan status game (apakah game sedang dijeda atau tidak)

    void Start()
    {
        // Pastikan menu pause tidak terlihat di awal
        pauseMenuUI.SetActive(false);

        // Menambahkan listener untuk tombol pause
        pauseButton.onClick.AddListener(TogglePause);
    }

    // Fungsi untuk mengaktifkan atau menonaktifkan pause
    void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame(); // Jika game sedang dijeda, lanjutkan permainan
        }
        else
        {
            PauseGame(); // Jika game tidak dijeda, pause permainan
        }
    }

    // Fungsi untuk pause game
    void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);  // Tampilkan menu pause
        Time.timeScale = 0f;  // Stop waktu dalam game
    }

    // Fungsi untuk melanjutkan game
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);  // Sembunyikan menu pause
        Time.timeScale = 1f;  // Kembalikan waktu dalam game normal
    }

    // Fungsi untuk keluar dari game
    public void QuitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit(); // Keluar dari game
    }

    // Fungsi untuk restart game
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Pastikan game berjalan normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Memuat ulang scene saat ini
    }

     public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Ganti "Main Menu" dengan nama scene utama Anda
    }
}
