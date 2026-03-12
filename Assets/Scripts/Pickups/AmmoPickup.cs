using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] private int ammoAmount = 100;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount);
    }

}
