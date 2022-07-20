using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float steeringSpeed;
    public float currentSpeed;

    public GameObject playerObject;

    [HideInInspector] public bool isDead;

    private Rigidbody2D rigidbody;

    private bool pressingLeft;
    private bool pressingRight;

    private bool pressingGas;
    private bool pressingStop;

    private void Start()
    {
        rigidbody = playerObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            /*
            if ((Input.GetAxis("Horizontal") > .1f || pressingRight) && rigidbody.velocity.x < 1f)
                rigidbody.AddForce(Vector2.right * Time.deltaTime * steeringSpeed, ForceMode2D.Impulse);
            if ((Input.GetAxis("Horizontal") < -.1f || pressingLeft) && rigidbody.velocity.x > -1f)
                rigidbody.AddForce(Vector2.left * Time.deltaTime * steeringSpeed, ForceMode2D.Impulse);
            */
            if (Input.GetAxis("Horizontal") > .1f || pressingRight)
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, new Vector2(4, 0), Time.deltaTime * steeringSpeed);
            if (Input.GetAxis("Horizontal") < -.1f || pressingLeft)
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, new Vector2(-4, 0), Time.deltaTime * steeringSpeed);
            if (Input.GetAxis("Horizontal") == 0 && !pressingRight && !pressingLeft)
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, new Vector2(0, 0), Time.deltaTime * steeringSpeed * 2);

            if (playerObject.transform.position.y < 0 && (Input.GetAxis("Vertical") > .1f || pressingGas))
                playerObject.transform.Translate(Vector2.up * Time.deltaTime * 4);
            if (playerObject.transform.position.y > -8 && (Input.GetAxis("Vertical") < -.1f || pressingStop))
                playerObject.transform.Translate(Vector2.down * Time.deltaTime * 4);

            currentSpeed = 10 + playerObject.transform.position.y;
        }
        else
            currentSpeed = 0;
    }
    public void PressingGas(bool isGas)
    {
        if (isGas)
            pressingGas = true;
        else
            pressingStop = true;
    }
    public void UnPressingGas(bool isGas)
    {
        if (isGas)
            pressingGas = false;
        else
            pressingStop = false;
    }
    public void PressingDir(bool isLeft)
    {
        if (isLeft)
            pressingLeft = true;
        else
            pressingRight = true;
    }
    public void UnPressingDir(bool isLeft)
    {
        if (isLeft)
            pressingLeft = false;
        else
            pressingRight = false;
    }
}
