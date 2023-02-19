using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marshmallow : MonoBehaviour, Burnable
{
    public Material toasty;
    public Material burnt;
    float toastThreshold = .5f;
    float burnThreshold = 1.0f;
    float burnAmount = 0;
    float coolingRate = .25f;
    IEnumerator cool;
    GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        cool = Cool();
        fire = transform.GetChild(0).gameObject;
    }

    public void AddHeat(float heatAmount)
    {
        //Debug.Log("Heat Added: " + heatAmount);
        StopCoroutine(cool);
        if (burnAmount < burnThreshold)
        {
            burnAmount += heatAmount;
        }

        if (burnAmount >= burnThreshold)
        {
            GetComponent<Renderer>().material = burnt;
            fire.SetActive(true);
        }
        else if(burnAmount >= toastThreshold)
        {
            GetComponent<Renderer>().material = toasty;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Fire>() != null)
        {
            Debug.Log("Start Cooling");
            StartCoroutine(cool);
        }
    }
    IEnumerator Cool()
    {
        yield return new WaitForFixedUpdate();
        while (burnAmount > 0)
        {
            burnAmount -= coolingRate * Time.deltaTime;
            Debug.Log("Cooling: " + burnAmount);
        }
        fire.SetActive(false);
    }
}
