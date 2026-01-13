using UnityEngine;
using UnityEngine.UI;

public class OltalamaSorusu : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject diyalogPaneli; 
    public Text diyalogMetni;       
    public Button tıklaButonu;      // Bu sefer DOĞRU cevap olacak (Güncelleme için)
    public Button reddetButonu;     // Bu sefer YANLIŞ cevap olacak

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
            
            // --- BUTON GÖREVLERİNİ YER DEĞİŞTİRDİK ---
            // Tıkla (Evet) butonu artık DoğruCevap fonksiyonunu çalıştıracak
            tıklaButonu.onClick.RemoveAllListeners();
            tıklaButonu.onClick.AddListener(DogruCevap);

            // Reddet (Hayır) butonu artık YanlisCevap fonksiyonunu çalıştıracak
            reddetButonu.onClick.RemoveAllListeners();
            reddetButonu.onClick.AddListener(YanlisCevap);

            // Yeni Olumlu Senaryo Metni
            diyalogMetni.text = "Sistem Güvenlik Güncellemesi Mevcut! Güvende kalmak için hemen yüklemek ister misin?";
        }
    }

    void YanlisCevap()
    {
        // Güncellemeyi reddetmek hata olduğu için can azaltıyoruz
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

        // Güncellemeyi kabul etmek doğru davranış
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