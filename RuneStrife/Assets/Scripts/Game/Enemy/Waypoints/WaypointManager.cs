using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {
    //singleton instance to manage waypoints
    public static WaypointManager Instance;
    //the possible enemy paths
    public List<Path> Paths = new List<Path>();

	// Use this for initialization
	void Awake () {
		if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
	}
	
    //Find the enemy spawn point
    public Vector3 FindSpawnPoint(int pathIndex)
    {
        return Paths[pathIndex].WayPoints[0].position;
    }
}

//Need a class for the Paths themselves
[System.Serializable]
public class Path
{
    public List<Transform> WayPoints = new List<Transform>();
}