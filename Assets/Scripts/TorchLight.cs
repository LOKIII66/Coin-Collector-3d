using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public Light torchLight;

    void Start()
    {
        if (torchLight == null)
            torchLight = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            torchLight.enabled = !torchLight.enabled;
        }
    }
}