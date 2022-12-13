// See https://aka.ms/new-console-template for more information


using System.Collections;
using BMC_Emulator;

var emulator = new Emulator();

emulator.LoadProgram(new[]
{
    "#START MOV R0,3", // reg R0 = 3
    "MUL R0,5",        // R0 = R0 * 3 => R0 = 15
    "OUT R0,2",        // print R0 (15)
    "MOV R1,14",       // reg R1 = 14
    "JMS #HALT",
    "OUT R1,2",        // print R1 (14)
    "#HALT HLT"              // expected result: 1529914
});

emulator.Run();