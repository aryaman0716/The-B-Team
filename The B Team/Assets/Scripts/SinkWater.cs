using UnityEngine;

public class SinkWater : MonoBehaviour
{
    private Animator a;
    public bool sinkFilled;
    
    void Start()
    {
        a = GetComponent<Animator>();     
    }

    // Update is called once per frame
    public void SetSinkFilled()
    {
        sinkFilled = true;
    }
}
