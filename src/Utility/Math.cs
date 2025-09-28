using Godot;
using System;
using System.Collections.Generic;

namespace OpenPetz
{
	public static class ZMath
	{
		// -32,768 to +32,767 // TO DO
		
		//  linearly interpolate an integer value between min and max based on percent (0-100)
		public static int MapValue(int _min, int _max, int _percent) {
			return _min + (_percent * (_max - _min)) / 100;
		}


		//	normalize an angle to the range -128 to 128
		public static int NormalizeAngle(int _a) {
			while (_a < -128)	_a += 256;
			while (_a > 128)		_a -= 256;
			return _a;
		}


		//	return the difference between two angles, in the range 0 to 256
		public static int DiffAngle(int _a, int _b) {
			while (_b < _a)		_b += 256;
			return _b - _a;
		}


		//	return the smallest difference between two angles, in the range -128 to 128
		public static int MinDiffAngle(int _a, int _b) {
			int diff = DiffAngle(_a, _b);
			if (diff <= 128)	return diff;
			else				return diff - 256;
		}
	}
}


