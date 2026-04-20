using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SnapTilesToGrid
{
    [MenuItem("MacroBunny/Show/PhysTile Alignment Tool")]
    public static void ShowPhysTileAlignmentTool()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(TileAlignment));
    }
}

public class TileAlignment : EditorWindow
{
    public static PhysTile selectedTile;
    private void OnGUI()
    {
        GUILayout.Label("PhysTile Alignment Tool", EditorStyles.boldLabel);
        GUILayout.Label("Controls last selected PhysTile.");

        GUILayout.BeginHorizontal(name = "DirectionalButtons");
        if (GUILayout.Button("Left"))
        {

        }
        GUILayout.BeginVertical(name = "UpDownButtons");
        if (GUILayout.Button("Up"))
        {

        }
        if (GUILayout.Button("Down"))
        {

        }
        GUILayout.EndVertical();
        if (GUILayout.Button("Right"))
        {

        }
        GUILayout.EndHorizontal();
    }
}
