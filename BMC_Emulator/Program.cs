// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BMC_Emulator;
using BMC_Emulator.Instructions;

var emulator = new Emulator();
//Change path to your path (*^^*)
string[] instructions = File.ReadAllLines("E:\\Users\\wotp3\\Source\\Repos\\MediumManComputerEmulator\\BMC_Emulator\\instructions.txt");
instructions = instructions
    .Where(x => !x.Trim().StartsWith("//")) // Removes lines that start with //
    .Where(x => !string.IsNullOrWhiteSpace(x)) // Removes empty lines
    .Select(x => x.Split("//")[0].Trim()) // Removes anything following a // on the same line
    .ToArray();

// Check for invalid registers
var invalidRegister = instructions
    .SelectMany(line => Regex.Matches(line, @"(?i)R[1-9][0-9]+"))
    .FirstOrDefault();

if (invalidRegister != null)
    throw new Exception($"Invalid register: {invalidRegister.Value}");


emulator.LoadProgram(instructions);
emulator.Run();