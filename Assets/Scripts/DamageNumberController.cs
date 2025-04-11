using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;
    private List<DamageNumber> numeberPool = new List<DamageNumber>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDamage(float damageAcount, Vector3 location) {
        int rounded = Mathf.RoundToInt(damageAcount);
        // DamageNumber newDamage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        
        DamageNumber newDamage = GetFromPool();
        newDamage.transform.position = location;
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);
    }

    public DamageNumber GetFromPool() {
        DamageNumber numberToOutput = null;
        if (numeberPool.Count == 0) {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        } else {
            numberToOutput = numeberPool[0];
            numeberPool.RemoveAt(0);
        }
        return numberToOutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace) {
        numberToPlace.gameObject.SetActive(false);
        numeberPool.Add(numberToPlace);
    }
}
