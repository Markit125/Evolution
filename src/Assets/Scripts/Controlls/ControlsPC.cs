using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsPC : MonoBehaviour
{
    public Camera cam;
    public float cspeed = 20f, boost;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene("Settings");
        if (Input.GetKey(KeyCode.LeftShift)) boost = 5f;
        else if (Input.GetKey(KeyCode.LeftControl)) boost = 0.25f;
        else boost = 1f;
        if (Input.GetKey(KeyCode.A)) cam.transform.position += new Vector3(-cspeed * boost * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.D)) cam.transform.position += new Vector3(cspeed * boost * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.W)) cam.transform.position += new Vector3(0, cspeed * boost * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.S)) cam.transform.position += new Vector3(0, -cspeed * boost * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.Q)) cam.GetComponent<Camera>().orthographicSize += cspeed * boost * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) cam.GetComponent<Camera>().orthographicSize -= cspeed * boost * Time.deltaTime;
    }
}
