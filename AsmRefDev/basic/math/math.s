.LCO:
    .string "%d + %d = %d"
    .def main
    .seh_proc main
main:
        push    %rbp        
        mov     %rbp, %rsp
        mov     %eax, 0
        pop     %rbp
        ret
    .seh_endproc
