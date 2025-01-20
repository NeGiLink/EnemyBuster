using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace MyAssets
{
    [Serializable]
    public class SaveStatusData
    {
        public float hpRatio;

        public float spRatio;

        public float speedRatio;

        public float powerRatio;

        public float defenseRatio;

        public StatusType type;

        public void ChangeData(SaveStatusData saveStatusData2)
        {
            hpRatio = saveStatusData2.hpRatio;
            spRatio = saveStatusData2.spRatio;
            speedRatio = saveStatusData2.speedRatio;
            powerRatio = saveStatusData2.powerRatio;
            defenseRatio = saveStatusData2.defenseRatio;
        }

        public void ResetData()
        {
            hpRatio = 0;
            spRatio = 0;
            speedRatio = 0;
            powerRatio = 0;
            defenseRatio = 0;
            type = StatusType.Null;
        }
    }
}
