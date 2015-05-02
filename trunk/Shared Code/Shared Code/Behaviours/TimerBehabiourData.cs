using System;

namespace SharedCode.Behaviours.Internal
{
    public class TimerBehabiourData 
    {
        /// <summary>
        /// Execute once or execute repeatedly.
        /// </summary>
        public bool loop;

        /// <summary>
        /// True if the timer is paused.
        /// </summary>
        public bool paused;

        /// <summary>
        /// Name of the method to call when the timer executes.
        /// </summary>
        public string methodName;

        /// <summary>
        /// The amount of time to pass before an execution occurs.
        /// </summary>
        public float frequency;

        /// <summary>
        /// The amount of elapsed time since the last execution.
        /// </summary>
        public float elpasedTime;

        /// <summary>
        /// The action to invoke when this timer executes.
        /// </summary>
        public Action action;
    };
};
