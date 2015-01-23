using UnityEngine;
using System.Collections;

public class MoveAllTheThings : MonoBehaviour {

    public Vector3 Slide;

    void FixedUpdate()
    {
        transform.position -= Slide * Time.fixedDeltaTime;

        //var objects = GameObject.FindGameObjectsWithTag("sliding");

        //foreach (var @object in objects)
        //{
        //    @object.transform.position = (@object.transform.position + Slide * Time.fixedDeltaTime);
        //}
    }
}
