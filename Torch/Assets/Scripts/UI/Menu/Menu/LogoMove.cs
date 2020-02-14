using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMove : MonoBehaviour
{

    public float speed;

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;

        Ray cameraRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y + Screen.height, Input.mousePosition.z));
        Plane groundPane = new Plane(Vector3.forward, Vector3.forward);
        float rayLenght;

        if (groundPane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            transform.LookAt(pointToLook * 5, Vector3.up);
        }
    }
}
