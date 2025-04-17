using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    public float throwPower;
    public Rigidbody2D theRB;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        theRB.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 向抛物方向旋转
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(theRB.velocity.x)));
    }
}
