using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace UdonSharp.Examples.Utilities
{
    /// <summary>
    /// Destroys the object when the user presses the Use button while holding it.
    /// Requires a VRC_Pickup component on the same GameObject.
    /// </summary>
    [AddComponentMenu("Udon Sharp/Utilities/Destroy On Pickup Use")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class DestroyOnPickupUse : UdonSharpBehaviour
    {
        public override void OnPickupUseDown()
        {
            Networking.Destroy(gameObject);
        }
    }
}
