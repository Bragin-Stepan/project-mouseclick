using UnityEngine;


public class PlayerInput
{
    private const int LeftMouseButtonKey = 0;
    private const int RightMouseButtonKey = 1;
    
    public bool OnClick => Input.GetMouseButtonDown(LeftMouseButtonKey);
    public bool OnRightClick => Input.GetMouseButtonDown(RightMouseButtonKey);
    public Vector3 PointPosition => Input.mousePosition;
    public Vector3 Direction => new (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
}
