/*
	Fudger in OpenPetz is very similar to Fudger in vanilla Petz.

	Reverse engineered by Cleo (Reflet@Yabiko) 2023-2025. Thank you!

*/

using Godot;
using System;
using System.Collections.Generic;

using OpenPetz;

public class Fudger 
{
	public enum EMode 
	{
		Linear			 = 0,
		AngleShortestArc = 1,
		AngleDirect		 = 2
	}
		
	public enum EDirectiveType 
	{
		Deactivated = 0,
		Suspended   = 1,
		Aim         = 2,
		Drift       = 3,
		Align       = 4,
		Decay       = 5,
		SwingTrue   = 6,
		SwingFalse  = 7
	}
	
	public enum EType 
	{
		Rotation = 0,
		Roll     = 1,
		Tilt     = 2,
		Num //used for allocating fudger list
	}
	
	public EMode FudgeMode = EMode.Linear;
	public EDirectiveType DirectiveType = EDirectiveType.Deactivated; // ?
	public EDirectiveType DirectiveTypeToResume = EDirectiveType.Deactivated;
		
	public bool UpdateWhenDeactivated = false;
		
	int AimTarget = 0;					//  [+10] transitioning current value to this value over time
	int LowerLimit = -0x7FFFFFFE;					//  [+14] value's lower limit (default 0x80000000)
	int UpperLimit = 0x7FFFFFFE;					//  [+18] value's upper limit (default 0x7FFFFFFE)
	int PreviousValue = 0;				//  [+1C] previous value of the thing (speed = current - previous)
	int CurrentValue = 0;
	
	//  aim variables
	double AimVelocity;					//  [+28] aka: aim rate
	double AimAccelQuantum;				//  [+38] per-tick velocity quantum for acceleration/braking
	double AimDefaultAccelPerTick;		//  [+40]
	double AimMaxVelocity;				//  [+48] scaling factor applied to interpolation speed; higher = snappier transitions
	double AimDefaultMaxVelocity;		//  [+50]
	double AimVelocityDamping;			//  [+58]
	double AimDefaultVelocityDamping;	//  [+60]
	bool DisableAiming;				//  [+34] if true, DoFudgeModeAim() just sets CurrentValue to AimTarget
	bool DefaultAimRateSet;			//  [+70]
	int AimRampTicks;					//  [+68] ticks to reach max velocity
	int AimLookaheadDiv;				//  [+6C] how much foresight to apply for smoothing/oscillation prevention purposes
	int AimDefaultVelocity = 0x7FFFFFFF;			//  [+30] default 0x7FFFFFFF
	
	//  drift variables
	int DriftRate;					//  [+74]

//  align variables

	double AlignProgress;				//  [+88]
	double AlignRate;					//  [+90]
	bool AlignOverStepsGo;				//  [+7C] if false, Fudger::AlignOverStepsGo() just returns
	int AlignTarget;					//  [+78]
	int AlignNumTicks;				//  [+80]
	int AlignCurTick;					//  [+84]

//  decay variables
	int DecayRate;					//  [+98]
	int PreviousAimTarget = 0;			//  [+9C]

//  swing variables
	double SwingPhaseRad;				//  [+A0] internal phase accumulator (state)
	double SwingDamping;					//  [+B0] damping factor; higher = more sensitive swinging
	int SwingRadius = 147;					//  [+A8] center of swing (default 147)
		
	public Fudger(EDirectiveType _type)
	{
		DirectiveType = _type;
	}
		
	public float GetCurrentAngle()
	{
		return CurrentValue / 128f * (float)Math.PI;
	}	
	
    public bool IsFudgingDegrees() 
	{
		return (FudgeMode == EMode.AngleShortestArc || FudgeMode == EMode.AngleDirect);
	}
	
    public void SetAimRate(int _maxVelocity)
	{
		AimMaxVelocity  = _maxVelocity / 100.0;
		AimAccelQuantum = AimMaxVelocity / AimRampTicks;

		if (AimAccelQuantum < 1.0)
			AimAccelQuantum = 1.0;
	}
	
