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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="kccParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IkccVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.rules"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRules([NotNull] kccParser.RulesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] kccParser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClass([NotNull] kccParser.ClassContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] kccParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] kccParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroup([NotNull] kccParser.GroupContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitId([NotNull] kccParser.IdContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.logic_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogic_id([NotNull] kccParser.Logic_idContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.control_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitControl_block([NotNull] kccParser.Control_blockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.control_id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitControl_id([NotNull] kccParser.Control_idContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.unary_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnary_ops([NotNull] kccParser.Unary_opsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.binary_arith_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinary_arith_ops([NotNull] kccParser.Binary_arith_opsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.binary_logic_ops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinary_logic_ops([NotNull] kccParser.Binary_logic_opsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.binary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinary([NotNull] kccParser.BinaryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBool([NotNull] kccParser.BoolContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="kccParser.arith_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArith_expr([NotNull] kccParser.Arith_exprContext context);
}
