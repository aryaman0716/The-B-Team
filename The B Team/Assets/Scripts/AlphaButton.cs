using UnityEngine;
using UnityEngine.UI;

public class AlphaButton : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float threshold = 0.5f;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = threshold;
    }
}
