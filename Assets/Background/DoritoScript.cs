using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorito : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab;
    public int rows = 5;              // Number of rows in the grid
    public int columns = 5;           // Number of columns in the grid
    public float spacing = 2.0f;      // Spacing between objects

    void Start()
   {
        // Calculate the total grid width and height
        float gridWidth = columns * spacing;
        float gridHeight = rows * spacing;

        // Calculate the starting position for the center of the grid
        Vector3 startPosition = new Vector3(-gridWidth / 2, -gridHeight / 2, 0);

        // Loop through rows
        for (int row = 0; row < rows; row++)
        {
            // Loop through columns
            for (int col = 0; col < columns; col++)
            {
                // Calculate the position for the current object with the offset
                float randomYOffset = Random.Range(-2.0f, 2.0f); // Random Y offset
                Vector3 spawnPosition = startPosition + new Vector3(col * spacing, row * spacing + randomYOffset, 0) + Vector3.forward * 50;

                // Generate a random rotation for the prefab
                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

                // Generate a random scale for the prefab
                Vector3 randomScale = (Random.Range(0, 2) == 0) ? new Vector3(1f, 1f, 1f) : new Vector3(0.6f, 0.6f, 0.6f);

                // Instantiate the prefab at the calculated position with the random rotation and scale
                GameObject instantiatedPrefab = Instantiate(prefab, spawnPosition, randomRotation);
                instantiatedPrefab.transform.localScale = randomScale;

                // Attach a rotating script to the instantiated prefab
                instantiatedPrefab.AddComponent<ContinuousRotation>();
            }
        }
    }

        // Update is called once per frame
        void Update()
        {

        }
    }
