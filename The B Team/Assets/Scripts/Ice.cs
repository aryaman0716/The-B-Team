using UnityEngine;

public class Ice : MonoBehaviour
{
    public GameObject keyCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Melt()
    {
        keyCard.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
