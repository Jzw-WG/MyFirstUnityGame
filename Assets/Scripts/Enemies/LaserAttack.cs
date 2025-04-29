using System.Collections;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public LineRenderer laserRenderer;
    public float laserLength = 10f;
    public float shrinkSpeed = 5f;
    public float slowDuration = 2f;
    public float slowFactor = 0.5f;
    public LayerMask playerLayer;

    private bool isFiring = false;
    private Vector3 laserDir;
    private Vector3 laserStartPos;
    private bool hasHitPlayer = false;

    void Start()
    {
        laserRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 用按键测试激活，正式版可以改成靠近玩家时触发
        {
            FireLaser();
        }

        if (isFiring)
        {
            UpdateLaser();
        }
    }

    void FireLaser()
    {
        // 确定方向
        laserStartPos = transform.position;
        laserDir = (FindObjectOfType<PlayerController>().transform.position - transform.position).normalized;

        // 设置初始Line
        laserRenderer.positionCount = 2;
        laserRenderer.SetPosition(0, laserStartPos);
        laserRenderer.SetPosition(1, laserStartPos + laserDir * laserLength);
        laserRenderer.enabled = true;

        isFiring = true;
        hasHitPlayer = false;
    }

    void UpdateLaser()
    {
        if (!hasHitPlayer)
        {
            // 检测激光是否碰到玩家
            RaycastHit2D hit = Physics2D.Raycast(laserStartPos, laserDir, laserLength, playerLayer);
            if (hit.collider != null)
            {
                hasHitPlayer = true;

                // 立刻开始缩短
                Debug.Log("命中玩家！");
                hit.collider.GetComponent<PlayerController>()?.ApplySlow(slowFactor, slowDuration);
            }
        }

        // 缩短激光
        Vector3 endPos = laserRenderer.GetPosition(1);
        endPos -= laserDir * shrinkSpeed * Time.deltaTime;

        // 更新Line
        laserRenderer.SetPosition(1, endPos);

        // 完全收回就禁用
        if (Vector3.Distance(laserRenderer.GetPosition(0), endPos) <= 0.1f)
        {
            isFiring = false;
            laserRenderer.enabled = false;
        }
    }
}
