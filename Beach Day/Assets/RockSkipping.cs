using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSkipping : MonoBehaviour
{
    Vector3[] lastPositions = new Vector3[3];
    IEnumerator trackVelocity;
    delegate void CollisionEvent();
    CollisionEvent collisionEvent;
    // Start is called before the first frame update
    public void Release()
    {
        for (int i = 0; i < lastPositions.Length; i++)
        {
            lastPositions[i] = new Vector3(0, 0, 0);
        }
        trackVelocity = TrackVelocity();
        StartCoroutine(trackVelocity);
        collisionEvent = StopTrackingVelocity;
    }

    IEnumerator TrackVelocity()
    {
        if (transform.position != lastPositions[0])
        {
            for (int i = lastPositions.Length - 1; i > 0; i--)
            {
                lastPositions[i] = lastPositions[i - 1];
            }
            lastPositions[0] = transform.position;
        }
        yield return new WaitForFixedUpdate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionEvent();
        
    }
    private void Reset()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
    void StopTrackingVelocity()
    {
        StopAllCoroutines();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            Vector3 motionVector = Vector3.zero;
            for (int i = 0; i < lastPositions.Length - 1; i++)
            {
                motionVector += lastPositions[i + 1] - lastPositions[i];
            }
            motionVector = motionVector / (lastPositions.Length - 1);
            motionVector = motionVector.normalized;
            Vector3 normalVector = new Vector3(0, 1, 0);
            Vector3 componentMult = new Vector3(normalVector.x * motionVector.x, normalVector.y * motionVector.y, normalVector.z * motionVector.z);
            float angle = Mathf.Acos(componentMult.x + componentMult.y + componentMult.z) * 180.0f / Mathf.PI;
            Skip(angle, motionVector, GetComponent<Rigidbody>().velocity.magnitude, GetComponent<Rigidbody>().velocity);
            collisionEvent = Reset;
        }
    }
    void Skip(float impactAngle, Vector3 impactVector,  float speed, Vector3 velocity)
    {
        if(impactAngle <= 65)
        { 
            GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, 0, velocity.z) * .95f;
            Vector3 forceVector = new Vector3(0, impactVector.y, 0) * speed;
            GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);
        }
    }
    
}
