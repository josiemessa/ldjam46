using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (tag == "objective")
            {
                MoveToObjective m = other.gameObject.GetComponent<MoveToObjective>();
                m.arrived = true;
                PersistentManager.Instance.Win = true;
            }
            PersistentManager.Instance.Running = false;
            Debug.Log("finishing");
        }
    }
}
