using UnityEngine;

public class Door : Switch
{
    [SerializeField] private GameObject openDoor;
    public override void Activate()
    {
        openDoor.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
