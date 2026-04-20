using UnityEngine;

public class CookedKey : MonoBehaviour
{
    private PlacementEmitter emitter;
    private PadlockTransform padlock;

    private bool unlocking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emitter = GetComponentInChildren<PlacementEmitter>();
        padlock = GameObject.Find("ShutterPadlock").GetComponent<PadlockTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!unlocking)
        {
            if (!emitter.IsPlaced) { return; }
            unlocking = true;
            Debug.Log("Unlock padlock");
            padlock.UnlockPadlock();
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
