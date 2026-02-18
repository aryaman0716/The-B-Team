using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string speaker;
    public Sprite portrait;
    [TextArea(3, 10)]
    public string sentence;
}

[System.Serializable]
public class DialogueSet
{
    public DialogueEntry[] entries;
}

[System.Serializable]
public class Dialogue
{
    public DialogueSet[] dialogueSets;
}
