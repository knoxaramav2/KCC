	.file	"helloworld.c"
	.section .rdata,"dr"
.LC0:
	.ascii "Sup %c\0"
	.text
	.globl	function
	.def	function;	.scl	2;	.type	32;	.endef
function:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$32, %rsp
	movl	%ecx, 16(%rbp)
	movl	16(%rbp), %edx
	leaq	.LC0(%rip), %rcx
	call	printf
	nop
	leave
	ret
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC1:
	.ascii "%d\15\12\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$48, %rsp
	call	__main
	movl	$2, -4(%rbp)
	movl	$2, %ecx
	call	malloc
	movq	%rax, -16(%rbp)
	movl	-4(%rbp), %eax
	addl	$7, %eax
	movl	%eax, %ecx
	call	function
	movl	-4(%rbp), %eax
	movl	%eax, %edx
	leaq	.LC1(%rip), %rcx
	call	printf
	movl	-4(%rbp), %eax
	leave
	ret
	.ident	"GCC: (GNU) 6.4.0"
	.def	printf;	.scl	2;	.type	64;	.endef
	.def	malloc;	.scl	2;	.type	64;	.endef
