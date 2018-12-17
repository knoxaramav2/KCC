#kcc K Compiler Collection
#compiles K Compiler

VERSION=-DKMP_VERSION=\"0.0.1\"
OUT_NAME= $(TITLE_COMPILER)
CLEANTEXT = o a
DBGFLAGS=
SRC=main.cpp options.cpp
O_SRC= $(SRC:.c=.o)
CFLAGS= $(C_COMMON)
LFLAGS= -L$(BIN_PATH)
EXE_PATH=$(BIN_PATH)/$(OUT_NAME)$(EXT)

all: $(EXE_PATH)
	echo $(OUT_NAME) Done.

.c.o:
	$(CC) $(CFLAGS) -c -o $@ $<

$(EXE_PATH) : $(O_SRC)
	$(CC) $(CFLAGS) -o $@ $(O_SRC) $(LFLAGS)

.PHONY: clean
clean:	
	rm -f $@ *.o

.PHONY: dbg
dbg: $(OUT_NAME)
	exec gdb -x $(EXE_PATH) $(DBGFLAGS)