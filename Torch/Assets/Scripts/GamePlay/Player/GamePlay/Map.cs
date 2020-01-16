using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject marqueur, map3DCenter, map3DPointerHolder;
    GameObject word;

    public Camera cam;

    public float rotationSpeed;

    public bool is3DMap;

    Vector3 marquerPosition;

    Dictionary<Transform, Transform> waypoints = new Dictionary<Transform, Transform>();

    // Use this for initialization
    void Start () {
        word = GameObject.FindWithTag("WorldPin");
    }
	
	// Update is called once per frame
	void Update () {

        marquerPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetMouseButton(2) && is3DMap)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            GameObject.FindWithTag("Map").transform.Rotate(Vector3.up,-rotX, Space.World);
            GameObject.FindWithTag("Map").transform.Rotate(Vector3.right,rotY, Space.World);

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!is3DMap)
            {
                Transform map = transform.GetChild(0);

                Ray mouse = cam.ScreenPointToRay(Input.mousePosition);

                Vector3 targetPos = new Vector3();
                RaycastHit point;
                GameObject marquerObject;
                if (Physics.Raycast(mouse, out point))
                {
                    targetPos = mouse.GetPoint(point.distance);
                    marquerObject = Instantiate(marqueur, targetPos, transform.rotation);
                    marquerObject.transform.SetParent(map);
                    float posX = marquerObject.transform.localPosition.x + 0.5f;
                    float posY = marquerObject.transform.localPosition.z + 0.5f;
                    SetPointInWord(posX, posY, marquerObject.transform);
                }
            }
            else
            {
                marquerPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                GameObject go = Instantiate(marqueur, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 0.5f), transform.rotation, map3DCenter.transform);
                go.transform.rotation = transform.rotation;
                go.transform.rotation.Set(90,0,0,0);
                go.transform.SetParent(GameObject.FindWithTag("Map").transform);
            }
        }
    }

    void SetPointInWord(float posX, float posY, Transform marquerObject)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.SetParent(word.transform);
        // Convertie les cordonée dans les cordonées du monde
        float locX = word.transform.localPosition.x;
        float locY = word.transform.localPosition.z;
        go.transform.localPosition = new Vector3(((-2 * locX) * posX + locX) + (-locX), word.transform.position.y, ((-2 * locY) * posY + locY) + (-locY));
        go.transform.localScale = new Vector3(-locX * 0.002f, 100, -locY * 0.002f);
        DestroyImmediate(go.GetComponent<BoxCollider>());
        go.GetComponent<MeshRenderer>().material.color = Color.blue;
        go.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        go.GetComponent<MeshRenderer>().receiveShadows = false;
        waypoints.Add(marquerObject, go.transform);
    }

    public void DeleteWayPoint(Transform waypoint)
    {
        Destroy(waypoints[waypoint].gameObject);
        waypoints.Remove(waypoint);
    }
}
