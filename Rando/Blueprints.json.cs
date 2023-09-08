using System;

namespace Blueprints.json
{

#region Config.json
    class Config
    {
        public string? version { get; set; }
        public string? language { get; set; }
        public List<ConfigChild_configurations>? configurations { get; set; }
    }
    
    class ConfigChild_configurations
    {
        public string? dir { get; set; }
        public string? mode { get; set; }
    }
#endregion Config.json

}