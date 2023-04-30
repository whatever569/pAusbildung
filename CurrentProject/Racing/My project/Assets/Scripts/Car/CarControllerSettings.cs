using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CarControllerSettings
{
    public float Acceleration;
    public float YTurnSpeed;
    public float DefaultDrag;
    public float InAirDrag;
    public float RollSpeed;
    public float GroundCheckDistance;
    public float JumpForce;
    public Transform GroundCheckOrigin;
    public LayerMask GroundLayer;
    public ForceMode RollForceMode;
    public ForceMode JumpForceMode;

    public CarControllerSettings(float acceleration, float yTurnSpeed, float defaultDrag, float inAirDrag, float rollSpeed, float groundCheckDistance, float jumpForce, Transform groundCheckOrigin, LayerMask groundLayer, ForceMode rollForceMode, ForceMode jumpForceMode)
    {
        Acceleration = acceleration;
        YTurnSpeed = yTurnSpeed;
        DefaultDrag = defaultDrag;
        InAirDrag = inAirDrag;
        RollSpeed = rollSpeed;
        GroundCheckDistance = groundCheckDistance;
        JumpForce = jumpForce;
        GroundCheckOrigin = groundCheckOrigin;
        GroundLayer = groundLayer;
        RollForceMode = rollForceMode;
        JumpForceMode = jumpForceMode;
    }
}

