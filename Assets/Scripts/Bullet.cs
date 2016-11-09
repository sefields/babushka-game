using UnityEngine;
using System.Collections;

// by Sam Fields

public class Bullet : MonoBehaviour {

    [SerializeField]
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	public void Fade()
    {
        anim.SetTrigger("fade");
    }

}
