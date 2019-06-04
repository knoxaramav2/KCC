	.file	"helloworld.c"
	.section .rdata,"dr"
.LC0:
	.ascii "%d\15\12\0"
	.text
	.globl	print
	.def	print;	.scl	2;	.type	32;	.endef
print:
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
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$48, %rsp
	call	__main
	movl	$5, -4(%rbp)
	movl	-4(%rbp), %eax
	movl	%eax, %ecx
	call	print
	movl	$0, %eax
	leave
	ret
	.ident	"GCC: (GNU) 6.4.0"
	.def	printf;	.scl	2;	.type	32;	.endef
