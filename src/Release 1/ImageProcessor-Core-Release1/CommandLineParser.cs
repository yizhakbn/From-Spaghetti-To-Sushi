using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bnaya.Samples
{
    public class CommandLineParser
    {
        private readonly IDictionary<string, string> _commands;
        public CommandLineParser(string[] args)
        {
            _commands = args.Select(m =>
                                {
                                    var splited = m.Split(':');
                                    if (splited.Length != 2)
                                        throw new FormatException("Command lines arg should use [:] to separate key and value");
                                    return splited;
                                })
                            .ToDictionary(m => m[0].ToLower(), m => m[1]);
        }

        public string this[string name] => _commands[name.ToLower()];
        public int ToInt(string name) => int.Parse(_commands[name.ToLower()]);
        public double ToDouble(string name) => double.Parse(_commands[name.ToLower()]);
        public DateTimeOffset ToDate(string name) => DateTimeOffset.Parse(_commands[name.ToLower()]);

    }
}
