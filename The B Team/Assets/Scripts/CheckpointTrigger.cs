using UnityEngine;
public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointIndex; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            // saving checkpoint using player prefs to keep it persistent across the game
            PlayerPrefs.SetInt("CheckpointIndex", checkpointIndex);
            PlayerPrefs.Save(); 
            Debug.Log("Checkpoint saved: " + checkpointIndex);
        }
    }
}
