using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StackoverflowTags.Types
{
    public class StackoverflowTypeAPI
    {
        [JsonPropertyName("items")]

        public List<Tag> items { get; set; }
        [JsonPropertyName("has_more")]

        public bool has_more { get; set; }
        [JsonPropertyName("quota_max")]

        public int quota_max { get; set; }
        [JsonPropertyName("quota_remaining")]

        public int quota_remaining { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("count")]
        public int count { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("has_synonyms")]
        public bool has_synonyms { get; set; }
        [JsonPropertyName("is_moderator_only")]
        public bool is_moderator_only { get; set; }
        [JsonPropertyName("is_required")]
        public bool is_required { get; set; }

    }
}
