using UnityEngine;

public class SesSusturucu : MonoBehaviour
{
    void Awake()
    {
        // Sahnedeki tüm ses kaynaklarını (hoparlörleri) bulur
        AudioSource[] tumSesler = FindObjectsOfType<AudioSource>();

        foreach (AudioSource ses in tumSesler)
        {
            // Eğer bu ses objesi bu sahnede yeni oluşturulmadıysa (önceki sahneden kaldıysa) sustur
            // Ya da direkt hepsini susturup bu sahneninkini sonra başlatabilirsin
            ses.Stop();
        }
    }
}