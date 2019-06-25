#Build test project

BIN_PATH = $(shell pwd)

CC = kcc
EXE_PATH=result

SRC=main.kcc dep.kcc

all: $(EXE_PATH)
	echo $(EXE_PATH) Done.

$(EXE_PATH) : $(SRC)
	$(CC) $^ -hv
