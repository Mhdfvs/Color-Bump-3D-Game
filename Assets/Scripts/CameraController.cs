using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ballPos;
    float offsetZ;
    // Start is called before the first frame update
    void Start()
    {
        offsetZ = this.transform.position.z - ballPos.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position =new Vector3(this.transform.position.x,this.transform.position.y,ballPos.position.z + offsetZ);
    }
}
