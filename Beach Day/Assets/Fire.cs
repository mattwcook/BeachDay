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
        float avgScale = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3;
        if (GetComponent<SphereCollider>() != null)
        {
            triggerSize = GetComponent<SphereCollider>().radius * avgScale;
        }
        else if (GetComponent<BoxCollider>() != null)
        {
            triggerSize = GetComponent<BoxCollider>().size.magnitude / 2 * avgScale;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.GetComponent<Burn>() != null)
        {
            float distanceToCenter = (transform.position - other.transform.position).magnitude;
            
            float burnAmount = maxBurnRate * (triggerSize - distanceToCenter) / triggerSize * Time.deltaTime;
            other.GetComponent<Burn>().AddHeat(burnAmount);
        }
    }


}
