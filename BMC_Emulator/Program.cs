// See https://aka.ms/new-console-template for more information


using System.Collections;
using BMC_Emulator;

var emulator = new Emulator();

emulator.LoadProgram(new[]
{
    "JMS #HELLO",
    "MOV R1,32",
    "#LOOP OUT R1,4",
    "ADD R1,1",
    "CMP R1,100",
    "BEQ #OUTPUT",
    "BRA #LOOP",
    "MOV R1,10",
    "#OUTPUT OUT R1,4",
    "HLT",
    "#HELLO MOV R0,128",
    "OUT R0,3",
    "RET"
// Output all the ASCII print characters

});

emulator.Run();