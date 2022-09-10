using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float pushForce = 50f;
    public float minimumVelocityX = 8f;
    Rigidbody rbd;
    GameMaster gm;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>();
        Invoke("GoBall", 1);
    }

    void Update()
    {
        Vector3 vel = rbd.velocity;
        // This is to make sure that the ball does not start slowing down past a certain point
        if (vel.x < minimumVelocityX && vel.x > -minimumVelocityX) // if velocity x is between negative and positive threshold
        {
            if (vel.x > 0)
                vel.x = minimumVelocityX;
            if (vel.x < 0)
                vel.x = -minimumVelocityX;

            rbd.velocity = vel;
        }


    }

    // Start the ball in a random direction
    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rbd.AddForce(new Vector3(pushForce, -15, 0));
        }
        else
        {
            rbd.AddForce(new Vector3(-pushForce, -15, 0));
        }
    }

    void ResetBall()
    {
        rbd.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector3 vel;
            vel.x = -rbd.velocity.x * 2f;
            vel.y = (rbd.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            vel.z = 0;
            rbd.velocity = vel;
        }

        if (coll.collider.CompareTag("Finish"))
        {
            if (coll.gameObject.name[1] == '1')
            {
                gm.TeamScored(2);
            }
            else
            {
                gm.TeamScored(1);
            }
            RestartGame();
        }
    }

}
