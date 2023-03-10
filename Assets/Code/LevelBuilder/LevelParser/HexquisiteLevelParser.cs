//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\HexquisiteLevel.g4 by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

#pragma warning disable CS3021
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public partial class HexquisiteLevelParser : Parser {
#pragma warning restore CS3021
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, INT=12, NEWLINE=13, WHITESPACE=14;
	public const int
		RULE_level = 0, RULE_version = 1, RULE_shapes = 2, RULE_keyshape = 3, 
		RULE_shape = 4, RULE_goals = 5, RULE_walls = 6, RULE_piece = 7, RULE_anchor = 8, 
		RULE_vector2int = 9;
	public static readonly string[] ruleNames = {
		"level", "version", "shapes", "keyshape", "shape", "goals", "walls", "piece", 
		"anchor", "vector2int"
	};

	private static readonly string[] _LiteralNames = {
		null, "'V'", "'K'", "'{'", "'}'", "'S'", "'G'", "'W'", "'A'", "'('", "','", 
		"')'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"INT", "NEWLINE", "WHITESPACE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "HexquisiteLevel.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static HexquisiteLevelParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public HexquisiteLevelParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public HexquisiteLevelParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class LevelContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public VersionContext version() {
			return GetRuleContext<VersionContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext vector2int() {
			return GetRuleContext<Vector2intContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public WallsContext walls() {
			return GetRuleContext<WallsContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public GoalsContext goals() {
			return GetRuleContext<GoalsContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ShapesContext shapes() {
			return GetRuleContext<ShapesContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(HexquisiteLevelParser.Eof, 0); }
		public LevelContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_level; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterLevel(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitLevel(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLevel(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public LevelContext level() {
		LevelContext _localctx = new LevelContext(Context, State);
		EnterRule(_localctx, 0, RULE_level);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 20;
			version();
			State = 21;
			vector2int();
			State = 22;
			walls();
			State = 23;
			goals();
			State = 24;
			shapes();
			State = 25;
			Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class VersionContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT() { return GetToken(HexquisiteLevelParser.INT, 0); }
		public VersionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_version; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterVersion(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitVersion(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVersion(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public VersionContext version() {
		VersionContext _localctx = new VersionContext(Context, State);
		EnterRule(_localctx, 2, RULE_version);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 27;
			Match(T__0);
			State = 28;
			Match(INT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ShapesContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ShapeContext[] shape() {
			return GetRuleContexts<ShapeContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ShapeContext shape(int i) {
			return GetRuleContext<ShapeContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public KeyshapeContext[] keyshape() {
			return GetRuleContexts<KeyshapeContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public KeyshapeContext keyshape(int i) {
			return GetRuleContext<KeyshapeContext>(i);
		}
		public ShapesContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_shapes; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterShapes(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitShapes(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitShapes(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ShapesContext shapes() {
		ShapesContext _localctx = new ShapesContext(Context, State);
		EnterRule(_localctx, 4, RULE_shapes);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 32;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				State = 32;
				ErrorHandler.Sync(this);
				switch (TokenStream.LA(1)) {
				case T__4:
					{
					State = 30;
					shape();
					}
					break;
				case T__1:
					{
					State = 31;
					keyshape();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				State = 34;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==T__1 || _la==T__4 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class KeyshapeContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public PieceContext[] piece() {
			return GetRuleContexts<PieceContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public PieceContext piece(int i) {
			return GetRuleContext<PieceContext>(i);
		}
		public KeyshapeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_keyshape; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterKeyshape(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitKeyshape(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitKeyshape(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public KeyshapeContext keyshape() {
		KeyshapeContext _localctx = new KeyshapeContext(Context, State);
		EnterRule(_localctx, 6, RULE_keyshape);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 36;
			Match(T__1);
			State = 37;
			Match(T__2);
			State = 39;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 38;
				piece();
				}
				}
				State = 41;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==T__7 || _la==T__8 );
			State = 43;
			Match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ShapeContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public PieceContext[] piece() {
			return GetRuleContexts<PieceContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public PieceContext piece(int i) {
			return GetRuleContext<PieceContext>(i);
		}
		public ShapeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_shape; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterShape(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitShape(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitShape(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ShapeContext shape() {
		ShapeContext _localctx = new ShapeContext(Context, State);
		EnterRule(_localctx, 8, RULE_shape);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 45;
			Match(T__4);
			State = 46;
			Match(T__2);
			State = 48;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 47;
				piece();
				}
				}
				State = 50;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==T__7 || _la==T__8 );
			State = 52;
			Match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class GoalsContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext[] vector2int() {
			return GetRuleContexts<Vector2intContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext vector2int(int i) {
			return GetRuleContext<Vector2intContext>(i);
		}
		public GoalsContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_goals; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterGoals(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitGoals(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitGoals(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public GoalsContext goals() {
		GoalsContext _localctx = new GoalsContext(Context, State);
		EnterRule(_localctx, 10, RULE_goals);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 54;
			Match(T__5);
			State = 55;
			Match(T__2);
			State = 57;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 56;
				vector2int();
				}
				}
				State = 59;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==T__8 );
			State = 61;
			Match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class WallsContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext[] vector2int() {
			return GetRuleContexts<Vector2intContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext vector2int(int i) {
			return GetRuleContext<Vector2intContext>(i);
		}
		public WallsContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_walls; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterWalls(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitWalls(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitWalls(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public WallsContext walls() {
		WallsContext _localctx = new WallsContext(Context, State);
		EnterRule(_localctx, 12, RULE_walls);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 63;
			Match(T__6);
			State = 64;
			Match(T__2);
			State = 68;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==T__8) {
				{
				{
				State = 65;
				vector2int();
				}
				}
				State = 70;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 71;
			Match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PieceContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public AnchorContext anchor() {
			return GetRuleContext<AnchorContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext vector2int() {
			return GetRuleContext<Vector2intContext>(0);
		}
		public PieceContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_piece; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterPiece(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitPiece(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPiece(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public PieceContext piece() {
		PieceContext _localctx = new PieceContext(Context, State);
		EnterRule(_localctx, 14, RULE_piece);
		try {
			State = 75;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__7:
				EnterOuterAlt(_localctx, 1);
				{
				State = 73;
				anchor();
				}
				break;
			case T__8:
				EnterOuterAlt(_localctx, 2);
				{
				State = 74;
				vector2int();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AnchorContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public Vector2intContext vector2int() {
			return GetRuleContext<Vector2intContext>(0);
		}
		public AnchorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_anchor; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterAnchor(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitAnchor(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAnchor(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AnchorContext anchor() {
		AnchorContext _localctx = new AnchorContext(Context, State);
		EnterRule(_localctx, 16, RULE_anchor);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 77;
			Match(T__7);
			State = 78;
			vector2int();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Vector2intContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] INT() { return GetTokens(HexquisiteLevelParser.INT); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT(int i) {
			return GetToken(HexquisiteLevelParser.INT, i);
		}
		public Vector2intContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_vector2int; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.EnterVector2int(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IHexquisiteLevelListener typedListener = listener as IHexquisiteLevelListener;
			if (typedListener != null) typedListener.ExitVector2int(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHexquisiteLevelVisitor<TResult> typedVisitor = visitor as IHexquisiteLevelVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVector2int(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Vector2intContext vector2int() {
		Vector2intContext _localctx = new Vector2intContext(Context, State);
		EnterRule(_localctx, 18, RULE_vector2int);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 80;
			Match(T__8);
			State = 81;
			Match(INT);
			State = 82;
			Match(T__9);
			State = 83;
			Match(INT);
			State = 84;
			Match(T__10);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,14,87,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,2,7,
		7,7,2,8,7,8,2,9,7,9,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,2,1,2,4,
		2,33,8,2,11,2,12,2,34,1,3,1,3,1,3,4,3,40,8,3,11,3,12,3,41,1,3,1,3,1,4,
		1,4,1,4,4,4,49,8,4,11,4,12,4,50,1,4,1,4,1,5,1,5,1,5,4,5,58,8,5,11,5,12,
		5,59,1,5,1,5,1,6,1,6,1,6,5,6,67,8,6,10,6,12,6,70,9,6,1,6,1,6,1,7,1,7,3,
		7,76,8,7,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,9,1,9,0,0,10,0,2,4,6,8,10,12,
		14,16,18,0,0,83,0,20,1,0,0,0,2,27,1,0,0,0,4,32,1,0,0,0,6,36,1,0,0,0,8,
		45,1,0,0,0,10,54,1,0,0,0,12,63,1,0,0,0,14,75,1,0,0,0,16,77,1,0,0,0,18,
		80,1,0,0,0,20,21,3,2,1,0,21,22,3,18,9,0,22,23,3,12,6,0,23,24,3,10,5,0,
		24,25,3,4,2,0,25,26,5,0,0,1,26,1,1,0,0,0,27,28,5,1,0,0,28,29,5,12,0,0,
		29,3,1,0,0,0,30,33,3,8,4,0,31,33,3,6,3,0,32,30,1,0,0,0,32,31,1,0,0,0,33,
		34,1,0,0,0,34,32,1,0,0,0,34,35,1,0,0,0,35,5,1,0,0,0,36,37,5,2,0,0,37,39,
		5,3,0,0,38,40,3,14,7,0,39,38,1,0,0,0,40,41,1,0,0,0,41,39,1,0,0,0,41,42,
		1,0,0,0,42,43,1,0,0,0,43,44,5,4,0,0,44,7,1,0,0,0,45,46,5,5,0,0,46,48,5,
		3,0,0,47,49,3,14,7,0,48,47,1,0,0,0,49,50,1,0,0,0,50,48,1,0,0,0,50,51,1,
		0,0,0,51,52,1,0,0,0,52,53,5,4,0,0,53,9,1,0,0,0,54,55,5,6,0,0,55,57,5,3,
		0,0,56,58,3,18,9,0,57,56,1,0,0,0,58,59,1,0,0,0,59,57,1,0,0,0,59,60,1,0,
		0,0,60,61,1,0,0,0,61,62,5,4,0,0,62,11,1,0,0,0,63,64,5,7,0,0,64,68,5,3,
		0,0,65,67,3,18,9,0,66,65,1,0,0,0,67,70,1,0,0,0,68,66,1,0,0,0,68,69,1,0,
		0,0,69,71,1,0,0,0,70,68,1,0,0,0,71,72,5,4,0,0,72,13,1,0,0,0,73,76,3,16,
		8,0,74,76,3,18,9,0,75,73,1,0,0,0,75,74,1,0,0,0,76,15,1,0,0,0,77,78,5,8,
		0,0,78,79,3,18,9,0,79,17,1,0,0,0,80,81,5,9,0,0,81,82,5,12,0,0,82,83,5,
		10,0,0,83,84,5,12,0,0,84,85,5,11,0,0,85,19,1,0,0,0,7,32,34,41,50,59,68,
		75
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
