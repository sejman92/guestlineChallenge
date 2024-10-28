using CommandLine;

namespace GuestlineChallenge;

public class OptionsManager
{
    public CommandLineOptions Options { get; private set; }

    public OptionsManager(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(parsedOptions => Options = parsedOptions)
            .WithNotParsed(errors => throw new ArgumentException("Invalid command line arguments"));
    }
}

public class CommandLineOptions
{
    [Option('h', "hotels",  Required = true, HelpText = "JSON filename that contains hotels details.")]
    public string HotelsFilePath { get; set; }
    
    [Option('b', "bookings",  Required = true, HelpText = "JSON filename that contains list of bookings.")]
    public string BookingsFilePath { get; set; }
}