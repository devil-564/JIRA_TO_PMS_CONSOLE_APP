using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class JiraProjectConverter : JsonConverter<RequiredProjectJSON>

{
    public override RequiredProjectJSON ReadJson(JsonReader reader, Type objectType, RequiredProjectJSON existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Deserialize the response into a dynamic object first to easily access Jira fields
        var jiraProject = JObject.Load(reader);

        // Create the custom project and map the fields from Jira's structure to your custom structure
        var customProject = new RequiredProjectJSON
        {
            //ProjectKey = jiraProject["key"]?.ToString(),
            //ProjectDescription = jiraProject["description"]?.ToString(),
            //LeadName = jiraProject["lead"]?["displayName"]?.ToString(),
            //IsActive = jiraProject["lead"]?["active"]?.ToObject<bool>() ?? false,
            //ProjectType = jiraProject["projectTypeKey"]?.ToString(),

        };

        return customProject;
    }

    public override void WriteJson(JsonWriter writer, RequiredProjectJSON value, JsonSerializer serializer)
    {
        //// This method is used to serialize the custom object back to JSON, if needed
        //writer.WriteStartObject();
        //writer.WritePropertyName("project_key");
        //writer.WriteValue(value.ProjectKey);
        //writer.WritePropertyName("description");
        //writer.WriteValue(value.ProjectDescription);
        //writer.WritePropertyName("lead");
        //writer.WriteValue(value.LeadName);
        //writer.WritePropertyName("active");
        //writer.WriteValue(value.IsActive);
        //writer.WritePropertyName("type");
        //writer.WriteValue(value.ProjectType);
        //writer.WriteEndObject();
    }
}
