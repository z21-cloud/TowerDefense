using UnityEngine;

public class GetRotation
{
    public Quaternion FromPositionToTarget(Vector2 objectPosition, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - objectPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        return targetRotation;
    }
}
