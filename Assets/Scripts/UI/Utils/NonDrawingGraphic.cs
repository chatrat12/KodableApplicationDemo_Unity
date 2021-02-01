#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
#endif
using UnityEngine.UI;

public class NonDrawingGraphic : Graphic
{
    public override void SetMaterialDirty() { return; }
    public override void SetVerticesDirty() { return; }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        return;
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
public class NonDrawingGraphicEditor : GraphicEditor
{
    public override void OnInspectorGUI()
    {
        base.serializedObject.Update();
        EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
        // skipping AppearanceControlsGUI
        base.RaycastControlsGUI();
        base.serializedObject.ApplyModifiedProperties();
    }
}
#endif