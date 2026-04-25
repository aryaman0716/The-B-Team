using UnityEngine;
using System.Collections;

public class ObjUIController : MonoBehaviour
{
    private Animator anim;
    public DialogueManager dm;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("dialoguePlaying", DialogueManager.dialoguePlaying);
        if (!DialogueManager.dialoguePlaying && Input.GetKeyDown(KeyCode.R))
        {
            if(dm != null)
            {
                dm.ReplayDialogue();
            }
            
        }
    }
}
