	.file	"helloworld.c"
	.text
	.globl	test
	.def	test;	.scl	2;	.type	32;	.endef
test:
	pushq	%rbp
	movq	%rsp, %rbp
	movl	%ecx, 16(%rbp)
	movl	%edx, 24(%rbp)
	movl	%r8d, 32(%rbp)
	movl	%r9d, 40(%rbp)
	nop
	popq	%rbp
	ret
	.def	__main;	.scl	2;	.type	32;	.endef
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$80, %rsp
	call	__main
	movl	$1, -4(%rbp)
	movl	$2, -8(%rbp)
	movl	$3, -12(%rbp)
	movl	$4, -16(%rbp)
	movl	$5, -20(%rbp)
	movl	-24(%rbp), %r9d
	movl	-12(%rbp), %r8d
	movl	-8(%rbp), %edx
	movl	-4(%rbp), %eax
	movl	-16(%rbp), %ecx
	movl	%ecx, 32(%rsp)
	movl	%eax, %ecx
	call	test
	movl	$0, %eax
	leave
	ret
	.ident	"GCC: (GNU) 6.4.0"
