using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Mutations;

[MutationType]
public static class MysqlEpisodesMutation
{
    [Error(typeof(ValidationException))]
    public static async Task<Episodes> CreateMysqlEpisode([Service] IMysqlEpisodesService episodesService, EpisodesDto episode)
        => await episodesService.CreateMysqlEpisodes(episode);
    
    [Error(typeof(ValidationException))]
    public static async Task<Episodes> UpdateMysqlEpisode([Service] IMysqlEpisodesService episodesService, EpisodesDto episode, Guid episodeId)
        => await episodesService.UpdateMysqlEpisodes(episode, episodeId);
        
    [Error(typeof(ValidationException))]
    public static async Task<Episodes> DeleteMysqlEpisode([Service] IMysqlEpisodesService episodesService, Guid episodeId)
        => await episodesService.DeleteMysqlEpisodes(episodeId);
}