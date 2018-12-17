#Compiles KCC Toolchain

#Tools
CC = g++
BITVRS = 32

#Compile directories
CMP_DIRS = Src/KOMP Src/LinK Src/KASM

#Default modes
DEF_BIT_MODE = 64
C_COMMON=-Iheaders -std=c++17 -Wall -g -m$(DEF_BIT_MODE)

#Paths
BIN_PATH = $(shell pwd)/bin

#Platform Support
ifeq ($(shell uname), Linux)
#Linux
	FixPath = $1
	EXT=
else
#Windows
	FixPath = $(subst /,\, $1)
	EXT=.exe
endif

#Module Setup
TITLE_COMPILER = kcc
TITLE_LINKER = klink
TITLE_ASSEMBLER = kasm
TITLE_ASSEMBLER_LIB = kasm_lib

export TITLE_COMPILER
export TITLE_LINKER
export TITLE_ASSEMBLER
export TITLE_ASSEMBLER_LIB

#Exports
export DEF_BIT_MODE
export BIN_PATH
export FixPath
export C_COMMON
export EXT
export CC

subdirs:
	for dir in $(CMP_DIRS); do \
	$(MAKE) -C $$dir; \
	done

.PHONY: run
run:
	bin/$(TITLE_COMPILER) -h

.PHONY: clean
clean:
	for dir in $(CMP_DIRS); do \
	 $(MAKE) clean -C $$dir; \
	done
	 rm -f $@ bin/*
	
build: clean subdirs run