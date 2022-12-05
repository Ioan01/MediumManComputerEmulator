// See https://aka.ms/new-console-template for more information


using System.Collections;
using BMC_Emulator;


BitArray array = new BitArray(new int[]{Int32.MaxValue});

Console.WriteLine(array.ToString());
var emulator = new Emulator();



emulator.LoadProgram(new []
{
    "INP R1,2",
    "OUT R1,4",
    "HLT"
});

emulator.Run();