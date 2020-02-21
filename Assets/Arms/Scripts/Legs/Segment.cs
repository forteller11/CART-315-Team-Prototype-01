using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Segment : MonoBehaviour, IPoolable
{
    public Segment Next;
    public Segment Previous;
    public event Action<Grabber> IsNewGrabberEvent;

    public void BecomeNewGrabber()
    {
        if (Next != null) //split arm if in center
            Next.GetComponent<SpringJoint2D>().connectedBody = null;
        
        var collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        transform.localScale *= Grabber.SCALE_MULTIPLY;
        
        var grabber = gameObject.AddComponent<Grabber>();

        grabber.gameObject.AddComponent<CircleCollider2D>().radius = collider.radius/3f;
        grabber.OpenColor = GetComponent<SpriteRenderer>().color;
        grabber.AttemptGrabColor = grabber.OpenColor * 1.75f;
        
        IsNewGrabberEvent?.Invoke(grabber);

    }
    
    public void SetActiveTo(bool trueFalse)
    {
        gameObject.SetActive(trueFalse);
    }

    public Action<IPoolable> ReturnToPool { get; set; }
    public GameObject PooledGameObject { get; set; }
    
    
    void Update()
    {
        //ReturnToPool.Invoke((IPoolable) this);
    }
}


