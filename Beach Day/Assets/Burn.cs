using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class Burn : MonoBehaviour
{
    ParticleSystem fire;
    IEnumerator cool;
    float heatEnergy = 0;
    float burnAmount = 0;
    public float burnThreshold = 1.0f;
    public float toastThreshold = .5f;
    public float coolingRate = .25f;
    public Material toasty;
    public Material burnt;
    // Start is called before the first frame update
    void Awake()
    {
        fire = GetComponent<ParticleSystem>();
        fire.Stop();
        //cool = Cool();
    }
    public void AddHeat(float heatAmount)
    {
        if (heatEnergy < burnThreshold)
        {
            heatEnergy += heatAmount;
        }
        if(heatEnergy > burnAmount)
        {
            burnAmount = heatEnergy;
        }
        if (heatEnergy >= burnThreshold)
        {
            fire.Play();
        }

        if (burnAmount >= burnThreshold)
        {
            GetComponent<Renderer>().material = burnt;
            
        }
        else if (burnAmount >= toastThreshold)
        {
            GetComponent<Renderer>().material = toasty;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Fire>() != null)
        {
            cool = Cool();
            StartCoroutine(cool);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Fire>() != null)
        {
            if (cool != null)
            {
                StopCoroutine(cool);
            }
        }
    }


    IEnumerator Cool()
    {
        Debug.Log("Cooling Begin");
        while (burnAmount > 0)
        {
            burnAmount -= coolingRate * Time.deltaTime;
            //Debug.Log("Cooling: " + burnAmount);
            yield return new WaitForFixedUpdate();
        }
        fire.Stop();
    }

}
