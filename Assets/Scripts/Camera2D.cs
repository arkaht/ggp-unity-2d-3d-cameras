using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Camera2D : MonoBehaviour
{
    [Header( "Character Follow" ), SerializeField]
    Transform target;
    [SerializeField]
    Vector3 positionOffset;
    [SerializeField]
    float positionOffsetDist = 500.0f, cursorOffset = 10.0f;

    [Header( "Smooth" ), SerializeField]
    bool isSmoothing;
    [SerializeField]
    float smoothSpeed = 10.0f;

    [Header( "UI" ), SerializeField]
    RawImage cursorImage;

    void FixedUpdate()
    {
        Vector3 new_position = target.position + positionOffset;

        //  cursor offset
        Vector2 cursor_pos = Mouse.current.position.ReadValue();
        Vector2 cursor_offset = new( Screen.width / 2 - cursor_pos.x, Screen.height / 2 - cursor_pos.y );
        float multiplier = Mathf.Clamp01( cursor_offset.magnitude / positionOffsetDist );
        Vector2 cursor_direction = -cursor_offset.normalized * multiplier;
        new_position += new Vector3( cursor_direction.x, 0.0f, cursor_direction.y ) * cursorOffset;

        //  smooth
        if ( isSmoothing )
        {
            new_position = Vector3.Lerp( transform.position, new_position, Time.deltaTime * smoothSpeed );
        }

        transform.position = new_position;
        cursorImage.transform.position = cursor_pos;
    }
}
