namespace uff.Domain.Commands {
    public class CommandResult {

        public CommandResult(bool valid, object log) {
            this.Valid = valid;
            this.Data = log;
        }
        /// Indica se a operação foi validada
        /// </summary>
        public bool Valid { get; set; }
       
        /// <summary>
        /// Log de erro
        /// </summary>
        public object Data { get; set; }

    }
}
