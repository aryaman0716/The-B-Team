using UnityEngine;

public class Dough : MonoBehaviour
{
    private PlacementEmitter emitter;
    private PadlockTransform padlock;

    private bool transforming;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emitter = GetComponent<PlacementEmitter>();
        padlock = GameObject.Find("ShutterPadlock").GetComponent<PadlockTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (emitter.IsPlaced && !transforming)
        {
            transforming = true;
            padlock.TransformDoughToKey(gameObject);
        }   
    }
}
