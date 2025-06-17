using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using UFF.Domain.Commands.Store;

public class StoreEditCommand
{
    public int StoreId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public bool OpenAutomatic { get; set; }
    public bool AttendSimultaneously { get; set; }
    public string PhoneNumber { get; set; }
    public string StoreSubtitle { get; set; }
    public bool AcceptOtherQueues { get; set; }
    public bool AnswerOutOfOrder { get; set; }
    public bool AnswerScheduledTime { get; set; }
    public int? TimeRemoval { get; set; }
    public bool ReleaseOrdersBeforeGetsQueued { get; set; }
    public bool StartServiceWithQRCode { get; set; }
    public bool ShareQueue { get; set; }
    public bool EndServiceWithQRCode { get; set; }
    public bool WhatsAppNotice { get; set; }
    public IFormFile Logo { get; set; }
    public IFormFile WallPaper { get; set; }
    public int CategoryId { get; set; }
    public string Instagram { get; set; }
    public string Facebook { get; set; }
    public string Youtube { get; set; }
    public string WebSite { get; set; }
    public string OpeningHours { get; set; }
    public string HighLights { get; set; }
    public List<OpeningHoursCreateCommand> OpeningHoursList =>
        !string.IsNullOrWhiteSpace(OpeningHours)
            ? JsonConvert.DeserializeObject<List<OpeningHoursCreateCommand>>(OpeningHours)
            : new List<OpeningHoursCreateCommand>();
    public List<HighLightCreateCommand> HighLightsList =>
        !string.IsNullOrWhiteSpace(HighLights)
            ? JsonConvert.DeserializeObject<List<HighLightCreateCommand>>(HighLights)
            : new List<HighLightCreateCommand>();
}
