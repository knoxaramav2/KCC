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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IKCCListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public partial class KCCBaseListener : IKCCListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.rules"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRules([NotNull] KCCParser.RulesContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.rules"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRules([NotNull] KCCParser.RulesContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.asm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAsm([NotNull] KCCParser.AsmContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.asm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAsm([NotNull] KCCParser.AsmContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock([NotNull] KCCParser.BlockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock([NotNull] KCCParser.BlockContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.class"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterClass([NotNull] KCCParser.ClassContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.class"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitClass([NotNull] KCCParser.ClassContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] KCCParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] KCCParser.StatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpression([NotNull] KCCParser.ExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpression([NotNull] KCCParser.ExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.call"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCall([NotNull] KCCParser.CallContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.call"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCall([NotNull] KCCParser.CallContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction([NotNull] KCCParser.FunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction([NotNull] KCCParser.FunctionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.function_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction_decl([NotNull] KCCParser.Function_declContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.function_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction_decl([NotNull] KCCParser.Function_declContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.function_call"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction_call([NotNull] KCCParser.Function_callContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.function_call"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction_call([NotNull] KCCParser.Function_callContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.assign_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssign_expr([NotNull] KCCParser.Assign_exprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.assign_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssign_expr([NotNull] KCCParser.Assign_exprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.var_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterVar_decl([NotNull] KCCParser.Var_declContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.var_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitVar_decl([NotNull] KCCParser.Var_declContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.group"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGroup([NotNull] KCCParser.GroupContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.group"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGroup([NotNull] KCCParser.GroupContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.asm_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAsm_id([NotNull] KCCParser.Asm_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.asm_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAsm_id([NotNull] KCCParser.Asm_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.value_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterValue_id([NotNull] KCCParser.Value_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.value_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitValue_id([NotNull] KCCParser.Value_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.symbol_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSymbol_id([NotNull] KCCParser.Symbol_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.symbol_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSymbol_id([NotNull] KCCParser.Symbol_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.logic_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLogic_id([NotNull] KCCParser.Logic_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.logic_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLogic_id([NotNull] KCCParser.Logic_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.identifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIdentifier([NotNull] KCCParser.IdentifierContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.identifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIdentifier([NotNull] KCCParser.IdentifierContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.control_block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterControl_block([NotNull] KCCParser.Control_blockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.control_block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitControl_block([NotNull] KCCParser.Control_blockContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.control_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterControl_id([NotNull] KCCParser.Control_idContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.control_id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitControl_id([NotNull] KCCParser.Control_idContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.unary_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_ops([NotNull] KCCParser.Unary_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.unary_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_ops([NotNull] KCCParser.Unary_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.binary_arith_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary_arith_ops([NotNull] KCCParser.Binary_arith_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.binary_arith_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary_arith_ops([NotNull] KCCParser.Binary_arith_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.assign_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssign_ops([NotNull] KCCParser.Assign_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.assign_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssign_ops([NotNull] KCCParser.Assign_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.binary_logic_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary_logic_ops([NotNull] KCCParser.Binary_logic_opsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.binary_logic_ops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary_logic_ops([NotNull] KCCParser.Binary_logic_opsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.binary"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary([NotNull] KCCParser.BinaryContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.binary"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary([NotNull] KCCParser.BinaryContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.bool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBool([NotNull] KCCParser.BoolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.bool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBool([NotNull] KCCParser.BoolContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.arith_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArith_expr([NotNull] KCCParser.Arith_exprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.arith_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArith_expr([NotNull] KCCParser.Arith_exprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.type_specifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterType_specifier([NotNull] KCCParser.Type_specifierContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.type_specifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitType_specifier([NotNull] KCCParser.Type_specifierContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.array"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArray([NotNull] KCCParser.ArrayContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.array"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArray([NotNull] KCCParser.ArrayContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.assembly"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssembly([NotNull] KCCParser.AssemblyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.assembly"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssembly([NotNull] KCCParser.AssemblyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="KCCParser.semi"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSemi([NotNull] KCCParser.SemiContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="KCCParser.semi"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSemi([NotNull] KCCParser.SemiContext context) { }

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
