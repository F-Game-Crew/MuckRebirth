using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreePlacement))]
public class TreePlacementEditor : Editor 
{
   public override void OnInspectorGUI () {
    TreePlacement treeGen = (TreePlacement) target;

    if (DrawDefaultInspector()) {
        if (treeGen.autoUpdate) {
            treeGen.Generate();
        }
    } 

    if (GUILayout.Button("Generate")) {
        treeGen.Generate();
    }
   } 
}
