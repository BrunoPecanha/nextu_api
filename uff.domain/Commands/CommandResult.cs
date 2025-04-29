namespace UFF.Domain.Commands {
    public class CommandResult {

        public CommandResult(bool valid, object log) {
            this.Valid = valid;
            this.Data = log;
        }

        public bool Valid { get; set; }

        public object Data { get; set; }
    }
}
