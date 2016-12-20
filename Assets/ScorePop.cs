using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScorePop : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1f);
        speed = 5f * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
	}

    public void UpdateScoreDisplay(int score)
    {
        GetComponent<TextMesh>().text = "+" + score.ToString();
    }
}
