using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef; 
    public float yumusaklik = 0.125f; 
    public Vector3 ofset = new Vector3(0, 0, -10f);

    void Start()
    {
        // Sahne yeni açıldığında eğer hedef boşsa Alican'ı otomatik bul
        if (hedef == null)
        {
            GameObject oyuncu = GameObject.FindGameObjectWithTag("Player");
            if (oyuncu != null) hedef = oyuncu.transform;
        }

        // Sahne başlar başlamaz kamerayı hemen Alican'ın olduğu yere ışınla
        if (hedef != null)
        {
            transform.position = hedef.position + ofset;
        }
    }

    void LateUpdate()
    {
        if (hedef != null)
        {
            Vector3 istenenPozisyon = hedef.position + ofset;
            transform.position = Vector3.Lerp(transform.position, istenenPozisyon, yumusaklik);
        }
    }
}