	public void SetDefaultAimRate(int _maxVelocity, int _rampTicks, int _dampingPercent, int _lookaheadDiv)
	{
		DefaultAimRateSet = true;

		if (_rampTicks >= 0)
			AimRampTicks = _rampTicks;

		if (_dampingPercent >= 0)
			AimVelocityDamping = _dampingPercent / 100.0;

		if (_lookaheadDiv >= 0)
			AimLookaheadDiv = _lookaheadDiv;

		SetAimRate(_maxVelocity);

		AimDefaultMaxVelocity = AimMaxVelocity;
		AimDefaultAccelPerTick = AimAccelQuantum;
		AimDefaultVelocityDamping = AimVelocityDamping;
	}

	public void DoFudgeModeAim(int _valueToFudge)
	{
		
	}
	
	public void DoFudgeModeDrift(int _valueToFudge)
	{
		_valueToFudge += DriftRate;
		if (IsFudgingDegrees())
			_valueToFudge = ZMath.NormalizeAngle(_valueToFudge);
		CurrentValue = _valueToFudge;
	}
	
	public void DoFudgeModeAlign(int _valueToFudge)
	{
	    var oldProg = (int)AlignProgress;
		AlignProgress += AlignRate;
		var newProg = (int)AlignProgress;

		_valueToFudge += (newProg - oldProg);

		if (IsFudgingDegrees())
			_valueToFudge = ZMath.NormalizeAngle(_valueToFudge);
		if (++AlignCurTick >= AlignNumTicks)
			DirectiveType = EDirectiveType.Deactivated;

		CurrentValue = _valueToFudge;	
	}
	
	public void DoFudgeModeDecay(int _valueToFudge)
	{
		_valueToFudge = (int)Math.Round((double)_valueToFudge + AimVelocity);
		if (IsFudgingDegrees())
			_valueToFudge = ZMath.NormalizeAngle(_valueToFudge);
		double oldVelocity = AimVelocity;
		AimVelocity += (double)(AimVelocity > 0.0 ? -DecayRate : DecayRate);
		if ((oldVelocity > 0.0) != (AimVelocity > 0.0))
			DirectiveType = EDirectiveType.Deactivated;
		CurrentValue = DriftRate + _valueToFudge;		
	}
	
	public void DoFudgeModeSwing(int _phase256)
	{
		double twoPi = Math.PI * 2d;

		double R = (double)SwingRadius;

	//	this check isn't actually in the game
		if (R != 0.0) {
			double k      = 10.0 / R;			// stiffness
			double omega  = twoPi / 256.0;

	//		base angle from 0..255 phase
			double theta = _phase256 * omega;

	//		if the aim target changed, re-phase to the shifted point
			int dTarget = AimTarget - PreviousAimTarget;
			if (dTarget != 0) {
				double x = R * Math.Cos(theta);
				double y = R * Math.Sin(theta) + (double)dTarget;
				theta = Math.Atan2(y,x);
			};

	//		damped pendulum-like update
			double accel = -k * Math.Sin(theta) - SwingDamping * SwingPhaseRad;
			double delta = 0.5 * accel + SwingPhaseRad + theta;

			SwingPhaseRad += delta;

	//		export per-tick delta back in 0..255 units (round half away from zero)
			double to256 = 256.0 / twoPi;
			CurrentValue = (int)Math.Round(delta * to256);
		} else CurrentValue = 0;		
	}
	
	public void Update()
	{
		int valueToFudge = 100; //to do: change
		
		switch (DirectiveType)
		{
			case (EDirectiveType.Aim):
				DoFudgeModeAim(valueToFudge);
				break;
			case (EDirectiveType.Drift):
				DoFudgeModeDrift(valueToFudge);
				break;
			case (EDirectiveType.Align):
				DoFudgeModeAlign(valueToFudge);
				break;
			case (EDirectiveType.Decay):
				DoFudgeModeDecay(valueToFudge);
				break;
			case (EDirectiveType.SwingFalse):
				DoFudgeModeSwing(valueToFudge);
				break;
			case (EDirectiveType.SwingTrue):
				DoFudgeModeSwing(valueToFudge);
				break;
		}
	}
	
}