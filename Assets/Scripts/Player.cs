/*using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   public int coins;
   public int health = 100;
   public float moveSpeed = 5f;
   public float jumpForce = 10f;
   public Transform groundCheck;
   public float groundCheckRadius = 0.2f;
   public LayerMask groundLayer;
   private Image healthImage;

   public AudioClip jumpClip;
   public AudioClip hurtClip;

   private Rigidbody2D rb;
   private bool isGrounded;

   private Animator animator;

   private SpriteRenderer spriteRenderer;

   private AudioSource audioSource;

   public int extraJumpsValue = 1;
   private int extraJumps;

   public GameObject gameOverPanel;





   void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
       healthImage = GameObject.FindWithTag("Health").GetComponent<Image>();
       audioSource = GetComponent<AudioSource>();

       extraJumps = extraJumpsValue;
       gameOverPanel.SetActive(false);
   }

   void Update()
   {
       float moveInput = Input.GetAxis("Horizontal");
       rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

       if(rb.linearVelocityX != 0)
       {
           if(rb.linearVelocityX > 0)
           {
               spriteRenderer.flipX = false;
           }
           else
           {
               spriteRenderer.flipX = true;
           }
       }


       if (isGrounded)
       {
           extraJumps = extraJumpsValue;
       }

       if (Input.GetKeyDown(KeyCode.Space))
       {
           if (isGrounded)
           {
               rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
               PlaySFX(jumpClip);
           }
           else if(extraJumps > 0)
           {
               rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
               extraJumps--;
               PlaySFX(jumpClip);
           }
       }

       SetAnimation(moveInput);

       healthImage.fillAmount = health / 100f;

       if(transform.position.y < -10)
       {
           Die();
       }
   }   

   private void FixedUpdate()
   {
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
   }
   private void SetAnimation(float moveInput)
   {
       if (isGrounded)
       {
           if (moveInput == 0)
           {
               animator.Play("Player_Idle");
           }
           else
           {
               animator.Play("Player_Run");
           }
       }
       else
       {
           if (rb.linearVelocity.y > 0)
           {
               animator.Play("Player_Jump");
           }
           else
           {
               animator.Play("Player_Fall");
           }
       }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if(collision.gameObject.tag == "Damage")
       {
           PlaySFX(hurtClip);
           health -= 25;
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
           StartCoroutine(BlinkRed());

           if(health <= 0)
           {
               Die();
           }
       }
   }

   private IEnumerator BlinkRed()
   {
       spriteRenderer.color = Color.red;
       yield return new WaitForSeconds(0.1f);
       spriteRenderer.color = Color.white;
   }
   /*private void Die()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
   }*

private void Die()
    {
        gameOverPanel.SetActive(true); // 🔥 PANEL AÇ
        Time.timeScale = 0f;           // 🔥 OYUN DUR
    }


    public void PlaySFX(AudioClip audioClip, float volume= 1f)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int coins;
    public int health = 100;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public AudioClip jumpClip;
    public AudioClip hurtClip;

    public int extraJumpsValue = 1;
    public GameObject gameOverPanel;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Image healthImage;

    private bool isGrounded;
    private int extraJumps;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        healthImage = GameObject.FindWithTag("Health").GetComponent<Image>();

        extraJumps = extraJumpsValue;

        gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isDead)
            return;

        float moveInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
            }
        }

        SetAnimation(moveInput);

        healthImage.fillAmount = health / 100f;

        if (transform.position.y < -10)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        PlaySFX(jumpClip);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
                animator.Play("Player_Idle");
            else
                animator.Play("Player_Run");
        }
        else
        {
            if (rb.linearVelocity.y > 0)
                animator.Play("Player_Jump");
            else
                animator.Play("Player_Fall");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        if (collision.gameObject.CompareTag("Damage"))
        {
            PlaySFX(hurtClip);

            health -= 25;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void PlaySFX(AudioClip audioClip, float volume = 1f)
    {
        if (audioSource == null || audioClip == null)
            return;

        audioSource.PlayOneShot(audioClip, volume);
    }
}*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int coins;
    public int health = 100;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public AudioClip jumpClip;
    public AudioClip hurtClip;

    public int extraJumpsValue = 1;

    public GameObject gameOverPanel;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Image healthImage;

    private bool isGrounded;
    private int extraJumps;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        healthImage = GameObject.FindWithTag("Health").GetComponent<Image>();

        extraJumps = extraJumpsValue;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isDead)
            return;

        float moveInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.x > 0)
            spriteRenderer.flipX = false;
        else if (rb.linearVelocity.x < 0)
            spriteRenderer.flipX = true;

        if (isGrounded)
            extraJumps = extraJumpsValue;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
            }
        }

        SetAnimation(moveInput);

        if (healthImage != null)
            healthImage.fillAmount = health / 100f;

        if (transform.position.y < -10)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        PlaySFX(jumpClip);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
                animator.Play("Player_Idle");
            else
                animator.Play("Player_Run");
        }
        else
        {
            if (rb.linearVelocity.y > 0)
                animator.Play("Player_Jump");
            else
                animator.Play("Player_Fall");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        if (collision.gameObject.CompareTag("Damage"))
        {
            health -= 25;

            PlaySFX(hurtClip);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}