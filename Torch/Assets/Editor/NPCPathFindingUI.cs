using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIPathFinding))]
public class NPCPathFindingUI : Editor
{

    public override void OnInspectorGUI()
    {
        // Implement latter
        DrawDefaultInspector();
    }

    void OnScene(SceneView scene)
    {
        if (Event.current.type != EventType.MouseDown || Event.current.button != 0)
            return;
        // convert GUI coordinates to screen coordinates
        Vector3 screenPosition = Event.current.mousePosition;
        screenPosition.y = Camera.current.pixelHeight - screenPosition.y;
        Ray ray = Camera.current.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        // use a different Physics.Raycast() override if necessary
        if (Physics.Raycast(ray, out hit))
        {
            // do stuff here using hit.point
            Debug.Log(hit.point);
            // tell the event system you consumed the click
            Event.current.Use();
        }
    }
}
