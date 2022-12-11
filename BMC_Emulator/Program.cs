// See https://aka.ms/new-console-template for more information


using System.Collections;
using BMC_Emulator;

var emulator = new Emulator();

emulator.LoadProgram(new[]
{
    "#START MOV R1,100",
    "OUT R1,2",
    "SUB R1,10",
    "OUT R1,2",
    "HLT"
});

emulator.Run();