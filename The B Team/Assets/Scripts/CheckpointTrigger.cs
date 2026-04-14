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
            // only call the objective completion if this is the first checkpoint
            if (checkpointIndex == 0)
            {
                ObjectiveManager.Instance.SetObjective("Find a way to power on the ventilation system.");
            }
            if (checkpointIndex == 1)
            {
                ObjectiveManager.Instance.SetObjective("Find a way to unlock the shutter.");
            }   
        }
    }
}
