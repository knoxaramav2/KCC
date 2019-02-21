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

//comparisons
GTR             : '>';
LSS             : '<';
EQU             : '==';
GTR_EQU         : '>=';
LSS_EQU         : '<=';

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
//REFERENCE       : '\@'; TODO FIX
LINE_COMMENT    : '#' ~[\r\n]* ->channel(HIDDEN);
BLOCK_COMMENT   : '#*' .*? '*#'->channel(HIDDEN);

//general identifies
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : [a-zA-Z_][a-zA-Z_0-9]*;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

/***Parser Rules***/

rules           : body_expr* EOF;

body_expr       : (entity group)?L_BRACKET . R_BRACKET;
group           : L_PARANTH expression R_PARANTH;
expression      : unary_ops?entity binary_arith_ops unary_ops?entity;

control_block   : IF
                | ELSE
                | WHILE
                | FOREACH
                ;

control_id      : CONTINUE
                | BREAK
                | RETURN
                | GOTO;

binary_arith_ops: SET
                | ADD
                | SUBTRACT
                | MULTIPLY
                | DIVIDE
                | EXPONENT
                | MODULO
                | SET_SUM
                | SET_DIFFERENCE
                | SET_PRODUCT
                | SET_QUOTIENT
                ;

binary_logic_ops: LOGIC_OR
                | LOGIC_AND
                | LOGIC_NOT
                | LOGIC_NAND
                | LOGIC_NOR
                | LOGIC_XOR
                | LOGIC_XNOR
                ;

unary_ops       : INCREMENT
                | DECRIMENT
                | LOGIC_NOT
                ;

entity          : DECIMAL | IDENTIFIER;

