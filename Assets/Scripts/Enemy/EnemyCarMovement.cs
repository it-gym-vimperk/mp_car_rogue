using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarMovement : MonoBehaviour
{
    //general
    Rigidbody rb;
    [SerializeField] Transform Target;

    //turning
    [SerializeField] Transform leftTurn;
    [SerializeField] Transform rightTurn;
    [SerializeField] float rotateSpeed;

    //moving
    [SerializeField] float speedAdder;
    [SerializeField] float maxSpeed;
    float speed;

        //Check for obs
    [SerializeField] Transform[] rayAimers;
    [SerializeField] float rayCheckDis;
    [SerializeField] LayerMask obsticleMask;
    public bool obsticle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (!obsticle)
        {
            CheckForObsticales();

            speed = Mathf.Clamp(speed += speedAdder * Time.fixedDeltaTime, 0, maxSpeed);

            Vector3 newVelocity = transform.forward * speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
            rb.angularVelocity = new Vector3(0,0,0);

            Turning();
        }
        else
        {
            StartCoroutine(MoveBack());

            speed = maxSpeed / 2;

            Vector3 newVelocity = -transform.forward * speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;

            if(!Physics.Raycast(transform.position, -transform.forward, rayCheckDis, obsticleMask))
            {
                if (!Physics.Raycast(transform.position, transform.right, rayCheckDis * 2, obsticleMask))
                {
                    TurnRight(rotateSpeed * 1.5f);
                }
                else if (!Physics.Raycast(transform.position, -transform.right, rayCheckDis * 2, obsticleMask))
                {
                    TurnLeft(rotateSpeed * 1.5f);
                }
            }
           
        }
    }

    void CheckForObsticales()
    {
        foreach(Transform rayAim in rayAimers)
        {
            Vector3 rayAimVector = rayAim.position - transform.position;

            obsticle = Physics.Raycast(transform.position, rayAimVector, rayCheckDis, obsticleMask);

            if (obsticle)
            {
                break;
            }
        }
    }

    void Turning()
    {
        float rightDistance = Vector3.Distance(Target.transform.position, rightTurn.position);
        float leftDistance = Vector3.Distance(Target.transform.position, leftTurn.position);


        if (rightDistance < leftDistance)
        {
            TurnLeft(rotateSpeed);
        }
        else if (rightDistance > leftDistance)
        {
            TurnRight(rotateSpeed);
        }
    }

    void TurnLeft(float rotateSpeed)
    {
        transform.Rotate(0, rotateSpeed * Time.fixedDeltaTime, 0);
    }

    void TurnRight(float rotateSpeed)
    {
        transform.Rotate(0, -rotateSpeed * Time.fixedDeltaTime, 0);
    }


    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(1f);

        obsticle = false;
    }
}
