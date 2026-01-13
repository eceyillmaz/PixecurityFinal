using UnityEngine;
using UnityEngine.UI;

public class NPC4_Diyalog : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject diyalogPaneli; 
    public Text diyalogMetni;       
    public Button baglanButonu;    // Yanlış Seçenek (Hemen Bağlan)
    public Button reddetButonu;    // Doğru Seçenek (Kendi İnternetimi Kullan)

    [Header("Referanslar")]
    public ALİCAN alicanScripti;    

    [Header("Ses Ayarları")]
    public AudioSource sesKaynagi;    // Hoparlör bileşeni
    public AudioClip dogruSesi;      // Doğru cevap tık sesi
    public AudioClip yanlisSesi;     // Yanlış cevap/can gitme sesi

    private bool etkilesimTamamlandi = false;

    void Start()
    {
        diyalogPaneli.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !etkilesimTamamlandi)
        {
            alicanScripti.hareketEdebilir = false; 
            diyalogPaneli.SetActive(true);

            baglanButonu.onClick.RemoveAllListeners();
            baglanButonu.onClick.AddListener(YanlisCevap);

            reddetButonu.onClick.RemoveAllListeners();
            reddetButonu.onClick.AddListener(DogruCevap);
            
            diyalogMetni.text = "GİZEMLİ SİNYAL:\n'Hey Alican! Şanslı günündesin. Burada şifresiz ve ücretsiz bir Wi-Fi ağı buldum: [BEDAVA_INTERNET]. Hemen bağlanıp oyunlarını indirmek ister misin?'";
        }
    }

    void YanlisCevap()
    {
        // --- SES EKLEME ---
        if (sesKaynagi != null && yanlisSesi != null) {
            sesKaynagi.PlayOneShot(yanlisSesi);
        }

        alicanScripti.CanAzalt(); 
        diyalogMetni.text = "EYVAH! Şifresiz ve halka açık Wi-Fi ağları güvenli değildir. Kötü niyetli kişiler bu ağ üzerinden bilgilerini çalabilir! Bir canın gitti.";
        Invoke("PaneliKapat", 3f);
    }

   void DogruCevap()
    {
        etkilesimTamamlandi = true;

        // --- SES EKLEME ---
        if (sesKaynagi != null && dogruSesi != null) {
            sesKaynagi.PlayOneShot(dogruSesi);
        }

        diyalogMetni.text = "HARİKA! Doğru bildin ve 25 puan kazandın!";
        
        if (PuanSistemi.instance != null) {
            PuanSistemi.instance.PuanArttir(25); 
        }
        
        Invoke("PaneliKapat", 2f);
    }

    void PaneliKapat()
    {
        diyalogPaneli.SetActive(false);
        alicanScripti.hareketEdebilir = true; 
        if(etkilesimTamamlandi) Destroy(gameObject); 
    }
}