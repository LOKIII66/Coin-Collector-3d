using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched by: " + other.name);

        if (other.CompareTag("Player"))
        {

            GameManager.Instance.AddScore();

            Destroy(gameObject);
        }
    }
}