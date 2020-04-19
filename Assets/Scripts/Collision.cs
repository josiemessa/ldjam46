using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            // Player achieving objective or hitting a monster
            case "Player":
                if (tag == "objective")
                {
                    PersistentManager.Instance.Win = true;
                }

                PersistentManager.Instance.Running = false;
                Debug.Log("finishing");
                break;
            case "wall":
                if (tag == "monster")
                {
                    // affect how the monster walks.
                }

                break;
        }
    }
}
