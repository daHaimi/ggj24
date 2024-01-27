using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float CarSpeed = 3;
    public float SpawnIntervalRangeFrom = 3f;
    public float SpawnIntervalRangeTo = 6f;

    private bool _firstRun = true;

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            yield return new WaitForSeconds(

                _firstRun ? 0 :
                Random.Range(SpawnIntervalRangeTo, SpawnIntervalRangeTo)

                );

            _firstRun = false;

            int randomIndex = Random.Range(0, GlobalDataSo.Instance.SpawnableCars.Count);
            var car = Instantiate(GlobalDataSo.Instance.SpawnableCars[randomIndex], transform.position, transform.rotation);
            car.GetComponent<CarDriving>().Speed = CarSpeed * 1.5f; //multiplier because I'm lazy
        }
    }
}
