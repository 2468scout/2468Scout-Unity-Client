using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class HeatmapsData
    {
        public List<Point> gearMapPointList, lowGoalMapPointList, highGoalMapPointList, climbMapPointList, hopperMapPointList;
        public List<bool> climbMapBoolList;
        public List<float> lowGoalMapFloatList, highGoalMapFloatList;
    }
}
