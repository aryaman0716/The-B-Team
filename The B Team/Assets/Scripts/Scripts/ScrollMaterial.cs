using UnityEngine;

public class ScrollMaterial : MonoBehaviour
{
    public MeshRenderer mr;
    private Material material;
    public float xSpd;
    public float ySpd;

    public bool scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(mr == null) { mr = GetComponent<MeshRenderer>(); }    
        if(mr == null) { mr = GetComponentInChildren<MeshRenderer>(); }
        if(mr != null) { material = mr.material; }
    }

    // Update is called once per frame
    void Update()
    {
        if (!scroll) { return; }
        ScrollTexture();
    }

    void ScrollTexture()
    {
        Vector2 offset = material.GetTextureOffset("_BaseMap");
        float xOffset = offset.x;
        float yOffset = offset.y;
        material.SetTextureOffset("_BaseMap", new Vector2(offset.x + xSpd, offset.y + ySpd));
    }
}
