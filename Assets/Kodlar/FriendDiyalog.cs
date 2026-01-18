using UnityEngine;
using UnityEngine.UI;

public class FriendDiyalog : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject diyalogPaneli; 
    public Text diyalogMetni; 
    public Button tamamButonu;

    [Header("Referanslar")]
    public ALİCAN alicanScripti;
    public GameObject virusObjesi; 

    private bool etkilesimTamamlandi = false;

    void Start()
    {
        // Oyun başında panel kapalı olsun
        if (diyalogPaneli != null) diyalogPaneli.SetActive(false);
        
        // Tamam butonuna tıklanınca fonksiyonu çalıştır
        if (tamamButonu != null)
        {
            tamamButonu.onClick.RemoveAllListeners();
            tamamButonu.onClick.AddListener(PaneliKapat);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Alican (Player) yanına gelirse
        if (other.CompareTag("Player") && !etkilesimTamamlandi)
        {
            if (alicanScripti != null) alicanScripti.hareketEdebilir = false; 
            
            diyalogPaneli.SetActive(true);
            
            diyalogMetni.text = "DİKKAT ALİCAN!\n\nSistemde yabancı bir virüs tespit edildi!" +
                    "Bazı zararlı yazılımlar bilgilerimize ulaşmak için bizi takip edebilir.\n\n" +
                    "Şimdi bilgilerini koruma zamanı! Virüse yakalanmadan haritadaki dosyaları topla " +
                    "ve onları güvenli çıkış kapısına ulaştır. Verilerin sana emanet!";
            
            etkilesimTamamlandi = true; 
        }
    }

    public void PaneliKapat()
    {
        diyalogPaneli.SetActive(false);
        
        if (alicanScripti != null) alicanScripti.hareketEdebilir = true;

        // viürüsü kaybet
        if (virusObjesi != null)
        {
            virusObjesi.SetActive(true); // Kapalı olan virüsü açar
            Debug.Log("Virüs aktif edildi! Kaçmaya başla!");
        }

        Destroy(gameObject); // Friend sahneden silinir
    }
}
