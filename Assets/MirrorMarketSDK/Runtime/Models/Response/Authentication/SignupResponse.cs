﻿using Unity.Plastic.Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class SignupResponse
    {
        [JsonProperty("message")]
        public string Message;
    }
}