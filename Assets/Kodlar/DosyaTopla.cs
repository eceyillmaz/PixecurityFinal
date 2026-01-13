using UnityEngine;

public class DosyaTopla : MonoBehaviour
{
    
    [Header("Ayarlar")]
    public int dosyaPuani = 50; // Her dosya toplandığında kaç puan eklensin?

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer dosyaya çarpan objenin Tag'i "Player" ise
        if (other.CompareTag("Player"))
        {
            // PuanSistemi scriptindeki "PuanArttir" fonksiyonunu çağırıyoruz
            // Not: PuanSistemi'nde 'instance' yapısı kullandığımızı varsayıyorum
            if (PuanSistemi.instance != null)
            {
                PuanSistemi.instance.PuanArttir(dosyaPuani);
                Debug.Log("Dosya toplandı, puan eklendi!");
            }
            else
            {
                Debug.LogWarning("Sahnede PuanSistemi bulunamadı! Lütfen kontrol et.");
            }

            // Dosyayı sahneden yok et
            Destroy(gameObject);
        }
    }
}