using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;

    private void Start()
    {
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = PlayerControl.Instance.maxEnergy;
        energyBar.value = PlayerControl.Instance.maxEnergy;
    }

    public void SetEnergy(int energyPoints)
    {
        energyBar.value = energyPoints;
    }
}
