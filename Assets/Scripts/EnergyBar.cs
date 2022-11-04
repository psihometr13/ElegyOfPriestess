using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public PlayerControl playerEnergy;

    private void Start()
    {
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = playerEnergy.maxEnergy;
        energyBar.value = playerEnergy.maxEnergy;
    }

    public void SetEnergy(int energyPoints)
    {
        energyBar.value = energyPoints;
    }
}
