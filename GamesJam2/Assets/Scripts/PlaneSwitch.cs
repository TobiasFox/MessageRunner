using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSwitch : MonoBehaviour
{
    [SerializeField] private Transform planeToSwitchTop;
    [SerializeField] private Transform planeToSwitchDown;

    public Transform GetPlaneSwitchTop()
    {
        return planeToSwitchTop;
    }

    public Transform GetPlaneSwitchDown()
    {
        return planeToSwitchDown;
    }

}
