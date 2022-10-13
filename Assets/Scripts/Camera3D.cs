using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera3D : MonoBehaviour
{
    [Header( "Character Follow" ), SerializeField]
    Transform characterTarget;
    [SerializeField]
    Vector3 positionOffset, lookTargetOffset;

    [Header( "Secondary Follow" ), SerializeField]
    Transform secondTarget;
    [SerializeField]
    Vector3 lookSecondOffset;
    [SerializeField]
    float lookSecondOffsetDist = 5.0f;
    bool isSecondTargetMode = true;

    [Header( "Smooth" ), SerializeField]
    bool isSmoothing;
    [SerializeField]
    float smoothSpeed = 10.0f;

    void FixedUpdate()
    {
        #region TargetPosition
        //  get position
        Vector3 new_position;
        if ( isSecondTargetMode )
        {
            new_position = characterTarget.position + transform.rotation * positionOffset;
        }
        else
        {
            new_position = characterTarget.position + characterTarget.rotation * positionOffset;
        }

        //  smooth
        if ( isSmoothing )
        {
            new_position = Vector3.Lerp( transform.position, new_position, Time.deltaTime * smoothSpeed );
        }

        //  apply
        transform.position = new_position;
        #endregion

        #region TargetRotation
        //   get target
        Transform look_target = isSecondTargetMode ? secondTarget : characterTarget;

        //  get rotation
        Vector3 target_rotation = look_target.position;
        if ( isSecondTargetMode )
        {
            float multiplier = 1.0f - Mathf.Clamp01( ( transform.position - look_target.position ).magnitude / lookSecondOffsetDist );
            if ( multiplier > 0.0f )
            {
                target_rotation += look_target.rotation * lookSecondOffset * multiplier;
            }
        }
        else
        {
            target_rotation += look_target.rotation * lookTargetOffset;
        }

        Quaternion new_rotation = Quaternion.LookRotation( target_rotation - transform.position );

        //  smooth
        if ( isSmoothing )
        {
            new_rotation = Quaternion.Lerp( transform.rotation, new_rotation, Time.deltaTime * smoothSpeed );
        }

        //  apply
        transform.rotation = new_rotation;
        #endregion
    }

    public void ToggleSwitch( InputAction.CallbackContext ctx )
    {
        isSecondTargetMode = !isSecondTargetMode;
    }
}
