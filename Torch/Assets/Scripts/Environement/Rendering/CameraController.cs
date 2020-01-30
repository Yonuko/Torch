using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Transform target; 							// Target to follow
    GameObject Player;
    float targetHeight = 1.8f;                         // Vertical offset adjustment
    float distance = 12.0f;                            // Default Distance
    float offsetFromWall = 1.2f;                       // Bring camera away from any colliding objects
    int maxDistance = 20;                       // Maximum zoom Distance
    float minDistance = 0.3f;                      // Minimum zoom Distance
    public static float xSpeed = 200.0f;                           // Orbit speed (Left/Right)
    public static float ySpeed = 200.0f;                           // Orbit speed (Up/Down)
    int yMinLimit = -80;                            // Looking up limit
    int yMaxLimit = 80;                             // Looking down limit
    int zoomRate = 40;                          // Zoom Speed
    float rotationDampening = 3.0f;                // Auto Rotation speed (higher = faster)
    float zoomDampening = 5.0f;                    // Auto Zoom speed (Higher = faster)
    LayerMask collisionLayers = -1;		// What the camera will collide with
    bool lockToRearOfTarget = false;             // Lock camera to rear of target
    bool allowMouseInputX = true;                // Allow player to control camera angle on the X axis (Left/Right)
    bool allowMouseInputY = true;                // Allow player to control camera angle on the Y axis (Up/Down)

    private double xDeg = 0.0f;
    private double yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind = false;

    private bool StopMoving = false, logEnabled = false;

    private GameObject[] playerAffichage = { null, null, null };


    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        // Make the rigid body not change rotation 
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        if (lockToRearOfTarget)
            rotateBehind = true;
    }

    void Update()
    {

        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        if (!Player)
        {
            Player = GameObject.FindWithTag("Player");
        }

        if (playerAffichage[0] == null)
        {
            if (target)
            {
                for (int i = 0; i < 3; i++)
                {
                    playerAffichage[i] = target.GetChild(i).gameObject;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            logEnabled = !logEnabled;
        }

    }

    //Only Move camera after everything else has been updated
    void LateUpdate()
    {
        // Don't do anything if target is not defined
        if (!target)
            return;

        Vector3 vTargetOffset;


        // If either mouse buttons are down, let the mouse govern camera position 
        if (GUIUtility.hotControl == 0)
        { 
            if (Input.GetMouseButton(1))
            {
                //Check to see if mouse input is allowed on the axis
                if (allowMouseInputX)
                    xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02;
                else
                    RotateBehindTarget();
                if (allowMouseInputY)
                    yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;

                //Interrupt rotating behind if mouse wants to control rotation
                if (!lockToRearOfTarget)
                    rotateBehind = false;
            }
            if (!Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
            }
        }
        yDeg = ClampAngle((float)yDeg, yMinLimit, yMaxLimit);

        // Set camera rotation 
        Quaternion rotation = Quaternion.Euler((float)yDeg, (float)xDeg, 0);

        if (!Input.GetMouseButton(1) && !Input.GetMouseButton(0) && Input.GetAxis("Vertical") != 0)
        {
            RotateBehindTarget();
        }

        if (!StopMoving)
        {
            // Calculate the desired distance 
            desiredDistance -= Input.GetAxis("ScrollValue") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            correctedDistance = desiredDistance;
        }

        // Calculate desired camera position
        vTargetOffset = new Vector3(0, -targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

        // Check for collision using the true target's desired registration point as set by user using height 
        RaycastHit collisionHit;

        // Dans ce cas là, on ajoute targetHeight à la hauteur pour pouvoir s'alligner avec le pivot de l'objet
        //Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);
        // On n'ajoute pas le targetHeight car le pivot de model Mixamo est à ces pied
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y, target.position.z);

        // If there was a collision, correct the camera position and calculate the corrected distance 
        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
        {
            // Calculate the distance from the original estimated position to the collision location,
            // subtracting out a safety "offset" distance from the object we hit.  The offset will help
            // keep the camera from being right on top of the surface we hit, which usually shows up as
            // the surface geometry getting partially clipped by the camera's front clipping plane.
            if (collisionHit.transform.gameObject != Player && !collisionHit.collider.isTrigger)
            {
                // If log are enable show the log of the objet that the camera hit
                if (logEnabled)
                {
                    Debug.Log("La caméra à toucher l'objet : " + collisionHit.transform.gameObject);
                }
                correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
                isCorrected = true;
            }
        }

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance 
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;

        // Keep within limits
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Recalculate position based on the new currentDistance 
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

        // Désactive l'affichage du joueur si la caméra est trop proche
        if (currentDistance < 0.5f)
        {
            for (int i = 0; i < 3; i++)
            {
                playerAffichage[i].SetActive(false);
            }
        }
        else if (!playerAffichage[0].activeInHierarchy)
        {
            for (int i = 0; i < 3; i++)
            {
                playerAffichage[i].SetActive(true);
            }
        }

        //Finally Set rotation and position of camera
        transform.rotation = rotation;
        transform.position = position;

    }

    void RotateBehindTarget()
    {
        float targetRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;
        xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);

        // Stop rotating behind if not completed
        if (targetRotationAngle == currentRotationAngle)
        {
            if (!lockToRearOfTarget)
                rotateBehind = false;
        }
        else
            rotateBehind = true;

    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
