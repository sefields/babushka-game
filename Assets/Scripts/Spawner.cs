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

	// Use this for initialization
	void Start () {
        myPath = transform.Find("Path").gameObject;
        Spawn();
	}

    void Spawn()
    {
        Instantiate(spawnySparks, transform.position, Quaternion.identity);
        myAgent = Instantiate(dollPrefab, transform.position, Quaternion.identity) as GameObject;
        BabushkaTarget respawn = myAgent.GetComponent<BabushkaTarget>();
        if (respawn != null) // This is the case where the thing that we spawned is a doll.
        {
            respawn.mySpawner = this.gameObject;
        }
        else //  This is the case where the thing that we spawned is a vehicle, etc, and we need to dig a bit to find the doll.
        {
            respawn = myAgent.GetComponentInChildren<BabushkaTarget>();
            respawn.mySpawner = this.gameObject;
        }
        myAgent.GetComponent<SplineController>().SplineRoot = myPath;
        myAgent.GetComponent<SplineController>().Duration = duration;
    }

    public IEnumerator WaitAndRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        Spawn();
    }
}
