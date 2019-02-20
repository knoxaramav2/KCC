grammar kcc;

main: 'int ' value;
value: ANY+;
ANY: .;