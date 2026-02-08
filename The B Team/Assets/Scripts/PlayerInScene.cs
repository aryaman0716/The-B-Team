// We ensure that the player persists across all the scenes in the game. 

using UnityEngine;
public class PlayerInScene : MonoBehaviour
{
    private static PlayerInScene instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
