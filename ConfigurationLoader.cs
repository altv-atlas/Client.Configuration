using System.Text.Json;
using System.Text.Json.Serialization;
using AltV.Net.Client;

namespace AltV.Atlas.Client.Configuration;

/// <summary>
/// Helper class to load JSON configuration files via Alt.File API
/// </summary>
public class ConfigurationLoader
{
    /// <summary>
    /// Set of options to use when serializing/deserializing JSON objects
    /// </summary>
    public static readonly JsonSerializerOptions JsonSerializerOptions = new( )
    {
        PropertyNameCaseInsensitive = true,
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
    
    /// <summary>
    /// Load a given JSON file with Alt.File API
    /// </summary>
    /// <param name="filePath">The JSON file to load</param>
    /// <typeparam name="T">The type it should be cast to</typeparam>
    /// <returns>The deserialized JSON object if it succeeded</returns>
    /// <exception cref="FileNotFoundException">File could not be found</exception>
    /// <exception cref="NullReferenceException">File could not be converted to the target type (T)</exception>
    public static T Load<T>( string filePath )
    {
        if( !Alt.FileExists( filePath ) )
            throw new FileNotFoundException( "[ATLAS] Failed to find {File}", filePath );


        var obj = JsonSerializer.Deserialize<T>( Alt.ReadFile( filePath ), JsonSerializerOptions );

        if( obj is null )
            throw new NullReferenceException( "[ATLAS] Failed to convert file to object" );

        return obj;
    }
}