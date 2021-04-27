using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public void _OnMasterChanged(float newVal)
    {
        GameSingleton.desiredMaster = newVal;
    }
}
