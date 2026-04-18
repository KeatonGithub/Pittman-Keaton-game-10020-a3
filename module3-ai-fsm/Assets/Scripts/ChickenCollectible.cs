using UnityEngine;

public class ChickenCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing that touched the Chicken is the Player
        if (other.CompareTag("Player"))
        {
            // Find the StateMachine in the scene to report the collection
            StateMachine sm = FindObjectOfType<StateMachine>();

            if (sm != null)
            {
                sm.AddChicken();

                Destroy(gameObject); // Remove the chicken from the scene
            }
        }
    }
}
