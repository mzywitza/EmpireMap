// <auto-generated />
namespace EmpireMap.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class ResourceDelivery : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ResourceDelivery));
        
        string IMigrationMetadata.Id
        {
            get { return "201303311722570_ResourceDelivery"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
