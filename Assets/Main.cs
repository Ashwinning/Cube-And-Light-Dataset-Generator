using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    #region Inspector variables
    [SerializeField]
    private GameObject[] prefabs;
    public GameObject camera;
    public GameObject light;
    #endregion

    #region values
    private int maxObjects = 100;
    private int minObjectsToPlaceInScene = 10;

    private float cameraMinRange = 10;
    private float cameraMaxRange = 50;
    private float cameraMinHeight = 1;
    private float cameraMaxHeight = 10;
    private float cameraMinFOV = 45;
    private float cameraMaxFOV = 75;

    private float maxRadiusToPlaceObjectsIn = 100;

    private float minScale = 0.5f;
    private float maxScale = 5f;

    private float minLightIntensity = 0.4f;
    private float maxLightIntensity = 1.0f;


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

    void GenerateNewLightParameters()
    {
        //
    }

    /// <summary>
    /// Sets a new camera transform for the scene
    /// </summary>
    void GenerateNewCameraParameters()
    {
        //Set position
        camera.transform.position = GetRandomPointBetweenTwoCircles(cameraMinRange, cameraMaxRange) + Vector3.up * Random.Range(cameraMinHeight, cameraMaxHeight);
        //Camera always looks at origin
        camera.transform.LookAt(Vector3.zero);
        //Set FOV
        camera.GetComponent<Camera>().fieldOfView = Random.Range(cameraMinFOV, cameraMaxFOV);
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
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)]);
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

    /// <summary>
    /// Returns a random point in the space between two concentric circles.
    /// </summary>
    /// <param name="minRadius"></param>
    /// <param name="maxRadius"></param>
    /// <returns></returns>
    Vector3 GetRandomPointBetweenTwoCircles(float minRadius, float maxRadius)
    {
        //Get a point on a unit circle (radius = 1) by normalizing a random point inside unit circle.
        Vector3 randomUnitPoint = Random.insideUnitCircle.normalized;
        //Now get a random point between the corresponding points on both the circles
        return GetRandomVector3Between(randomUnitPoint * minRadius, randomUnitPoint * maxRadius);
    }

    /// <summary>
    /// Returns a random vector3 between min and max. (Inclusive)
    /// </summary>
    /// <returns>The <see cref="UnityEngine.Vector3"/>.</returns>
    /// <param name="min">Minimum.</param>
    /// <param name="max">Max.</param>
    Vector3 GetRandomVector3Between(Vector3 min, Vector3 max)
    {
        return min + Random.Range(0, 1) * (max - min);
    }


}
