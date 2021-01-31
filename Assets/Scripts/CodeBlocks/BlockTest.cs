using UnityEngine;

public class BlockTest : MonoBehaviour
{
    private async void Awake()
    {
        var main = new MainBlock();
        await main.Execute();
    }
}
