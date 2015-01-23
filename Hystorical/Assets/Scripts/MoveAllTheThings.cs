using UnityEngine;
using System.Collections;

public class MoveAllTheThings : MonoBehaviour {

    public Vector3 Slide;

    void FixedUpdate()
    {
        var rigidBodies = GameObject.FindObjectsOfType<Rigidbody>();

        foreach (var rigidBody in rigidBodies)
            rigidBody.MovePosition(rigidBody.position + Slide * Time.fixedDeltaTime);
    }
}
