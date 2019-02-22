grammar kcc;

options{
    language = CSharp;
}


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

//special
CLASS           : 'class';
THIS            : 'this';
TRUE            : 'true';
FALSE           : 'false';

//binary other
JOINT           : ':';

//enclosures
L_BRACKET       : '[';
R_BRACKET       : ']';
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


//rules           : class * EOF;
trules          : cmpnd_expr * EOF;

block           : L_BRACE (~R_BRACE|class|block)* R_BRACE;
class           : CLASS id class|block;

cmpnd_expr      : (smpl_expr) (binary_logic_ops|binary_arith_ops) (smpl_expr|cmpnd_expr) SEMI;
smpl_expr       : unary_ops?IDENTIFIER unary_ops?;

id              : DECIMAL | IDENTIFIER | logic_id;
logic_id        : (TRUE | FALSE);

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

unary_ops       : INCREMENT
                | DECRIMENT
                | LOGIC_NOT
                ;

binary_logic_ops: LOGIC_OR
                | LOGIC_AND
                | LOGIC_NOT
                | LOGIC_NAND
                | LOGIC_NOR
                | LOGIC_XOR
                | LOGIC_XNOR
                | GTR
                | LSS
                | EQU
                | GTR_EQU
                | LSS_EQU
                ;

arith_expr      :arith_expr binary_arith_ops arith_expr
                | L_PARANTH arith_expr R_PARANTH
                | id
                | SUBTRACT id
                | (INCREMENT|DECRIMENT) id;
logic_expr      :;

