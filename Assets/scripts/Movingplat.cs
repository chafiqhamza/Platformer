using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingplat : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] float speed = 1.5f;
    int direction = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = CurrentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);
        float distance = (target - (Vector2)platform.position).magnitude;
        if(distance <= 0.1f) 
        {
            direction *= -1;
        }
    }
    Vector2 CurrentMovementTarget() 
    {
        if(direction == 1) 
        {
            return start.position ;
        }
        else { return end.position ;}
    }
    private void OnDrawGizmos()
    {
        if (platform != null && start != null && end != null)
        {
            Gizmos.DrawLine(platform.transform.position, start.position);
            Gizmos.DrawLine(platform.transform.position, end.position);
        }
    }
}
