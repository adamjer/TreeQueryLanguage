
namespace TreeQueryLanguage.Assurance.TreeStructure
{
    public class Root : Node
    {
        public Root()
        {
            this.Parent = null;
            this.ID = "root";
            this.Label = "root";
            this.Name = "root";
            this.Description = "root";
            this.Children = new Nodes();
        }
    }
}

