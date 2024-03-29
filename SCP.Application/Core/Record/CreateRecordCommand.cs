﻿namespace SCP.Application.Core.Record
{
    public class CreateRecordCommand
    {
        public string Title { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Pw { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public string ForResource { get; set; }
        public string SafeId { get; set; }

        public string Signature { get; set; } = null!;
        public string ClientPrivK { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
