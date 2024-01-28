using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float CarSpeed = 3;
    public float SpawnIntervalRangeFrom = 3f;
    public float SpawnIntervalRangeTo = 6f;

    private bool _firstRun = true;
    private float _timer = 0f;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_firstRun || _timer >= Random.Range(SpawnIntervalRangeFrom, SpawnIntervalRangeTo))
        {
            _firstRun = false;
            _timer = 0f;

            SpawnCar();
        }
    }

    void SpawnCar()
    {
        int randomIndex = Random.Range(0, GlobalDataSo.Instance.SpawnableCars.Count);
        var car = Instantiate(GlobalDataSo.Instance.SpawnableCars[randomIndex], transform.position, transform.rotation);
        car.GetComponent<CarDriving>().Speed = CarSpeed * 1.5f; // multiplier because I'm lazy
    }
}
