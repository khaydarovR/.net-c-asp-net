﻿namespace SCP.Application.Core.OAuth
{
    public class GitHubTokenResponseDTO
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }

}
