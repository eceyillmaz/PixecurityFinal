using UnityEngine;
using TMPro; 

public class KazandinEkrani : MonoBehaviour
{
    public TextMeshProUGUI sonucMetni;

    void Start()
    {
        // PuanSistemi içindeki static toplamPuan'ı çekiyoruz
        if (sonucMetni != null)
        {
            sonucMetni.text = "TEBRİKLER! Alican'ın dünyasını virüslerden kurtarmasına ve güvenli dosyaları toplamasına yardım ettin.\n" + 
                              "TOPLAM PUAN: " + PuanSistemi.toplamPuan + " / 275\n" +
                              "SİBER DÜNYAYI KURTARDIN!";
        }
        
    
    }
}
