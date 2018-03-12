using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoreRun
{

    public class CommandItem
    {
        public string command;
    }

    public class CommandRouter
    {
        private Queue<CommandItem> CommandQueue = new Queue<CommandItem>();
        private Thread ThreadProcess = null;

        public void Set_PureCommand(string command)
        {
            CommandItem newItem = new CommandItem();
            newItem.command = command;
            CommandQueue.Enqueue(newItem);
        }

        public void Start_Process()
        {
            this.ThreadProcess = new Thread(new ThreadStart(Process));
        }

        private void Process()
        {

        }



    }
}
