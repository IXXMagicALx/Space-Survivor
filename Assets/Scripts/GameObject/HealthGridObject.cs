using UnityEngine;

public class HealthGridObject : MonoBehaviour
{
    public GameObject Active;

    public void IsFilled(bool isActive)
    {
        Active.SetActive(isActive);
    }
}
