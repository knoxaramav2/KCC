//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:/Dev/C#/KCC/Antlr\KCC.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="KCCParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IKCCListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.rules"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRules([NotNull] KCCParser.RulesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.rules"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRules([NotNull] KCCParser.RulesContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.asm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAsm([NotNull] KCCParser.AsmContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.asm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAsm([NotNull] KCCParser.AsmContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] KCCParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] KCCParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClass([NotNull] KCCParser.ClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClass([NotNull] KCCParser.ClassContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] KCCParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] KCCParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] KCCParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] KCCParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCall([NotNull] KCCParser.CallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCall([NotNull] KCCParser.CallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction([NotNull] KCCParser.FunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction([NotNull] KCCParser.FunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGroup([NotNull] KCCParser.GroupContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGroup([NotNull] KCCParser.GroupContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.asm_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAsm_id([NotNull] KCCParser.Asm_idContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.asm_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAsm_id([NotNull] KCCParser.Asm_idContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterId([NotNull] KCCParser.IdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitId([NotNull] KCCParser.IdContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.symbol_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSymbol_id([NotNull] KCCParser.Symbol_idContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.symbol_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSymbol_id([NotNull] KCCParser.Symbol_idContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.logic_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogic_id([NotNull] KCCParser.Logic_idContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.logic_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogic_id([NotNull] KCCParser.Logic_idContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.control_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterControl_block([NotNull] KCCParser.Control_blockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.control_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitControl_block([NotNull] KCCParser.Control_blockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.control_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterControl_id([NotNull] KCCParser.Control_idContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.control_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitControl_id([NotNull] KCCParser.Control_idContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.unary_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnary_ops([NotNull] KCCParser.Unary_opsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.unary_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnary_ops([NotNull] KCCParser.Unary_opsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.binary_arith_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinary_arith_ops([NotNull] KCCParser.Binary_arith_opsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.binary_arith_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinary_arith_ops([NotNull] KCCParser.Binary_arith_opsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.binary_logic_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinary_logic_ops([NotNull] KCCParser.Binary_logic_opsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.binary_logic_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinary_logic_ops([NotNull] KCCParser.Binary_logic_opsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.binary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinary([NotNull] KCCParser.BinaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.binary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinary([NotNull] KCCParser.BinaryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBool([NotNull] KCCParser.BoolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBool([NotNull] KCCParser.BoolContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.arith_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArith_expr([NotNull] KCCParser.Arith_exprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.arith_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArith_expr([NotNull] KCCParser.Arith_exprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.typeSpecifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeSpecifier([NotNull] KCCParser.TypeSpecifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.typeSpecifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeSpecifier([NotNull] KCCParser.TypeSpecifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.array"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArray([NotNull] KCCParser.ArrayContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.array"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArray([NotNull] KCCParser.ArrayContext context);
}
