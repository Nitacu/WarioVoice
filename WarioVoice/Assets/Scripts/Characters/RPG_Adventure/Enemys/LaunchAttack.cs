using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAttack : MonoBehaviour
{
    private const float _SPEED = 12;
    public Transform _finalPosition;
    public AudioClip _crash;
    public bool _audioCrash = true;
    float T;

    public void shoot()
    {
        float angle;

        Vector2 vector = _finalPosition.position - transform.position;

        Vector3 toTarget = _finalPosition.position - transform.position;

        // Set up the terms we need to solve the quadratic equations.
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = _SPEED * _SPEED + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

        // Check whether the target is reachable at max speed or less.
        if (discriminant < 0)
        {
            // Target is too far away to hit at this speed.
            // Abort, or fire at max speed in its general direction?
            Debug.Log("no puedo golpear al objetivo");
        }

        float discRoot = Mathf.Sqrt(discriminant);

        float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));
        T = T_lowEnergy; ;

        //Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        // Apply the calculated velocity (do not use force, acceleration, or impulse modes)
        GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);

        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<FinalBoss>().activeAttack();
        if (_audioCrash)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = _crash;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
