using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public Upd_PlayerControl playerEnergy;

    private void Start()
    {
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<Upd_PlayerControl>();
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = playerEnergy.maxEnergy;
        energyBar.value = playerEnergy.maxEnergy;
    }

    public void SetEnergy(float energyPoints)
    {
        energyBar.value = energyPoints;
    }
}
