using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalGecis : MonoBehaviour
{
    [Header("Hedef Sahne Ayarı")]
    public string hedefSahneAdi; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(hedefSahneAdi))
            {
                SceneManager.LoadScene(hedefSahneAdi);
            }
            else
            {
                Debug.LogError("HATA: Portalda 'Hedef Sahne Adi' boş bırakılmış!");
            }
        }
    }
}
