//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:/Dev/C#/KCC/Antlr\kcc.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419


using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IkccListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public partial class kccBaseListener : IkccListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.rules"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRules([NotNull] kccParser.RulesContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.rules"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRules([NotNull] kccParser.RulesContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock([NotNull] kccParser.BlockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock([NotNull] kccParser.BlockContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.class"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterClass([NotNull] kccParser.ClassContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.class"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitClass([NotNull] kccParser.ClassContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] kccParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] kccParser.StatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpression([NotNull] kccParser.ExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpression([NotNull] kccParser.ExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.group"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGroup([NotNull] kccParser.GroupContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.group"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGroup([NotNull] kccParser.GroupContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterId([NotNull] kccParser.IdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitId([NotNull] kccParser.IdContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.logic_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLogic_id([NotNull] kccParser.Logic_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.logic_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLogic_id([NotNull] kccParser.Logic_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.control_block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterControl_block([NotNull] kccParser.Control_blockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.control_block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitControl_block([NotNull] kccParser.Control_blockContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.control_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterControl_id([NotNull] kccParser.Control_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.control_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitControl_id([NotNull] kccParser.Control_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.unary_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_ops([NotNull] kccParser.Unary_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.unary_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_ops([NotNull] kccParser.Unary_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.binary_arith_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary_arith_ops([NotNull] kccParser.Binary_arith_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.binary_arith_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary_arith_ops([NotNull] kccParser.Binary_arith_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.binary_logic_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary_logic_ops([NotNull] kccParser.Binary_logic_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.binary_logic_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary_logic_ops([NotNull] kccParser.Binary_logic_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.binary"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary([NotNull] kccParser.BinaryContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.binary"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary([NotNull] kccParser.BinaryContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.bool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBool([NotNull] kccParser.BoolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.bool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBool([NotNull] kccParser.BoolContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="kccParser.arith_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArith_expr([NotNull] kccParser.Arith_exprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="kccParser.arith_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArith_expr([NotNull] kccParser.Arith_exprContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
