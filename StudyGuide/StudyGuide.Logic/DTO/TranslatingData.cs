﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.DTO
{
    class TranslatingData
    {
        [JsonProperty("text")]
        public string[] Text { get; set; }
    }
}
