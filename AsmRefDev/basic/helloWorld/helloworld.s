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
	.section .rdata,"dr"
.LC4:
	.ascii "%f %f %f %f %e\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$144, %rsp
	movaps	%xmm6, -48(%rbp)
	movaps	%xmm7, -32(%rbp)
	movaps	%xmm8, -16(%rbp)
	call	__main
	movss	.LC0(%rip), %xmm0
	movss	%xmm0, -52(%rbp)
	movss	.LC1(%rip), %xmm0
	movss	%xmm0, -56(%rbp)
	movss	.LC2(%rip), %xmm0
	movss	%xmm0, -60(%rbp)
	movss	.LC3(%rip), %xmm0
	movss	%xmm0, -64(%rbp)
	movss	-52(%rbp), %xmm0
	mulss	-56(%rbp), %xmm0
	movss	%xmm0, -68(%rbp)
	movss	-56(%rbp), %xmm0
	addss	-64(%rbp), %xmm0
	subss	-68(%rbp), %xmm0
	movss	-52(%rbp), %xmm1
	addss	%xmm1, %xmm0
	movss	%xmm0, -52(%rbp)
	cvtss2sd	-68(%rbp), %xmm1
	cvtss2sd	-64(%rbp), %xmm0
	cvtss2sd	-60(%rbp), %xmm4
	cvtss2sd	-56(%rbp), %xmm3
	cvtss2sd	-52(%rbp), %xmm2
	movq	%xmm4, %rax
	movq	%rax, %rdx
	movq	%rdx, -88(%rbp)
	movsd	-88(%rbp), %xmm8
	movq	%rax, -88(%rbp)
	movsd	-88(%rbp), %xmm5
	movq	%xmm3, %rax
	movq	%rax, %rdx
	movq	%rdx, -88(%rbp)
	movsd	-88(%rbp), %xmm7
	movq	%rax, -88(%rbp)
	movsd	-88(%rbp), %xmm4
	movq	%xmm2, %rax
	movq	%rax, %rdx
	movq	%rdx, -88(%rbp)
	movsd	-88(%rbp), %xmm6
	movsd	%xmm1, 40(%rsp)
	movsd	%xmm0, 32(%rsp)
	movapd	%xmm8, %xmm3
	movq	%xmm5, %r9
	movapd	%xmm7, %xmm2
	movq	%xmm4, %r8
	movapd	%xmm6, %xmm1
	movq	%rax, %rdx
	leaq	.LC4(%rip), %rcx
	call	printf
	movl	$0, %eax
	movaps	-48(%rbp), %xmm6
	movaps	-32(%rbp), %xmm7
	movaps	-16(%rbp), %xmm8
	leave
	ret
	.section .rdata,"dr"
	.align 4
.LC0:
	.long	1084227584
	.align 4
.LC1:
	.long	1083388723
	.align 4
.LC2:
	.long	1107644908
	.align 4
.LC3:
	.long	1113469747
	.ident	"GCC: (GNU) 6.4.0"
	.def	printf;	.scl	2;	.type	32;	.endef
