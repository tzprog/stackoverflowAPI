using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StackoverflowTags.Models
{
    public class StackoverflowModel
    {
        public int Id { get; set; }

        public string TagName { get; set; }
        public string TagQuestionsCounter { get; set; }

        public double PercentPopularity { get; set; }
    }

    public class StackoverflowModelAPI
    {
        [JsonPropertyName("items")]

        public List<Item> items { get; set; }
        [JsonPropertyName("has_more")]

        public bool has_more { get; set; }
        [JsonPropertyName("quota_max")]

        public int quota_max { get; set; }
        [JsonPropertyName("quota_remaining")]

        public int quota_remaining { get; set; }
    }
    public class Item
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

        public int id { get; set; }
        public int lp { get; set; }
        public double PercentPopularity { get; set; }
    }
}
