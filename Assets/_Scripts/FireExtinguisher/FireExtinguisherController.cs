using UnityEngine;

public class FireExtinguisherController : MonoBehaviour
{
    public bool pinRemoved = false;
    public bool leverPressed = false;

    public void RemovePin()
    {
        pinRemoved = true;
        Debug.Log("Pin removed");
    }

    public void PressLever(bool pressed)
    {
        leverPressed = pressed;
    }
}
