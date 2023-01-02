# Little Man Computer-like Emulator
### Inspired by  [Peter Higginson's Online Emulator](https://peterhigginson.co.uk/RISC/)
##
##
## Specifications:
- 16-bit Operation Size (6 bit opcode and 10 bit arguments )
- 256-Word (512 byte) Memory Size
- 2 General Purpose 16-bit Registers
- 8-bit Link Register
- 8-bit Stack Pointer
- 29 instructions
- 4 1-bit Flags (Zero, Negative ,Carry ,Overflow )

## Syntax
- General syntax :  <operation name> [<arg1>],[<arg2>]
- Labels : #LabelName <instruction> <arguments> 
        BRANCH/JUMP #LabelName
- Immediate values : <operation name>, 0-255

## Workflow
- Program is loaded line by line into the Emulator. 
- The emulator will first parse labels. For example if it encounters the label #LABELNAME at line 1 it will add it to an internal dictionary as [#LABELNAME,1] and will replace each further occurance of #LABELNAME with 1.
- Then, the emulator will decode each instruction by matching the string with a regex to find out 
-- the instruction
-- the arguments
- Depending on the operation name, it will instantiate an Instruction child class and pass the arguments to the constructor. For example, if STR is found, a StoreInstruction will be instantiated.
- Then, the operation will parse the arguments and return a 16-bit integer representing the instruction bits.
- These 16 bits are created by : (6-bit opcode  << 10) | (10 bits arguments)
- For example for the instruction STR R0,127
    The opcode of STR is 0x3 << 10 
    Seeing as the first argument is register 0, the first bit from the argument bits will be 0
    Since the second argument contains an immediate value, the next bit will be 1 to specify that an 8-bit immediate value is used. Therefore, the last 8 bits will represent our immedaite value.
    Hence, we have 000 011 0000000000 (opcode) | 000 000 0 000000000 (register selector) | 000 000 0 1(bit that shows that the following 8 bits are an immediate value)01111111(8-bit immediate value)
- Then, the 16-bit number is stored in the virtual memory at increasing addresses.
- Then, we start the emulator, which will 
    - Read the the 16-bit word at memory[ProgramCounter]
    - Increment Program Counter
    - Insantiate an Instruction child class based on the first 6 bits, and pass the following 10 bits as arguments
    - Execute the Instruction passing the emulator as parameter to the Instruction
   


## Instruction Structure

### 6-bit opcode and 10-bit arguments

| o 	| o 	| o 	| o 	| o 	| o 	| a 	| a 	| a 	| a 	| a 	| a 	| a 	| a 	| a 	| a 	|
|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|---	|

Since the first 6 bits, only the last 10 bits are usable as arguments. Usually, the first bit from the argument space is used as register selector/ register destination (if applicable) and the last 9 bits are used in the following manner : if the most significant bit in the last 9 bits is 1, then the following 8 bits are used as an immediate value in the operation - if that bit is 0, then the least significant bit from the following 8 bits is used to select the register from which the value will be extracted.

