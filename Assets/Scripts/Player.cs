using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    
    public AudioClip flapSound;

    public Sprite[] sprites;
    private int spriteIndex = 0;

    public float gravity = -9.8f;
    public float strenght = 5f;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))//space or mouse button makes the bird flap
        {
            direction = Vector3.up * strenght;
            AudioSource.PlayClipAtPoint(flapSound, transform.position, 1f);
        }

        direction.y += gravity * Time.deltaTime;//apply gravity to direction

        transform.position += direction * Time.deltaTime;//update character position
    }

    private void OnEnable()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }

    private void AnimateSprite()
    {
        spriteIndex++;//get next sprite index

        if (spriteIndex >= sprites.Length)//loop around to the first sprite
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacle":
            {
                FindAnyObjectByType<GameManager>().GameOver();//SHOULD CHANGE ASAP
            }break;

            case "Scoring":
            {
                FindAnyObjectByType<GameManager>().IncreaseScore();//SHOULD CHANGE ASAP
            }break;
        }
    }
}
