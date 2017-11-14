using UnityEngine;
using UnityEditor;

// The icon has to be stored in Assets/Gizmos

public class AntMovementGizmoDrawer
{
    /*[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmoForMyScript(AntMovement scr, GizmoType gizmoType)
    {
        Vector3 position = scr.transform.position;

        if (Vector3.Distance(position, Camera.current.transform.position) > 10f)
            Gizmos.DrawIcon(position, "arrow.png", true);
    }*/


    [DrawGizmo(GizmoType.Active)]
    static void DrawGizmoForMyScript(AntMovement scr, GizmoType gizmoType)
    {
        Transform transform = scr.gameObject.transform;
        
        //if (Vector3.Distance(position, Camera.current.transform.position) > 10f)
        //Gizmos.DrawIcon(transform.position, "arrow.tiff", true);
        Gizmos.DrawLine(transform.position, transform.position+transform.up);
    }
}
