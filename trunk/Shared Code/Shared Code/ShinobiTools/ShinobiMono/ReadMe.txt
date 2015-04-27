////////////////////////////////////////
//           Shinobi:Mono (v1.0)
//
// Copyright © 2013-2014 Shinobi Tools
////////////////////////////////////////


-----------------------
How to create a timer.
-----------------------

1.) Extend from ShinobiMono.
2.) Create a timer.

Timers can be created in multiple ways.

String: Use a srtring to specify a method to call.
Action: Set a timer to invoke an Action
Lambda: Inline a timer by using an anonymous method.


Frequency:         The amount of time to pass before an execution occurs.
Loop:              Execute the timer once or repeatedly.
SomeMethodToCall:  The method to invoke when the timer executes.
IgnoreTimerScale:  Make the timer ignore the default time-scale.
              
SetTimer( Frequency, Loop, SomeMethodToCall, IgnoreTimeScale )


using ShinobiTools;
public class MyClass : ShinobiMono
{
	public void Awake( )
	{
		SetTimer( 0.5f, false, "SayHello" );                         <------ String

		SetTimer( 0.5f, false, SayHello );                           <------ Action 

		SetTimer( 0.5f, false, ( ) => { Debug.Log( "Hello" ); } );   <------ Lambda
	}

	public void SayHello( )
	{
		Debug.Log( "Hello" );
	}
}