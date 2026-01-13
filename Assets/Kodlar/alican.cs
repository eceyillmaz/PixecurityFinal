using UnityEngine;
using UnityEngine.SceneManagement;

public class ALİCAN : MonoBehaviour
{
    [Header("Karakter Değerleri")]
    public float speed = 6f;
    public float jumpForce = 12f;
    public float karakterScale = 1f; 

    // Static olduğu için sahneler arası sayı tutulur ama görsele haber verilmelidir
    public static int canHafizasi = 3; 
    public bool hareketEdebilir = true; 
    public bool konustugumIcinDur = false; 

    [Header("Can Simgeleri")]
    public SpriteRenderer[] kalpler; 

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private Transform kameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        kameraTransform = transform.Find("Main Camera"); 
        
        Time.timeScale = 1; 
        hareketEdebilir = true;
        karakterScale = Mathf.Abs(transform.localScale.x);

        // SAHNE BAŞLADIĞINDA: Mevcut can neyse kalpleri ona göre boya
        CanSimgeleriniDenetle(); 
    }

    // --- BU FONKSİYONU DİKKATLİCE GÜNCELLE ---
    public void CanAzalt() 
    { 
        canHafizasi -= 1; 
        Debug.Log("CAN GİTTİ! Kalan: " + canHafizasi);
        
        // Can düştüğü an kalpleri karartmak için bu fonksiyonu burada ÇAĞIRMALISIN
        CanSimgeleriniDenetle(); 
    }

    public void CanSimgeleriniDenetle()
    {
        // Kalpler dizisi boşsa hata verme, çık
        if (kalpler == null || kalpler.Length == 0) return;

        // Önce hepsini beyaz (dolu) yap
        for (int i = 0; i < kalpler.Length; i++) { if(kalpler[i] != null) kalpler[i].color = Color.white; }

        // Can hafızasına göre sondan başlayarak kalpleri siyah/gri yap
        // 2 can kaldıysa 3. kalp kararır
        if (canHafizasi <= 2 && kalpler.Length > 2) kalpler[2].color = new Color(0.2f, 0.2f, 0.2f);
        // 1 can kaldıysa 2. kalp de kararır
        if (canHafizasi <= 1 && kalpler.Length > 1) kalpler[1].color = new Color(0.2f, 0.2f, 0.2f);

        // Can bittiyse "Oyun Bitti" sahnesine git
        if (canHafizasi <= 0)
        {
            canHafizasi = 3; 
            SceneManager.LoadScene("oyunbitti"); 
        }
    }

    void Update()
    {
        if (konustugumIcinDur || !hareketEdebilir) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isRunning", false);
            return; 
        }

        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (move > 0) transform.localScale = new Vector3(karakterScale, karakterScale, 1f);
        else if (move < 0) transform.localScale = new Vector3(-karakterScale, karakterScale, 1f);

        if (kameraTransform != null)
        {
            Vector3 yeniScale = kameraTransform.localScale;
            if (transform.localScale.x < 0) yeniScale.x = -1f;
            else yeniScale.x = 1f;
            kameraTransform.localScale = yeniScale;
        }

        anim.SetBool("isRunning", move != 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("isJumping"); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) { if (collision.gameObject.CompareTag("Ground")) isGrounded = true; }
    private void OnCollisionExit2D(Collision2D collision) { if (collision.gameObject.CompareTag("Ground")) isGrounded = false; }
}