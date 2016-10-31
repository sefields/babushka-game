using UnityEngine;
using System.Collections;

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
