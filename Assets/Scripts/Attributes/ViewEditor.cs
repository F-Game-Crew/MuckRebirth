using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Viewable))]
public class ViewEditor : Editor 
{
    private void OnSceneGUI () {
        Viewable view = (Viewable)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(view.transform.position, Vector3.up, Vector3.forward, 360, view.radius);

        Vector3 viewAngle01 = DirectionFromAngle(view.transform.eulerAngles.y, -view.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(view.transform.eulerAngles.y, view.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(view.transform.position, view.transform.position + viewAngle01 * view.radius);
        Handles.DrawLine(view.transform.position, view.transform.position + viewAngle02 * view.radius);

        if (view.canSeeTarget) {
            Handles.color = Color.green;
            Handles.DrawLine(view.transform.position, view.targetRef.transform.position);
        }
    }
    
    private Vector3 DirectionFromAngle (float eulerY, float angleInDegrees) {
       angleInDegrees += eulerY;
       return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
