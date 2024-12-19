using Godot;
using System;

// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using System.Collections.Generic;

namespace Kaitai
{
	/*public partial class Scp : KaitaiStruct
	{
		public static Scp FromFile(string fileName)
		{
			return new Scp(new KaitaiStream(fileName));
		}


		public enum Verbs
		{
			Startpos = 1073741824,
			Actiondone0 = 1073741825,
			Actionstart1 = 1073741826,
			Alignscripts0 = 1073741827,
			Alignballtoptsetup3 = 1073741828,
			Alignballtoptgo0 = 1073741829,
			Alignballtoptstop0 = 1073741830,
			Alignfudgeballtoptsetup2 = 1073741831,
			Blendtoframe3 = 1073741832,
			Cuecode1 = 1073741833,
			Debugcode1 = 1073741834,
			Disablefudgeaim1 = 1073741835,
			Disableswing0 = 1073741836,
			Donetalking0 = 1073741837,
			Donetalking1 = 1073741838,
			Enablefudgeaim1 = 1073741839,
			Enableswing1 = 1073741840,
			Endblock0 = 1073741841,
			Endblockalign0 = 1073741842,
			Gluescripts0 = 1073741843,
			Gluescriptsball1 = 1073741844,
			Interruptionsdisable0 = 1073741845,
			Interruptionsenable0 = 1073741846,
			Lookatlocation2 = 1073741847,
			Lookatlocationeyes2 = 1073741848,
			Lookatrandompt0 = 1073741849,
			Lookatrandompteyes0 = 1073741850,
			Lookatsprite1 = 1073741851,
			Lookatspriteeyes1 = 1073741852,
			Lookatuser0 = 1073741853,
			Lookforward0 = 1073741854,
			Lookforwardeyes0 = 1073741855,
			Null0 = 1073741856,
			Null1 = 1073741857,
			Null2 = 1073741858,
			Null3 = 1073741859,
			Null4 = 1073741860,
			Null5 = 1073741861,
			Null6 = 1073741862,
			Playaction2 = 1073741863,
			Playactionrecall2 = 1073741864,
			Playactionstore2 = 1073741865,
			Playlayeredaction3 = 1073741866,
			Playlayeredaction4 = 1073741867,
			Playlayeredactioncallback5 = 1073741868,
			Playlayeredactioncallback6 = 1073741869,
			Playtransitiontoaction1 = 1073741870,
			Rand2 = 1073741871,
			Resetfudger1 = 1073741872,
			Resumefudging1 = 1073741873,
			Resumelayerrotation1 = 1073741874,
			Sequence2 = 1073741875,
			Sequencetoend1 = 1073741876,
			Sequencetostart1 = 1073741877,
			Setblendoffset3 = 1073741878,
			Setfudgeaimdefaults5 = 1073741879,
			Setfudgerdrift2 = 1073741880,
			Setfudgerrate2 = 1073741881,
			Setfudgertarget2 = 1073741882,
			Setfudgernow2 = 1073741883,
			Setheadtrackacuteness = 1073741884,
			Setheadtrackmode1 = 1073741885,
			Setlayeredbaseframe2 = 1073741886,
			Setmotionscale1 = 1073741887,
			Setmotionscale2 = 1073741888,
			Setreverseheadtrack1 = 1073741889,
			Setrotationpivotball1 = 1073741890,
			Soundemptyqueue0 = 1073741891,
			Soundloop1 = 1073741892,
			Soundsetpan1 = 1073741893,
			Soundplay1 = 1073741894,
			Soundplay2 = 1073741895,
			Soundplay3 = 1073741896,
			Soundplay4 = 1073741897,
			Soundplay5 = 1073741898,
			Soundqueue1 = 1073741899,
			Soundqueue2 = 1073741900,
			Soundqueue3 = 1073741901,
			Soundqueue4 = 1073741902,
			Soundqueue5 = 1073741903,
			Soundsetdefltvocpitch1 = 1073741904,
			Soundsetpitch1 = 1073741905,
			Soundsetvolume1 = 1073741906,
			Soundstop0 = 1073741907,
			Startlistening0 = 1073741908,
			Startblockloop1 = 1073741909,
			Startblockcallback2 = 1073741910,
			Startblockchance1 = 1073741911,
			Startblockdialogsynch0 = 1073741912,
			Startblockelse0 = 1073741913,
			Startblocklisten0 = 1073741914,
			Stopfudging1 = 1073741915,
			Suspendfudging1 = 1073741916,
			Suspendlayerrotation1 = 1073741917,
			Tailsetneutral1 = 1073741918,
			Tailsetrestoreneutral1 = 1073741919,
			Tailsetwag1 = 1073741920,
			Targetsprite4 = 1073741921,
			Throwme0 = 1073741922,
			Endpos = 1073741923,
		}

		public enum Cuecodes
		{
			Introdone = 0,
			Intronotdone = 1,
			Grabobject = 2,
			Releaseobject = 3,
			Lookatinterest = 4,
			Lookatinteractor = 5,
			Useobject = 6,
			Swatobject = 7,
			Gnawobject = 8,
			Scratchobject = 9,
			Dighole = 10,
			Fillhole = 11,
			Trip = 12,
			Snoreactive = 13,
			Snorein = 14,
			Snoreout = 15,
			Snoredream = 16,
			Atefood = 17,
			Scare = 18,
			Stephandl = 19,
			Stephandr = 20,
			Stepfootl = 21,
			Stepfootr = 22,
			Stomphandl = 23,
			Stomphandr = 24,
			Stompfootl = 25,
			Stompfootr = 26,
			Land = 27,
			Scuff = 28,
			Showlinez = 29,
			Hidelinez = 30,
			None = 31,
			Cursor = 32,
			Shelf = 33,
			Otherpet = 34,
			Focussprite1 = 35,
			Focussprite2 = 36,
			Focussprite3 = 37,
			Percentchance = 38,
			Ifsoundadult = 39,
			Isadoptionkit = 40,
		}

		public enum Fudgers
		{
			Rotation = 0,
			Roll = 1,
			Tilt = 2,
			Headrotation = 3,
			Headtilt = 4,
			Headcock = 5,
			Reyelidheight = 6,
			Leyelidheight = 7,
			Reyelidtilt = 8,
			Leyelidtilt = 9,
			Eyetargetx = 10,
			Eyetargety = 11,
			Xtrans = 12,
			Ytrans = 13,
			Scalex = 14,
			Scaley = 15,
			Scalez = 16,
			Ballscale = 17,
			Masterscale = 18,
			Reyesizexxx = 19,
			Leyesizexxx = 20,
			Rarmsizexxx = 21,
			Larmsizexxx = 22,
			Rlegsizexxx = 23,
			Llegsizexxx = 24,
			Rhandsizexxx = 25,
			Lhandsizexxx = 26,
			Rfootsizexxx = 27,
			Lfootsizexxx = 28,
			Headsizexxx = 29,
			Bodyextend = 30,
			Frontlegextend = 31,
			Hindlegextend = 32,
			Faceextend = 33,
			Headenlarge = 34,
			Headenlargebalance = 35,
			Earextend = 36,
			Footenlarge = 37,
			Footenlargebalance = 38,
			Prerotation = 39,
			Preroll = 40,
			Addballz0 = 41,
			Addballzflower1 = 42,
			Addballzheart2 = 43,
			Addballzquestion3 = 44,
			Addballzexclamation4 = 45,
			Addballzlightbulboff5 = 46,
			Addballzstickman6 = 47,
			Addballzcrossbones7 = 48,
			Addballzlightning8 = 49,
			Addballzbrokenheart9 = 50,
			Addballzsnowone10 = 51,
			Addballzsnowtwo11 = 52,
			Addballzsnowthree12 = 53,
			Addballzlightbulbon13 = 54,
			Addballztears14 = 55,
			Addballzoddlove15 = 56,
			Morph = 57,
			Botheyelidheights = 58,
			Botheyelidtilts = 59,
			Botheyesizes = 60,
			Botharmsizes = 61,
			Bothlegsizes = 62,
			Rightlimbsizes = 63,
			Leftlimbsizes = 64,
			Alllimbsizes = 65,
			Bothhandsizes = 66,
			Bothfeetsizes = 67,
			Rightdigitsizes = 68,
			Leftdigitsizes = 69,
			Alldigitsizes = 70,
			Allfudgers = 71,
		}
		public Scp(KaitaiStream p__io, KaitaiStruct p__parent = null, Scp p__root = null) : base(p__io)
		{
			m_parent = p__parent;
			m_root = p__root ?? this;
			_read();
		}
		private void _read()
		{
			_intro = m_io.ReadBytes(62);
			_totalStates = m_io.ReadU4le();
			_startState = m_io.ReadU4le();
			_unknown = m_io.ReadU8le();
			_actioncount = m_io.ReadU4le();
			_actions = new List<Action>();
			for (var i = 0; i < Actioncount; i++)
			{
				_actions.Add(new Action(m_io, this, m_root));
			}
			_sizeOfScripts = m_io.ReadU4le();
			__raw_scripts = m_io.ReadBytes((SizeOfScripts * 4));
			var io___raw_scripts = new KaitaiStream(__raw_scripts);
			_scripts = new Scripts(io___raw_scripts, this, m_root);
			_ender = m_io.ReadBytes(62);
		}
		public partial class Action : KaitaiStruct
		{
			public static Action FromFile(string fileName)
			{
				return new Action(new KaitaiStream(fileName));
			}

			public Action(KaitaiStream p__io, Scp p__parent = null, Scp p__root = null) : base(p__io)
			{
				m_parent = p__parent;
				m_root = p__root;
				f_scripts = false;
				_read();
			}
			private void _read()
			{
				_actionId = m_io.ReadU4le();
				_scriptCount = m_io.ReadU4le();
				_startState = m_io.ReadU4le();
				_endState = m_io.ReadU4le();
				_loopModifier = m_io.ReadU4le();
				_changesState = m_io.ReadU2le();
				_glueBall = m_io.ReadU2le();
				_layerMask = m_io.ReadU2le();
				_unknown4 = m_io.ReadU2le();
				_startOffset = m_io.ReadU4le();
			}
			private bool f_scripts;
			private List<Script> _scripts;
			public List<Script> Scripts
			{
				get
				{
					if (f_scripts)
						return _scripts;
					KaitaiStream io = M_Root.M_Io;
					long _pos = io.Pos;
					io.Seek(((86 + (M_Root.Actioncount * 32)) + (StartOffset * 4)));
					_scripts = new List<Script>();
					for (var i = 0; i < ScriptCount; i++)
					{
						_scripts.Add(new Script(io, this, m_root));
					}
					io.Seek(_pos);
					f_scripts = true;
					return _scripts;
				}
			}
			private uint _actionId;
			private uint _scriptCount;
			private uint _startState;
			private uint _endState;
			private uint _loopModifier;
			private ushort _changesState;
			private ushort _glueBall;
			private ushort _layerMask;
			private ushort _unknown4;
			private uint _startOffset;
			private Scp m_root;
			private Scp m_parent;
			public uint ActionId { get { return _actionId; } }
			public uint ScriptCount { get { return _scriptCount; } }
			public uint StartState { get { return _startState; } }
			public uint EndState { get { return _endState; } }
			public uint LoopModifier { get { return _loopModifier; } }
			public ushort ChangesState { get { return _changesState; } }
			public ushort GlueBall { get { return _glueBall; } }
			public ushort LayerMask { get { return _layerMask; } }
			public ushort Unknown4 { get { return _unknown4; } }
			public uint StartOffset { get { return _startOffset; } }
			public Scp M_Root { get { return m_root; } }
			public Scp M_Parent { get { return m_parent; } }
		}
		public partial class Scripts : KaitaiStruct
		{
			public static Scripts FromFile(string fileName)
			{
				return new Scripts(new KaitaiStream(fileName));
			}

			public Scripts(KaitaiStream p__io, Scp p__parent = null, Scp p__root = null) : base(p__io)
			{
				m_parent = p__parent;
				m_root = p__root;
				_read();
			}
			private void _read()
			{
				_scripts = new List<Script>();
				{
					var i = 0;
					while (!m_io.IsEof) {
						_scripts.Add(new Script(m_io, this, m_root));
						i++;
					}
				}
			}
			private List<Script> _scripts;
			private Scp m_root;
			private Scp m_parent;
			public List<Script> Scripts { get { return _scripts; } }
			public Scp M_Root { get { return m_root; } }
			public Scp M_Parent { get { return m_parent; } }
		}
		public partial class Script : KaitaiStruct
		{
			public static Script FromFile(string fileName)
			{
				return new Script(new KaitaiStream(fileName));
			}

			public Script(KaitaiStream p__io, KaitaiStruct p__parent = null, Scp p__root = null) : base(p__io)
			{
				m_parent = p__parent;
				m_root = p__root;
				_read();
			}
			private void _read()
			{
				_size = m_io.ReadU4le();
				_command = new List<Verbs>();
				for (var i = 0; i < (Size - 1); i++)
				{
					_command.Add(((Scp.Verbs) m_io.ReadS4le()));
				}
			}
			private uint _size;
			private List<Verbs> _command;
			private Scp m_root;
			private KaitaiStruct m_parent;
			public uint Size { get { return _size; } }
			public List<Verbs> Command { get { return _command; } }
			public Scp M_Root { get { return m_root; } }
			public KaitaiStruct M_Parent { get { return m_parent; } }
		}
		private byte[] _intro;
		private uint _totalStates;
		private uint _startState;
		private ulong _unknown;
		private uint _actioncount;
		private List<Action> _actions;
		private uint _sizeOfScripts;
		private Scripts _scripts;
		private byte[] _ender;
		private Scp m_root;
		private KaitaiStruct m_parent;
		private byte[] __raw_scripts;
		public byte[] Intro { get { return _intro; } }
		public uint TotalStates { get { return _totalStates; } }
		public uint StartState { get { return _startState; } }
		public ulong Unknown { get { return _unknown; } }
		public uint Actioncount { get { return _actioncount; } }
		public List<Action> Actions { get { return _actions; } }
		public uint SizeOfScripts { get { return _sizeOfScripts; } }
		public Scripts Scripts { get { return _scripts; } }
		public byte[] Ender { get { return _ender; } }
		public Scp M_Root { get { return m_root; } }
		public KaitaiStruct M_Parent { get { return m_parent; } }
		public byte[] M_RawScripts { get { return __raw_scripts; } } 
	} */
}
