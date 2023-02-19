using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    float maxBurnRate = 1.0f;
    float triggerSize;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<SphereCollider>() != null)
        {
            triggerSize = GetComponent<SphereCollider>().radius * transform.lossyScale.magnitude;
        }
        else if (GetComponent<BoxCollider>() != null)
        {
            triggerSize = GetComponent<BoxCollider>().size.magnitude / 2 * transform.lossyScale.magnitude;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay: " + other.name);
        
        if (other.GetComponent<Burnable>() != null)
        {
            Debug.Log("Is Burnable");
            float distanceToCenter = (transform.position - other.transform.position).magnitude;
            float burnAmount = maxBurnRate * (triggerSize - distanceToCenter) / triggerSize * Time.deltaTime;
            other.GetComponent<Burnable>().AddHeat(burnAmount);
        }
    }


}
