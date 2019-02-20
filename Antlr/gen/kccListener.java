// Generated from E:/Dev/C#/KCC/Antlr\kcc.g4 by ANTLR 4.7.2
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link kccParser}.
 */
public interface kccListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link kccParser#chat}.
	 * @param ctx the parse tree
	 */
	void enterChat(kccParser.ChatContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#chat}.
	 * @param ctx the parse tree
	 */
	void exitChat(kccParser.ChatContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#line}.
	 * @param ctx the parse tree
	 */
	void enterLine(kccParser.LineContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#line}.
	 * @param ctx the parse tree
	 */
	void exitLine(kccParser.LineContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#message}.
	 * @param ctx the parse tree
	 */
	void enterMessage(kccParser.MessageContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#message}.
	 * @param ctx the parse tree
	 */
	void exitMessage(kccParser.MessageContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#name}.
	 * @param ctx the parse tree
	 */
	void enterName(kccParser.NameContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#name}.
	 * @param ctx the parse tree
	 */
	void exitName(kccParser.NameContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#command}.
	 * @param ctx the parse tree
	 */
	void enterCommand(kccParser.CommandContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#command}.
	 * @param ctx the parse tree
	 */
	void exitCommand(kccParser.CommandContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#emoticon}.
	 * @param ctx the parse tree
	 */
	void enterEmoticon(kccParser.EmoticonContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#emoticon}.
	 * @param ctx the parse tree
	 */
	void exitEmoticon(kccParser.EmoticonContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#link}.
	 * @param ctx the parse tree
	 */
	void enterLink(kccParser.LinkContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#link}.
	 * @param ctx the parse tree
	 */
	void exitLink(kccParser.LinkContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#color}.
	 * @param ctx the parse tree
	 */
	void enterColor(kccParser.ColorContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#color}.
	 * @param ctx the parse tree
	 */
	void exitColor(kccParser.ColorContext ctx);
	/**
	 * Enter a parse tree produced by {@link kccParser#mention}.
	 * @param ctx the parse tree
	 */
	void enterMention(kccParser.MentionContext ctx);
	/**
	 * Exit a parse tree produced by {@link kccParser#mention}.
	 * @param ctx the parse tree
	 */
	void exitMention(kccParser.MentionContext ctx);
}