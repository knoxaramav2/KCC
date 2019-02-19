grammar ParserExample;

//https://tomassetti.me/getting-started-with-antlr-in-csharp/
//https://www.antlr.org/
//https://tomassetti.me/antlr-mega-tutorial/

/*
Parser rules
*/

chat	: line line EOF;
line	: name SAYS opinion NEWLINE;
name	: WORD;
opinion	: TEXT;

//Lexer rules

fragment A	:('A'|'a');
fragment S	:('S'|'s');
fragment Y	:('Y'|'y');

fragment LOWERCASE : [a-z];
fragment UPPERCASE : [A-Z];

SAYS				: S A Y S;
WORD				: (LOWERCASE | UPPERCASE)+;
TEXT				: '"' .*? '"' ;
WHITESPACE			: (' '|'\t')+ -> skip;
NEWLINE				: ('\r'? '\n' | '\r')+ ;



