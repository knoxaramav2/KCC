	.file	"helloworld.c"
	.def	__main;	.scl	2;	.type	32;	.endef
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$48, %rsp
	call	__main
	movss	.LC0(%rip), %xmm0
	movss	%xmm0, -4(%rbp)
	movss	.LC1(%rip), %xmm0
	movss	%xmm0, -8(%rbp)
	movss	-4(%rbp), %xmm0
	mulss	-8(%rbp), %xmm0
	movss	%xmm0, -4(%rbp)
	movss	-4(%rbp), %xmm0
	addss	-8(%rbp), %xmm0
	movss	%xmm0, -12(%rbp)
	movss	-12(%rbp), %xmm0
	cvttss2si	%xmm0, %eax
	leave
	ret
	.section .rdata,"dr"
	.align 4
.LC0:
	.long	1092616192
	.align 4
.LC1:
	.long	3254910976
	.ident	"GCC: (GNU) 6.4.0"
