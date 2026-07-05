using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    [Header("UI Panelleri")]
    public GameObject controlsPanel; 

    void Start()
    {
        // Oynanış sahnesinden menüye dönülürse fare imleci görünür ve serbest olsun
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Oyun başında Kontroller paneli kapalı
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }

    public void Bolum1Ac()
    {
        SceneManager.LoadScene("Bolum1"); 
    }

    public void Bolum2Ac()
    {
        SceneManager.LoadScene("Bolum2");
    }

    // Kontroller Butonuna basılınca çalışacak fonksiyon 
    public void KontrolleriAc()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }
    }

    // Kontroller panelindeki Geri butonuna basılınca çalışacak fonksiyon
    public void KontrolleriKapat()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }

    public void OyundanCik()
    {
        Application.Quit(); // Oyunu kapatır
        Debug.Log("Oyundan Çıkıldı!"); 
    }
}