## Instructions
#
#
#
| Ins | opcode<br>(first 6 bits) | 1 bit                                                                                    | 9 bits                                                                                                                                                                                                                                               | Description                                                                                                                                                                                     | Example                                                                                                                                                                                                                                                                                                                                                                                                            |
| --- | ------------------------ | ---------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| INP | 0x00                     | register selector                                                                        | input channel                                                                                                                                                                                                                                        | input - input channel 2 - ascii keyboard                                                                                                                                                        |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| OUT | 0x01                     | register selector                                                                        | input channel                                                                                                                                                                                                                                        | output - output channel 2 - numeric console, 3 - binary console, 4 - ascii console                                                                                                              |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| LDR | 0x02                     | register selector                                                                        | 1st bit - whether load is done with adress from another register / immediate value<br>last 8 bits : memory address if immediate value is used / lsb register selector                                                                                | load reg ( in memory)                                                                                                                                                                           | LDR R0,R1<br>LDR R1,255<br>000010 0 0 000 0000 1<br>000010 1 1 1111 1111                                                                                                                                                                                                                                                                                                                                           |
| STR | 0x03                     | register selector                                                                        | 1st bit - whether store is done with adress from another register / immediate value<br>last 8 bits : memory address if immediate value is used / lsb register selector                                                                               | store reg (in memory)                                                                                                                                                                           | STR R0, R1, STR R0,128                                                                                                                                                                                                                                                                                                                                                                                           |
| HLT | 0x04                     | \-                                                                                       | \-                                                                                                                                                                                                                                                   | stop                                                                                                                                                                                            |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| JMS | 0x05                     | if 0, use register from lsb from the 9 bits, otherwise use 8 bit immediate address value | immediate value if used / register selector                                                                                                                                                                                                          | jump                                                                                                                                                                                            | JMS R0<br>000101 0 0000 0000 0<br>JMS 255<br>000101 1 01111 1111<br>JMS #LABEL                                                                                                                                                                                                                                                                                                                                     |
| PSH | 0x06                     | register selector                                                                        | \-                                                                                                                                                                                                                                                   | stack push                                                                                                                                                                                      |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| POP | 0x07                     | register selector                                                                        | \-                                                                                                                                                                                                                                                   | stack pop                                                                                                                                                                                       |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| RET | 0x08                     | \-                                                                                       | \-                                                                                                                                                                                                                                                   | suboutine return                                                                                                                                                                                |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| CMP | 0x09                     | register selector                                                                        | 1st bit - whether comparison is done with another register / immediate value<br><br>immediate comparison : next 8 bits are the value with which the register is compared<br><br>register comparison : lsb indicates the register used for comparison | compare<br>CMP A,B // NZCV<br>A == B // 0110<br>A < B // 1000<br>A > B // 0010                                                                                                                  | CMP R0,255<br>command bits look like : opcode(first 6) - 0(register selector) - 1 (first bit indicates immediate comparison is done) 1111 1111(255, imediate value)<br>so : <opcode>0 1 1111 1111<br><br>CMP R1,R0<br>command bits look like : opcode(first 6) - 1(register selector) - 0 (first bit indicates register comparison is done) 0000 0000(0, because register 0 is used)<br>so : <opcode>0 0 0000 0000 |
| BRA | 0x0A                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | branch always                                                                                                                                                                                   |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| BEQ | 0x0B                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | br equal<br>when Z=1, C=1                                                                                                                                                                       |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| BRZ | 0x0C                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | branch if accumulator is zero<br><br>                                                                                                                                                           |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| BMI | 0x0D                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | Branch if minus<br>(when N == 1)                                                                                                                                                                |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| BPL | 0x0E                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | branch plus                                                                                                                                                                                     |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| BGT | 0x0F                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | branch >                                                                                                                                                                                        |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| BLT | 0x10                     | \-                                                                                       | 8 bit mem address (msb unused)                                                                                                                                                                                                                       | branch <                                                                                                                                                                                        |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| ADD | 0x11                     | register destination                                                                     | 1st bit - whether operation is done with another register / immediate value<br>register operation : lsb indicates the register used for operation<br>immediate operation : next 8 bits are the value with which the register does the operation<br>  | addition                                                                                                                                                                                        | ADD R0, R1<br>command bits : opcode(first 6),0(register 0 used), 0(first bit in the 9 bits indicates register is used as second operand), 0000 0001 (register 1 used)<br>so : <opcode>0 0 0000 0001                                                                                                                                                                                                                |
| SUB | 0x12                     | register destination                                                                     | subtraction                                                                                                                                                                                                                                          |
| MUL | 0x13                     | register destination                                                                     | multiplication                                                                                                                                                                                                                                       |
| DIV | 0x14                     | register destination                                                                     | division                                                                                                                                                                                                                                             |
| MOD | 0x15                     | register destination                                                                     | modulus                                                                                                                                                                                                                                              |
| AND | 0x16                     | register destination                                                                     | bitwise and                                                                                                                                                                                                                                          |
| OR  | 0x17                     | register destination                                                                     | bitwise or                                                                                                                                                                                                                                           | XOR R0, 128<br>command bits : opcode(first 6),1(register 1 used), 1(first bit in the 9 bits indicates immediate value is used as second operand), 1000 0000 (128)<br>so : <opcode>1 1 1000 0000 |
| XOR | 0x18                     | register destination                                                                     | bitwise xor                                                                                                                                                                                                                                          |
| \>> | 0x19                     | register destination                                                                     | shift right                                                                                                                                                                                                                                          |
| <<  | 0x1A                     | register destination                                                                     | shift left                                                                                                                                                                                                                                           |
| NOT | 0x19                     | register destination                                                                     | \-                                                                                                                                                                                                                                                   | bitwise not                                                                                                                                                                                     |                                                                                                                                                                                                                                                                                                                                                                                                                    |
| MOV | 0x1C                     | register destination                                                                     | 8-bit immediate value (msb unused)                                                                                                                                                                                                                   | move imeediate value into register                                                                                                                                                              |
