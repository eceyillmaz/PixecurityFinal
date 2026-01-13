using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuYoneticisi : MonoBehaviour
{
    [Header("UI Panelleri")]
    public GameObject hakkindaPaneli; 

    [Header("Ses Ayarları")]
    public AudioSource menuSesKaynagi; // Hoparlör
    public AudioClip butonTiklamaSesi; // Tık sesi
    public AudioClip panelAcmaSesi;    // Hakkında paneli açılma sesi

    void Start()
    {
        if (hakkindaPaneli != null)
        {
            hakkindaPaneli.SetActive(false);
        }
    }

    public void OyunaBasla()
    {
        if(butonTiklamaSesi != null && menuSesKaynagi != null) menuSesKaynagi.PlayOneShot(butonTiklamaSesi);
        SceneManager.LoadScene("Bolum1"); 
    }

    public void HakkindaAc()
    {
        if(panelAcmaSesi != null && menuSesKaynagi != null) menuSesKaynagi.PlayOneShot(panelAcmaSesi);
        if (hakkindaPaneli != null) hakkindaPaneli.SetActive(true);
    }

    public void HakkindaKapat()
    {
        if(butonTiklamaSesi != null && menuSesKaynagi != null) menuSesKaynagi.PlayOneShot(butonTiklamaSesi);
        if (hakkindaPaneli != null) hakkindaPaneli.SetActive(false);
    }

    // --- İŞTE EKSİK OLAN VE BUTONDA SEÇMEN GEREKEN KISIM ---
    public void AnaMenuyeGit()
    {
        if(butonTiklamaSesi != null && menuSesKaynagi != null) menuSesKaynagi.PlayOneShot(butonTiklamaSesi);
        
        // "baslangic" yazan yere Ana Menü sahnenin Unity'deki adını tam yaz!
        SceneManager.LoadScene("baslangic"); 
    }
}