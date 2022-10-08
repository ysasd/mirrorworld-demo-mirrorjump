﻿using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class FetchMultipleNftsByOwnersRequest
    {
        [JsonProperty("owners")] public List<string> Owners;

        [JsonProperty("limit")] public long Limit;

        [JsonProperty("offset")] public long Offset;
    }
}