grammar kcc;

/***Lexer Rules***/

//control operators
IF          : 'if';
ELSE        : 'else';
WHILE       : 'while';
FOREACH     : 'foreach';
CONTINUE    : 'continue';
BREAK       : 'break';
RETURN      : 'return';
GOTO        : 'goto';

//arithmetic operators
SET             : '=';
ADD             : '+';
SUBTRACT        : '-';
MULTIPLY        : '*';
DIVIDE          : '/';
EXPONENT        : '**';
MODULO          : '%' ;
SET_SUM         : '+=';
SET_DIFFERENCE  : '-=';
SET_PRODUCT     : '*=';
SET_QUOTIENT    : '/=';
INCREMENT       : '++';
DECRIMENT       : '--';

//logical operators
LOGIC_OR        : '||';
LOGIC_AND       : '&&';
LOGIC_NOT       : '!';
LOGIC_NAND      : '!&';
LOGIC_NOR       : '!|';
LOGIC_XOR       : '^|';
LOGIC_XNOR      : '^!';

//bitwise operators
BITWISE_AND     : '&';
BITWISE_OR      : '|';
BITWISE_INVERT  : '^' ;

//special declaratives
CLASS           : 'class';

//enclosures
L_BRACKET       : '{';
R_BRACKET       : '}';
L_PARANTH       : '(';
R_PARANTH       : ')';
L_BRACE         : '{';
R_BRACE         : '}';

//other operators
REFERENCE       : '\@';
LINE_COMMENT    : '#' ~[\r\n|\n]* ->channel(HIDDEN);
BLOCK_COMMENT   : '#*' .*? '*#'->channel(HIDDEN);

//general identifies
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : [a-zA-Z_][a-zA-Z_0-9]*;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

/***Parser Rules***/



