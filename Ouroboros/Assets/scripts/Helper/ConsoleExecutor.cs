using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleExecutor : MonoBehaviour
{
    //========================================================================================================
    /// How many log lines should be retained?
    /// Note that strings submitted to appendLogLine with embedded newlines will be counted as a single line.
    const int scrollbackSize = 20;

    // Scroll Queue, Command History & Command Distionary:
    Queue<string> scrollback = new Queue<string>(scrollbackSize);
    System.Collections.Generic.List<string> commandHistory = new System.Collections.Generic.List<string>();
    Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();
    GameStateController gamestate;

    public string[] log { get; private set; } //Copy of scrollback as an array for easier use by ConsoleView
    const string repeatCmdName = "!!"; //Name of the repeat command, constant since it needs to skip these if they are in the command history

    //========================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Goooooooooooooood!");
        gamestate = GameObject.FindObjectOfType(typeof(GameStateController)) as GameStateController;

        //When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
        RegisterCommand("gamestate", GameState, "access game objects and execute functions");
        RegisterCommand("babble", Babble, "Example command that demonstrates how to parse arguments. babble [word] [# of times to repeat]");
        RegisterCommand("echo", Echo, "echoes arguments back as array (for testing argument parser)");
        RegisterCommand("help", Help, "Print this help.");
        RegisterCommand("hide", Hide, "Hide the console.");
        RegisterCommand(repeatCmdName, RepeatCommand, "Repeat last command.");
        RegisterCommand("reload", Reload, "Reload game.");
        RegisterCommand("resetprefs", ResetPrefs, "Reset & saves PlayerPrefs.");
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate == null)
        {
            gamestate = GameObject.FindObjectOfType(typeof(GameStateController)) as GameStateController;
        }
    }
    
    //Function interface for command execution logic implementation!
    public delegate void CommandHandler(string[] args);

    #region Event declarations
    // Used to communicate with ConsoleView
    public delegate void LogChangedHandler(string[] log);
    public event LogChangedHandler logChanged;
    public delegate void VisibilityChangedHandler(bool visible);
    public event VisibilityChangedHandler visibilityChanged;
    #endregion

    /// 
    /// Object to hold information about each command
    /// 
    class CommandRegistration
    {
        public string command { get; private set; }
        public CommandHandler handler { get; private set; }
        public string help { get; private set; }

        public CommandRegistration(string command, CommandHandler handler, string help){
            this.command = command;
            this.handler = handler;
            this.help = help;
        }
    }
    
    //========================================================================================================
    #region Command handlers//Implement new commands in this region of the file.
    /// A test command to demonstrate argument checking/parsing.
    /// Will repeat the given word a specified number of times.
    //---------------------------------------------------------
    void Babble(string[] args)
    {
        int cmdIdx = 4;
        while (cmdIdx != 0)
        {
            string cmd = commandHistory[cmdIdx];
            if (string.Equals(repeatCmdName, cmd))
            {
                continue;
            }
            RunCommandString(cmd);
            cmdIdx--;
            break;
        }
    }
    //---------------------------------------------------------
    void Reload(string[] args)
    {
        //Application.LoadLevel(Application.loadedLevel);
    }
    //---------------------------------------------------------
    void Help(string[] args)
    {
        foreach (var item in commands)
        {
            AppendLogLine(item.Value.command);
        }

    }
    //---------------------------------------------------------
    void Hide(string[] args)
    {

    }
    //---------------------------------------------------------
    void RepeatCommand(string[] args)
    {

    }
    //---------------------------------------------------------
    void Echo(string[] args)
    {

    }
    //---------------------------------------------------------
    void ResetPrefs(string[] args)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    //---------------------------------------------------------
    void GameState(string[] args)
    {
        if (args.Length > 0)
        {
            gamestate.executeStateCommand(args[0]);
        }
        else
        {
            AppendLogLine(gamestate.AvailableObjects());
        }

    }
    //---------------------------------------------------------
    #endregion
    //---------------------------------------------------------
    
    void RegisterCommand(string command, CommandHandler handler, string help)
    {
        commands.Add(command, new CommandRegistration(command, handler, help));
    }
    //--------------------------------------------------------------------------
    //SETUP ends here...

    public void AppendLogLine(string line)
    {
        Debug.Log(line);

        if (scrollback.Count >= scrollbackSize)
        {
            scrollback.Dequeue();
        }
        scrollback.Enqueue(line);

        log = scrollback.ToArray();
        if (logChanged != null)
        {
            logChanged(log);
        }
    }

    public void RunCommandString(string commandString)
    {
        Debug.Log("Running command: " + commandString);
        AppendLogLine("$ " + commandString);

        string[] parsedCommand = ParseArguments(commandString);
        string[] args = new string[0];
        if (parsedCommand.Length == commandString.Length)
        {
            return;
        }
        else if (parsedCommand.Length >= 2)
        {
            int argsCount = parsedCommand.Length - 1;
            args = new string[argsCount];
            System.Array.Copy(parsedCommand, 1, args, 0, argsCount);
        }
        RunCommand(parsedCommand[0].ToLower(), args);
        commandHistory.Add(commandString);
    }

    public void RunCommand(string command, string[] args)
    {
        CommandRegistration reg = null;
        if (!commands.TryGetValue(command, out reg))
        {
            AppendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
        }
        else
        {
            if (reg.handler == null)
            {
                AppendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
            }
            else
            {
                reg.handler(args);
            }
        }
    }

    //Argument parsing! String --TO--> String[]
    static string[] ParseArguments(string commandString)
    {
        LinkedList<char> parmChars = new LinkedList<char>(commandString.ToCharArray());
        bool inQuote = false;
        var node = parmChars.First;
        while (node != null)
        {
            var next = node.Next;
            if (node.Value == '"')
            {
                inQuote = !inQuote;
                parmChars.Remove(node);
            }
            if (!inQuote && node.Value == ' ')
            {
                node.Value = '\n';
            }
            node = next;
        }
        char[] parmCharsArr = new char[parmChars.Count];
        parmChars.CopyTo(parmCharsArr, 0);
        return (new string(parmCharsArr)).Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    }




}
