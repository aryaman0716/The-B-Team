using UnityEngine;
using System.Collections.Generic;

public class GeneralDoor : MonoBehaviour
{
    public bool locked;
    public bool opened;
    public bool mouseOver;

    public float interactRange;

    private Animator a;
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(a == null) { Debug.LogWarning("Animator not initialised"); }
        if(playerTransform == null) { Debug.LogWarning("Player transform not found"); }
        SetDoorLocked(locked);

    }

    // Update is called once per frame
    void Update()
    {
        if (locked) { return; }

        if (mouseOver && Input.GetMouseButtonDown(0))
        {
            if (a.GetBool("Opened") == true) { a.SetBool("Opened", false); return; }
            if (a.GetBool("Opened") == false) { a.SetBool("Opened", true); return; }
        }

        
    }

    void OnMouseOver()
    {
        var dist = Vector3.Distance(playerTransform.position, transform.position);
        if (dist > interactRange) { mouseOver = false; return;}
        mouseOver = true;
        if (locked) { Debug.Log("Door locked."); return;  }
        
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }

    public void SetDoorLocked(bool val)
    {
        locked = val;
        Debug.Log("Door should be " + val);
        Debug.Log("Door is now " + locked);

        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            if(val == true) { r.material.SetColor("_BaseColor", Color.red); }
            else { r.material.SetColor("_BaseColor", Color.green); }
        }
    }
}
