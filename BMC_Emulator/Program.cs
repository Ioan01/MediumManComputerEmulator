// See https://aka.ms/new-console-template for more information


using System.Collections;
using BMC_Emulator;


var emulator = new Emulator();



emulator.LoadProgram(new []
{
    "#START MOV R1,100",
    "OUT R1,2",
    "ADD R1,10",
    "OUT R1,2",
    "PSH R0",
    "PSH R0",
    "PSH R0",
    "POP R1",
    "JMS #START",
    "HLT"
});

emulator.Run();