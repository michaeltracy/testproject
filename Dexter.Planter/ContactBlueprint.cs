using Dexter.Model;
using Plant.Core;

namespace Dexter.Planter
{
    class ContactBlueprint : IBlueprint
    {
        public void SetupPlant(BasePlant plant)
        {
            plant.DefinePropertiesOf<Contact>(new
            {
                Name = "Random User",
                Email = new Sequence<string>(count => "random_" + count + "@example.com")
            });
        }
    }
}
