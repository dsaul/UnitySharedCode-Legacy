using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode.Behaviours.Internal
{
    public class TimerBehaviour : MonoBehaviour
    {
        /// <summary>
        /// How long the object lives before dying. A lifespan of 0.0 means the object lives forever.
        /// </summary>
        public float lifeSpan;

        /// <summary>
        /// 
        /// </summary>
		private List<TimerBehaviourData> _timers;

        /// <summary>
        /// Creates a new instance of type ShinobiMono.
        /// </summary>
        public TimerBehaviour( )
        {
			_timers = new List<TimerBehaviourData>();
        }

        #region Unity Engine Callbacks

		protected virtual void Update()
        {
            // Update the objects timers.
            UpdateTimers( );

            // Update lifespan
            if( lifeSpan != 0.0f )
            {
                lifeSpan -= Time.deltaTime;
                if( lifeSpan <= 0.0001f )
                {
                    lifeSpan = 0.0f;

                    OnLifeSpanExpired( );
                    return;
                }
            }
        }

        protected virtual void OnDestroy( )
        {
            _timers = null;
        }

        #endregion

        /// <summary>
        /// Called when the life span for this object expires.
        /// </summary>
		protected virtual void OnLifeSpanExpired()
        {

        }

        /// <summary>
        /// Sets a timer to call a given method at a set interval.
        /// Frequency rates of 0.0 will disable the previous timer.
        /// </summary>
        /// <param name="inFrequency">The amount of time to pass before an execution occurs.</param>
        /// <param name="inLoop">Whether to execute once or execute repeatedly.</param>
        /// <param name="inMethodName">Name of the method to call when the timer executes.</param>
        public void SetTimer( float inFrequency, bool inLoop, string inMethodName, bool inIgnoreTimeScale = false )
        {
            // Validate the method name before continuing.
            if( string.IsNullOrEmpty( inMethodName ) )
            {
                Debug.Log( "Failed to create timer. String is empty" );
                return;
            }

            bool isDupe = false;

            // Search for a duplicate before creating a new instance.
            for( int timerIdx = 0; timerIdx < _timers.Count; timerIdx++ )
            {
                TimerBehaviourData timer = _timers[timerIdx];

                if( timer.methodName == inMethodName )
                {
                    isDupe = true;

                    // If the frequency is 0, flag it for removal
                    if( inFrequency == 0.0f )
                    {
                        // We'll remove this timer next time we tick all the timers.
                        timer.frequency = 0.0f;
                    }
                    else
                    {
                        // Update the timer with the new info.
                        timer.loop            = inLoop;
                        timer.frequency       = inFrequency;
                        timer.elpasedTime     = 0.0f;
                    }
                    timer.paused = false;

                    break;
                }
            }

            // If this timer is not a dupe, create a new one.
            if( !isDupe )
            {
                TimerBehaviourData newTimer      = new TimerBehaviourData( );
                newTimer.loop            = inLoop;
                newTimer.action          = null;
                newTimer.paused          = false;
                newTimer.frequency       = inFrequency;
                newTimer.methodName      = inMethodName;
                newTimer.elpasedTime     = 0.0f;

                _timers.Add( newTimer );
            }
        }

        /// <summary>
        /// Sets a timer to call the given action at a set interval.
        /// Frequency rates of 0.0 will disable the previous timer.
        /// 
        /// Note: 
        /// Anonymous methods cannot be searched or updated. Use anonymous
        /// methods for single shot script calls.
        /// </summary>
        /// <param name="inFrequency">The amount of time to pass between executions.</param>
        /// <param name="inLoop">Whether to execute once or execute repeatedly.</param>
        /// <param name="inAction">The action to invoke when this timer executes.</param>
        public void SetTimer( float inFrequency, bool inLoop, Action inAction)
        {
            bool isDupe = false;

            // Search for a duplicate before creating a new instance.
            for( int timerIdx = 0; timerIdx < _timers.Count; timerIdx++ )
            {
                TimerBehaviourData timer = _timers[timerIdx];

                if( timer.action == inAction )
                {
                    isDupe = true;

                    // If the frequency is 0, disable the timer.
                    if( inFrequency == 0.0f )
                    {
                        // We'll remove this timer next time we tick all the timers.
                        timer.frequency = 0.0f;
                    }
                    else
                    {
                        // Update the timer with the new info.
                        timer.loop            = inLoop;
                        timer.frequency       = inFrequency;
                        timer.elpasedTime     = 0.0f;
                    }
                    timer.paused = false;

                    break;
                }
            }

            // If this timer is not a dupe, create a new one.
            if( !isDupe )
            {
                TimerBehaviourData newTimer      = new TimerBehaviourData( );
                newTimer.loop            = inLoop;
                newTimer.action          = inAction;
                newTimer.paused          = false;
                newTimer.frequency       = inFrequency;
                newTimer.methodName      = "";
                newTimer.elpasedTime     = 0.0f;

                _timers.Add( newTimer );
            }
        }

        /// <summary>
        /// Update all active timers.
        /// </summary>
        public void UpdateTimers( )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( !_timers[i].paused )
                {
					_timers[i].elpasedTime += Time.deltaTime;
                }
            }

            bool removeTimer = false;
            for( int i = 0; i < _timers.Count; i++ )
            {
                // Check for a disabled timer.
                if( _timers[i].frequency == 0.0f )
                {
                    _timers.RemoveAt( i-- );
                }
                else if( _timers[i].frequency < _timers[i].elpasedTime )
                {
                    removeTimer = false;

                    // Calculate how many times the timer may have elapsed.
                    int callCount = _timers[i].loop ? (int)( _timers[i].elpasedTime / _timers[i].frequency ) : 1;

                    // Mark the timer for removal if it's not looping.
                    if( !_timers[i].loop )
                    {
                        removeTimer = true;
                    }
                    else
                    {
                        // Reset the timer.
                        _timers[i].elpasedTime = 0.0f;
                    }

                    // Now call the function.
                    while( callCount > 0 )
                    {
                        if( _timers[i].action == null )
                        {
                            SendMessage( _timers[i].methodName );
                        }
                        else
                        {
                            _timers[i].action( );
                        }

                        callCount--;

                        // Check to see if the timer was cleared from the last call.
                        if( _timers[i].frequency == 0.0f )
                        {
                            removeTimer = true;
                            break;
                        }
                        else if( _timers[i].elpasedTime == 0.0f )
                        {
                            // If the timer was reset, do not flag it for removal.
                            removeTimer = false;
                        }
                    }

                    // Check to see if this timer should be removed.
                    if( removeTimer )
                    {
                        _timers.RemoveAt( i-- );
                    }
                }
            }
        }

        /// <summary>
        /// Clears all the timers from this object.
        /// </summary>
        public void ClearAllTimers( )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                // Flag the timer for removal.
                _timers[i].frequency = 0.0f;
            }
        }

        /// <summary>
        /// Pauses or unpauses all of the objects timers.
        /// </summary>
        /// <param name="pause"></param>
        public void PauseAllTimers( bool pause )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                // Set the pause value.
                _timers[i].paused = pause;
            }
        }

        /// <summary>
        /// Pauses or unpauses a timer.
        /// </summary>
        /// <param name="pause">Whether to pause or unpause a timer.</param>
        /// <param name="inMethodName"><Name of the timer to pause or unpause./param>
        public void PauseTimer( bool pause, string inMethodName )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].methodName == inMethodName )
                {
                    // Set the pause value.
                    _timers[i].paused = pause;
                }
            }
        }

        /// <summary>
        /// Pauses or unpauses a timer.
        /// </summary>
        /// <param name="pause">Whether to pause or unpause a timer.</param>
        /// <param name="inAction"><Action to pause or unpause./param>
        public void PauseTimer( bool pause, Action inAction )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].action == inAction )
                {
                    // Set the pause value.
                    _timers[i].paused = pause;
                }
            }
        }

        /// <summary>
        /// Clears a previously set timer.
        /// </summary>
        /// <param name="inMethodName">Name of the timer to remove.</param>
        public void ClearTimer( string inMethodName )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].methodName == inMethodName )
                {
                    // Flag the timer for removal.
                    _timers[i].frequency = 0.0f;
                }
            }
        }

        /// <summary>
        /// Clears a previously set timer.
        /// </summary>
        /// <param name="inAction">Action to remove.</param>
        public void ClearTimer( Action inAction )
        {
			if (null == _timers)
				return;
			
			for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].action == inAction )
                {
                    // Flag the timer for removal.
                    _timers[i].frequency = 0.0f;
                }
            }
        }

        /// <summary>
        /// Checks if the specified timer is active.
        /// </summary>
        /// <param name="inMethodName">Name of the timer to check.</param>
        /// <returns>True if the timer is active, false otherwise.</returns>
        public bool IsTimerActive( string inMethodName )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].methodName == inMethodName )
                {
                    return ( _timers[i].frequency > 0.0f );
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the specified timer is active.
        /// </summary>
        /// <param name="inAction">Action to check.</param>
        /// <returns>True if the timer is active, false otherwise.</returns>
        public bool IsTimerActive( Action inAction )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].action == inAction )
                {
                    return ( _timers[i].frequency > 0.0f );
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the specified timers elapsed time.
        /// </summary>
        /// <param name="inMethodName">Name of the timer to access.</param>
        /// <returns>The elapsed time since the last execution, or -1.0 if the timer is inactive.</returns>
        public float GetTimerElapsedTime( string inMethodName )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].methodName == inMethodName )
                {
                    return _timers[i].elpasedTime;
                }
            }
            return -1.0f;
        }

        /// <summary>
        /// Gets the specified timers elapsed time.
        /// </summary>
        /// <param name="inAction">Action to access.</param>
        /// <returns>The elapsed time since the last execution, or -1.0 if the timer is inactive.</returns>
        public float GetTimerElapsedTime( Action inAction )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].action == inAction )
                {
                    return _timers[i].elpasedTime;
                }
            }
            return -1.0f;
        }

        /// <summary>
        /// Gets the specified timers frequency rate.
        /// </summary>
        /// <param name="inMethodName">Name of the timer to access.</param>
        /// <returns>The frequency rate of the timer, or -1.0 if the timer is inactive.</returns>
        public float GetTimerFrequency( string inMethodName )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].methodName == inMethodName )
                {
                    return _timers[i].frequency;
                }
            }
            return -1.0f;
        }

        /// <summary>
        /// Gets the specified timers frequency rate.
        /// </summary>
        /// <param name="inAction">Action to access.</param>
        /// <returns>The frequency rate of the timer, or -1.0 if the timer is inactive.</returns>
        public float GetTimerFrequency( Action inAction )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].action == inAction )
                {
                    return _timers[i].frequency;
                }
            }
            return -1.0f;
        }

        /// <summary>
        /// Gets the specified timers remaining time before it executes.
        /// </summary>
        /// <param name="inMethodName">Name of the method to access.</param>
        /// <returns>The remaining time before the timer executes, or -1.0 if the timer is inactive.</returns>
        public float GetRemainingTime( string inMethodName )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].methodName == inMethodName )
                {
                    return ( _timers[i].frequency - _timers[i].elpasedTime );
                }
            }
            return -1.0f;
        }

        /// <summary>
        /// Gets the specified timers remaining time before it executes.
        /// </summary>
        /// <param name="inAction">Action to access.</param>
        /// <returns>The remaining time before the timer executes, or -1.0 if the timer is inactive.</returns>
        public float GetRemainingTime( Action inAction )
        {
            for( int i = 0; i < _timers.Count; i++ )
            {
                if( _timers[i].action == inAction )
                {
                    return ( _timers[i].frequency - _timers[i].elpasedTime );
                }
            }
            return -1.0f;
        }
    };
};
