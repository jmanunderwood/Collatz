using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;
using Collatz;
using Newtonsoft.Json;

namespace Collatz
{
    class CommandLineInterface
    {

        public struct CollatzOutput
        {
            public int seed;
            public int[] collatzNumbers;
            public int stoppingTime;
            
        }

        public static void InitStruct(CollatzOutput o)
        {
            o.seed = -1;
            //o.collatzNumbers[0] = -1;
            o.stoppingTime = -1;
            //o.leadingDigits[0] = -1;
        }


        public static void Main(string[] args)
        {
            List<int> leadingDigits = new List<int>(); 
            
            CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);

            CommandOption seed = commandLineApplication.Option(
                "-s | --seed <seed>",
                "The integer which specifies where the algorith starts",
                CommandOptionType.SingleValue);
            CommandOption range = commandLineApplication.Option(
                "-r | --range <>",
                "Specifies the number of seeds to run. If no seed is " +
                "specified the range will start at 1",
                CommandOptionType.MultipleValue);
            CommandOption stoppingTime = commandLineApplication.Option(
                "-t",
                "Calculate stopping time",
                CommandOptionType.NoValue);
            CommandOption leadingDigit = commandLineApplication.Option(
                "-l",
                "Calculate the leading Digit distribution",
                CommandOptionType.NoValue);
            CommandOption output = commandLineApplication.Option(
                "-o | --output | -$ <filename.collatz>",
                "Output to a .collatz file. ![TOO MUCH WORK; TOO LAZY; NOT WORKING]",
                CommandOptionType.SingleValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            
            commandLineApplication.OnExecute(() =>
            {
                CollatzOutput currentOutput;
                currentOutput = new CollatzOutput();
                InitStruct(currentOutput);
                CollatzOutput[] rangeOutputs;

                if (stoppingTime.HasValue())
                {

                    currentOutput.stoppingTime = currentOutput.collatzNumbers.Length;
                }
                if (output.HasValue())
                {

                }
                if (range.HasValue()) //-r
                {
                    rangeOutputs = new CollatzOutput[Int32.Parse(range.Value())];
                    for (int k = 0; k < rangeOutputs.Length; k++)
                    {
                        InitStruct(rangeOutputs[k]);
                    }

                    int n = Int32.Parse(range.Value());
                    Console.WriteLine(n);
                    if (seed.HasValue()) //-r seed: Perform the algorithm on the range provided with seed=seed
                    {
                        int currentSeed = Int32.Parse(seed.Value());
                        for (int i = 0; i < n; i++)
                        {
                            rangeOutputs[i].collatzNumbers = Collatz.CollatzNumbers(currentSeed+i).ToArray();
                            rangeOutputs[i].seed = currentSeed+i;
                            if (leadingDigit.HasValue())
                            {
                                leadingDigits.AddRange(Collatz.LeadingDigits(rangeOutputs[i].collatzNumbers));
                            }
                        }
                    }
                    else //-r: Perform the algorithm on the range provided with seed=1
                    {

                        int currentSeed = 4;
                        Console.WriteLine("E");

                        for (int i = 0; i < n; i++)
                        {
                            Console.WriteLine(rangeOutputs[i].collatzNumbers);

                            rangeOutputs[i].collatzNumbers = Collatz.CollatzNumbers(currentSeed+i).ToArray();
                            rangeOutputs[i].seed = currentSeed+i;
                            if (leadingDigit.HasValue())
                            {
                                leadingDigits.AddRange(Collatz.LeadingDigits(rangeOutputs[i].collatzNumbers));
                            }

                        }
                    }
                    
                    //If -r specified then display range values
                    /*
                    Iteration: 0
                    Seed: 1
                    Collatz Numbers: `,`,`,`,`,`


                    */
                    for (int i = 0; i < n; i++)
                    {
                        
                        Console.WriteLine("Iteration: " + i);
                        Console.WriteLine("Seed: " + rangeOutputs[i].seed);
                        Console.WriteLine("Collatz Numbers: ");
                        for (int j = 0; j < rangeOutputs[i].collatzNumbers.Length; j++)
                        {
                            Console.Write(rangeOutputs[i].collatzNumbers[j].ToString() + "; ");
                        }
                        Console.WriteLine();
                        if (rangeOutputs[i].stoppingTime != -1) Console.WriteLine("Stopping Time: " + rangeOutputs[i].stoppingTime);

                        //----------------------------------------------------------
                        for (int z = 0; z < 50; z++)
                        {
                            Console.Write("-");
                        }
                        Console.WriteLine();

                    }
                }
                else
                {
                    if (seed.HasValue()) //If no -r specified, then display one set of values
                    {
                        currentOutput.collatzNumbers = Collatz.CollatzNumbers(Int32.Parse(seed.Value())).ToArray();
                        currentOutput.seed = Int32.Parse(seed.Value());

                        Console.WriteLine("Seed: " + currentOutput.seed);
                        Console.WriteLine("Collatz Numbers: ");
                        for (int i = 0; i < currentOutput.collatzNumbers.Length; i++)
                        {
                            Console.Write(currentOutput.collatzNumbers[i].ToString() + "; ");
                        }
                        Console.WriteLine();
                        if (currentOutput.stoppingTime != -1) Console.WriteLine("Stopping Time: " + currentOutput.stoppingTime);

                    }
                    else
                    {
                        commandLineApplication.ShowHelp();
                    }

                    
                        
                }
                if (leadingDigit.HasValue())
                {
                    Console.WriteLine("Leading Digits Distribution");
                    leadingDigits.Sort();
                    Dictionary<int, int> LeadingDigitDistribution = new Dictionary<int, int>();
                    LeadingDigitDistribution = Collatz.DigitDistribution(leadingDigits);
                    
                    foreach(KeyValuePair<int,int> i in LeadingDigitDistribution)
                    {
                        Console.WriteLine(i.Key+": "+i.Value);
                    }

                    Console.WriteLine();
                }


                    return 0;
            }
            );
            commandLineApplication.Execute(args);
        }
    }
}
