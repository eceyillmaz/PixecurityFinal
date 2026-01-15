using UnityEngine;
using UnityEngine.UI;

public class npc1 : MonoBehaviour {
    public GameObject diyalogPaneli;
    public Text diyalogMetni;
    public GameObject butonGrubu; 
    public Button tamamButonu;    

    [Header("Ses Ayarları")]
    public AudioSource sesKaynagi; 
    public AudioClip butonSesi;  

    void Start() {
        diyalogPaneli.SetActive(false); 
        tamamButonu.onClick.AddListener(KapatPanel);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Alican'ın durması için 
            other.GetComponent<ALİCAN>().konustugumIcinDur = true;

            diyalogPaneli.SetActive(true);
            butonGrubu.SetActive(false);           
            tamamButonu.gameObject.SetActive(true); 
            
            diyalogMetni.text = "Selam Alican! Siber kahraman olma yolunda ilk dersin: Güçlü bir şifre asla adın veya doğum tarihin olmamalıdır!";
        }
    }

    void KapatPanel() {
        // ses
        if (sesKaynagi != null && butonSesi != null) {
            sesKaynagi.PlayOneShot(butonSesi); // Sesi bir kez çal
        }

        // Paneli kapatınca Alican'ı tekrar serbest bırak
        GameObject.FindGameObjectWithTag("Player").GetComponent<ALİCAN>().konustugumIcinDur = false;

        diyalogPaneli.SetActive(false);
    }
}
