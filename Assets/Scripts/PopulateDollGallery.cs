using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PopulateDollGallery : MonoBehaviour {

    [SerializeField] private GameObject buttonPrefab;
    public GameControl gc;


	// Use this for initialization
	void Start () {
        gc = GameObject.Find("GameControl").GetComponent<GameControl>();
        Dictionary<string, bool> dollsCollected = new Dictionary<string, bool>();
        dollsCollected = GameControl.control.GetDollsCollected();

        RectTransform p = transform.Find("Panel").GetComponent<RectTransform>();
        Vector2 canvasScale = new Vector2(GetComponent<Canvas>().transform.localScale.x, GetComponent<Canvas>().transform.localScale.y);
        Rect pWorld = GetWorldRect(p, canvasScale);
        int count = dollsCollected.Count;
        float spacing = pWorld.height / count;
        float counter = pWorld.height/2;

        foreach (KeyValuePair<string, bool> d in dollsCollected)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab, new Vector3(transform.position.x, counter, transform.position.z), Quaternion.identity, transform);
            counter += spacing;
            Text[] texts = button.GetComponentsInChildren<Text>();
            texts[0].text = d.Key;
            texts[1].text = d.Value.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //  Credit: http://answers.unity3d.com/questions/1100493/convert-recttransformrect-to-rect-world.html
    static public Rect GetWorldRect(RectTransform rt, Vector2 scale)
    {
        // Convert the rectangle to world corners and grab the top left
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 topLeft = corners[0];

        // Rescale the size appropriately based on the current Canvas scale
        Vector2 scaledSize = new Vector2(scale.x * rt.rect.size.x, scale.y * rt.rect.size.y);

        return new Rect(topLeft, scaledSize);
    }
}
