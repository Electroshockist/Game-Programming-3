using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform Camera;
    public Transform Player;

    public bool hasBounds;
    public Vector3 minCamBounds;
    public Vector3 maxCamBounds;

    // Update is called once per frame
    void Update(){
        Camera.position = new Vector3(Player.position.x, Player.position.y, -10);

        if (hasBounds)
        {
            Camera.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCamBounds.x, maxCamBounds.x),
                Mathf.Clamp(transform.position.y, minCamBounds.y, maxCamBounds.x),
                -10
            );
        }
    }
}
