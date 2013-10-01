using System;
using System.Collections.Generic;
using System.Text;

namespace CZAOS.controls
{
    public class RowCommandEventHandler : EventArgs
    {
        public RowCommandEventHandler(string arg)
        {
            this.CommandArgument = arg;
            this.CommandName = string.Empty;
        }

        public RowCommandEventHandler(string arg, string comname)
        {
            this.CommandArgument = arg;
            this.CommandName = comname;
        }

        public string CommandArgument
        {
            get;
            private set;
        }

        public string CommandName
        {
            get;
            private set;
        }
    }
}
