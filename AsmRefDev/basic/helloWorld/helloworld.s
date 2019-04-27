	.file	"helloworld.c"
	.globl	globdat
	.data
	.align 4
globdat:
	.long	5
	.text
	.globl	testfnc
	.def	testfnc;	.scl	2;	.type	32;	.endef
	.seh_proc	testfnc
testfnc:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	.seh_endprologue
	movl	%ecx, 16(%rbp)
	movl	%edx, 24(%rbp)
	movl	16(%rbp), %edx
	movl	24(%rbp), %eax
	addl	%edx, %eax
	popq	%rbp
	ret
	.seh_endproc
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "Hello World\0"
.LC1:
	.ascii "%s %d\15\12\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
	.seh_proc	main
main:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$48, %rsp
	.seh_stackalloc	48
	.seh_endprologue
	call	__main
	leaq	.LC0(%rip), %rax
	movq	%rax, -8(%rbp)
	movl	$6, -12(%rbp)
	movl	-12(%rbp), %eax
	movl	$8, %edx
	movl	%eax, %ecx
	call	testfnc
	movl	%eax, %edx
	movq	-8(%rbp), %rax
	movl	%edx, %r8d
	movq	%rax, %rdx
	leaq	.LC1(%rip), %rcx
	call	printf
	movl	$0, %eax
	addq	$48, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.ident	"GCC: (GNU) 6.4.0"
	.def	printf;	.scl	2;	.type	32;	.endef
