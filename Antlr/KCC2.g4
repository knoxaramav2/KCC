grammar KCC2;

options{
    language= CSharp;
}

/*
high                : '*'|'/';
low                 : '+'|'-';

test                : test high test
                    | test low test
                    | '(' test ')'
                    | integer
                    ;*/

//rules               : assembly_decl +;

//constructs
//assembly_decl       : ASSEMBLY symbol_id asm_block;
//class_decl          : 'class' symbol_id class_block;
//function_decl        : symbol_id symbol_id fnc_header function_block;

//blocks
//asm_block           : L_BRACE ((class_decl|function_decl|~R_BRACE)+)? R_BRACE;
//class_block         : L_BRACE ((function_decl|var_decl|~R_BRACE)+)? R_BRACE;
//function_block      : L_BRACE ((var_decl|keyword_call|function_call|expression|~R_BRACE)+)? R_BRACE  ;


//groups
fnc_header          :   L_PARANTH ((var_decl)(','(var_decl)+)?|~R_PARANTH)? R_PARANTH;

call_group          :   L_PARANTH (((value_id|expression)(','(value_id|expression))+)?|~R_PARANTH)? R_PARANTH;
expr_group          :   ;


index_anyvalue      : index_string|index_integer|index_variable;
index_string        : '['string']';
index_integer       : '['integer']';
index_variable      : '[' ']';

//keywords
keywords            : 'return'
                    | 'print'
                    | 'exit'
                    ;


//operations
var_decl            : symbol_id (index_anyvalue)? symbol_id ('=' (expression|value_id))?;
keyword_call        : keywords (call_group|value_id)?;
function_call       : symbol_id call_group;

unary_expr          : rv_unary_ops #rv
                    | lv_unary_ops #lv
                    ;

//operations
/*
expression          : (unary_expr|value_id) binary_ops (unary_expr|value_id|expression)
                    | expression binary_ops (expression|value_id)
                    | unary_expr;*/

expression          : '-' expression
                    | expression oop_2 expression
                    | expression oop_3 expression
                    | expression oop_4 expression
                    | value_id
                    |
                    ;

oop_1               : symbol_id'.'symbol_id (('.'symbol_id)+)?
                    ;

oop_2               : symbol_id INCREMENT#post_inc
                    | symbol_id DECRIMENT#post_dec
                    | function_call#fnc_call
                    | index_anyvalue#index
                    ;

oop_3               : INCREMENT symbol_id#pre_inc
                    | DECRIMENT symbol_id#pre_dec
                    | NOT value_id#not
                    | BIT_INVERT value_id#invert
                    | '(' restric_id ')' value_id#cast
                    ;

oop_4               :  MULTIPLY value_id#product
                    |  DIVIDE value_id#quotient
                    |  MODULO value_id#remainder
                    |  EXPONENT value_id#power
                    ;

//resolution
rv_unary_ops        : symbol_id increment
                    | symbol_id decrement;

lv_unary_ops        : increment symbol_id
                    | decrement symbol_id
                    | unary_symb symbol_id;

increment           : INCREMENT;
decrement           : DECRIMENT;

binary_ops          : math_ops
                    | assign_ops
                    | logic_ops
                    | bitwise_ops
                    ;

unary_symb          : NOT | BIT_INVERT;

assign_ops          : SET
                    | SET_SUM
                    | SET_DIFFERENCE
                    | SET_PRODUCT
                    | SET_QUOTIENT
                    ;

math_ops            : ADD
                    | SUBTRACT
                    | MULTIPLY
                    | DIVIDE
                    | EXPONENT
                    | MODULO
                    ;

logic_ops           : AND
                    | NAND
                    | OR
                    | NOR
                    | XOR
                    | XNOR
                    ;

bitwise_ops         : BIT_AND
                    | BIT_OR
                    | BIT_XOR
                    | BIT_LSHIFT
                    | BIT_RSHIFT
                    ;

symbol_id           : IDENTIFIER (('.'IDENTIFIER)+)?;
restric_id          : IDENTIFIER;
value_id            : bool |string | char | decimal | integer | symbol_id;

bool                : 'true' | 'false';
decimal             : '-'?DECIMAL;
integer             : '-'?INTEGER;
string              : STRINGLIT;
char                : '\'' ALPHA '\'';

/*
 * Lexer Rules
 */

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

//logic operators
AND             : '&&';
NAND            : '!&';
OR              : '||';
NOR             : '!|';
XOR             : '|||';
XNOR            : '!||';
NOT             : '!';

//bitwise operators

BIT_AND         : '&';
BIT_OR          : '|';
BIT_INVERT      : '~';
BIT_LSHIFT      : '<<';
BIT_RSHIFT      : '>>';
BIT_XOR         : '^';

fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

fragment ALPHA  : [a-zA-Z_];

fragment NEWLINE : '\r'?'\n';


//enclosures
L_BRACKET       : '[';
R_BRACKET       : ']';
L_PARANTH       : '(';
R_PARANTH       : ')';
L_BRACE         : '{';
R_BRACE         : '}';

ASSEMBLY            : 'asm' | 'assembly';

//general identifiers
STRINGLIT       : STRINGLITPART '"';
STRINGLITPART   : '"' (~["\\\n] | '\\' (. | EOF))*;

INTEGER         : [0-9];
DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

