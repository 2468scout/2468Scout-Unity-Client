using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Time
    {
        public int millisecond, second, minute;
        public Time(int millisecond, int second, int minute)
        {
            this.millisecond = millisecond;
            this.second = second;
            this.minute = minute;
        }
        public void subtract(Time subtractTime)
        {
            this.millisecond = this.millisecond - subtractTime.millisecond;
            this.second = this.second - subtractTime.second;
            this.minute = this.minute - subtractTime.minute;
        }
    }
}
