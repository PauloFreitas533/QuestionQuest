using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] public Image lifeOn1;
    [SerializeField] public Image lifeOff1;
    [SerializeField] public Image lifeOn2;
    [SerializeField] public Image lifeOff2;
    [SerializeField] public Image lifeOn3;
    [SerializeField] public Image lifeOff3;
    [SerializeField] 
    public MovementDirection movementDirection;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    public int life;
    public int lifeMax = 3;
    private bool isDeath = false;
    private Rigidbody2D rb;
    public Text keyTxt;
    public GameObject keyPrefab;
    public bool keyHas;
    public bool heartHas;
    public AudioSource hurtAudio, deathAudioSource;
    public float hitRecoveryTime = 1.0f;
    private float lastHitTime = -999f;
    public int key;
    public bool heartCollected = false;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        life = lifeMax;
        rb = GetComponent<Rigidbody2D>();
        key = 0;
        keyHas = false;
        heartHas = false;
	  this.movementDirection = MovementDirection.Right;
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");

        if (moveX > 0 )
        {
            this.movementDirection = MovementDirection.Right;
        }
        else if (moveX < 0)
        {
            this.movementDirection = MovementDirection.Left;
        }
        keyTxt.text = key.ToString();

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && !isDeath)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0 && !isDeath)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling && !isDeath)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        //m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        //m_animator.SetBool("WallSlide", m_isWallSliding);

        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }

        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling && !isDeath)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling && !isDeath)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !m_rolling && !m_isWallSliding && !isDeath)
{
    StartCoroutine(RollForDuration(0.6f));
}


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && !isDeath)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && !isDeath)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        if (gameObject.CompareTag("chestgold"))
        {
            Debug.Log("chestgold");
            m_animator.SetTrigger("chestgold");
            //keyPrefab.gameObject.SetActive(true);
            //col.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

private IEnumerator RollForDuration(float duration)
{
    // Salvar os valores originais do Box Collider
    BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
    Vector2 originalOffset = boxCollider2D.offset;
    Vector2 originalSize = boxCollider2D.size;

    // Definir os novos valores do Box Collider
    boxCollider2D.offset = new Vector2(originalOffset.x, 0.3678495f);
    boxCollider2D.size = new Vector2(originalSize.x, 0.6116992f);

    m_animator.SetTrigger("Roll");
    m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);

    // Aguardar o tempo de duração
    yield return new WaitForSeconds(duration);

    // Restaurar os valores originais do Box Collider
    boxCollider2D.offset = originalOffset;
    boxCollider2D.size = originalSize;

    // Reiniciar o rolo
    m_rolling = false;
    m_animator.ResetTrigger("Roll");
}

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("thorn") && Time.time > lastHitTime + hitRecoveryTime)
        {
            lastHitTime = Time.time;
            this.Damage();
        }

            if (col.gameObject.CompareTag("key"))
        {
            key = key + 1;
            keyHas = true;
            Destroy(col.gameObject);
        }

    }

    public void Damage()
    {
        life -= 1;

        switch (life)
        {
            case 2:
                m_animator.SetTrigger("Hurt");
                hurtAudio.Play();
                lifeOn3.enabled = true;
                lifeOff3.enabled = false;
                lifeOn2.enabled = false;
                lifeOff2.enabled = true;
                lifeOn1.enabled = false;
                lifeOff1.enabled = true;
                break;
            case 1:
                m_animator.SetTrigger("Hurt");
                hurtAudio.Play();
                lifeOn3.enabled = true;
                lifeOff3.enabled = false;
                lifeOn2.enabled = true;
                lifeOff2.enabled = false;
                lifeOn1.enabled = false;
                lifeOff1.enabled = true;
                break;
            case 0:
                m_animator.SetTrigger("Death");
                deathAudioSource.Play();
                lifeOn3.enabled = true;
                lifeOff3.enabled = false;
                lifeOn2.enabled = true;
                lifeOff2.enabled = false;
                lifeOn1.enabled = true;
                lifeOff1.enabled = false;
                isDeath = true;
                Invoke("LoadGameOverScene", 2.5f);
                break;
            default:
                break;
        }
        heartCollected = false;
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(2);
    }
}
