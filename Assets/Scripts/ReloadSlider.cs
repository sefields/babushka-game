using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using VRStandardAssets.ShootingGallery;

public class ReloadSlider : MonoBehaviour {

    private float sliderSpeed;
    [SerializeField]
    private float sliderSpeedNormal = .5f;
    [SerializeField]
    private float sliderSpeedFail = .25f;
    [SerializeField]
    private Slider mySlider;
    [SerializeField]
    private VRInput m_VRInput;                                     // Read input from headset
    [SerializeField]
    private ReloadHandle handle;
    [SerializeField]
    private ShootingGalleryGun sgg;
    [SerializeField]
    private GameObject swagZone;

	// Use this for initialization
	void OnEnable()
    {
        m_VRInput.OnDown += HandleDown;
        StartCoroutine(FillReloadSlider());
        RandomizeSwagZone();
    }

    void OnDisable()
    {
        m_VRInput.OnDown -= HandleDown;
        StopCoroutine(FillReloadSlider());
        handle.forceFalse();    //  This is due to a bug where one success would lead to permanent success bc OnTriggerExit never occurs
        mySlider.value = 0;
    }

    private void FadeIn()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            i.CrossFadeAlpha(1, .5f, false);
        }
    }

    private void FadeOut()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            i.CrossFadeAlpha(0, .5f, false);
        }
    }

    private IEnumerator FillReloadSlider()
    {
        sliderSpeed = sliderSpeedNormal;
        float timer = 0f;
        swagZone.SetActive(true);

        while (mySlider.value < 1)
        {
            mySlider.value += Time.deltaTime * sliderSpeed ;
            timer += Time.deltaTime;
            yield return null;
        }

        mySlider.value = 1;
        sgg.Reload();
        gameObject.SetActive(false);
    }

    private void HandleDown()
    {
        if (handle.GetSuccess())
        {
            sgg.Reload();
            gameObject.SetActive(false);
        }
        else {
            sliderSpeed = sliderSpeedFail;
            swagZone.SetActive(false);
            return;
        }
    }

    private void RandomizeSwagZone()
    {
        float xPos = Random.Range(-40f, 40f);
        swagZone.transform.localPosition = new Vector3(xPos, swagZone.transform.localPosition.y, swagZone.transform.localPosition.z);
    }
}
