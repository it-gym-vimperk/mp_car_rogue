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
        //jakou kl�vesu z WSAD hr�� ma�k�
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //pokud hr�� ma�k� n�jakou kl�vesu, a je na zemi, tak se auto rozjede
        if (z != 0 && grounded)
        {
            Vector3 comparingVec = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            //p�i n�hl� zm�n� sm�ru (pokud jsme jeli dop�edu a n�hle jsme za�ali jet dozadu), ud�l� zm�nu plynule
            #region ud�l�no ChatGPT
            if ((speed > 1 && ((z > 0 && Vector3.Dot(comparingVec, transform.forward) < 0) ||
                (z < 0 && Vector3.Dot(comparingVec, transform.forward) > 0))))
            {
                // Gradually reduce speed before reversing direction
                speed = Mathf.Clamp(speed -= speedAdder * 2f * Time.fixedDeltaTime, 0, maxSpeed);
            }
            #endregion
            //norm�ln� p�id�n� rychlosti do maximaln� rychlosti
            else if (!drifting)
            {
                moveVector = z * transform.forward;
                speed = Mathf.Clamp(speed += speedAdder * Time.fixedDeltaTime, 0, maxSpeed); //speed nikdy nep�ekro�� maxspeed
            }
        }
        //hr�� ji� nema�k� kl�vesud, tak�e doch�zi k postupn�mu spomalen� auta
        else if (speed > 0 && grounded)  
        {
            speed = Mathf.Clamp(speed -= speedAdder * Time.fixedDeltaTime, 0, maxSpeed);
        }
        //spomalen� auta ve vzduchu
        else if (!grounded)
        {
            speed = Mathf.Clamp(speed -= speedAdder * 0.5f * Time.fixedDeltaTime, 0, maxSpeed);
        }

        //p�i nara�en� do zd�, resetuje rychlost na mal� ��slo, aby mohl hr�� vycouvat
        if ((Physics.CheckSphere(transform.position + transform.forward, 0.9f, obsticleMask) ||
           Physics.CheckSphere(transform.position - transform.forward, 0.9f, obsticleMask)))
        {
            speed = 2;
            moveVector = z * transform.forward;
        }

        //p�edan� rychlosti, a udr�en� rychlosti na y
        Vector3 newVelocity = moveVector.normalized * speed;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;

        //ota�en� auta pouze p�i j�zd�
        if (Mathf.RoundToInt(speed) > 2 && grounded && !drifting)
        {
            transform.Rotate(0, x * rotateSpeed * Time.fixedDeltaTime, 0); //samotn� ota�en� auta
        }

        if (grounded)
        {
            Drift();
        }
    }
    void Drift()
    {
        x = Input.GetAxis("Horizontal");

        //pokud ma�k�me SPACE, nem�me 0 rychlost a zat���me, tak driftujeme
        if (Input.GetKey(KeyCode.Space) && speed > 3 && Input.GetAxis("Horizontal") != 0)
        {
            //drifting
            if (!drifting)
            {
                speed -= inicialSpeedChange * speed;
            }

            drifting = true;
            vectorsLerped = false;
            

            //samotn� driftov�n� (sn�en� ztr�ty rychlosti a zv��en� ot��iv� rychlosti)
            if (speed > 0)
            {
                speed -= speedAdder *0.25f * Time.fixedDeltaTime;

                if (Mathf.RoundToInt(speed) > 2 && grounded)
                {
                    transform.Rotate(0, x * rotateSpeed *driftRotationMultiplier* Time.fixedDeltaTime, 0); //samotn� ota�en� auta p�i driftu
                }
            }
            else
            {
                speed = 0;
            }
        }
        //konec driftu a reset poupraven�ch prom�n�ch
        else
        {
            //setrva�nost po konci driftu
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
