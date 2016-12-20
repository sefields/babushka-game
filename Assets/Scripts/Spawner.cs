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

	// Use this for initialization
	void Start () {
        Spawn();
	}

    void Spawn()
    {
        // Pick a random spawn point from my children
        spawnPointCount = transform.childCount;
        int randomSpawnPointIndex = Random.Range(0, spawnPointCount);
        Transform spawnPoint = transform.GetChild(randomSpawnPointIndex);
        // Debug.Log("Spawner count is" + spawnPointCount + ". I have selected spawner #" + randomSpawnPointIndex);

        // Do the actual instantiations
        Instantiate(spawnySparks, spawnPoint.position, Quaternion.identity);
        myAgent = Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity) as GameObject;

        // Reach into the doll I just spawned and hand it a reference to me
        BabushkaTarget respawn = myAgent.GetComponent<BabushkaTarget>();
        if (respawn != null) // Case 1: myAgent is a doll.
        {
            respawn.mySpawner = this.gameObject;
        }
        else //  Case 2: myAgent is a vehicle or something, so we dig and find the doll within.
        {
            respawn = myAgent.GetComponentInChildren<BabushkaTarget>();
            respawn.mySpawner = this.gameObject;
        }

        // Set the agent on its path
        myAgent.GetComponent<SplineController>().SplineRoot = spawnPoint.Find("Path").gameObject;
        myAgent.GetComponent<SplineController>().Duration = duration;
    }

    public IEnumerator WaitAndRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        Spawn();
    }
}
