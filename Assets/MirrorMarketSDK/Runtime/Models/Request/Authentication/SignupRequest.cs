﻿using Unity.Plastic.Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class SignupRequest
    {
        [JsonProperty("email")] public string Email;
    }
}