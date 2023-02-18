using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    [SerializeField] float flapStrength = 5f;
    [SerializeField] float zRotation = 0f;
    [SerializeField] float rotationOffset = 20f;
    [SerializeField] int rotationStrength = 0;

    public Rigidbody2D rb;
    public CapsuleCollider2D cCollider;
    public Manager Manager;
    public bool isBirdAlive = true;

    //sprite changer variables 
    public Sprite[] sprites;
    public float changeInterval = 1f;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;
    private float timer = 0f;    


    private PlayerInput playerInput;
    private InputAction tapScreenAction;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

        Manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
    }

    void Update()
    {

        switchSprites();
        birdRotation();
        

        if(Input.GetKeyDown(KeyCode.Space) && isBirdAlive == true){
            rb.velocity = Vector2.up * flapStrength;
        }
        if(transform.position.y < -40 || transform.position.y > 40){
            Die();
        }
    }
    //Touch Detect
    void Awake(){
        playerInput = GetComponent<PlayerInput>();
        tapScreenAction = playerInput.actions["Flap"];
    }
    void OnEnable(){
        tapScreenAction.performed += Tapped;
    }

    void Tapped(InputAction.CallbackContext Context){
        if(isBirdAlive == true){
            rb.velocity = Vector2.up * flapStrength;
        }
    }
    //Bird rotation
    void birdRotation(){
        zRotation = rotationOffset + (rb.velocity.y / 3) * rotationStrength;
        transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }

    void OnCollisionEnter2D(Collision2D collision){
        Die();
    }
    //When Bird dies
    void Die(){
        rotationStrength = -20;
        rb.velocity = new Vector2(Random.Range(-60,60), Random.Range(40,60));
        Manager.gameOver();
        cCollider.enabled = false;
        isBirdAlive = false;
    }

    void switchSprites(){
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            timer = 0f;
        }
    }
}

