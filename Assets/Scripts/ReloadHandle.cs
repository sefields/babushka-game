using UnityEngine;
using System.Collections;

// by Sam Fields

public class ReloadHandle : MonoBehaviour {

    private bool success;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("SwagZone"))
        {
            success = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("SwagZone"))
        {
            success = false;
        }
    }

    public bool GetSuccess()
    {
        return success;
    }

    public void forceFalse()
    {
        success = false;
    }
}
