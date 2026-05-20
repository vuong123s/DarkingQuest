using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

// Dieu khien nhan vat: di chuyen, nhay, danh, nhan sat thuong.
public class Player : MonoBehaviour
{
	public float speed;
    Rigidbody2D rb;
    bool facingRight = true; // nhan vat luc dau quay ve ben phai

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;//check our circle is hitting, chi dinh cac layer la ground
    public float jumpForce;

    public bool isTouchingFront; // kiem tra dang cham vao nhan vat phia truoc khong
    public Transform frontCheck;
    bool wallSliding; // co dang leo tuong ko
    public float wallSlidingSpeed;//toc do leo tuong

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    Animator animator;

    public int health;

    public float timeBetweenAttack;
    float nextAttackTime;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer; // dinh nghia cac lop(layer) la enemy
    public int damage;

    public SpriteRenderer weaponRenderer; // hien thi sprite (hinh anh)

    AudioSource source;

    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip pickupSound;
    public AudioClip swingSound;
    public AudioClip deathSound;

    public GameObject blood;//Tao mau

    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
		// Lay cac component can thiet khi bat dau.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
		initialPosition = transform.position;
	}
    public float maxVelocity = 15f;
	Vector3 initialPosition;//khi bao giu vi tri dau game
	public float minY = -23.2f;// khi roi xuong do cao nho hon thi bien mat tro lại ve cu
    // Update is called once per frame
    void Update()
    {
		// Xu ly tan cong theo cooldown.
        if(Time.time > nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<CameraShake>().Shake();
                animator.SetTrigger("attack");
                source.clip = swingSound;
                source.Play();
                nextAttackTime = Time.time + timeBetweenAttack;
            }

        }
		if (transform.position.y< minY)//tro lai ve vi tri cu khi roi xuong
		{
			health -= damage;
			transform.position = initialPosition;
			rb.linearVelocity = Vector3.zero;

		}
        //Chet
		if (health <= 0)
		{
			GameOver();
			Destroy(gameObject);
		}

        // Lay input va di chuyen theo truc ngang.
		float input = Input.GetAxisRaw("Horizontal");
        // di chuyen theo chieu ngang truc x, y giu nguyen
        rb.linearVelocity = new Vector2(input*speed, rb.linearVelocity.y);

        // khi di chuyen sang phai va dang quay mat ve ben trai
        if(input >0 && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = !facingRight;
        } else if(input < 0 && facingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = !facingRight;
        }

		// Cap nhat animation chay/dung.
		if (input != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Kiem tra dang cham dat.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

		// Nhay khi dang cham dat.
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
            source.clip = jumpSound;
            source.Play();
        }

		// Kiem tra va cham tuong de wall slide.
		isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if(isTouchingFront == true && isGrounded==false && input != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

		if(wallSliding)
        {
            //Mathf.Clamp(gia tri can kep, min, max)
            //-tra ve chinh no neu giua min va max,< min=>min, >max => max
            // khi khong nhan nhay len thi se tuot xuong, va khong nho hon toc do wallsliding, lam cho player rot xuong tu tu hon=> -wallSlidingSpeed la toc do tut xuong
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && wallSliding == true)
        {
            // Bat dau wall jump va tu tat sau wallJumpTime.
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);//invoke la goi method sau 1 khoang thoi gian=> xong tg nhay thi tuot xuong
        }
        
        if(wallJumping == true)
        {
            // nhay ve phia trai, input=am, phai = duong=> -input luon luon duong
            rb.linearVelocity = new Vector2(xWallForce * -input, yWallForce);
            source.clip = jumpSound;
            source.Play();
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping=false;
    }

    public void TakeDamage(int damage)
    {
        source.clip = hurtSound;
        source.Play();
        FindObjectOfType<CameraShake>().Shake();
        health -= damage;
        if(health <= 0)
        {
			GameOver();
			Destroy(gameObject);
        }
		//Quaternion.identity nhan dang dau cham. Tao ban sao perfab tai vi tri position, k thay doi huong
		Instantiate(blood, transform.position, Quaternion.identity);
    }

    public void Attack()
    {
        // Quet enemy trong vung tan cong.
        // tim tat ca cac doi tuong tai vi tri attackPoint, ban kinh attackRange(xac dinh boi OverlapCircleAll) va cham toi, thuoc lop enemyLayer
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D col in enemiesToDamage)
        {
            // truy cap class Enemy tham chieu den phuong thuc TakeDamage
            col.GetComponent<Enemy>().TakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected() // dung de ve gizmos (ho tro ve duong)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // ve 1 hinh cau day(wire phere), tam la attackPoint, ban kinh attackRange
    }

    public void Equip(Weapon weapon)
    {
        // Trang bi vu khi moi va cap nhat chi so.
        source.clip = pickupSound;
        source.Play();
        damage = weapon.damage;
        attackRange = weapon.attackRange;
        weaponRenderer.sprite = weapon.graphic;
        Destroy(weapon.gameObject);
    }
    void GameOver()
    {
        gameOverPanel.SetActive(true);

    }
}
