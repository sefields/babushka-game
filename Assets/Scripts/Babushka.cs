using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using UnityEngine.UI;

// by Sam Fields

public class Babushka : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private SelectionRadialBabushka m_SelectionRadialBabushka;

    [SerializeField]
    private Material m_NormalMaterial;
    [SerializeField]
    private Material m_OverMaterial;
    [SerializeField]
    private Material m_ClickedMaterial;
    [SerializeField]
    private Material m_DoubleClickedMaterial;
    [SerializeField]
    private Renderer[] m_Renderers;

    public GameObject happySparks;

    [SerializeField] private bool m_GazeOver;

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        m_SelectionRadialBabushka.OnSelectionComplete += HandleSelectionComplete;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        m_SelectionRadialBabushka.OnSelectionComplete -= HandleSelectionComplete;
    }

    //  This gets called every frame that you hover over the object
    private void HandleOver()
    {
        m_SelectionRadialBabushka.Show(); 
        foreach (Renderer m_Renderer in m_Renderers)
            m_Renderer.material = m_OverMaterial;
        m_GazeOver = true;
    }

    private void HandleOut()
    {
        m_SelectionRadialBabushka.Hide();
        foreach (Renderer m_Renderer in m_Renderers)
            m_Renderer.material = m_NormalMaterial;
        m_GazeOver = false;
    }

    private void HandleClick()
    {
        foreach (Renderer m_Renderer in m_Renderers)
            m_Renderer.material = m_ClickedMaterial;
    }

    private void HandleDoubleClick()
    {
        foreach (Renderer m_Renderer in m_Renderers)
            m_Renderer.material = m_DoubleClickedMaterial;
    }

    private void HandleSelectionComplete()
    {
        if (m_GazeOver) { 
            CollectMe();
            Debug.Log("Gaze is on me!");
        }
    }

    private void CollectMe()
    {
        m_SelectionRadialBabushka.Hide();
        Instantiate(happySparks, transform.position, Quaternion.identity);
        int currScore = int.Parse(GameObject.Find("DebugT").GetComponent<Text>().text);
        currScore += 1;
        GameObject.Find("DebugT").GetComponent<Text>().text = currScore.ToString();
        Destroy(gameObject);
    }
}
