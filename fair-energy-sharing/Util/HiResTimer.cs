﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Util
{
    /// <summary>
    /// timer class used for calculate time
    /// </summary>
    public class HiResTimer
    {
        private bool isPerfCounterSupported = false;
        private Int64 frequency = 0;
        private Int64 startValue = 0;

        // Windows CE native library with QueryPerformanceCounter().
        private const string lib = "Kernel32.dll";
        [System.Runtime.InteropServices.DllImport(lib)]
        private static extern int QueryPerformanceCounter(ref Int64 count);
        [System.Runtime.InteropServices.DllImport(lib)]
        private static extern int QueryPerformanceFrequency(ref Int64 frequency);
        
        public HiResTimer()
        {
            // Query the high-resolution timer only if it is supported.
            // A returned frequency of 1000 typically indicates that it is not
            // supported and is emulated by the OS using the same value that is
            // returned by Environment.TickCount.
            // A return value of 0 indicates that the performance counter is
            // not supported.
            int returnVal = QueryPerformanceFrequency(ref frequency);

            if (returnVal != 0 && frequency != 1000)
            {
                // The performance counter is supported.
                isPerfCounterSupported = true;
            }
            else
            {
                // The performance counter is not supported. Use
                // Environment.TickCount instead.
                frequency = 1000;
            }
        }

        public Int64 Frequency
        {
            get
            {
                return frequency;
            }
        }

        public Int64 Value
        {
            get
            {
                Int64 tickCount = 0;

                if (isPerfCounterSupported)
                {
                    // Get the value here if the counter is supported.
                    QueryPerformanceCounter(ref tickCount);
                    return tickCount;
                }
                else
                {
                    // Otherwise, use Environment.TickCount.
                    return (Int64)Environment.TickCount;
                }
            }
        }

        /// <summary>
        /// restart timer
        /// </summary>
        public void ReStart()
        {
            startValue = this.Value;
        }       

        /// <summary>
        /// calculate elapse time
        /// </summary>
        public double TimeElapseInTenthsOfMilliseconds
        {
            get
            {
                var now = this.Value;

                return (now - startValue) * 10000.0 / this.Frequency;
            }
        }

        //static void Main()
        //{
        //    HiResTimer timer = new HiResTimer();

        //    timer.ReStart();

        //    //do something

        //    var elapseTime = timer.TimeElapseInTenthsOfMilliseconds;
        //}
    }
    }

