﻿using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class AnimeDTO
{
    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }
    public string? Url { get; set; }
    public ImagesDTO? Images { get; set; }
    public TrailerDTO? Trailer { get; set; }
    public bool? Approved { get; set; }
    public List<TitleDTO>? Titles { get; set; }
    public string? Title { get; set; }

    [JsonPropertyName("title_english")]
    public string? TitleEnglish { get; set; }

    [JsonPropertyName("title_japanese")]
    public string? TitleJapanese { get; set; }
    public List<string>? TitleSynonyms { get; set; }
    public string? Type { get; set; }
    public string? Source { get; set; }
    public int? Episodes { get; set; }
    public string Status { get; set; }
    public bool? Airing { get; set; }
    public AiredDTO? Aired { get; set; }
    public string? Duration { get; set; }
    public string? Rating { get; set; }
    public double Score { get; set; }
    public int? ScoredBy { get; set; }
    public int? Rank { get; set; }
    public int? Popularity { get; set; }
    public int? Members { get; set; }
    public int? Favorites { get; set; }
    public string? Synopsis { get; set; }
    public string? Background { get; set; }
    public string? Season { get; set; }
    public int? Year { get; set; }
    public BroadcastDTO? Broadcast { get; set; }
    public List<ProducerDTO>? Producers { get; set; }
    public List<ProducerDTO>? Licensors { get; set; }
    public List<ProducerDTO>? Studios { get; set; }
    public List<GenreDTO>? Genres { get; set; }
    public List<GenreDTO>? ExplicitGenres { get; set; }
    public List<GenreDTO>? Themes { get; set; }
    public List<GenreDTO>? Demographics { get; set; }
}
