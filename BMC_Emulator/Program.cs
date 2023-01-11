using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BMC_Emulator;
using BMC_Emulator.Instructions;

var emulator = new Emulator();

string[] instructions = File.ReadAllLines("..\\..\\..\\instructions.txt");
instructions = instructions
    .Where(x => !x.Trim().StartsWith("//")) // Removes lines that start with //
    .Where(x => !string.IsNullOrWhiteSpace(x)) // Removes empty lines
    .Select(x => x.Split("//")[0].Trim()) // Removes anything following a // on the same line
    .ToArray();

// Check for invalid registers
var invalidRegister = instructions
    .SelectMany(line => Regex.Matches(line, @"R([2-9][0-9]*|1[0-9]+)"))
    .FirstOrDefault();

if (invalidRegister != null)
    throw new Exception($"Invalid register: {invalidRegister.Value}");

foreach (string i in instructions)
{
    string pattern = @"^(STR|LDR)";
    Regex rgx = new Regex(pattern);
    Match match = rgx.Match(i);
    bool exist_R = match.Success;

    if (exist_R)
    {
        string pattern2 = @",([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5]|R[01])\b";
        Regex rgx2 = new Regex(pattern2);
        Match match2 = rgx2.Match(i);
        bool exist = match2.Success;
        if (!exist)
        {
            throw new Exception($"Invalid value!");
        }
    }

    //throw new Exception($"Invalid value!");
}

emulator.LoadProgram(instructions);
emulator.Run();