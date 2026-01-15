using UnityEngine;
using TMPro; // TextMeshPro kullanımı için
using UnityEngine.SceneManagement;

public class PuanSistemi : MonoBehaviour
{
    public static PuanSistemi instance;
    public TextMeshProUGUI puanYazisi;
    public static int toplamPuan = 0; 
    
    [Header("Ses Ayarları")]
    public AudioClip toplamaSesi; // Çalınacak ses dosyası
    private AudioSource sesKaynagi;

    void Awake()
    {
        // Singleton Yapısı:  obje sahneler arası silinmez
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Objeyi sahneler arası koru
            
            // Sesin kesilmemesi için objeye bir AudioSource 
            sesKaynagi = gameObject.AddComponent<AudioSource>();
            sesKaynagi.playOnAwake = false;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start() { YaziyiBulVeGuncelle(); }

    // Sahne her değiştiğinde yazı objesini tekrar bulur
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { YaziyiBulVeGuncelle(); }

    public void YaziyiBulVeGuncelle()
    {
        // Yeni sahnedeki "PuanYazisi" ismini bulur
        if (puanYazisi == null)
        {
            GameObject bulunanObj = GameObject.Find("PuanYazisi");
            if (bulunanObj != null) 
                puanYazisi = bulunanObj.GetComponent<TextMeshProUGUI>();
        }

        if (puanYazisi != null) 
            puanYazisi.text = "Puan: " + toplamPuan.ToString();
    }

    public void PuanArttir(int miktar)
    {
        toplamPuan += miktar;
        

        if (toplamaSesi != null && sesKaynagi != null)
        {
            sesKaynagi.PlayOneShot(toplamaSesi);
        }

        YaziyiBulVeGuncelle(); 
        Debug.Log("Puan Arttı! Yeni Puan: " + toplamPuan);
    }
}
