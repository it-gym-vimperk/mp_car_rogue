using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneCarMovement : MonoBehaviour
{
    //general
    Rigidbody rb;
    [SerializeField] Transform Target;
    [SerializeField] Transform Player;

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

    //boss attacks
    [SerializeField] float dashingCooldown;
    [SerializeField] float dashLenght;
    [SerializeField] float dashDis;
    [SerializeField] float dashAngle;
    float dashTimer;
    bool dashing;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Debug.Log(obsticle);

        if (!obsticle)
        {
            CheckForObsticales();
            Vector3 newVelocity = transform.forward * speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
            rb.angularVelocity = new Vector3(0, 0, 0);

            if (!dashing)
            {
                CheckForObsticales();

                speed = Mathf.Clamp(speed += speedAdder * Time.fixedDeltaTime, 0, maxSpeed);

                Turning();
            }

            if (Vector3.Distance(Player.position, transform.position) <= dashDis && Vector3.Angle(transform.forward, Player.transform.position - transform.position) < dashAngle)
            {
                dashing = true;
            }

            if (dashing)
            {
                DashingAttack();
            }
        }
        else
        {
            StartCoroutine(MoveBack());
            dashing = false;

            speed = maxSpeed / 2;

            Vector3 newVelocity = -transform.forward * speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;

            if (!Physics.Raycast(transform.position, -transform.forward, rayCheckDis, obsticleMask))
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

    #region movement logic
    void CheckForObsticales()
    {
        foreach (Transform rayAim in rayAimers)
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

    void TurnLeft(float rotateSpeedVoid)
    {
        transform.Rotate(0, rotateSpeedVoid * Time.fixedDeltaTime, 0);
    }

    void TurnRight(float rotateSpeedVoid)
    {
        transform.Rotate(0, -rotateSpeedVoid * Time.fixedDeltaTime, 0);
    }

    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(1f);

        obsticle = false;
    }
    #endregion

    void DashingAttack()
    {
        Debug.Log("Dashing " + speed);

        speed = Mathf.Clamp(speed += speedAdder * 10 * Time.fixedDeltaTime, 0, maxSpeed * 7);

        dashTimer += 1 * Time.fixedDeltaTime;

        if(dashTimer > dashLenght)
        {
            dashTimer = 0;
            dashing = false;
            StartCoroutine(renewDash());
        }
    }
    
    IEnumerator renewDash()
    {
        yield return new WaitForSeconds(dashingCooldown);

        dashing = true;
    }
}
