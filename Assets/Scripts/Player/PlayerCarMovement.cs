using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMovement : MonoBehaviour
{
    //basic driving
    float x;
    float z;
    Rigidbody rb;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask obsticleMask;
    bool grounded;

    //speed related
    public float maxSpeed;
    [SerializeField] float speedAdder;
    float speed;

    //turning related
    public float stearingSpeed;
    float rotateSpeed;

    Vector3 moveVector;

    //drifting
    [SerializeField] float inicialSpeedChange;
    [SerializeField] float driftRotationMultiplier;

    [SerializeField] float lerpSpeed;
    [SerializeField] float timeToLerp;
    float lerpTimer;

    bool vectorsLerped;
    bool drifting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        maxSpeed = SavePayerStats.maxSpeed;
        stearingSpeed = SavePayerStats.stearingSpeed;

        rotateSpeed = stearingSpeed;
    }


    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(transform.position, 0.6f, groundMask);
        Driving();
    }

    void Driving()
    {
        //jakou klávesu z WSAD hráè maèká
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //pokud hráè maèká nìjakou klávesu, a je na zemi, tak se auto rozjede
        if (z != 0 && grounded)
        {
            Vector3 comparingVec = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            //pøi náhlé zmìnì smìru (pokud jsme jeli dopøedu a náhle jsme zaèali jet dozadu), udìlá zmìnu plynule
            #region udìláno ChatGPT
            if ((speed > 1 && ((z > 0 && Vector3.Dot(comparingVec, transform.forward) < 0) ||
                (z < 0 && Vector3.Dot(comparingVec, transform.forward) > 0))))
            {
                // Gradually reduce speed before reversing direction
                speed = Mathf.Clamp(speed -= speedAdder * 2f * Time.fixedDeltaTime, 0, maxSpeed);
            }
            #endregion
            //normální pøidání rychlosti do maximalní rychlosti
            else if (!drifting)
            {
                moveVector = z * transform.forward;
                speed = Mathf.Clamp(speed += speedAdder * Time.fixedDeltaTime, 0, maxSpeed); //speed nikdy nepøekroèí maxspeed
            }
        }
        //hráè již nemaèká klávesud, takže docházi k postupnému spomalení auta
        else if (speed > 0 && grounded)  
        {
            speed = Mathf.Clamp(speed -= speedAdder * Time.fixedDeltaTime, 0, maxSpeed);
        }
        //spomalení auta ve vzduchu
        else if (!grounded)
        {
            speed = Mathf.Clamp(speed -= speedAdder * 0.5f * Time.fixedDeltaTime, 0, maxSpeed);
        }

        //pøi naražení do zdí, resetuje rychlost na malé èíslo, aby mohl hráè vycouvat
        if ((Physics.CheckSphere(transform.position + transform.forward, 0.9f, obsticleMask) ||
           Physics.CheckSphere(transform.position - transform.forward, 0.9f, obsticleMask)))
        {
            speed = 2;
            moveVector = z * transform.forward;
        }

        //pøedaní rychlosti, a udržení rychlosti na y
        Vector3 newVelocity = moveVector.normalized * speed;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;

        //otaèení auta pouze pøi jízdì
        if (Mathf.RoundToInt(speed) > 2 && grounded && !drifting)
        {
            transform.Rotate(0, x * rotateSpeed * Time.fixedDeltaTime, 0); //samotné otaèení auta
        }

        if (grounded)
        {
            Drift();
        }
    }
    void Drift()
    {
        x = Input.GetAxis("Horizontal");

        //pokud maèkáme SPACE, nemáme 0 rychlost a zatáèíme, tak driftujeme
        if (Input.GetKey(KeyCode.Space) && speed > 3 && Input.GetAxis("Horizontal") != 0)
        {
            //drifting
            if (!drifting)
            {
                speed -= inicialSpeedChange * speed;
            }

            drifting = true;
            vectorsLerped = false;
            

            //samotné driftování (snížení ztráty rychlosti a zvýšení otáèivé rychlosti)
            if (speed > 0)
            {
                speed -= speedAdder *0.25f * Time.fixedDeltaTime;

                if (Mathf.RoundToInt(speed) > 2 && grounded)
                {
                    transform.Rotate(0, x * rotateSpeed *driftRotationMultiplier* Time.fixedDeltaTime, 0); //samotné otaèení auta pøi driftu
                }
            }
            else
            {
                speed = 0;
            }
        }
        //konec driftu a reset poupravených promìných
        else
        {
            //setrvaènost po konci driftu
            if (drifting && speed > 3)
            {
                lerpTimer += Time.fixedDeltaTime;
                Debug.Log(speed);

                if (!vectorsLerped)
                {
                   moveVector = Vector3.Lerp(moveVector, transform.forward * z, lerpSpeed * Time.fixedDeltaTime);
                }

                if (Vector3.Distance(moveVector, transform.forward) < 0.2f)
                {
                    vectorsLerped = true;
                }

                if (vectorsLerped || lerpTimer > timeToLerp)
                {
                    drifting = false;
                    rotateSpeed = stearingSpeed;
                    lerpTimer = 0;
                }
                 
            }
            else
            {
                drifting = false;
                rotateSpeed = stearingSpeed;
                lerpTimer = 0;
            }
        }
    }
}
