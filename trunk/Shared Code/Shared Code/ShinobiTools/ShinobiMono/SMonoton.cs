////////////////////////////////////////////////////////////
// SMonoton.cs
//
// Copyright © 2013-2104 Shinobi Tools, All Rights Reserved.
////////////////////////////////////////////////////////////
using UnityEngine;
#pragma warning disable 649
namespace ShinobiTools
{
    public class SMonoton<T> : ShinobiMono where T : ShinobiMono, new( )
    {
        /// <summary>
        /// 
        /// </summary>
        private static T _instance;

        /// <summary>
        /// 
        /// </summary>
        private static bool _hasBeenInitialized;

        /// <summary>
        /// 
        /// </summary>
        private static bool _applicationIsQuiting;

        #region Unity Engine Callbacks

        public void Awake( )
        {
            if( _instance == null )
            {
                _instance = this as T;
                _hasBeenInitialized = true;

                OnSingletonAwake( );
            }
            else
            {
                Debug.LogError( "Multiple monotons on the scene: " + gameObject.name );

                DestroyImmediate( this );
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnSingletonAwake( )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public static T Instance
        {
            get
            {
                if( _applicationIsQuiting )
                {
                    Debug.LogError( "Monoton is being called after the application quit. Possible from an OnDestroy( )" );
                }

                if( _instance == null && !_hasBeenInitialized )
                {
                    _instance = FindObjectOfType( typeof( T ) ) as T ?? new GameObject( "SMonoton_" + typeof( T ) ).AddComponent<T>( );

                    DontDestroyOnLoad( _instance.gameObject );

                    _hasBeenInitialized = true;
                }
                else
                {
                    if( _instance == null && _hasBeenInitialized )
                    {
                        Debug.LogError( "Reinstantiate singleton is not allowed. One reason might be that" + _instance.gameObject.name + " is called from OnDestroy( )" );
                    }
                }
                return _instance;
            }
        }
    };
};
