using UnityEngine;

public class UIGameCursor : MonoBehaviour
{
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        GameCursor.BlocksChanged += (blocks) =>
        {
            foreach(var block in blocks)
            {
                block.transform.SetParent(this.transform);
            }
        };
    }

    private void Update()
    {
        _rect.anchoredPosition = Input.mousePosition;
    }
}
