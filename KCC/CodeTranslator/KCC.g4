grammar KCC;

options{
    language = CSharp;
}



/***Parser Rules***/


rules           : asm+ EOF;
//rules          : statement * EOF;

asm             : assembly symbol_id block;
block           : L_BRACE (block|expression|~R_BRACE)+ R_BRACE

                | L_BRACE (class|function|block|expression|~R_BRACE)+ R_BRACE
                //: L_BRACE (class|~R_BRACE)+ R_BRACE
                //| L_BRACE (function|~R_BRACE)+ R_BRACE
                //| L_BRACE (block|~R_BRACE)+ R_BRACE
                //| L_BRACE (expression|~R_BRACE)+ R_BRACE
                ;

class           : CLASS symbol_id block
                | statement
                ;



statement       : call
                | expression* semi
                | IDENTIFIER IDENTIFIER group block semi
                ;
expression      : assign_expr
                //| symbol_id expression
                | var_decl
                | symbol_id
                | group
                | unary_ops expression
                | left=expression op=binary right=expression
                | bool
                | type_specifier symbol_id
                ;

call            : symbol_id group;
function        : function_decl
                | function_call
                ;
function_decl   : identifier function_call block;
function_call   : identifier group;
assign_expr     : identifier assign_ops (value_id|expression);
var_decl        : identifier assign_expr
                | identifier symbol_id
                ;
group           : L_PARANTH (expression|~R_PARANTH)? R_PARANTH;

asm_id          : (identifier DOT)*?IDENTIFIER;
value_id        : DECIMAL | IDENTIFIER | logic_id;
symbol_id       : (IDENTIFIER)(DOT IDENTIFIER)*?;//x or x.y.z
logic_id        : (TRUE | FALSE);
identifier      : type_specifier
                | symbol_id
                | control_block
                | RETURN
                | control_id
                ;



control_block   : IF
                | ELSE
                | WHILE
                | FOREACH
                ;

control_id      : CONTINUE
                | BREAK
                | GOTO
                | RETURN
                ;

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

assign_ops      : SET
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
                | value_id
                | SUBTRACT value_id
                | (INCREMENT|DECRIMENT) value_id;

type_specifier   : ('int'
                | 'sint'
                | 'double'
                | 'char'
                | 'byte'
                | 'string'
                | 'class'
                | 'bool')
                | type_specifier array;

array           : L_BRACKET value_id? R_BRACKET;

assembly        : 'asm' | 'assembly';

semi            : SEMI;

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

fragment ALPHA  : [a-zA-Z_]
                ;

//general identifies
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

