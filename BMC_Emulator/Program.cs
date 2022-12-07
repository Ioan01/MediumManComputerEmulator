// See https://aka.ms/new-console-template for more information


using System.Collections;
using BMC_Emulator;


var emulator = new Emulator();



emulator.LoadProgram(new []
{
    "MOV R0,100",
    "OUT R1,2",
    "PSH R0",
    "PSH R0",
    "PSH R0",
    "POP R1",
    "HLT"
});

emulator.Run();