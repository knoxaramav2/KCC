#Compiles KCC Toolchain

#Compile directories
CMP_DIRS = Src/KOMP Src/LinK Src/KASM

#Default modes
DEF_BIT_MODE = 64

#Paths
BINPATH = $(shell pwd)/bin

subdirs:
	for dir in $(CMP_DIRS); do \
	$(MAKE) -C $$dir; \
	done

.PHONY: run
run:
	bin/KCC

.PHONY: clean
clean:
	for dir in $(CMP_DIRS); do \
	$(MAKE) clean -C $$dir; \
	done
	
