using UnityEngine;
using UnityEngine.UI;

public class OltalamaSorusu : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject diyalogPaneli; 
    public Text diyalogMetni;       
    public Button tıklaButonu;      
    public Button reddetButonu;     

    [Header("Referanslar")]
    public ALİCAN alicanScripti;    

    [Header("Ses Ayarları")]
    public AudioSource sesKaynagi;  // Hoparlör bileşeni
    public AudioClip dogruSesi;     // Başarı tıkı
    public AudioClip yanlisSesi;    // Hata/Can gitme sesi

    private bool etkilesimTamamlandi = false;

    void Start()
    {
        diyalogPaneli.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Temas gerçekleşince ve daha önce cevaplanmamışsa
        if (other.CompareTag("Player") && !etkilesimTamamlandi)
        {
            if(alicanScripti != null) alicanScripti.hareketEdebilir = false; // Alican'ı durdur
            
            diyalogPaneli.SetActive(true);
            
           
            tıklaButonu.onClick.RemoveAllListeners();
            tıklaButonu.onClick.AddListener(DogruCevap);

         
            reddetButonu.onClick.RemoveAllListeners();
            reddetButonu.onClick.AddListener(YanlisCevap);

        
            diyalogMetni.text = "Sistem Güvenlik Güncellemesi Mevcut! Güvende kalmak için hemen yüklemek ister misin?";
        }
    }

    void YanlisCevap()
    {
   
        if (sesKaynagi != null && yanlisSesi != null) {
            sesKaynagi.PlayOneShot(yanlisSesi); // Hata sesi
        }

        if(alicanScripti != null) alicanScripti.CanAzalt(); // Kalp kararır
        
        diyalogMetni.text = "EYVAH! Güncellemeyi reddettiğin için sistemin savunmasız kaldı ve virüs bulaştı! Bir canın gitti. ipucu:İPUCU: Yazılım güncellemeleri, sistemdeki güvenlik açıklarını kapatan yamalar içerir. " +
                           "Bunları reddetmek, kapıyı hırsızlara açık bırakmak gibidir!";
        Invoke("PaneliKapat", 3f);
    }

    void DogruCevap()
    {
        etkilesimTamamlandi = true;

        if (sesKaynagi != null && dogruSesi != null) {
            sesKaynagi.PlayOneShot(dogruSesi); // Başarı sesi
        }

        diyalogMetni.text = "HARİKA! Sistemini güncel tutarak siber saldırganlara karşı büyük bir zafer kazandın!";
        
        if (PuanSistemi.instance != null) {
            PuanSistemi.instance.PuanArttir(25); // Puan ekle
        }
        
        Invoke("PaneliKapat", 2f);
    }

    void PaneliKapat()
    {
        diyalogPaneli.SetActive(false);
        if(alicanScripti != null) alicanScripti.hareketEdebilir = true; // Alican'ı serbest bırak
        
        if(etkilesimTamamlandi) Destroy(gameObject); 
    }
}
