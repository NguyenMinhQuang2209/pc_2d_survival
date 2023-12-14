using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform equipmentPosition;
    private void Update()
    {
        if (equipmentPosition != null)
        {
            Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(equipmentPosition.position);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 dir = mousePosition - playerScreenPosition;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            equipmentPosition.rotation = Quaternion.Euler(new(0f, 0f, angle));
        }
    }
}
