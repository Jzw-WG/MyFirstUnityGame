using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherSeven : MonoBehaviour
{
    public float absorbInterval = 6f;
    public float absorbDuration = 3f;
    public float absorbTime = 2f;
    public float absorbRadius = 8f;
    public Transform huluTransform;
    public GameObject sprite;

    private float absorbCounter;

    public static List<BrotherSeven> AllAbsorbers = new List<BrotherSeven>();

    void OnEnable() => AllAbsorbers.Add(this);
    void OnDisable() => AllAbsorbers.Remove(this);

    void Start()
    {
        absorbCounter = absorbInterval;
    }

    void Update()
    {
        absorbCounter -= Time.deltaTime;
        if (absorbCounter <= 0f)
        {
            absorbCounter = absorbInterval;
            StartCoroutine(AbsorbWeapons());
        }
    }

    BrotherSeven FindClosestAbsorber(Vector3 weaponPos)
    {
        BrotherSeven closest = null;
        float minDist = float.MaxValue;

        foreach (var absorber in BrotherSeven.AllAbsorbers)
        {
            float dist = Vector3.Distance(weaponPos, absorber.huluTransform.position);
            if (dist < absorber.absorbRadius && dist < minDist)
            {
                closest = absorber;
                minDist = dist;
            }
        }

        return closest;
    }

    void DisableWeaponAttack(GameObject weapon)
    {
        var damager = weapon.GetComponent<EnemyDamager>();
        if (damager != null) damager.enabled = false;

        var col = weapon.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    
        // 若你使用其他组件也可一并禁用
    }

    IEnumerator AbsorbWeapons()
    {
        sprite.SetActive(true);
        GameObject[] allWeapons = GameObject.FindGameObjectsWithTag("Weapon Item");

        bool absorbedAny = false;
        foreach (GameObject weapon in allWeapons)
        {
            if (weapon == null) {
                continue;
            }
            var targetAbsorber = FindClosestAbsorber(weapon.transform.position);
            if (targetAbsorber != null)
            {
                DisableWeaponAttack(weapon);
                StartCoroutine(MoveAndShrinkToHulu(weapon, targetAbsorber.huluTransform));
                absorbedAny = true;
            }
        }

        // 延迟关闭葫芦显示（保证动画还在进行）
        if (absorbedAny)
        {
            yield return new WaitForSeconds(absorbDuration);
        }
        sprite.SetActive(false);

        yield return null;
    }

    IEnumerator MoveAndShrinkToHulu(GameObject weapon, Transform target)
    {
        // 有点吵
        // SFXManager.instance.PlaySFXPitched(17);
        Vector3 startPos = weapon.transform.position;
        Vector3 startScale = weapon.transform.localScale;

        float t = 0f;
        while (t < absorbTime)
        {
            t += Time.deltaTime;
            float percent = t / absorbTime;

            weapon.transform.position = Vector3.Lerp(startPos, target.position, percent);
            weapon.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, percent);

            yield return null;
        }

        Destroy(weapon);
    }
}
