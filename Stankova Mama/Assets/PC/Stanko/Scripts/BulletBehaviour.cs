using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{

    SpriteRenderer render;

    void Range() 
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        Invoke("Range", 3f);
        render = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!render.isVisible) 
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        this.transform.Translate(
            new Vector3(0.3f * System.Math.Sign(this.transform.localScale.x),
                0,0));
    }
}
