using UnityEngine;
using System.Collections;

// by Sam Fields

public class BulletUpdate : MonoBehaviour {

    [SerializeField]
    private Bullet[] bullets;   //  Assign in Inspector

	public void UpdateDisplay(int ammo)
    {
        bullets[ammo - 1].Fade();
    }

    public void ResetDisplay()
    {
        foreach (Bullet b in bullets)
            b.Fade();
    }
}
