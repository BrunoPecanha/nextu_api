﻿namespace UFF.Domain.Commands {
    public class CommandResult {

        public CommandResult(bool valid, object log, string message = "") {
            this.Valid = valid;
            this.Data = log;
            this.Message = message;
        }

        public bool Valid { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
