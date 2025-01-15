using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] Vector3 notBehindWallPos;
    [SerializeField] Vector3 notBehindWallRot;

    [SerializeField] Vector3 behindWallPos;
    [SerializeField] Vector3 behindWallRot;

    [SerializeField] float lerpSpeed;
    [SerializeField] LayerMask obsticleMask;
    bool behindWall;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 camNotBehindWallPos = target.position + notBehindWallPos;
        Vector3 direction = target.position - camNotBehindWallPos;

        Debug.Log(direction);

        behindWall = Physics.Raycast(camNotBehindWallPos, direction, Vector3.Distance(target.position, camNotBehindWallPos), obsticleMask);

        Debug.Log(behindWall);

        if (!behindWall)
        {
            Quaternion notBehindWallRotQ = Quaternion.Euler(notBehindWallRot);

            transform.position = Vector3.Lerp(transform.position, target.position + notBehindWallPos, lerpSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, notBehindWallRotQ, lerpSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Quaternion behindWallRotQ = Quaternion.Euler(behindWallRot);

            transform.position = Vector3.Lerp(transform.position, target.position + behindWallPos, lerpSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, behindWallRotQ, lerpSpeed * Time.fixedDeltaTime);
        }
    }
}
