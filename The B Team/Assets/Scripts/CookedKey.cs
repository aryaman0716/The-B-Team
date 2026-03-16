using UnityEngine;

public class CookedKey : MonoBehaviour
{
    private PlacementEmitter emitter;
    private PadlockTransform padlock;

    private bool unlocking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emitter = GetComponent<PlacementEmitter>();
        padlock = GameObject.Find("ShutterPadlock").GetComponent<PadlockTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (emitter.IsPlaced && !unlocking)
        {
            unlocking = true;
            Debug.Log("Unlock padlock");
            padlock.UnlockPadlock(gameObject);
        }
    }
}
