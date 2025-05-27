using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;

    public Animator anim;

    public float pickupRange = 2f;
    // public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;

    public List<Skill> unassignedSkills, assignedSkills;

    public int maxWeapons = 3;

    private bool isSlowed = false;
    private float originalSpeed;

    [HideInInspector]
    public List<Weapon> fullyLeveledWeapons = new List<Weapon>();
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (assignedWeapons.Count == 0) {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }

        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        Debug.Log(moveInput);

        // 消除斜向移动速度变快的问题
        moveInput.Normalize();
        // * Time.deltaTime 消除帧率带来的速度差
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput != Vector3.zero) {
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }
    }

    public void AddWeapon(int weaponNumber) {
        if (weaponNumber < unassignedWeapons.Count) {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd) {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }

    public void ApplySlow(float factor, float duration) {
        if (!isSlowed) {
            isSlowed = true;
            originalSpeed = moveSpeed;
            moveSpeed *= factor;
            StartCoroutine(RemoveSlow(duration));
        }
    }

    IEnumerator RemoveSlow(float time) {
        yield return new WaitForSeconds(time);
        moveSpeed = originalSpeed;
        isSlowed = false;
    }
}
