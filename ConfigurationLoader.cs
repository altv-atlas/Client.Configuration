using System.Text.Json;
using System.Text.Json.Serialization;
using AltV.Net.Client;

namespace AltV.Atlas.Client.Configuration;

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