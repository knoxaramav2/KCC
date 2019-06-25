	.section .rdata,"dr"
.Ltext0:
.LC0:
	.string	"%d\r\n"
	
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	push	rbp
	mov		rbp, rsp
	sub 	rsp, 16
	
	mov     DWORD PTR [rbp-4], 3
	mov     edx, DWORD PTR [rbp-4]
	mov     eax, edx
	sal     eax, 3
	sub     eax, edx
	mov     esi, eax
	mov     edi, OFFSET FLAT:.LC0
	mov     eax, 0
	call    printf
	mov     eax, 0
	leave
	ret
	.ident	"GCC: (GNU) 6.4.0"
	.def	printf;	.scl	2;	.type	32;	.endef
