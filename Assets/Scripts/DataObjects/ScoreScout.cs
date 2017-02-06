using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class ScoreScout
    {
        public bool bColor;
        public int iMatchNumber;
        public string sEventCode;
        public List<int> increase1TimeList, increase5TimeList, increase40TimeList, increase50TimeList, increase60TimeList;
        public ScoreScout(bool bColor, int iMatchNumber, string sEventCode)
        {
            this.bColor = bColor;
            this.iMatchNumber = iMatchNumber;
            this.sEventCode = sEventCode;
            increase1TimeList = new List<int>();
            increase5TimeList = new List<int>();
            increase40TimeList = new List<int>();
            increase50TimeList = new List<int>();
            increase60TimeList = new List<int>();
        }
    }
}
