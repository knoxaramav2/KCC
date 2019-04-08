grammar KCC;

options{
    language= CSharp;
}


rules               : assembly +;

assembly            : ASSEMBLY symbol_id block_struct;
class               : 'class' symbol_id block_struct;

//enclosures
//restricted group options
decl_group          : L_PARANTH (var_proto_decl|~R_PARANTH) ((','var_proto_decl|~R_PARANTH)+)? R_PARANTH
                    | L_PARANTH (var_proto_decl|~R_PARANTH)? R_PARANTH;

call_group          : L_PARANTH (value_id|~R_PARANTH) ((','value_id|~R_PARANTH)+)? R_PARANTH
                    | L_PARANTH (value_id|~R_PARANTH)? R_PARANTH;

group               : L_PARANTH (instruction|~R_PARANTH) ((','instruction|~R_PARANTH)+)? R_PARANTH
                    | L_PARANTH (instruction|~R_PARANTH)? R_PARANTH;

//defines properties of a body
block_struct        : L_BRACE ((inst_body|~R_BRACE)+)? R_BRACE
                    ;
//contains executable instructions
block_exec          : L_BRACE ((instruction|~R_BRACE)+)? R_BRACE
                    ;

block               : L_BRACE ((.|~R_BRACE)+)? R_BRACE;

//allowed in definition
inst_body           : var_decl
                    | fnc_proto
                    | fnc_decl
                    | class;
//allowed in function body
inst_exec           : instruction
                    ;

instruction         : var_decl SEMI?
                    | symbol_id assignment
                    | keywords symbol_id? SEMI?
                    ;

//keywords
keywords            : return;
return              : RETURN expression? SEMI;

//declarations
var_proto_decl      :symbol_id symbol_id (SET (value_id | symbol_id))?
                    ;
var_decl            :symbol_id symbol_id (assignment)?
                    ;
fnc_call            : symbol_id call_group;
fnc_decl            : fnc_proto block_exec;
fnc_proto           : symbol_id restric_id decl_group SEMI?;

//basic
assignment          : assign_ops expression
                    | symbol_id assign_ops expression;

unary_expr          : lv_unary_ops
                    | rv_unary_ops
                    | unary_symb symbol_id
                    ;

//resolution
rv_unary_ops        : symbol_id increment
                    | symbol_id decrement;

lv_unary_ops        : increment symbol_id
                    | decrement symbol_id;

increment           : INCREMENT;
decrement           : DECRIMENT;

binary_ops          : ADD
                    | SUBTRACT
                    | MULTIPLY
                    | DIVIDE
                    | EXPONENT
                    | MODULO
                    ;

unary_symb          : NOT;

assign_ops          : SET
                    | SET_SUM
                    | SET_DIFFERENCE
                    | SET_PRODUCT
                    | SET_QUOTIENT
                    ;



symbol_id           : IDENTIFIER (('.'IDENTIFIER)+)?;
restric_id          : IDENTIFIER;
value_id            : STRINGLIT | DECIMAL | IDENTIFIER;

expression          : unary_expr
                    | fnc_call
                    | value_id; 

string              : STRINGLIT;
char                : '\'' ALPHA '\'';



/*
 * Lexer Rules
 */

//keywords
RETURN              : 'return';

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
NOT             : '!';


fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

fragment ALPHA  : [a-zA-Z_];


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

DECIMAL         : '-'?[0-9]+('.'[0-9])?;
IDENTIFIER      : ALPHA+ (DECIMAL+)?;//[a-zA-Z_]([a-zA-Z_0-9])+;
SEMI            : ';';
WS              : [ \r\t\u000C\n]+ -> skip ;

