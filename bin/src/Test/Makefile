#Build test project

BIN_PATH = $(shell pwd)

CC = kcc
EXE_PATH=result

SRC=main.kcc

all: compile
	echo $(EXE_PATH) Done.

compile : $(SRC)
	$(CC) $@ -hv --output=$(EXE_PATH)
