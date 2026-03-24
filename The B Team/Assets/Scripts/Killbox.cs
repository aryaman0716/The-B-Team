using UnityEngine;
public class Killbox : MonoBehaviour
{
    public Transform checkpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                // we set the checkpoint if it's assigned, otherwise we just respawn at the default spawn point
                if (checkpoint != null)
                {
                    respawn.SetCheckpoint(checkpoint);
                }

                respawn.Respawn();
            }
        }
    }
}
