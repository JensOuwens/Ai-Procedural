using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void DestroyThisWeapon()
    {
        Destroy(gameObject);
    }
}
