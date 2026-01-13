using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class VirusTakip : MonoBehaviour
{
    public Transform hedef; 
    public float hiz = 3f;
    public float izAraligi = 0.3f;
    
    private List<Vector2> ayakIzleri = new List<Vector2>();
    private float zamanlayici;
    private Rigidbody2D rb;

    [Header("Ses Ayarları")]
    public AudioSource sahneMuzigi; // Sahnedeki normal çalan AudioSource
    public AudioClip takipMuzigi;   // Canavar gelince çalacak yeni müzik

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        if(hedef != null) ayakIzleri.Add(hedef.position);

        // --- SES DEĞİŞTİRME KISMI ---
        if (sahneMuzigi != null && takipMuzigi != null)
        {
            sahneMuzigi.Stop();          // Eski müziği durdur
            sahneMuzigi.clip = takipMuzigi; // Yeni müziği tak
            sahneMuzigi.loop = true;      // Döngüye al
            sahneMuzigi.Play();          // Başlat
        }
    }

    void Update()
    {
        if (hedef == null) return;
        zamanlayici += Time.deltaTime;
        if (zamanlayici >= 0.1f)
        {
            if (ayakIzleri.Count == 0 || Vector2.Distance(hedef.position, ayakIzleri[ayakIzleri.Count - 1]) > izAraligi)
            {
                ayakIzleri.Add(hedef.position);
            }
            zamanlayici = 0;
        }
    }

    void FixedUpdate()
    {
        if (ayakIzleri.Count > 0)
        {
            Vector2 hedefNokta = ayakIzleri[0];
            transform.position = Vector2.MoveTowards(transform.position, hedefNokta, hiz * Time.deltaTime);
            if (Vector2.Distance(transform.position, hedefNokta) < 0.1f)
            {
                ayakIzleri.RemoveAt(0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ayakIzleri.Clear();
            
            // Can azaltma işlemi
            ALİCAN.canHafizasi -= 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}