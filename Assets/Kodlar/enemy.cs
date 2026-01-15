using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour 
{
    public GameObject diyalogPaneli;
    public Text diyalogMetni;
    public GameObject butonGrubu; 
    public Button tamamButonu;      
    public Button dogru; 
    public Button yanlis; 
    public ALİCAN alicanScripti;

    [Header("Görünme Ayarları")]
    public float gorunmeMesafesi = 5f; 
    private bool gorunduMu = false; 
    private SpriteRenderer sprite;
    private Collider2D col;

    private bool soruBilindi = false;

    void Start() 
    {
        diyalogPaneli.SetActive(false);
        
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        
        // Başta sprite'ı kapatıyoruz
        if(sprite != null) sprite.enabled = false;

        dogru.onClick.RemoveAllListeners();
        dogru.onClick.AddListener(YanlisCevapVerildi); 

        yanlis.onClick.RemoveAllListeners();
        yanlis.onClick.AddListener(DogruCevapVerildi); 
    }

    void Update()
    {
        // Mesafe kontrolü
        if (!gorunduMu && alicanScripti != null)
        {
            float mesafe = Vector2.Distance(transform.position, alicanScripti.transform.position);
            
            if (mesafe <= gorunmeMesafesi)
            {
                DüsmaniGoster();
            }
        }
    }

    void DüsmaniGoster()
    {
        gorunduMu = true;
        if(sprite != null) sprite.enabled = true; 
        Debug.Log("Düşman belirdi!");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Sadece düşman görünür olduğunda diyalog açılır
        if (gorunduMu && other.CompareTag("Player") && !soruBilindi) 
        {
            alicanScripti.hareketEdebilir = false; 

            diyalogPaneli.SetActive(true);
            butonGrubu.SetActive(true);             
            tamamButonu.gameObject.SetActive(false); 
            diyalogMetni.text = "DUR! Sorumu bilmeden geçemezsin:\n\nSiber dünyada güçlü bir şifre oluştururken doğum tarihimizi kullanmak yeterli ve güvenlidir.";
        }
    }

    void YanlisCevapVerildi() 
    {
        if(alicanScripti != null) alicanScripti.CanAzalt(); 
        diyalogMetni.text = "YANLIŞ! HAHAH! Doğum tarihin kolayca bulunabilir. Tekrar dene!";
    }

    void DogruCevapVerildi() 
    {
        if (!soruBilindi) 
        {
            soruBilindi = true;
            diyalogMetni.text = "AFERİN! Doğru bildin ve 25 Puan kazandın. Artık devam edebilirsin.";
            
            if (PuanSistemi.instance != null)
            {
                PuanSistemi.instance.PuanArttir(25);
            }

            butonGrubu.SetActive(false);            
            tamamButonu.gameObject.SetActive(true); 
            
            tamamButonu.onClick.RemoveAllListeners(); 
            tamamButonu.onClick.AddListener(YoluAc);
        }
    }

    void YoluAc() 
    {
        if(alicanScripti != null) alicanScripti.hareketEdebilir = true; 
        diyalogPaneli.SetActive(false);
        Destroy(gameObject, 0.1f); 
    }
}
