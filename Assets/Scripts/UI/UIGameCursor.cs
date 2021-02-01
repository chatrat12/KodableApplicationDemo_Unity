using UnityEngine;

public class UIGameCursor : MonoBehaviour
{
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        GameCursor.BlocksChanged += (blocks) =>
        {
            if (blocks == null) return;
            foreach (var block in blocks)
            {
                block.transform.SetParent(this.transform);
            }
        };
    }

    private void Update()
    {
        _rect.anchoredPosition = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            if (GameCursor.DropArea != null && GameCursor.CodeBlocks != null && GameCursor.CodeBlocks.Length > 0)
            {
                GameCursor.DropArea.OnBlockDropped();
                GameCursor.CodeBlocks = null;
            }
            else
                GameCursor.ClearBlocks();
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label($"Drop Area: {GameCursor.DropArea}");
        GUILayout.EndVertical();
    }
}
