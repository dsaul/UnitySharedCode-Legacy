////////////////////////////////////////////////////////////
// SGameTime.cs
//
// Copyright © 2013-2014 Shinobi Tools, All Rights Reserved.
////////////////////////////////////////////////////////////
using UnityEngine;

namespace ShinobiTools
{
    public class SGameTime : SMonoton<SGameTime>
    {
        /// <summary>
        /// Holds the real time since start up.
        /// </summary>
        private float _actualTime;

        /// <summary>
        /// Holds the actual change in time since the last frame.
        /// </summary>
        private float _actualDeltaTime;

        #region Unity Engine Callbacks

        protected override void OnSingletonAwake( )
        {
            _actualTime = Time.realtimeSinceStartup;
        }

		protected override void Update()
        {
            base.Update( );

            float totalElapsedTime = Time.realtimeSinceStartup;

            _actualDeltaTime = Mathf.Clamp01( totalElapsedTime - _actualTime );
            _actualTime      = totalElapsedTime;
        }

        #endregion

        /// <summary>
        /// No time-scale delta time.
        /// </summary>
        /// <returns>Change in time since the last frame.</returns>
        public float GetActualDeltaTime( )
        {
            return _actualDeltaTime;
        }

        /// <summary>
        /// No time-scale delta time.
        /// </summary>
        public float ActualDeltaTime
        {
            get
            {
                return _actualDeltaTime;
            }
        }
    };
};
