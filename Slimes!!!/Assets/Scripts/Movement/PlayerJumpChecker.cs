using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpChecker
{
    public bool CanJump(Collider2D collider)
    {
        return collider.IsTouchingLayers(LayerMask.GetMask("Default", "Floor"));
    }
}
