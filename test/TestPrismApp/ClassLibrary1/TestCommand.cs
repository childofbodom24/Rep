using System;
using System.Windows.Input;

namespace ClassLibrary1
{
    public class TestCommand
    {
        public TestCommand(string name, ICommand command)
        {
            this.Name = name;
            this.Command = command;
        }

        public string Name { get; private set; }
        public ICommand Command { get; private set; }
    }
}
