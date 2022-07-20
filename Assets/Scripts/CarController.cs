using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject explosion;

    public Color[] carColors;
    public int direction;
    public float speed;
    private SpriteRenderer sprite;
    private Rigidbody2D rigidbody;
    private int health = 30;
    private PlayerStats stats;
    private bool isDead;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = carColors[Random.Range(0, carColors.Length)];
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (direction > 1)
        {
            sprite.flipY = false;
            if (rigidbody.velocity.y < speed * .5f)
                rigidbody.AddForce(Vector2.up * Time.deltaTime * speed, ForceMode2D.Impulse);
        }
        else
        {
            sprite.flipY = true;
            if (rigidbody.velocity.y > -speed * .5f)
                rigidbody.AddForce(Vector2.down * Time.deltaTime * speed, ForceMode2D.Impulse);
        }

        if (transform.position.y > 80 || transform.position.y < -30)
            Destroy(gameObject);
        else if (health <= 0 && !isDead)
            Death();
    }
    private void Death()
    {
        Instantiate(explosion, transform);

        isDead = true;

        speed = 0;
        sprite.color = new Color(.2f, .2f, .2f);

        rigidbody.freezeRotation = false;

        if (stats != null)
        {
            stats.sm.PlaySound(2);
            stats.ScoreChange(50);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= Mathf.RoundToInt(collision.relativeVelocity.magnitude * 10);

        if (collision.gameObject.CompareTag("Player"))
        {
            stats = collision.gameObject.GetComponent<PlayerCollision>().stats;
            stats.ScoreChange(5);
        }
        else if (collision.gameObject.CompareTag("Car"))
            health = 0;
    }
}
