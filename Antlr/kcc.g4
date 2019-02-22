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
BITWISE_INVERT  : '~' ;

//special
CLASS           : 'class';
THIS            : 'this';
TRUE            : 'true';
FALSE           : 'false';
ASSEMBLY        : 'assembly';

//binary other
JOINT           : ':';
DOT             : '.';

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


rules           : asm * EOF;
//trules          : statement * EOF;

asm             : ASSEMBLY IDENTIFIER block;
block           : L_BRACE (function|statement|block|~R_BRACE)* R_BRACE;
class           : CLASS IDENTIFIER block
                | statement
                ;

statement       : call
                | expression* SEMI
                | IDENTIFIER IDENTIFIER group block SEMI
                ;
expression      : symbol_id
                | group
                | unary_ops expression
                | left=expression op=binary right=expression
                | bool
                | id
                ;

call            : expression group SEMI;
function        : IDENTIFIER IDENTIFIER group block;
group           : L_PARANTH (expression|~R_PARANTH)? R_PARANTH;





asm_id          : (IDENTIFIER DOT)*?IDENTIFIER;
id              : DECIMAL | IDENTIFIER | logic_id;
symbol_id       : IDENTIFIER (DOT IDENTIFIER)*?;
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

unary_ops       : INCREMENT
                | DECRIMENT
                | LOGIC_NOT
                ;

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
                | GTR
                | LSS
                | EQU
                | GTR_EQU
                | LSS_EQU
                ;

binary          : binary_arith_ops | binary_logic_ops;

bool            : TRUE | FALSE;

arith_expr      :arith_expr binary_arith_ops arith_expr
                | L_PARANTH arith_expr R_PARANTH
                | id
                | SUBTRACT id
                | (INCREMENT|DECRIMENT) id;
