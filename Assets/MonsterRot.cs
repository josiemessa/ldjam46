using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MonsterRot : MonoBehaviour
{
    public int tick;
    public int speed;
    // public Vector3 target = new Vector3(5.0f, 0.0f, 0.0f);
    public int idx = 0;
    public float elapsedTime;

    private Vector3[] directions = new[]
        {Vector3.negativeInfinity};

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= tick)
        {
            // transform.rotation.SetLookRotation(directions[idx]);
            transform.Rotate(directions[idx], Space.World);
            Debug.Log("Rotation set to: " + directions[idx]);
            elapsedTime = 0;
            if (idx >= directions.Length)
            {
                idx = 0;
            }
            else
            {
                idx++;
            }
        }

        transform.Translate(Vector2.up * (speed * Time.deltaTime));
        elapsedTime += Time.deltaTime;

        // Spin the object around the world origin at 20 degrees/second.
        // transform.RotateAround(target, Vector3.up, 30 * Time.deltaTime);
    }
}