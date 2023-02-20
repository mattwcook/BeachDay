using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidepoolRock : MonoBehaviour
{
    public GameObject[] tidepoolAnimals;
    enum Size { Small, Medium, Large};
    [SerializeField] Size rockSize;
    public float[] smallWeightDistribution = new float[] { .5f, .3f, .2f};
    public float[] mediumWeightDistribution = new float[] { 0.5f, .1f, .25f, .15f,};
    public float[] largeWeightDistribution = new float[] { 0.5f, .05f, .15f, .15f, .1f,  .05f};
    float[] weightDisribution;
    float smallSpawnRadius = .1f;
    float mediumSpawnRadius = .2f;
    float largeSpawnRadius = .3f;
    float spawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        if (rockSize == Size.Large)
        {
            weightDisribution = largeWeightDistribution;
            spawnRadius = largeSpawnRadius;
        }
        else if (rockSize == Size.Medium)
        {
            weightDisribution = mediumWeightDistribution;
            spawnRadius = mediumSpawnRadius;
        }
        else if (rockSize == Size.Small)
        {
            weightDisribution = smallWeightDistribution;
            spawnRadius = smallSpawnRadius;
        }
    }

    void RockLifted()
    {
        float randomNum = Random.Range(0, 1.0f);
        float weightTotal = weightDisribution[0];
        int numberAnimals = 0;
        for (int i = 0; i < weightDisribution.Length; i++)
        {
            if (randomNum <= weightTotal + weightDisribution[i])
            {
                numberAnimals = i;
            }
            else
            {
                weightTotal += weightDisribution[i];
            }
        }
        GameObject newAnimal;
        for (int i = 0; i < numberAnimals; i++)
        {
            float spawnAngle = Random.Range(-Mathf.PI, Mathf.PI);
            float x = Mathf.Cos(spawnAngle) * Random.Range(0, spawnRadius);
            float z = Mathf.Sin(spawnAngle) * Random.Range(0, spawnRadius); ;
            Vector3 spawnPosition = transform.position + new Vector3(x, 0, z);
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, Random.Range(-180,180), 0));
            newAnimal = Instantiate(tidepoolAnimals[Random.Range(0, tidepoolAnimals.Length)],spawnPosition,spawnRotation);
            newAnimal.transform.localScale *= Random.Range(.75f, 1.5f);
        }

    }

    
}
