using UnityEngine;

public class Destructible : MonoBehaviour
{
    public void DestroyBox()
    {
        Debug.Log("[Destructible] " + gameObject.name + " bliver ødelagt!");
        Destroy(gameObject);
    }
}
