﻿namespace _3.Command
{
    using System.Collections.Generic;

    public class ModifyPrice
    {
        private readonly List<ICommand> commands;
        private ICommand command;

        public ModifyPrice()
        {
            this.commands = new List<ICommand>();
        }

        public void SetCommand(ICommand command) 
        {
            this.command = command;
        }

        public string Invoke() 
        {
            this.commands.Add(this.command);
            return command.ExecuteAction();
        }
    }
}