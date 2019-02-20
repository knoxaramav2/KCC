// Generated from E:/Dev/C#/KCC/Antlr\kcc.g4 by ANTLR 4.7.2
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link kccParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface kccVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link kccParser#chat}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitChat(kccParser.ChatContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#line}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLine(kccParser.LineContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#message}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMessage(kccParser.MessageContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#name}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitName(kccParser.NameContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#command}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCommand(kccParser.CommandContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#emoticon}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEmoticon(kccParser.EmoticonContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#link}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLink(kccParser.LinkContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#color}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitColor(kccParser.ColorContext ctx);
	/**
	 * Visit a parse tree produced by {@link kccParser#mention}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMention(kccParser.MentionContext ctx);
}