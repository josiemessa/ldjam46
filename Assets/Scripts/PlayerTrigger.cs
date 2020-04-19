using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            return;
        }

        if (other.gameObject.CompareTag("objective"))
        {
            PersistentManager.Instance.Win = true;
        }

        PersistentManager.Instance.Running = false;
        Debug.Log("finishing");
    }
}