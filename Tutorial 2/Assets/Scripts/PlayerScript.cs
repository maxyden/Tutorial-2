using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
  

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

   public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject Player;

     private Rigidbody rb;
    private int count;
    private int lives;

    public AudioClip musicClipOne;

     public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;

  


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;

        lives = 3;

        SetCountText();
        winTextObject.SetActive(false);

        SetCountText();
        loseTextObject.SetActive(false);

        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    void update ()
    {
         if (Input.GetKeyUp(KeyCode.E))

        {
            anim.SetFloat("State", 1);

         }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetFloat("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            Destroy(collision.collider.gameObject); 

        }
        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetCountText();
            Destroy (collision.collider.gameObject); 
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 2), ForceMode2D.Impulse); 
            }
        }
    }

     void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
        if (count == 4)
        {
            transform.position = new Vector3(63.0f,0.0f);
            lives = 3;
        }
        else if (count == 8)
        {
            winTextObject.SetActive(true);
            Player.SetActive(false);

            musicSource.clip = musicClipOne;
            musicSource.Stop();

            musicSource.clip = musicClipTwo;
            musicSource.Play();
             musicSource.loop = false;
        }
        livesText.text = "lives: " + lives.ToString();
        if (lives == 0)
        {
            musicSource.clip = musicClipOne;
            musicSource.Stop();


            loseTextObject.SetActive(true);
            Player.SetActive(false);
        }


    }
}