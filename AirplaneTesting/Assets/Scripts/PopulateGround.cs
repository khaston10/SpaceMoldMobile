using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGround : MonoBehaviour
{
    public Terrain groundPlane;
    public GameObject bushPrefab;

    Vector3[] vertices;

    private GameObject temp;
    private Vector3 tempVec;


    // Creating new world variables
    public float worldBounds;
    public int numberOfBushes;

    void Start()
    { 

        // Create bushes
        for (int i = 0; i < numberOfBushes; i++)
        {
            float randX = Random.Range(-worldBounds, worldBounds);
            float randZ = Random.Range(-worldBounds, worldBounds);
            tempVec = new Vector3(randX, -12f, randZ);
            temp = Instantiate(bushPrefab);
            temp.transform.position = tempVec;
        }
    }

    public int FindHeightOfGroundAtXZ(float x, float z)
    {

        return 3;
    }

}
