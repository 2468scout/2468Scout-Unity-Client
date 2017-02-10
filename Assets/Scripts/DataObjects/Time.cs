using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Time
    {
        public int millisecond, second, minute, hour;
        public Time(System.DateTime time)
        {
            this.millisecond = time.Millisecond;
            this.second = time.Second;
            this.minute = time.Minute;
            this.hour = time.Hour;
        }
        public Time(int milliseconds)
        {
            this.hour = (int)(milliseconds / 3600000);
            this.minute = (int)(milliseconds / 60000) - (60 * hour);
            this.second = (int)(milliseconds / 1000) - (60 * minute) - (3600 * hour);
            this.millisecond = milliseconds - (1000 * second) - (60000 * minute) - (3600000 * hour);
        }

        public Time TimeSince(Time subtractTime)
        {
            int subtractMilliseconds = subtractTime.sumMilliseconds();
            int thisMilliseconds = sumMilliseconds();
            return new Time(thisMilliseconds - subtractMilliseconds);
        }
        public override string ToString()
        {
            if (this.second < 10)
            {
                return "" + minute + ":0" + second;
            }
            return "" + minute + ":" + second;
        }
        public int sumMilliseconds()
        {
            return millisecond + (1000 * (second + (60 * (minute + (60 * hour)))));
        }
    }
}
