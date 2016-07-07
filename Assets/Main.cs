using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

    #region Public variables
    private GameObject[] prefabs;
    public GameObject camera;
    public GameObject light;
    #endregion

    #region values
    private int maxObjects = 100;
    private int minObjectsToPlaceInScene = 10;

    private float cameraMinRange = 10;
    private float cameraMaxRange = 50;
    private float cameraMinFOV = 0;
    private float cameraMaxFOV = 0;

    private float maxRadiusToPlaceObjectsIn = 100;

    private float minScale = 0.5f;
    private float maxScale = 5f;
    #endregion

    // Use this for initialization
    void Start ()
    {
	    
	}

    /// <summary>
    /// Picks the number of objects to place in a scene.
    /// Scales and positions those objects randomly.
    /// Hides the rest.
    /// </summary>
    /// <param name="objectsInScene"></param>
    void GenerateNewScene(List<GameObject> objectsInScene)
    {
        //How many objects are we placing in this scene?
        int objectsToPlaceInScene = Random.Range(minObjectsToPlaceInScene, maxObjects);

        //track which object we're placing
        int i = 0;
        foreach (GameObject obj in objectsInScene)
        {
            if (i <= objectsToPlaceInScene)
            {
                //Make sure object is visible
                obj.GetComponent<Renderer>().enabled = true;
                //Scale object randomly
                obj.transform.localScale = GetRandomScale();
                //Place object at random position and offset y to compensate for scale
                obj.transform.position = GetRandomPosition() + new Vector3(0, obj.transform.localScale.y / 2, 0);
            }
            else
            {
                //Hide object
                obj.GetComponent<Renderer>().enabled = false;
            }

        }
    }

    void GenerateNewCamera()
    {

    }

    /// <summary>
    /// Instantiates `maxObjects` no. of objects. and adds them to the specified list.
    /// </summary>
    /// <param name="gameObjects"></param>
    void InstantiateAllObjects(List<GameObject> gameObjects)
    {
        for (int i = 0; i <= maxObjects; i++)
        {
            //Instantiate Random Prefab
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length - 1]);
            //Add to list
            gameObjects.Add(obj);
        }
    }

    /// <summary>
    /// Gets a random Vector 3 in a `maxRadiusToPlaceObjectsIn` from the origin.
    /// (Y is always 0)
    /// </summary>
    /// <param name="radius"></param>
    Vector3 GetRandomPosition()
    {
        return Random.insideUnitCircle * maxRadiusToPlaceObjectsIn;
    }

    /// <summary>
    /// Generates a random Vector3 between minScale and maxScale
    /// </summary>
    /// <returns></returns>
    Vector3 GetRandomScale()
    {
        return new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
    }

}
