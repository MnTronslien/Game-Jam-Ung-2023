using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorito : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab;

    public GameObject prefabToSpawn;  // Reference to the prefab you want to spawn
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
                Vector3 spawnPosition = startPosition + new Vector3(col * spacing, row * spacing, 0);

                // Instantiate the prefab at the calculated position
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
