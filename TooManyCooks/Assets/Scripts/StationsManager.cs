using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationsManager : MonoBehaviour
{
    public static StationsManager instance;

    public List<Station> existingStations;
    public List<Station> chosenStations;

    public GameObject ingredientMixPrefab;

    private void Awake()
    {
        Init();
        InstantiateStations();
        ResetStationCode();
        GetStationCode();
    }

    public virtual void Init()
    {
        instance = this;
    }

    void InstantiateStations()
    {
        //Instantiate();
        //stationList.Add();
    }

    void GetStationCode()
    {
        foreach (Station _station in chosenStations)
        {
            if(_station.stationType == Station.StationType.KitchenCounter)
            {
                _station.stationCode += 10;
            }
            else if(_station.stationType == Station.StationType.CampFire)
            {
                _station.stationCode += 20;
            }

            if(_station.stationFunction == Station.StationFunction.CuttingBoard)
            {
                _station.stationCode += 1;
            }
            else if(_station.stationFunction == Station.StationFunction.Grill)
            {
                _station.stationCode += 3;
            }

            //Debug.Log(_station.gameObject.name + " Code : " + _station.stationCode);
        }
    }

    void ResetStationCode()
    {
        foreach (Station _station in chosenStations)
        {
            if(_station.stationCode != 0)
            {
                _station.stationCode = 0;
            }
        }
    }
}
