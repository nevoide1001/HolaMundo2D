using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Jetpack jetpack;
    [SerializeField] private Slider fuelSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (jetpack != null && fuelSlider != null)
        {
            fuelSlider.minValue = 0;
            fuelSlider.maxValue = jetpack.maxEnergy;
            fuelSlider.value = jetpack.energy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (jetpack != null && fuelSlider != null)
        {
            fuelSlider.value = jetpack.energy;
        }
    }
}
