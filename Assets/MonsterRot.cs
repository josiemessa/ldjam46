using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MonsterRot : MonoBehaviour
{
    Vector3 currentEulerAngles;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("z pressed");
            //modifying the Vector3, based on input multiplied by speed and time
            currentEulerAngles += new Vector3(0, 0, 90);


            //apply the change to the gameObject
            transform.eulerAngles = currentEulerAngles;
        }

    }
}