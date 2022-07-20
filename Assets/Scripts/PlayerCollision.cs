using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerStats stats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            Bonus bonus = collision.GetComponent<Bonus>();

            switch (bonus.type)
            {
                case (BonusType)0:
                    stats.FuelChange(bonus.change);
                    break;
                case (BonusType)1:
                    stats.HealthChange(bonus.change);
                    break;
                default:
                    Debug.Log($"Type of bonus {bonus.name} is undefined");
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int velocity = Mathf.RoundToInt(collision.relativeVelocity.magnitude * 5);

        stats.HealthChange(-velocity);

        //GetComponent<Rigidbody2D>().freezeRotation = false;
    }
}
