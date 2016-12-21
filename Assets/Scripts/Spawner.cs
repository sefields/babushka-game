using UnityEngine;
using System.Collections;
using VRStandardAssets.ShootingGallery;

// by Sam Fields

public class Spawner : MonoBehaviour {

    //  This is the spawner. It may spawn either a Doll or an object on which Doll(s) ride. 

    [SerializeField]
    GameObject dollPrefab;

    [SerializeField]
    GameObject spawnySparks;

    [SerializeField]
    float duration;

    GameObject myAgent;

    private GameObject myPath;

    int spawnPointCount;

    bool[] activeSpawnPoints;

	// Use this for initialization
	void Start () {

        activeSpawnPoints = new bool[transform.childCount];
        for (int i = 0; i < activeSpawnPoints.Length; i++ )
        {
            activeSpawnPoints[i] = false;
        }
        //Spawn();
	}

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Spawn();
        }
    }
    
    // Precondition: Currently, this function is only called at the beginning
    //      of the game or when myAgent has been destroyed.
    // Postcondition: A spawn point is randomly selected from my children. Then, 
    //      myAgent is created from dollPrefab, at that spawn point.
    public GameObject Spawn()
    {
        // Pick a random spawn point from my children
        spawnPointCount = transform.childCount;
        int randomSpawnPointIndex = Random.Range(0, spawnPointCount);
        if (activeSpawnPoints[randomSpawnPointIndex])
        {
            Debug.Log("That spawn point is already active.");
            return null;
        }
        activeSpawnPoints[randomSpawnPointIndex] = true;
        Transform spawnPoint = transform.GetChild(randomSpawnPointIndex);
        // Debug.Log("Spawner count is" + spawnPointCount + ". I have selected spawner #" + randomSpawnPointIndex);

        // Do the actual instantiations
        Instantiate(spawnySparks, spawnPoint.position, Quaternion.identity);
        myAgent = Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity) as GameObject;

        // Reach into the doll I just spawned and hand it a reference to me
        BabushkaTarget respawn = myAgent.GetComponent<BabushkaTarget>();
        if (respawn != null) // Case 1: myAgent is a doll.
        {
            respawn.SetMySpawner(this.gameObject, randomSpawnPointIndex);
        }
        else //  Case 2: myAgent is a vehicle or something, so we dig and find the doll within.
        {
            respawn = myAgent.GetComponentInChildren<BabushkaTarget>();
            respawn.SetMySpawner(this.gameObject, randomSpawnPointIndex);
        }

        // Set the agent on its path
        myAgent.GetComponent<SplineController>().SplineRoot = spawnPoint.Find("Path").gameObject;
        myAgent.GetComponent<SplineController>().Duration = duration;
        return myAgent;
    }

    public IEnumerator WaitAndRespawn(float time, int index)
    {
        activeSpawnPoints[index] = false;
        yield return new WaitForSeconds(time);
        Spawn();
    }

    void PrintMyActives()
    {
        Debug.Log("My actives:");
        for (int i = 0; i < activeSpawnPoints.Length; i++)
        {
            Debug.Log(activeSpawnPoints[i]);
        }
    }
}
