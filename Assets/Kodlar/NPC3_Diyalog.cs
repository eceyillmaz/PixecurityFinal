using UnityEngine;
using UnityEngine.UI;

public class NPC3_Diyalog : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject diyalogPaneli; 
    public Text diyalogMetni;       
    public Button paylasButonu;    // Yanlış Seçenek
    public Button reddetButonu;    // Doğru Seçenek

    [Header("Referanslar")]
    public ALİCAN alicanScripti;    

    [Header("Ses Ayarları")]
    public AudioSource sesKaynagi;    
    public AudioClip dogruSesi;     
    public AudioClip yanlisSesi;     

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

            // Butonları temizle ve bu NPC'ye özel görevleri ata
            paylasButonu.onClick.RemoveAllListeners();
            paylasButonu.onClick.AddListener(YanlisCevap);

            reddetButonu.onClick.RemoveAllListeners();
            reddetButonu.onClick.AddListener(DogruCevap);
            
            diyalogMetni.text = "YABANCI:\n'Selam Alican! Oynadığın oyunda çok nadir bulunan 'Süper Kahraman Kostümü'nü sana hediye etmek istiyorum. Sadece hesabının kullanıcı adını ve şifreni buraya yazarsan, kostümü hemen karakterine ekleyebilirim. Ne dersin?";
        }
    }

    void YanlisCevap()
    {
        //ses
        if (sesKaynagi != null && yanlisSesi != null) {
            sesKaynagi.PlayOneShot(yanlisSesi);
        }

        alicanScripti.CanAzalt(); // Kalbi karartır
        diyalogMetni.text = "EYVAH! Şifreni paylaştığında hesabın başkalarının eline geçebilir ve tüm emeğini kaybedebilirsin. Unutma: Şifre kişiye özeldir, kimseyle paylaşılmaz! Bir canın gitti.";
        Invoke("PaneliKapat", 3f);
    }

    void DogruCevap()
    {
        etkilesimTamamlandi = true;

      
        if (sesKaynagi != null && dogruSesi != null) {
            sesKaynagi.PlayOneShot(dogruSesi);
        }

        diyalogMetni.text = "HARİKA! Gerçek oyun yöneticileri veya tanımadığın oyuncular senden asla şifreni istemez. Şifreni paylaşmamak, dijital kaleni korumanın ilk kuralıdır! +25 Puan.";
        
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
