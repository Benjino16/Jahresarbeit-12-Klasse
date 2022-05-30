using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] int activateObjects = 3;


    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObjects.Add(transform.GetChild(i).gameObject);
        }
    }
    public void Generate()
    {
        foreach (GameObject obstecle in gameObjects)
        {
            obstecle.SetActive(false);
        }

        for (int i = 0; i < activateObjects; i++)
        {
            gameObjects[Random.Range(0, gameObjects.Count)].SetActive(true);
        }
    }
}
