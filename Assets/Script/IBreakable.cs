using UnityEngine;
public interface IBreakable
{
    void BreakObject();
}

public interface IKnockback
{
    void DoKnockback(Vector3 normalVector);
}

public interface IAttachableObject
{
    void AttachObject(Transform parentPoint);
    void ReleaseObject(Transform parentPoint);
}

public interface IConveyBelt
{
    void ApplyConveyorForce(Vector3 conveyForce);
    //void ResetExternalForce();
}

public interface IFanpPlatform
{
    void ApplyFanForce(Vector3 fanForce,float maxFanSpeed);
    //void GravityClamp(float clampForce);
    //void ResetFanForce();
}

public interface IRunnableWall
{
    bool DisableGravity();
    bool EnableGravity();
    
}

public interface IDeath
{
    void DoDeath();
}

public interface IActivation
{
    void DoActivate();
}